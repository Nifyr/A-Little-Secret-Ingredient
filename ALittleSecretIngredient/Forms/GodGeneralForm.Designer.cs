namespace ALittleSecretIngredient.Forms
{
    partial class GodGeneralForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GodGeneralForm));
            checkBox1 = new CheckBox();
            groupBox1 = new GroupBox();
            button1 = new Button();
            groupBox2 = new GroupBox();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            checkBox3 = new CheckBox();
            button2 = new Button();
            checkBox2 = new CheckBox();
            groupBox3 = new GroupBox();
            button6 = new Button();
            checkBox9 = new CheckBox();
            button3 = new Button();
            checkBox4 = new CheckBox();
            groupBox4 = new GroupBox();
            button4 = new Button();
            checkBox5 = new CheckBox();
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox5 = new GroupBox();
            checkBox7 = new CheckBox();
            button5 = new Button();
            checkBox6 = new CheckBox();
            groupBox6 = new GroupBox();
            checkBox10 = new CheckBox();
            checkBox8 = new CheckBox();
            groupBox7 = new GroupBox();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            checkBox11 = new CheckBox();
            groupBox8 = new GroupBox();
            button23 = new Button();
            button24 = new Button();
            button25 = new Button();
            button26 = new Button();
            button27 = new Button();
            button28 = new Button();
            button29 = new Button();
            button30 = new Button();
            button31 = new Button();
            button32 = new Button();
            checkBox12 = new CheckBox();
            button22 = new Button();
            button21 = new Button();
            button20 = new Button();
            button19 = new Button();
            button18 = new Button();
            button17 = new Button();
            button16 = new Button();
            button15 = new Button();
            button14 = new Button();
            button13 = new Button();
            checkBox17 = new CheckBox();
            groupBox9 = new GroupBox();
            label2 = new Label();
            numericUpDown2 = new NumericUpDown();
            checkBox13 = new CheckBox();
            toolTip1 = new ToolTip(components);
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox8.SuspendLayout();
            groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            SuspendLayout();
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(6, 26);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(106, 24);
            checkBox1.TabIndex = 0;
            checkBox1.Text = "Randomize";
            toolTip1.SetToolTip(checkBox1, "This modifies the amount of *engage points* required for a character to engage again. It is possible that the engage meter UI may become bugged as a result of this.");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(checkBox1);
            groupBox1.Location = new Point(3, 263);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 111);
            groupBox1.TabIndex = 1;
            groupBox1.TabStop = false;
            groupBox1.Text = "Engage Meter Size";
            // 
            // button1
            // 
            button1.Location = new Point(6, 56);
            button1.Name = "button1";
            button1.Size = new Size(188, 29);
            button1.TabIndex = 1;
            button1.Text = "Numbers";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(checkBox3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(checkBox2);
            groupBox2.Location = new Point(3, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 254);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "Engage+ Characters";
            // 
            // numericUpDown1
            // 
            numericUpDown1.DecimalPlaces = 3;
            numericUpDown1.Location = new Point(6, 201);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(188, 27);
            numericUpDown1.TabIndex = 4;
            toolTip1.SetToolTip(numericUpDown1, "For what portion of the emblems would you like to make their effects available to characters through *engage+*?");
            numericUpDown1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // label1
            // 
            label1.Location = new Point(6, 158);
            label1.Name = "label1";
            label1.Size = new Size(188, 40);
            label1.TabIndex = 3;
            label1.Text = "Engage+ Link % per emblem:";
            // 
            // checkBox3
            // 
            checkBox3.Location = new Point(6, 111);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(188, 44);
            checkBox3.TabIndex = 2;
            checkBox3.Text = "Randomly link other emblems";
            toolTip1.SetToolTip(checkBox3, "This will allow any character to engage+ into any *emblem*, as opposed to solely Emblem Alear. Please note that this has the potential to cause bugged emblem menus.");
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(6, 76);
            button2.Name = "button2";
            button2.Size = new Size(188, 29);
            button2.TabIndex = 1;
            button2.Text = "Characters";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // checkBox2
            // 
            checkBox2.Location = new Point(6, 26);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(188, 44);
            checkBox2.TabIndex = 0;
            checkBox2.Text = "Randomize Emblem Alear link";
            toolTip1.SetToolTip(checkBox2, "Under normal circumstances, Alear can engage+ with an ally and receive a plethora of benefits. With this, another character will *assume* this capability instead.");
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.AutoSize = true;
            groupBox3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox3.Controls.Add(button6);
            groupBox3.Controls.Add(checkBox9);
            groupBox3.Controls.Add(button3);
            groupBox3.Controls.Add(checkBox4);
            groupBox3.Location = new Point(3, 380);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(200, 176);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "Engage Attacks";
            // 
            // button6
            // 
            button6.Location = new Point(6, 121);
            button6.Name = "button6";
            button6.Size = new Size(188, 29);
            button6.TabIndex = 3;
            button6.Text = "Enemy Engage Atttacks";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(6, 91);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(165, 24);
            checkBox9.TabIndex = 2;
            checkBox9.Text = "Randomize enemies";
            toolTip1.SetToolTip(checkBox9, "This will alter the engage attack of every emblem *utilized* by opponents.");
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(6, 56);
            button3.Name = "button3";
            button3.Size = new Size(188, 29);
            button3.TabIndex = 1;
            button3.Text = "Ally Engage Attacks";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(6, 26);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(144, 24);
            checkBox4.TabIndex = 0;
            checkBox4.Text = "Randomize allies";
            toolTip1.SetToolTip(checkBox4, "This will *alter* the engage attack for each of the obtainable emblems.");
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.AutoSize = true;
            groupBox4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox4.Controls.Add(button4);
            groupBox4.Controls.Add(checkBox5);
            groupBox4.Location = new Point(3, 562);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(200, 111);
            groupBox4.TabIndex = 3;
            groupBox4.TabStop = false;
            groupBox4.Text = "Bond Link Skills";
            // 
            // button4
            // 
            button4.Location = new Point(6, 56);
            button4.Name = "button4";
            button4.Size = new Size(188, 29);
            button4.TabIndex = 1;
            button4.Text = "Skills";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(6, 26);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(106, 24);
            checkBox5.TabIndex = 0;
            checkBox5.Text = "Randomize";
            toolTip1.SetToolTip(checkBox5, "The engage attacks' upgrade paths due to bond link will be modified as a *result* of this.");
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox3);
            flowLayoutPanel1.Controls.Add(groupBox4);
            flowLayoutPanel1.Controls.Add(groupBox5);
            flowLayoutPanel1.Controls.Add(groupBox6);
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Controls.Add(groupBox8);
            flowLayoutPanel1.Controls.Add(groupBox9);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(836, 433);
            flowLayoutPanel1.TabIndex = 4;
            // 
            // groupBox5
            // 
            groupBox5.AutoSize = true;
            groupBox5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox5.Controls.Add(checkBox7);
            groupBox5.Controls.Add(button5);
            groupBox5.Controls.Add(checkBox6);
            groupBox5.Location = new Point(209, 3);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(200, 141);
            groupBox5.TabIndex = 4;
            groupBox5.TabStop = false;
            groupBox5.Text = "Bond Linked Emblems";
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(9, 91);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(101, 24);
            checkBox7.TabIndex = 2;
            checkBox7.Text = "Force Pairs";
            toolTip1.SetToolTip(checkBox7, "Enabling this will result in bond links triggering in both directions, in *pairs*.");
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(6, 56);
            button5.Name = "button5";
            button5.Size = new Size(188, 29);
            button5.TabIndex = 1;
            button5.Text = "Emblems";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(9, 26);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(106, 24);
            checkBox6.TabIndex = 0;
            checkBox6.Text = "Randomize";
            toolTip1.SetToolTip(checkBox6, "This will alter the emblem that *triggers* a bond link for each individual emblem.");
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            groupBox6.AutoSize = true;
            groupBox6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox6.Controls.Add(checkBox10);
            groupBox6.Controls.Add(checkBox8);
            groupBox6.Location = new Point(209, 150);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(200, 106);
            groupBox6.TabIndex = 5;
            groupBox6.TabStop = false;
            groupBox6.Text = "Bond Level Tables";
            // 
            // checkBox10
            // 
            checkBox10.Location = new Point(6, 56);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(188, 24);
            checkBox10.TabIndex = 1;
            checkBox10.Text = "Shuffle enemies";
            toolTip1.SetToolTip(checkBox10, "This will alter the set of bonuses that opponents receive from *bond levels*.");
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            checkBox8.Location = new Point(6, 26);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(188, 24);
            checkBox8.TabIndex = 0;
            checkBox8.Text = "Shuffle allies";
            toolTip1.SetToolTip(checkBox8, "This alters the set of bonuses your characters receive from *bond levels*.");
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.AutoSize = true;
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(button12);
            groupBox7.Controls.Add(button11);
            groupBox7.Controls.Add(button10);
            groupBox7.Controls.Add(button9);
            groupBox7.Controls.Add(button8);
            groupBox7.Controls.Add(button7);
            groupBox7.Controls.Add(checkBox11);
            groupBox7.Location = new Point(209, 262);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(200, 286);
            groupBox7.TabIndex = 6;
            groupBox7.TabStop = false;
            groupBox7.Text = "Engraving Stats";
            // 
            // button12
            // 
            button12.Location = new Point(6, 231);
            button12.Name = "button12";
            button12.Size = new Size(188, 29);
            button12.TabIndex = 11;
            button12.Text = "Dodge";
            button12.UseVisualStyleBackColor = true;
            button12.Click += Button12_Click;
            // 
            // button11
            // 
            button11.Location = new Point(6, 196);
            button11.Name = "button11";
            button11.Size = new Size(188, 29);
            button11.TabIndex = 9;
            button11.Text = "Avoid";
            button11.UseVisualStyleBackColor = true;
            button11.Click += Button11_Click;
            // 
            // button10
            // 
            button10.Location = new Point(6, 161);
            button10.Name = "button10";
            button10.Size = new Size(188, 29);
            button10.TabIndex = 7;
            button10.Text = "Crit";
            button10.UseVisualStyleBackColor = true;
            button10.Click += Button10_Click;
            // 
            // button9
            // 
            button9.Location = new Point(6, 126);
            button9.Name = "button9";
            button9.Size = new Size(188, 29);
            button9.TabIndex = 5;
            button9.Text = "Hit";
            button9.UseVisualStyleBackColor = true;
            button9.Click += Button9_Click;
            // 
            // button8
            // 
            button8.Location = new Point(6, 91);
            button8.Name = "button8";
            button8.Size = new Size(188, 29);
            button8.TabIndex = 3;
            button8.Text = "Weight";
            button8.UseVisualStyleBackColor = true;
            button8.Click += Button8_Click;
            // 
            // button7
            // 
            button7.Location = new Point(6, 56);
            button7.Name = "button7";
            button7.Size = new Size(188, 29);
            button7.TabIndex = 1;
            button7.Text = "Might";
            button7.UseVisualStyleBackColor = true;
            button7.Click += Button7_Click;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(6, 26);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(140, 24);
            checkBox11.TabIndex = 0;
            checkBox11.Text = "Randomize stats";
            toolTip1.SetToolTip(checkBox11, "The stat changes resulting from each emblem's *engraving* will be modified through this.");
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            groupBox8.AutoSize = true;
            groupBox8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox8.Controls.Add(button23);
            groupBox8.Controls.Add(button24);
            groupBox8.Controls.Add(button25);
            groupBox8.Controls.Add(button26);
            groupBox8.Controls.Add(button27);
            groupBox8.Controls.Add(button28);
            groupBox8.Controls.Add(button29);
            groupBox8.Controls.Add(button30);
            groupBox8.Controls.Add(button31);
            groupBox8.Controls.Add(button32);
            groupBox8.Controls.Add(checkBox12);
            groupBox8.Controls.Add(button22);
            groupBox8.Controls.Add(button21);
            groupBox8.Controls.Add(button20);
            groupBox8.Controls.Add(button19);
            groupBox8.Controls.Add(button18);
            groupBox8.Controls.Add(button17);
            groupBox8.Controls.Add(button16);
            groupBox8.Controls.Add(button15);
            groupBox8.Controls.Add(button14);
            groupBox8.Controls.Add(button13);
            groupBox8.Controls.Add(checkBox17);
            groupBox8.Location = new Point(415, 3);
            groupBox8.Name = "groupBox8";
            groupBox8.Size = new Size(200, 806);
            groupBox8.TabIndex = 7;
            groupBox8.TabStop = false;
            groupBox8.Text = "Static Sync Stat Bonus";
            // 
            // button23
            // 
            button23.Location = new Point(6, 751);
            button23.Name = "button23";
            button23.Size = new Size(188, 29);
            button23.TabIndex = 29;
            button23.Text = "Enemy Move";
            button23.UseVisualStyleBackColor = true;
            button23.Click += Button23_Click;
            // 
            // button24
            // 
            button24.Location = new Point(6, 716);
            button24.Name = "button24";
            button24.Size = new Size(188, 29);
            button24.TabIndex = 28;
            button24.Text = "Enemy Build";
            button24.UseVisualStyleBackColor = true;
            button24.Click += Button24_Click;
            // 
            // button25
            // 
            button25.Location = new Point(6, 681);
            button25.Name = "button25";
            button25.Size = new Size(188, 29);
            button25.TabIndex = 27;
            button25.Text = "Enemy Resistance";
            button25.UseVisualStyleBackColor = true;
            button25.Click += Button25_Click;
            // 
            // button26
            // 
            button26.Location = new Point(6, 646);
            button26.Name = "button26";
            button26.Size = new Size(188, 29);
            button26.TabIndex = 26;
            button26.Text = "Enemy Magic";
            button26.UseVisualStyleBackColor = true;
            button26.Click += Button26_Click;
            // 
            // button27
            // 
            button27.Location = new Point(6, 611);
            button27.Name = "button27";
            button27.Size = new Size(188, 29);
            button27.TabIndex = 25;
            button27.Text = "Enemy Defense";
            button27.UseVisualStyleBackColor = true;
            button27.Click += Button27_Click;
            // 
            // button28
            // 
            button28.Location = new Point(6, 576);
            button28.Name = "button28";
            button28.Size = new Size(188, 29);
            button28.TabIndex = 24;
            button28.Text = "Enemy Luck";
            button28.UseVisualStyleBackColor = true;
            button28.Click += Button28_Click;
            // 
            // button29
            // 
            button29.Location = new Point(6, 541);
            button29.Name = "button29";
            button29.Size = new Size(188, 29);
            button29.TabIndex = 23;
            button29.Text = "Enemy Speed";
            button29.UseVisualStyleBackColor = true;
            button29.Click += Button29_Click;
            // 
            // button30
            // 
            button30.Location = new Point(6, 506);
            button30.Name = "button30";
            button30.Size = new Size(188, 29);
            button30.TabIndex = 22;
            button30.Text = "Enemy Dexterity";
            button30.UseVisualStyleBackColor = true;
            button30.Click += Button30_Click;
            // 
            // button31
            // 
            button31.Location = new Point(6, 471);
            button31.Name = "button31";
            button31.Size = new Size(188, 29);
            button31.TabIndex = 21;
            button31.Text = "Enemy Strength";
            button31.UseVisualStyleBackColor = true;
            button31.Click += Button31_Click;
            // 
            // button32
            // 
            button32.Location = new Point(6, 436);
            button32.Name = "button32";
            button32.Size = new Size(188, 29);
            button32.TabIndex = 20;
            button32.Text = "Enemy HP";
            button32.UseVisualStyleBackColor = true;
            button32.Click += Button32_Click;
            // 
            // checkBox12
            // 
            checkBox12.AutoSize = true;
            checkBox12.Location = new Point(6, 406);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new Size(188, 24);
            checkBox12.TabIndex = 19;
            checkBox12.Text = "Randomize enemy stats";
            toolTip1.SetToolTip(checkBox12, "This will alter the stat changes that are continuously in effect while *opponents* are synced with emblems.");
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // button22
            // 
            button22.Location = new Point(6, 371);
            button22.Name = "button22";
            button22.Size = new Size(188, 29);
            button22.TabIndex = 18;
            button22.Text = "Ally Move";
            button22.UseVisualStyleBackColor = true;
            button22.Click += Button22_Click;
            // 
            // button21
            // 
            button21.Location = new Point(6, 336);
            button21.Name = "button21";
            button21.Size = new Size(188, 29);
            button21.TabIndex = 17;
            button21.Text = "Ally Build";
            button21.UseVisualStyleBackColor = true;
            button21.Click += Button21_Click;
            // 
            // button20
            // 
            button20.Location = new Point(6, 301);
            button20.Name = "button20";
            button20.Size = new Size(188, 29);
            button20.TabIndex = 15;
            button20.Text = "Ally Resistance";
            button20.UseVisualStyleBackColor = true;
            button20.Click += Button20_Click;
            // 
            // button19
            // 
            button19.Location = new Point(6, 266);
            button19.Name = "button19";
            button19.Size = new Size(188, 29);
            button19.TabIndex = 13;
            button19.Text = "Ally Magic";
            button19.UseVisualStyleBackColor = true;
            button19.Click += Button19_Click;
            // 
            // button18
            // 
            button18.Location = new Point(6, 231);
            button18.Name = "button18";
            button18.Size = new Size(188, 29);
            button18.TabIndex = 11;
            button18.Text = "Ally Defense";
            button18.UseVisualStyleBackColor = true;
            button18.Click += Button18_Click;
            // 
            // button17
            // 
            button17.Location = new Point(6, 196);
            button17.Name = "button17";
            button17.Size = new Size(188, 29);
            button17.TabIndex = 9;
            button17.Text = "Ally Luck";
            button17.UseVisualStyleBackColor = true;
            button17.Click += Button17_Click;
            // 
            // button16
            // 
            button16.Location = new Point(6, 161);
            button16.Name = "button16";
            button16.Size = new Size(188, 29);
            button16.TabIndex = 7;
            button16.Text = "Ally Speed";
            button16.UseVisualStyleBackColor = true;
            button16.Click += Button16_Click;
            // 
            // button15
            // 
            button15.Location = new Point(6, 126);
            button15.Name = "button15";
            button15.Size = new Size(188, 29);
            button15.TabIndex = 5;
            button15.Text = "Ally Dexterity";
            button15.UseVisualStyleBackColor = true;
            button15.Click += Button15_Click;
            // 
            // button14
            // 
            button14.Location = new Point(6, 91);
            button14.Name = "button14";
            button14.Size = new Size(188, 29);
            button14.TabIndex = 3;
            button14.Text = "Ally Strength";
            button14.UseVisualStyleBackColor = true;
            button14.Click += Button14_Click;
            // 
            // button13
            // 
            button13.Location = new Point(6, 56);
            button13.Name = "button13";
            button13.Size = new Size(188, 29);
            button13.TabIndex = 1;
            button13.Text = "Ally HP";
            button13.UseVisualStyleBackColor = true;
            button13.Click += Button13_Click;
            // 
            // checkBox17
            // 
            checkBox17.AutoSize = true;
            checkBox17.Location = new Point(6, 26);
            checkBox17.Name = "checkBox17";
            checkBox17.Size = new Size(167, 24);
            checkBox17.TabIndex = 0;
            checkBox17.Text = "Randomize ally stats";
            toolTip1.SetToolTip(checkBox17, "This will alter the stat changes that are continuously in effect while your characters are synced with emblems. These stat changes are typically exclusive to *enemies*.");
            checkBox17.UseVisualStyleBackColor = true;
            // 
            // groupBox9
            // 
            groupBox9.AutoSize = true;
            groupBox9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox9.Controls.Add(label2);
            groupBox9.Controls.Add(numericUpDown2);
            groupBox9.Controls.Add(checkBox13);
            groupBox9.Location = new Point(621, 3);
            groupBox9.Name = "groupBox9";
            groupBox9.Size = new Size(200, 129);
            groupBox9.TabIndex = 4;
            groupBox9.TabStop = false;
            groupBox9.Text = "Weapon Restriction";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 53);
            label2.Name = "label2";
            label2.Size = new Size(157, 20);
            label2.TabIndex = 6;
            label2.Text = "Weapon Restriction %:";
            // 
            // numericUpDown2
            // 
            numericUpDown2.DecimalPlaces = 3;
            numericUpDown2.Location = new Point(6, 76);
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(188, 27);
            numericUpDown2.TabIndex = 5;
            toolTip1.SetToolTip(numericUpDown2, "Please specify the *percentage* of emblems that should be restricted to engage weapons.");
            numericUpDown2.Value = new decimal(new int[] { 5, 0, 0, 0 });
            // 
            // checkBox13
            // 
            checkBox13.AutoSize = true;
            checkBox13.Location = new Point(6, 26);
            checkBox13.Name = "checkBox13";
            checkBox13.Size = new Size(106, 24);
            checkBox13.TabIndex = 0;
            checkBox13.Text = "Randomize";
            toolTip1.SetToolTip(checkBox13, "This will reassign which emblems restrict your *characters* to engage weapons while engaged. However, it may cause bugged attack animations.");
            checkBox13.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 60000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            // 
            // GodGeneralForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(836, 433);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "GodGeneralForm";
            Text = "General Emblem Settings";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            groupBox9.ResumeLayout(false);
            groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        public CheckBox checkBox1;
        private GroupBox groupBox1;
        public Button button1;
        private GroupBox groupBox2;
        public CheckBox checkBox3;
        public Button button2;
        public CheckBox checkBox2;
        private Label label1;
        public NumericUpDown numericUpDown1;
        private GroupBox groupBox3;
        public Button button3;
        public CheckBox checkBox4;
        private GroupBox groupBox4;
        public Button button4;
        public CheckBox checkBox5;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox5;
        public Button button5;
        public CheckBox checkBox6;
        public CheckBox checkBox7;
        private GroupBox groupBox6;
        public CheckBox checkBox8;
        public CheckBox checkBox9;
        public Button button6;
        public CheckBox checkBox10;
        private GroupBox groupBox7;
        public Button button12;
        public Button button11;
        public Button button10;
        public Button button9;
        public Button button8;
        public Button button7;
        public CheckBox checkBox11;
        private GroupBox groupBox8;
        public Button button13;
        public CheckBox checkBox17;
        public Button button14;
        public Button button15;
        public Button button16;
        public Button button18;
        public Button button17;
        public Button button21;
        public Button button20;
        public Button button19;
        public Button button22;
        public Button button23;
        public Button button24;
        public Button button25;
        public Button button26;
        public Button button27;
        public Button button28;
        public Button button29;
        public Button button30;
        public Button button31;
        public Button button32;
        public CheckBox checkBox12;
        private GroupBox groupBox9;
        public CheckBox checkBox13;
        private Label label2;
        public NumericUpDown numericUpDown2;
        private ToolTip toolTip1;
    }
}