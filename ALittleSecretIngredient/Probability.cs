using System.Runtime.Serialization;

namespace ALittleSecretIngredient
{
    /// <summary>
    ///  Container for classes handling discrete probability distributions.
    /// </summary>
    public static class Probability
    {
        internal static Random RNG { get; } = new();

        /// <summary>
        ///  Converts a uniform distribution 0-1 to an approximated normal distribution.
        /// </summary>
        private static double QuantileFunction(double p, double mean, double std)
        {
            if (p >= 1)
                return double.PositiveInfinity;
            if (p <= 0)
                return double.NegativeInfinity;
            return mean + Math.Sqrt(2) * std * InvErf(2 * p - 1);
        }

        private static double InvErf(double z)
        {
            double sum = 0;
            double precision = 0.000000001;
            for (int k = 0; true; k++)
            {
                if (k == c.Count)
                    c.Add(C(k));
                if (double.IsInfinity(c[k]))
                    break;
                double add = c[k] / (2 * k + 1) * Math.Pow(Math.Sqrt(Math.PI) / 2 * z, 2 * k + 1);
                if (Math.Abs(add) < precision)
                    break;
                sum += add;
            }
            return sum;
        }

        private static double C(double k)
        {
            if (k == 0)
                return 1;
            double sum = 0;
            for (int m = 0; m <= k - 1; m++)
                sum += c[(int)(k - 1 - m)] / (m + 1) / (2 * m + 1) * c[m];
            return sum;
        }

        private static readonly List<double> c = new();
        public static readonly string[] numericDistributionNames = new string[]
        {
            "Uniform, Constant",
            "Uniform, Relative",
            "Uniform, Proportional",
            "Normal, Constant",
            "Normal, Relative",
            "Normal, Proportional",
            "Redistribution"
        };
        public static readonly string[] selectionDistributionNames = new string[]
        {
            "Empirical",
            "Uniform",
            "Redistribution"
        };
        public static readonly string[] numericDistributionDescriptions = new string[]
        {
            "Selects all values using the same boundaries:\nY ~ U(min, max)",
            "Selects all values using relative boundaries:\nY ~ U(x + min, x + max)",
            "Selects all values using proportionally defined boundaries:\nY ~ U(x * min, x * max)",
            "Selects all values using the same mean and standard deviation:\nY ~ N(mean, std)",
            "Selects all values using the same standard deviation:\nY ~ N(x, std)",
            "Selects all values using proportionally defined standard deviations:\nY ~ N(x, std * x)",
            "Shuffles all values."
        };
        public static readonly string[] selectionDistributionDescriptions = new string[]
        {
            "Selects values based on their weight value. Larger weight implies higher frequency.",
            "Selects values from the specified sample. Configure checkboxes to control inclusion.",
            "Shuffles all values."
        };
        public static readonly (string, string, string)[] numericDistributionArgNames = new (string, string, string)[]
        {
            ("Randomization Probability", "Min", "Max"),
            ("Randomization Probability", "Min", "Max"),
            ("Randomization Probability", "Min", "Max"),
            ("Randomization Probability", "Mean", "Standard Deviation"),
            ("Randomization Probability", "Standard Deviation", ""),
            ("Randomization Probability", "Standard Deviation", ""),
            ("Randomization Probability", "", "")
        };

        /// <summary>
        ///  Returns a distribution object with the specified config.
        /// </summary>
        public static IDistribution CreateDistribution(List<double> args)
        {
            switch (args[0])
            {
                case 0:
                    return new UniformConstant(args[1], args[2], args[3]);
                case 1:
                    return new UniformRelative(args[1], args[2], args[3]);
                case 2:
                    return new UniformProportional(args[1], args[2], args[3]);
                case 3:
                    return new NormalConstant(args[1], args[2], args[3]);
                case 4:
                    return new NormalRelative(args[1], args[2]);
                case 5:
                    return new NormalProportional(args[1], args[2]);
                case 6:
                    List<int> weights = args.Skip(2).Select(d => (int)d).ToList();
                    return new Empirical(args[1], weights);
                case 7:
                    List<bool> selection = args.Skip(2).Select(d => d == 1).ToList();
                    return new UniformSelection(args[1], selection);
                case 8:
                    return new Redistribution(args[1]);
                default:
                    throw new ArgumentException("Invalid 1st arg: " + args[0]);
            }
        }

