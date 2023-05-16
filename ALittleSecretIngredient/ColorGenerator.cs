using static ALittleSecretIngredient.Probability;

namespace ALittleSecretIngredient
{
    internal static class ColorGenerator
    {
        internal static Color RandomColor()
        {
            return Color.FromArgb(RNG.Next(256), RNG.Next(256), RNG.Next(256));
        }

        internal static List<Color> RandomColors(int count)
        {
            List<Color> colors = new();
            for (int i = 0; i < count; i++)
                colors.Add(RandomColor());
            return colors;
        }

        private enum ColorHarmonyRule
        {
            Analogous, Monochromatic, Triad,
            Complementary, SplitComplementary, DoubleSplitComplementary,
            Square, Compound, Shades
        }

        internal static List<Color> RandomPalette(int colorCount)
        {
            return Enum.GetValues<ColorHarmonyRule>().GetRandom() switch
            {
                ColorHarmonyRule.Analogous => RandomAnalogous(colorCount),
                ColorHarmonyRule.Monochromatic => RandomMonochromatic(colorCount),
                ColorHarmonyRule.Triad => RandomTriad(colorCount),
                ColorHarmonyRule.Complementary => RandomComplementary(colorCount),
                ColorHarmonyRule.SplitComplementary => RandomSplitComplementary(colorCount),
                ColorHarmonyRule.DoubleSplitComplementary => RandomDoubleSplitComplementary(colorCount),
                ColorHarmonyRule.Square => RandomSquare(colorCount),
                ColorHarmonyRule.Compound => RandomCompound(colorCount),
                ColorHarmonyRule.Shades => RandomShades(colorCount),
                _ => throw new NotImplementedException()
            };
        }

        private static List<Color> RandomAnalogous(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            double hueShift = RNG.NextDouble() * 10 + 10;
            double hueAcc = 0;
            for (int i = 1; i < colorCount; i++)
            {
                hueAcc += hueShift;
                palette.Add(palette[0].AdjustHue(hueAcc)
                    .AdjustSaturation(RNG.NextDouble() * 25 - 12.5)
                    .AdjustBrightness(RNG.NextDouble() * 50 - 25));
            }
            return palette;
        }

        private static List<Color> RandomMonochromatic(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            for (int i = 1; i < colorCount; i++)
                palette.Add(palette[0]
                    .AdjustSaturation(RNG.NextDouble() * 160 - 80)
                    .AdjustBrightness(RNG.NextDouble() * 160 - 80));
            return palette;
        }

        private static List<Color> RandomTriad(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            double hueShift = 120;
            double hueAcc = 0;
            for (int i = 1; i < colorCount; i++)
            {
                hueAcc += hueShift;
                palette.Add(palette[0].AdjustHue(hueAcc)
                    .AdjustSaturation(RNG.NextDouble() * 50 - 25)
                    .AdjustBrightness(RNG.NextDouble() * 160 - 80));
            }
            return palette;
        }

        private static List<Color> RandomComplementary(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            double hueShift = 180;
            double hueAcc = 0;
            for (int i = 1; i < colorCount; i++)
            {
                hueAcc += hueShift;
                palette.Add(palette[0].AdjustHue(hueAcc)
                    .AdjustSaturation(RNG.NextDouble() * 100 - 50)
                    .AdjustBrightness(RNG.NextDouble() * 160 - 80));
            }
            return palette;
        }

        private static List<Color> RandomSplitComplementary(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            double hueSplit = RNG.NextDouble() * 10 + 10;
            for (int i = 1; i < colorCount; i++)
            {
                double hueShift = (i % 3) switch
                {
                    0 => 0,
                    1 => 180 - hueSplit,
                    2 => 180 + hueSplit,
                    _ => throw new NotImplementedException("What")
                };
                palette.Add(palette[0].AdjustHue(hueShift)
                    .AdjustSaturation(RNG.NextDouble() * 50 - 25)
                    .AdjustBrightness(RNG.NextDouble() * 160 - 80));
            }
            return palette;
        }

