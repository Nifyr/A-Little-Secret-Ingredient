namespace ALittleSecretIngredient.Forms
{
    partial class AssetTableForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssetTableForm));
            flowLayoutPanel1 = new FlowLayoutPanel();
            groupBox7 = new GroupBox();
            checkBox6 = new CheckBox();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox1 = new CheckBox();
            checkBox20 = new CheckBox();
            groupBox1 = new GroupBox();
            checkBox12 = new CheckBox();
            checkBox11 = new CheckBox();
            checkBox10 = new CheckBox();
            checkBox9 = new CheckBox();
            checkBox8 = new CheckBox();
            checkBox7 = new CheckBox();
            checkBox13 = new CheckBox();
            groupBox2 = new GroupBox();
            checkBox14 = new CheckBox();
            checkBox21 = new CheckBox();
            groupBox3 = new GroupBox();
            checkBox16 = new CheckBox();
            groupBox4 = new GroupBox();
            checkBox23 = new CheckBox();
            checkBox18 = new CheckBox();
            checkBox22 = new CheckBox();
            checkBox19 = new CheckBox();
            checkBox17 = new CheckBox();
            checkBox15 = new CheckBox();
            groupBox5 = new GroupBox();
            button19 = new Button();
            button17 = new Button();
            button16 = new Button();
            button15 = new Button();
            button14 = new Button();
            button13 = new Button();
            button12 = new Button();
            button11 = new Button();
            button10 = new Button();
            button9 = new Button();
            button8 = new Button();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            button18 = new Button();
            checkBox24 = new CheckBox();
            toolTip1 = new ToolTip(components);
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel5 = new TableLayoutPanel();
            tableLayoutPanel6 = new TableLayoutPanel();
            flowLayoutPanel1.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Controls.Add(groupBox7);
            flowLayoutPanel1.Controls.Add(groupBox1);
            flowLayoutPanel1.Controls.Add(groupBox2);
            flowLayoutPanel1.Controls.Add(groupBox3);
            flowLayoutPanel1.Controls.Add(groupBox4);
            flowLayoutPanel1.Controls.Add(groupBox5);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(3, 2, 3, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(732, 325);
            flowLayoutPanel1.TabIndex = 7;
            // 
            // groupBox7
            // 
            groupBox7.AutoSize = true;
            groupBox7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox7.Controls.Add(tableLayoutPanel1);
            groupBox7.Location = new Point(3, 3);
            groupBox7.MaximumSize = new Size(180, 0);
            groupBox7.MinimumSize = new Size(180, 0);
            groupBox7.Name = "groupBox7";
            groupBox7.Size = new Size(180, 197);
            groupBox7.TabIndex = 13;
            groupBox7.TabStop = false;
            groupBox7.Text = "Model Swap";
            // 
            // checkBox6
            // 
            checkBox6.AutoSize = true;
            checkBox6.Location = new Point(3, 153);
            checkBox6.Name = "checkBox6";
            checkBox6.Size = new Size(121, 19);
            checkBox6.TabIndex = 6;
            checkBox6.Text = "Restrict by gender";
            toolTip1.SetToolTip(checkBox6, "This limits model swaps to *exclusively* swap characters of the same gender.");
            checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Location = new Point(3, 128);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new Size(86, 19);
            checkBox5.TabIndex = 5;
            checkBox5.Text = "Mix groups";
            toolTip1.SetToolTip(checkBox5, "This enables model swaps to interchange identities among the groups of characters defined above. Please note that it *may* result in visual bugs.");
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Location = new Point(15, 103);
            checkBox4.Margin = new Padding(15, 3, 3, 3);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new Size(120, 19);
            checkBox4.TabIndex = 4;
            checkBox4.Text = "Include corrupted";
            toolTip1.SetToolTip(checkBox4, "This incorporates the corrupted versions of each emblem into model swaps. However, it is important to be aware that this may lead to visual *bugs.*");
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(3, 78);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(115, 19);
            checkBox3.TabIndex = 3;
            checkBox3.Text = "Shuffle emblems";
            toolTip1.SetToolTip(checkBox3, "This exchanges the identities of the emblems. It is important to note that it may result in *visual* bugs.");
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new Point(3, 53);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(135, 19);
            checkBox2.TabIndex = 2;
            checkBox2.Text = "Shuffle named NPCs";
            toolTip1.SetToolTip(checkBox2, "This exchanges the *identities* of non-playable characters. It is important to note that it may result in visual bugs.");
            checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(15, 28);
            checkBox1.Margin = new Padding(15, 3, 3, 3);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(129, 19);
            checkBox1.TabIndex = 1;
            checkBox1.Text = "Include protagonist";
            toolTip1.SetToolTip(checkBox1, "This guarantees that Alear's *identity* is likewise swapped. Please be aware that this will result in the removal of the option to select Alear's gender.");
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox20
            // 
            checkBox20.AutoSize = true;
            checkBox20.Location = new Point(3, 3);
            checkBox20.Name = "checkBox20";
            checkBox20.Size = new Size(110, 19);
            checkBox20.TabIndex = 0;
            checkBox20.Text = "Shuffle playable";
            toolTip1.SetToolTip(checkBox20, "This alters the identities of playable characters. However, it is important to note that it may result in *visual* bugs.");
            checkBox20.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.AutoSize = true;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(tableLayoutPanel2);
            groupBox1.Location = new Point(3, 206);
            groupBox1.MaximumSize = new Size(180, 0);
            groupBox1.MinimumSize = new Size(180, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(180, 197);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Outfit Swap";
            // 
            // checkBox12
            // 
            checkBox12.AutoSize = true;
            checkBox12.Location = new Point(3, 153);
            checkBox12.Name = "checkBox12";
            checkBox12.Size = new Size(86, 19);
            checkBox12.TabIndex = 6;
            checkBox12.Text = "Mix groups";
            toolTip1.SetToolTip(checkBox12, "This ensures that outfit swaps can take *place* across the predefined groups displayed above.");
            checkBox12.UseVisualStyleBackColor = true;
            // 
            // checkBox11
            // 
            checkBox11.AutoSize = true;
            checkBox11.Location = new Point(3, 128);
            checkBox11.Name = "checkBox11";
            checkBox11.Size = new Size(129, 19);
            checkBox11.TabIndex = 5;
            checkBox11.Text = "Shuffle shop outfits";
            toolTip1.SetToolTip(checkBox11, "This exchanges the *outfits* that characters can wear in the Somniel.");
            checkBox11.UseVisualStyleBackColor = true;
            // 
            // checkBox10
            // 
            checkBox10.AutoSize = true;
            checkBox10.Location = new Point(3, 103);
            checkBox10.Name = "checkBox10";
            checkBox10.Size = new Size(142, 19);
            checkBox10.TabIndex = 4;
            checkBox10.Text = "Shuffle engage outfits";
            toolTip1.SetToolTip(checkBox10, "This exchanges the outfits worn while *engaged.*");
            checkBox10.UseVisualStyleBackColor = true;
            // 
            // checkBox9
            // 
            checkBox9.AutoSize = true;
            checkBox9.Location = new Point(3, 78);
            checkBox9.Name = "checkBox9";
            checkBox9.Size = new Size(147, 19);
            checkBox9.TabIndex = 3;
            checkBox9.Text = "Shuffle emblem outfits";
            toolTip1.SetToolTip(checkBox9, "This exchanges the outfits *worn* by emblems.");
            checkBox9.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            checkBox8.AutoSize = true;
            checkBox8.Location = new Point(3, 53);
            checkBox8.Name = "checkBox8";
            checkBox8.Size = new Size(148, 19);
            checkBox8.TabIndex = 2;
            checkBox8.Text = "Shuffle personal outfits";
            toolTip1.SetToolTip(checkBox8, "This exchanges the outfits that are exclusively assigned to *each* individual character.");
            checkBox8.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            checkBox7.AutoSize = true;
            checkBox7.Location = new Point(15, 28);
            checkBox7.Margin = new Padding(15, 3, 3, 3);
            checkBox7.Name = "checkBox7";
            checkBox7.Size = new Size(120, 19);
            checkBox7.TabIndex = 1;
            checkBox7.Text = "Include corrupted";
            toolTip1.SetToolTip(checkBox7, "This incorporates the *corrupted* version of each class into outfit swaps as well.");
            checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox13
            // 
            checkBox13.AutoSize = true;
            checkBox13.Location = new Point(3, 3);
            checkBox13.Name = "checkBox13";
            checkBox13.Size = new Size(128, 19);
            checkBox13.TabIndex = 0;
            checkBox13.Text = "Shuffle class outfits";
            toolTip1.SetToolTip(checkBox13, "This alters the outfits worn by characters based on their *respective* classes.");
            checkBox13.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.AutoSize = true;
            groupBox2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox2.Controls.Add(tableLayoutPanel3);
            groupBox2.Location = new Point(3, 409);
            groupBox2.MaximumSize = new Size(180, 0);
            groupBox2.MinimumSize = new Size(180, 0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(180, 72);
            groupBox2.TabIndex = 15;
            groupBox2.TabStop = false;
            groupBox2.Text = "Character Color Palette";
            // 
            // checkBox14
            // 
            checkBox14.AutoSize = true;
            checkBox14.Location = new Point(3, 28);
            checkBox14.Name = "checkBox14";
            checkBox14.Size = new Size(142, 19);
            checkBox14.TabIndex = 1;
            checkBox14.Text = "Ensure color harmony";
            toolTip1.SetToolTip(checkBox14, "This ensures that the generated color palettes adhere to *principles* of color harmony.");
            checkBox14.UseVisualStyleBackColor = true;
            // 
            // checkBox21
            // 
            checkBox21.AutoSize = true;
            checkBox21.Location = new Point(3, 3);
            checkBox21.Name = "checkBox21";
            checkBox21.Size = new Size(147, 19);
            checkBox21.TabIndex = 0;
            checkBox21.Text = "Randomize outfit color";
            toolTip1.SetToolTip(checkBox21, "This alters the color palettes of *characters,* thereby impacting the outfits they wear.");
            checkBox21.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            groupBox3.AutoSize = true;
            groupBox3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox3.Controls.Add(tableLayoutPanel4);
            groupBox3.Location = new Point(3, 487);
            groupBox3.MaximumSize = new Size(180, 0);
            groupBox3.MinimumSize = new Size(180, 0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(180, 47);
            groupBox3.TabIndex = 16;
            groupBox3.TabStop = false;
            groupBox3.Text = "Mount Model Swap";
            // 
            // checkBox16
            // 
            checkBox16.AutoSize = true;
            checkBox16.Location = new Point(3, 3);
            checkBox16.Name = "checkBox16";
            checkBox16.Size = new Size(144, 19);
            checkBox16.TabIndex = 0;
            checkBox16.Text = "Shuffle mount models";
            toolTip1.SetToolTip(checkBox16, "This exchanges the appearances of the mounts that characters can *ride* on.");
            checkBox16.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            groupBox4.AutoSize = true;
            groupBox4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox4.Controls.Add(tableLayoutPanel5);
            groupBox4.Location = new Point(189, 3);
            groupBox4.MaximumSize = new Size(180, 0);
            groupBox4.MinimumSize = new Size(180, 0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(180, 172);
            groupBox4.TabIndex = 17;
            groupBox4.TabStop = false;
            groupBox4.Text = "Character Animations";
            // 
            // checkBox23
            // 
            checkBox23.AutoSize = true;
            checkBox23.Location = new Point(3, 128);
            checkBox23.Name = "checkBox23";
            checkBox23.Size = new Size(100, 19);
            checkBox23.TabIndex = 6;
            checkBox23.Text = "Shuffle in hub";
            toolTip1.SetToolTip(checkBox23, "This exchanges the animations of *characters* while in the Somniel and during post-battle exploration.");
            checkBox23.UseVisualStyleBackColor = true;
            // 
            // checkBox18
            // 
            checkBox18.AutoSize = true;
            checkBox18.Location = new Point(15, 103);
            checkBox18.Margin = new Padding(15, 3, 3, 3);
            checkBox18.Name = "checkBox18";
            checkBox18.Size = new Size(107, 19);
            checkBox18.TabIndex = 5;
            checkBox18.Text = "Include generic";
            toolTip1.SetToolTip(checkBox18, "This guarantees that *NPC* animations are also altered.");
            checkBox18.UseVisualStyleBackColor = true;
            // 
            // checkBox22
            // 
            checkBox22.AutoSize = true;
            checkBox22.Location = new Point(3, 78);
            checkBox22.Name = "checkBox22";
            checkBox22.Size = new Size(126, 19);
            checkBox22.TabIndex = 4;
            checkBox22.Text = "Shuffle in cutscene";
            toolTip1.SetToolTip(checkBox22, "This exchanges the animations of characters during *cutscenes.*");
            checkBox22.UseVisualStyleBackColor = true;
            // 
            // checkBox19
            // 
            checkBox19.AutoSize = true;
            checkBox19.Location = new Point(3, 53);
            checkBox19.Name = "checkBox19";
            checkBox19.Size = new Size(112, 19);
            checkBox19.TabIndex = 3;
            checkBox19.Text = "Shuffle in dialog";
            toolTip1.SetToolTip(checkBox19, "This exchanges the character poses *displayed* in text boxes during dialogues.");
            checkBox19.UseVisualStyleBackColor = true;
            // 
            // checkBox17
            // 
            checkBox17.AutoSize = true;
            checkBox17.Location = new Point(15, 28);
            checkBox17.Margin = new Padding(15, 3, 3, 3);
            checkBox17.Name = "checkBox17";
            checkBox17.Size = new Size(107, 19);
            checkBox17.TabIndex = 2;
            checkBox17.Text = "Include generic";
            toolTip1.SetToolTip(checkBox17, "This ensures that *NPC* animations are also altered.");
            checkBox17.UseVisualStyleBackColor = true;
            // 
            // checkBox15
            // 
            checkBox15.AutoSize = true;
            checkBox15.Location = new Point(3, 3);
            checkBox15.Name = "checkBox15";
            checkBox15.Size = new Size(110, 19);
            checkBox15.TabIndex = 0;
            checkBox15.Text = "Shuffle in menu";
            toolTip1.SetToolTip(checkBox15, "This exchanges the animations *displayed* in menus and while hovering over characters.");
            checkBox15.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            groupBox5.AutoSize = true;
            groupBox5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox5.Controls.Add(tableLayoutPanel6);
            groupBox5.Location = new Point(375, 3);
            groupBox5.MaximumSize = new Size(180, 0);
            groupBox5.MinimumSize = new Size(180, 0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(180, 636);
            groupBox5.TabIndex = 18;
            groupBox5.TabStop = false;
            groupBox5.Text = "Model Shape and Size";
            // 
            // button19
            // 
            button19.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button19.AutoSize = true;
            button19.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button19.Location = new Point(3, 586);
            button19.Name = "button19";
            button19.Size = new Size(168, 25);
            button19.TabIndex = 20;
            button19.Text = "Map Scale Wing";
            button19.UseVisualStyleBackColor = true;
            button19.Click += Button19_Click;
            // 
            // button17
            // 
            button17.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button17.AutoSize = true;
            button17.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button17.Location = new Point(3, 555);
            button17.Name = "button17";
            button17.Size = new Size(168, 25);
            button17.TabIndex = 19;
            button17.Text = "Map Scale Head";
            button17.UseVisualStyleBackColor = true;
            button17.Click += Button17_Click;
            // 
            // button16
            // 
            button16.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button16.AutoSize = true;
            button16.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button16.Location = new Point(3, 524);
            button16.Name = "button16";
            button16.Size = new Size(168, 25);
            button16.TabIndex = 18;
            button16.Text = "Map Scale All";
            button16.UseVisualStyleBackColor = true;
            button16.Click += Button16_Click;
            // 
            // button15
            // 
            button15.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button15.AutoSize = true;
            button15.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button15.Location = new Point(3, 493);
            button15.Name = "button15";
            button15.Size = new Size(168, 25);
            button15.TabIndex = 17;
            button15.Text = "Volume Scale Legs";
            button15.UseVisualStyleBackColor = true;
            button15.Click += Button15_Click;
            // 
            // button14
            // 
            button14.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button14.AutoSize = true;
            button14.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button14.Location = new Point(3, 462);
            button14.Name = "button14";
            button14.Size = new Size(168, 25);
            button14.TabIndex = 16;
            button14.Text = "Volume Scale Arms";
            button14.UseVisualStyleBackColor = true;
            button14.Click += Button14_Click;
            // 
            // button13
            // 
            button13.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button13.AutoSize = true;
            button13.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button13.Location = new Point(3, 431);
            button13.Name = "button13";
            button13.Size = new Size(168, 25);
            button13.TabIndex = 15;
            button13.Text = "Volume Torso";
            button13.UseVisualStyleBackColor = true;
            button13.Click += Button13_Click;
            // 
            // button12
            // 
            button12.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button12.AutoSize = true;
            button12.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button12.Location = new Point(3, 400);
            button12.Name = "button12";
            button12.Size = new Size(168, 25);
            button12.TabIndex = 14;
            button12.Text = "Volume Abdomen";
            button12.UseVisualStyleBackColor = true;
            button12.Click += Button12_Click;
            // 
            // button11
            // 
            button11.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button11.AutoSize = true;
            button11.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button11.Location = new Point(3, 369);
            button11.Name = "button11";
            button11.Size = new Size(168, 25);
            button11.TabIndex = 13;
            button11.Text = "Volume Bust";
            button11.UseVisualStyleBackColor = true;
            button11.Click += Button11_Click;
            // 
            // button10
            // 
            button10.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button10.AutoSize = true;
            button10.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button10.Location = new Point(3, 338);
            button10.Name = "button10";
            button10.Size = new Size(168, 25);
            button10.TabIndex = 12;
            button10.Text = "Volume Legs";
            button10.UseVisualStyleBackColor = true;
            button10.Click += Button10_Click;
            // 
            // button9
            // 
            button9.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button9.AutoSize = true;
            button9.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button9.Location = new Point(3, 307);
            button9.Name = "button9";
            button9.Size = new Size(168, 25);
            button9.TabIndex = 11;
            button9.Text = "Volume Arms";
            button9.UseVisualStyleBackColor = true;
            button9.Click += Button9_Click;
            // 
            // button8
            // 
            button8.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button8.AutoSize = true;
            button8.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button8.Location = new Point(3, 276);
            button8.Name = "button8";
            button8.Size = new Size(168, 25);
            button8.TabIndex = 10;
            button8.Text = "Scale Feet";
            button8.UseVisualStyleBackColor = true;
            button8.Click += Button8_Click;
            // 
            // button7
            // 
            button7.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button7.AutoSize = true;
            button7.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button7.Location = new Point(3, 245);
            button7.Name = "button7";
            button7.Size = new Size(168, 25);
            button7.TabIndex = 9;
            button7.Text = "Scale Legs";
            button7.UseVisualStyleBackColor = true;
            button7.Click += Button7_Click;
            // 
            // button6
            // 
            button6.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button6.AutoSize = true;
            button6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button6.Location = new Point(3, 214);
            button6.Name = "button6";
            button6.Size = new Size(168, 25);
            button6.TabIndex = 8;
            button6.Text = "Scale Hands";
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // button5
            // 
            button5.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button5.AutoSize = true;
            button5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button5.Location = new Point(3, 183);
            button5.Name = "button5";
            button5.Size = new Size(168, 25);
            button5.TabIndex = 7;
            button5.Text = "Scale Arms";
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // button4
            // 
            button4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button4.AutoSize = true;
            button4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button4.Location = new Point(3, 152);
            button4.Name = "button4";
            button4.Size = new Size(168, 25);
            button4.TabIndex = 6;
            button4.Text = "Scale Shoulders";
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button3.AutoSize = true;
            button3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button3.Location = new Point(3, 121);
            button3.Name = "button3";
            button3.Size = new Size(168, 25);
            button3.TabIndex = 5;
            button3.Text = "Scale Torso";
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button2.AutoSize = true;
            button2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button2.Location = new Point(3, 90);
            button2.Name = "button2";
            button2.Size = new Size(168, 25);
            button2.TabIndex = 4;
            button2.Text = "Scale Neck";
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button1.AutoSize = true;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(3, 59);
            button1.Name = "button1";
            button1.Size = new Size(168, 25);
            button1.TabIndex = 3;
            button1.Text = "Scale Head";
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button18
            // 
            button18.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            button18.AutoSize = true;
            button18.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button18.Location = new Point(3, 28);
            button18.Name = "button18";
            button18.Size = new Size(168, 25);
            button18.TabIndex = 2;
            button18.Text = "Scale All";
            button18.UseVisualStyleBackColor = true;
            button18.Click += Button18_Click;
            // 
            // checkBox24
            // 
            checkBox24.AutoSize = true;
            checkBox24.Location = new Point(3, 3);
            checkBox24.Name = "checkBox24";
            checkBox24.Size = new Size(85, 19);
            checkBox24.TabIndex = 0;
            checkBox24.Text = "Randomize";
            toolTip1.SetToolTip(checkBox24, "This alters the values that determine the shapes and *sizes* of characters' bodies.");
            checkBox24.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 60000;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(checkBox6, 0, 6);
            tableLayoutPanel1.Controls.Add(checkBox20, 0, 0);
            tableLayoutPanel1.Controls.Add(checkBox5, 0, 5);
            tableLayoutPanel1.Controls.Add(checkBox1, 0, 1);
            tableLayoutPanel1.Controls.Add(checkBox4, 0, 4);
            tableLayoutPanel1.Controls.Add(checkBox2, 0, 2);
            tableLayoutPanel1.Controls.Add(checkBox3, 0, 3);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 19);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 7;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(174, 175);
            tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Controls.Add(checkBox12, 0, 6);
            tableLayoutPanel2.Controls.Add(checkBox13, 0, 0);
            tableLayoutPanel2.Controls.Add(checkBox11, 0, 5);
            tableLayoutPanel2.Controls.Add(checkBox7, 0, 1);
            tableLayoutPanel2.Controls.Add(checkBox10, 0, 4);
            tableLayoutPanel2.Controls.Add(checkBox8, 0, 2);
            tableLayoutPanel2.Controls.Add(checkBox9, 0, 3);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 19);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 7;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(174, 175);
            tableLayoutPanel2.TabIndex = 8;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(checkBox14, 0, 1);
            tableLayoutPanel3.Controls.Add(checkBox21, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 19);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.Size = new Size(174, 50);
            tableLayoutPanel3.TabIndex = 8;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.AutoSize = true;
            tableLayoutPanel4.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(checkBox16, 0, 0);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 19);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.Size = new Size(174, 25);
            tableLayoutPanel4.TabIndex = 8;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Controls.Add(checkBox23, 0, 5);
            tableLayoutPanel5.Controls.Add(checkBox15, 0, 0);
            tableLayoutPanel5.Controls.Add(checkBox18, 0, 4);
            tableLayoutPanel5.Controls.Add(checkBox17, 0, 1);
            tableLayoutPanel5.Controls.Add(checkBox22, 0, 3);
            tableLayoutPanel5.Controls.Add(checkBox19, 0, 2);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(3, 19);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 6;
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.RowStyles.Add(new RowStyle());
            tableLayoutPanel5.Size = new Size(174, 150);
            tableLayoutPanel5.TabIndex = 8;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel6.Controls.Add(button19, 0, 19);
            tableLayoutPanel6.Controls.Add(checkBox24, 0, 0);
            tableLayoutPanel6.Controls.Add(button17, 0, 18);
            tableLayoutPanel6.Controls.Add(button18, 0, 1);
            tableLayoutPanel6.Controls.Add(button16, 0, 17);
            tableLayoutPanel6.Controls.Add(button1, 0, 2);
            tableLayoutPanel6.Controls.Add(button15, 0, 16);
            tableLayoutPanel6.Controls.Add(button2, 0, 3);
            tableLayoutPanel6.Controls.Add(button14, 0, 15);
            tableLayoutPanel6.Controls.Add(button3, 0, 4);
            tableLayoutPanel6.Controls.Add(button13, 0, 14);
            tableLayoutPanel6.Controls.Add(button4, 0, 5);
            tableLayoutPanel6.Controls.Add(button12, 0, 13);
            tableLayoutPanel6.Controls.Add(button5, 0, 6);
            tableLayoutPanel6.Controls.Add(button11, 0, 12);
            tableLayoutPanel6.Controls.Add(button6, 0, 7);
            tableLayoutPanel6.Controls.Add(button10, 0, 11);
            tableLayoutPanel6.Controls.Add(button7, 0, 8);
            tableLayoutPanel6.Controls.Add(button9, 0, 10);
            tableLayoutPanel6.Controls.Add(button8, 0, 9);
            tableLayoutPanel6.Dock = DockStyle.Fill;
            tableLayoutPanel6.Location = new Point(3, 19);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 20;
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.RowStyles.Add(new RowStyle());
            tableLayoutPanel6.Size = new Size(174, 614);
            tableLayoutPanel6.TabIndex = 21;
            // 
            // AssetTableForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(732, 325);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            Name = "AssetTableForm";
            Text = "Asset Table Settings";
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox7.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox7;
        public CheckBox checkBox5;
        public CheckBox checkBox4;
        public CheckBox checkBox3;
        public CheckBox checkBox2;
        public CheckBox checkBox1;
        public CheckBox checkBox20;
        public CheckBox checkBox6;
        private GroupBox groupBox1;
        public CheckBox checkBox13;
        public CheckBox checkBox7;
        public CheckBox checkBox8;
        public CheckBox checkBox11;
        public CheckBox checkBox10;
        public CheckBox checkBox9;
        public CheckBox checkBox12;
        private GroupBox groupBox2;
        public CheckBox checkBox14;
        public CheckBox checkBox21;
        private GroupBox groupBox3;
        public CheckBox checkBox16;
        private GroupBox groupBox4;
        public CheckBox checkBox15;
        public CheckBox checkBox17;
        public CheckBox checkBox19;
        public CheckBox checkBox18;
        public CheckBox checkBox22;
        public CheckBox checkBox23;
        private GroupBox groupBox5;
        public CheckBox checkBox24;
        public Button button19;
        public Button button17;
        public Button button16;
        public Button button15;
        public Button button14;
        public Button button13;
        public Button button12;
        public Button button11;
        public Button button10;
        public Button button9;
        public Button button8;
        public Button button7;
        public Button button6;
        public Button button5;
        public Button button4;
        public Button button3;
        public Button button2;
        public Button button1;
        public Button button18;
        private ToolTip toolTip1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private TableLayoutPanel tableLayoutPanel5;
        private TableLayoutPanel tableLayoutPanel6;
    }
}