        /// <summary>
        ///  Calculates the standard deviation of a sequence of double values.
        /// </summary>
        public static double StandardDeviation(this IList<double> observations)
        {
            double sum = 0;
            if (observations.Count > 1)
            {
                double avg = observations.Average();
                for (int i = 0; i < observations.Count; i++)
                    sum += Math.Pow(avg - observations[i], 2) / (observations.Count - 1);
            }
            return Math.Sqrt(sum);
        }

        /// <summary>
        ///  Calculates the standard deviation of a sequence of integer values.
        /// </summary>
        public static double StandardDeviation(this IList<int> observations)
        {
            return observations.Select(i => (double)i).ToList().StandardDeviation();
        }

        public interface IDistribution
        {
            /// <summary>
            ///  Returns the config of the distribution as a List of doubles.
            /// </summary>
            public List<double> GetConfig();
        }

        public interface INumericDistribution : IDistribution
        {
            /// <summary>
            ///  Returns an independent random number by this distribution.
            /// </summary>
            public double Next(double oldValue);
        }

        public interface ISelectionDistribution : IDistribution
        {
            /// <summary>
            ///  Returns an independent random number by this distribution.
            /// </summary>
            public int Next(int oldValue);
        }

        public class UniformConstant : INumericDistribution
        {
            public double p;
            public double min;
            public double max;

            public UniformConstant() { }

            public UniformConstant(double p, double min, double max)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.min = Math.Round(Math.Min(min, max), 3);
                this.max = Math.Round(Math.Max(min, max), 3);
            }