        private static List<Color> RandomDoubleSplitComplementary(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            double hueSplit = RNG.NextDouble() * 10 + 10;
            for (int i = 1; i < colorCount; i++)
            {
                double hueShift = (i % 5) switch
                {
                    0 => 0,
                    1 => hueSplit,
                    2 => 180 - hueSplit,
                    3 => 180 + hueSplit,
                    4 => 360 - hueSplit,
                    _ => throw new NotImplementedException("What")
                };
                palette.Add(palette[0].AdjustHue(hueShift)
                    .AdjustSaturation(RNG.NextDouble() * 50 - 25));
            }
            return palette;
        }

        private static List<Color> RandomSquare(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            double hueShift = 90;
            double hueAcc = 0;
            for (int i = 1; i < colorCount; i++)
            {
                hueAcc += hueShift;
                palette.Add(palette[0].AdjustHue(hueAcc)
                    .AdjustSaturation(RNG.NextDouble() * 50 - 25));
            }
            return palette;
        }

        private static List<Color> RandomCompound(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            for (int i = 1; i < colorCount; i++)
            {
                double hueShift = (i % 4) switch
                {
                    0 => 0,
                    1 => 30,
                    2 => 150,
                    3 => 165,
                    _ => throw new NotImplementedException("What")
                };
                palette.Add(palette[0].AdjustHue(hueShift)
                    .AdjustSaturation(RNG.NextDouble() * 200 - 100)
                    .AdjustBrightness(RNG.NextDouble() * 200 - 100));
            }
            return palette;
        }

        private static List<Color> RandomShades(int colorCount)
        {
            if (colorCount <= 0) return new();
            List<Color> palette = new() { RandomColor() };
            for (int i = 1; i < colorCount; i++)
                palette.Add(palette[0]
                    .AdjustBrightness(RNG.NextDouble() * 160 - 80));
            return palette;
        }

        internal static Color AdjustHue(this Color c, double degrees)
        {
            double radians = degrees * Math.PI / 180.0;
            double sinA = Math.Sin(radians);
            double cosA = Math.Cos(radians);
            return Color.FromArgb(
                (byte)Math.Clamp(
                    c.R * (cosA + (1.0 - cosA) / 3.0) +
                    c.G * (1.0 / 3.0 * (1.0 - cosA) - Math.Sqrt(1.0 / 3.0) * sinA) +
                    c.B * (1.0 / 3.0 * (1.0 - cosA) + Math.Sqrt(1.0 / 3.0) * sinA),
                    0, 255),
                (byte)Math.Clamp(
                    c.R * (1.0 / 3.0 * (1.0 - cosA) + Math.Sqrt(1.0 / 3.0) * sinA) +
                    c.G * cosA + 1.0 / 3.0 * (1.0 - cosA) +
                    c.B * (1.0 / 3.0 * (1.0 - cosA) - Math.Sqrt(1.0 / 3.0) * sinA),
                    0, 255),
                (byte)Math.Clamp(
                    c.R * (1.0 / 3.0 * (1.0 - cosA) - Math.Sqrt(1.0 / 3.0) * sinA) +
                    c.G * (1.0 / 3.0 * (1.0 - cosA) + Math.Sqrt(1.0 / 3.0) * sinA) +
                    c.B * cosA + 1.0 / 3.0 * (1.0 - cosA),
                    0, 255));
        }

        internal static Color AdjustSaturation(this Color c, double increase)
        {
            byte min = Math.Min(c.R, Math.Min(c.G, c.B));
            byte max = Math.Max(c.R, Math.Max(c.G, c.B));
            double middle = (min + max) / 2.0;
            double spread = max - min;
            if (increase < -spread)
                increase = -spread;
            return Color.FromArgb(
                (byte)Math.Clamp((c.R - middle) / spread * increase + c.R, 0, 255),
                (byte)Math.Clamp((c.G - middle) / spread * increase + c.G, 0, 255),
                (byte)Math.Clamp((c.B - middle) / spread * increase + c.B, 0, 255));
        }

        internal static Color AdjustBrightness(this Color c, double increase)
        {
            double brightness = (c.R + c.G + c.B) / 3.0;
            double factor = (brightness + increase) / brightness;
            return Color.FromArgb(
                (byte)Math.Clamp(c.R * factor, 0, 255),
                (byte)Math.Clamp(c.G * factor, 0, 255),
                (byte)Math.Clamp(c.B * factor, 0, 255));
        }
    }
}