            public double Next(double value)
            {
                return RNG.NextDouble() * 100 < p ? (max - min) * RNG.NextDouble() + min : value;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    0,
                    p,
                    min,
                    max
                };
                return l;
            }
        }

        public class UniformRelative : INumericDistribution
        {
            public double p;
            public double min;
            public double max;

            public UniformRelative() { }

            public UniformRelative(double p, double min, double max)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.min = Math.Round(Math.Min(min, max), 3);
                this.max = Math.Round(Math.Max(min, max), 3);
            }

            public double Next(double value)
            {
                return RNG.NextDouble() * 100 < p ? value + (max - min) * RNG.NextDouble() + min : value;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    1,
                    p,
                    min,
                    max
                };
                return l;
            }
        }

        public class UniformProportional : INumericDistribution
        {
            public double p;
            public double minX;
            public double maxX;

            public UniformProportional() { }

            public UniformProportional(double p, double minX, double maxX)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.minX = Math.Round(Math.Min(minX, maxX), 3);
                this.maxX = Math.Round(Math.Max(minX, maxX), 3);
            }

            public double Next(double value)
            {
                return RNG.NextDouble() * 100 < p ? value * ((maxX - minX) * RNG.NextDouble() + minX) : value;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    2,
                    p,
                    minX,
                    maxX
                };
                return l;
            }
        }

        public class NormalConstant : INumericDistribution
        {
            public double p;
            public double mean;
            public double standardDeviation;

            public NormalConstant() { }

            public NormalConstant(double p, double mean, double standardDeviation)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.mean = Math.Round(mean, 3);
                this.standardDeviation = Math.Round(standardDeviation, 3);
            }

            public double Next(double value)
            {
                return RNG.NextDouble() * 100 < p ? QuantileFunction(RNG.NextDouble(), mean, standardDeviation) : value;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    3,
                    p,
                    mean,
                    standardDeviation
                };
                return l;
            }
        }

        public class NormalRelative : INumericDistribution
        {
            public double p;
            public double standardDeviation;

            public NormalRelative() { }

            public NormalRelative(double p, double standardDeviation)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.standardDeviation = Math.Round(standardDeviation, 3);
            }

            public double Next(double value)
            {
                return RNG.NextDouble() * 100 < p ? QuantileFunction(RNG.NextDouble(), value, standardDeviation) : value;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    4,
                    p,
                    standardDeviation
                };
                return l;
            }
        }

        public class NormalProportional : INumericDistribution
        {
            public double p;
            public double standardDeviationX;

            public NormalProportional() { }

            public NormalProportional(double p, double standardDeviationX)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.standardDeviationX = Math.Round(standardDeviationX, 3);
            }

            public double Next(double value)
            {
                return RNG.NextDouble() * 100 < p ? QuantileFunction(RNG.NextDouble(), value, standardDeviationX * value) : value;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    5,
                    p,
                    standardDeviationX
                };
                return l;
            }
        }

        public class Empirical : ISelectionDistribution
        {
            public double p;
            public List<int> weights;
            public int totalWeight;

            public Empirical()
            {
                weights = new();
            }

            public Empirical(double p, List<int> weights)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.weights = weights;
                totalWeight = 0;
                for (int i = 0; i < weights.Count; i++)
                {
                    weights[i] = Math.Max(weights[i], 0);
                    totalWeight += weights[i];
                }
                if (totalWeight == 0)
                    this.p = 0;
            }

            public int Next(int value)
            {
                if (RNG.NextDouble() * 100 >= p)
                    return value;

                int n = RNG.Next(totalWeight);
                int selection = -1;
                for (int i = 0; n >= 0; i++)
                {
                    selection = i;
                    n -= Math.Max(weights[i], 0);
                }
                return selection;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    6,
                    p
                };
                l.AddRange(weights.Select(i => (double)i));
                return l;
            }
        }

        public class UniformSelection : ISelectionDistribution
        {
            public double p;
            public List<bool> selection;
            public int selectionCount;
            public UniformSelection()
            {
                selection = new();
            }

            public UniformSelection(double p, List<bool> selection)
            {
                this.p = Math.Clamp(p, 0, 100);
                this.selection = selection;
                selectionCount = 0;
                for (int i = 0; i < selection.Count; i++)
                    if (selection[i])
                        selectionCount++;
                if (selectionCount == 0)
                    this.p = 0;
            }

            public int Next(int value)
            {
                if (RNG.NextDouble() * 100 >= p)
                    return value;

                int n = RNG.Next(selectionCount);
                int item = -1;
                for (int i = 0; n >= 0; i++)
                {
                    item = i;
                    if (selection[i])
                        n--;
                }
                return item;
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    7,
                    p
                };
                l.AddRange(selection.Select(b => b ? 1.0 : 0.0));
                return l;
            }
        }

        public class Redistribution : IDistribution
        {
            public double p;

            public Redistribution() { }

            public Redistribution(double p)
            {
                this.p = p;
            }

            public void Randomize<T>(IList<T> values)
            {
                List<int> indicesToShuffle = new();
                List<T> pool = new();
                for (int i = 0; i < values.Count; i++)
                    if (RNG.NextDouble() * 100 < p)
                    {
                        indicesToShuffle.Add(i);
                        pool.Add(values[i]);
                    }
                for (int j = indicesToShuffle.Count - 1; j >= 0; j--)
                {
                    int i = indicesToShuffle[j];
                    T value = pool[RNG.Next(0, pool.Count)];
                    values[i] = value;
                    indicesToShuffle.RemoveAt(j);
                    pool.Remove(value);
                }
            }

            public List<double> GetConfig()
            {
                List<double> l = new()
                {
                    8,
                    p
                };
                return l;
            }
        }

        /// <summary>
        /// Randomizes a specific property of a list of objects.
        /// </summary>
        /// <typeparam name="A">Object</typeparam>
        /// <typeparam name="B">Property</typeparam>
        /// <param name="targets">The objects whose property is to be changed.</param>
        /// <param name="get">Getter for property.</param>
        /// <param name="set">Setter for property.</param>
        /// <param name="distribution">Distribution</param>
        /// <param name="min">Minimum value.</param>
        /// <param name="max">Maximum value.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Randomize<A, B>(this IList<A> targets, Func<A, B> get, Action<A, B> set,
            IDistribution distribution, double min, double max)
        {
            if (distribution is ISelectionDistribution)
                throw new ArgumentException("Unsupported distribution.");
            targets.Randomize(get, set, distribution, null!);
            foreach (A target in targets)
                if (get(target) is byte x0 && (byte)Math.Clamp(x0, min, max) is B n0)
                    set(target, n0);
                else if (get(target) is sbyte x1 && (sbyte)Math.Clamp(x1, min, max) is B n1)
                    set(target, n1);
                else if (get(target) is int x2 && (int)Math.Clamp(x2, min, max) is B n2)
                    set(target, n2);
                else if (get(target) is float x3 && (float)Math.Clamp(x3, min, max) is B n3)
                    set(target, n3);
                else if (get(target) is short x4 && (short)Math.Clamp(x4, min, max) is B n4)
                    set(target, n4);
                else
                    throw new ArgumentException("Unsupported type: " + get(target)!.GetType().Name);
        }

        /// <summary>
        /// Randomizes a specific property of a list of objects.
        /// </summary>
        /// <typeparam name="A">Object</typeparam>
        /// <typeparam name="B">Property</typeparam>
        /// <param name="targets">The objects whose property is to be changed.</param>
        /// <param name="get">Getter for property.</param>
        /// <param name="set">Setter for property.</param>
        /// <param name="distribution">Distribution</param>
        /// <param name="pool">Selection pool.</param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void Randomize<A, B>(this IList<A> targets, Func<A, B> get, Action<A, B> set,
            IDistribution distribution, IList<B> pool)
        {
            if (distribution is INumericDistribution n)
                for (int i = 0; i < targets.Count; i++)
                {
                    if (get(targets[i]) is byte x0 && (byte)Math.Round(n.Next(x0)) is B n0)
                        set(targets[i], n0);
                    else if (get(targets[i]) is sbyte x1 && (sbyte)Math.Round(n.Next(x1)) is B n1)
                        set(targets[i], n1);
                    else if (get(targets[i]) is int x2 && (int)Math.Round(n.Next(x2)) is B n2)
                        set(targets[i], n2);
                    else if (get(targets[i]) is float x3 && (float)n.Next(x3) is B n3)
                        set(targets[i], n3);
                    else if (get(targets[i]) is short x4 && (short)Math.Round(n.Next(x4)) is B n4)
                        set(targets[i], n4);
                    else if (get(targets[i]) != null)
                        throw new ArgumentException("Unsupported type: " + get(targets[i])!.GetType().Name);
                    else
                        throw new ArgumentNullException("Element in target was null.", (Exception)null!);
                }
            else if (distribution is ISelectionDistribution s)
                for (int i = 0; i < targets.Count; i++)
                    set(targets[i], pool[s.Next(pool.IndexOf(get(targets[i])))]);
            else if (distribution is Redistribution r)
            {
                pool = new List<B>(targets.Select(get));
                r.Randomize(pool);
                for (int i = 0; i < targets.Count; i++)
                    set(targets[i], pool[i]);
            }
            else
                throw new ArgumentException("Unsupported distribution.");
        }

        /// <summary>
        /// Randomly returns true or false.
        /// </summary>
        /// <param name="p">Likelyhood of true in percent.</param>
        /// <returns>True p% of the time, false otherwise.</returns>
        internal static bool Occur(this double p) => RNG.NextDouble() * 100 < p;

        /// <summary>
        /// Returns a uniformely random element in this list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static T GetRandom<T>(this IEnumerable<T> list) => list.ElementAt(RNG.Next(list.Count()));
    }
}