namespace MarioGolf64LevelDataManager
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblOrder = new System.Windows.Forms.Label();
            this.cbOrder = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadRom = new System.Windows.Forms.Button();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cbLevels = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLength1 = new System.Windows.Forms.TextBox();
            this.txtOffset1 = new System.Windows.Forms.TextBox();
            this.btnAppend1 = new System.Windows.Forms.Button();
            this.btnReplace1 = new System.Windows.Forms.Button();
            this.btnExport1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtLength2 = new System.Windows.Forms.TextBox();
            this.txtOffset2 = new System.Windows.Forms.TextBox();
            this.btnAppend2 = new System.Windows.Forms.Button();
            this.btnReplace2 = new System.Windows.Forms.Button();
            this.btnExport2 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLength3 = new System.Windows.Forms.TextBox();
            this.txtOffset3 = new System.Windows.Forms.TextBox();
            this.btnAppend3 = new System.Windows.Forms.Button();
            this.btnReplace3 = new System.Windows.Forms.Button();
            this.btnExport3 = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtLength4 = new System.Windows.Forms.TextBox();
            this.txtOffset4 = new System.Windows.Forms.TextBox();
            this.btnAppend4 = new System.Windows.Forms.Button();
            this.btnReplace4 = new System.Windows.Forms.Button();
            this.btnExport4 = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtLength5 = new System.Windows.Forms.TextBox();
            this.txtOffset5 = new System.Windows.Forms.TextBox();
            this.btnAppend5 = new System.Windows.Forms.Button();
            this.btnReplace5 = new System.Windows.Forms.Button();
            this.btnExport5 = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtLength6 = new System.Windows.Forms.TextBox();
            this.txtOffset6 = new System.Windows.Forms.TextBox();
            this.btnAppend6 = new System.Windows.Forms.Button();
            this.btnReplace6 = new System.Windows.Forms.Button();
            this.btnExport6 = new System.Windows.Forms.Button();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.txtLength7 = new System.Windows.Forms.TextBox();
            this.txtOffset7 = new System.Windows.Forms.TextBox();
            this.btnAppend7 = new System.Windows.Forms.Button();
            this.btnReplace7 = new System.Windows.Forms.Button();
            this.btnExport7 = new System.Windows.Forms.Button();
            this.btnDebugTable = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveDataDialog = new System.Windows.Forms.SaveFileDialog();
            this.openDataDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblOrder);
            this.groupBox1.Controls.Add(this.cbOrder);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.btnLoadRom);
            this.groupBox1.Controls.Add(this.lblLevel);
            this.groupBox1.Controls.Add(this.cbLevels);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(586, 95);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Level Select";
            // 
            // lblOrder
            // 
            this.lblOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblOrder.AutoSize = true;
            this.lblOrder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrder.Location = new System.Drawing.Point(368, 22);
            this.lblOrder.Name = "lblOrder";
            this.lblOrder.Size = new System.Drawing.Size(51, 16);
            this.lblOrder.TabIndex = 6;
            this.lblOrder.Text = "Order:";
            // 
            // cbOrder
            // 
            this.cbOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrder.Enabled = false;
            this.cbOrder.FormattingEnabled = true;
            this.cbOrder.Items.AddRange(new object[] {
            "By Cup",
            "By Table"});
            this.cbOrder.Location = new System.Drawing.Point(348, 50);
            this.cbOrder.Name = "cbOrder";
            this.cbOrder.Size = new System.Drawing.Size(90, 24);
            this.cbOrder.TabIndex = 5;
            this.cbOrder.SelectedIndexChanged += new System.EventHandler(this.cbOrder_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(494, 22);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 48);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // btnLoadRom
            // 
            this.btnLoadRom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadRom.Location = new System.Drawing.Point(26, 26);
            this.btnLoadRom.Name = "btnLoadRom";
            this.btnLoadRom.Size = new System.Drawing.Size(75, 48);
            this.btnLoadRom.TabIndex = 3;
            this.btnLoadRom.Text = "Load...";
            this.btnLoadRom.UseVisualStyleBackColor = true;
            this.btnLoadRom.Click += new System.EventHandler(this.btnLoadRom_Click);
            // 
            // lblLevel
            // 
            this.lblLevel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLevel.AutoSize = true;
            this.lblLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevel.Location = new System.Drawing.Point(222, 26);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(50, 16);
            this.lblLevel.TabIndex = 1;
            this.lblLevel.Text = "Level:";
            // 
            // cbLevels
            // 
            this.cbLevels.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cbLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLevels.Enabled = false;
            this.cbLevels.FormattingEnabled = true;
            this.cbLevels.Location = new System.Drawing.Point(127, 50);
            this.cbLevels.Name = "cbLevels";
            this.cbLevels.Size = new System.Drawing.Size(199, 24);
            this.cbLevels.TabIndex = 0;
            this.cbLevels.SelectedIndexChanged += new System.EventHandler(this.cbLevels_SelectedIndexChanged);
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.groupBox2);
            this.flowLayoutPanel.Controls.Add(this.groupBox3);
            this.flowLayoutPanel.Controls.Add(this.groupBox4);
            this.flowLayoutPanel.Controls.Add(this.groupBox5);
            this.flowLayoutPanel.Controls.Add(this.groupBox6);
            this.flowLayoutPanel.Controls.Add(this.groupBox7);
            this.flowLayoutPanel.Controls.Add(this.groupBox8);
            this.flowLayoutPanel.Controls.Add(this.btnDebugTable);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel.Enabled = false;
            this.flowLayoutPanel.Location = new System.Drawing.Point(4, 99);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Size = new System.Drawing.Size(586, 426);
            this.flowLayoutPanel.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox2.Controls.Add(this.txtLength1);
            this.groupBox2.Controls.Add(this.txtOffset1);
            this.groupBox2.Controls.Add(this.btnAppend1);
            this.groupBox2.Controls.Add(this.btnReplace1);
            this.groupBox2.Controls.Add(this.btnExport1);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(285, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Block 1 (Height Data 1)";
            // 
            // txtLength1
            // 
            this.txtLength1.Location = new System.Drawing.Point(181, 29);
            this.txtLength1.Name = "txtLength1";
            this.txtLength1.ReadOnly = true;
            this.txtLength1.Size = new System.Drawing.Size(81, 22);
            this.txtLength1.TabIndex = 4;
            // 
            // txtOffset1
            // 
            this.txtOffset1.Location = new System.Drawing.Point(23, 29);
            this.txtOffset1.Name = "txtOffset1";
            this.txtOffset1.ReadOnly = true;
            this.txtOffset1.Size = new System.Drawing.Size(123, 22);
            this.txtOffset1.TabIndex = 3;
            // 
            // btnAppend1
            // 
            this.btnAppend1.Location = new System.Drawing.Point(200, 57);
            this.btnAppend1.Name = "btnAppend1";
            this.btnAppend1.Size = new System.Drawing.Size(75, 33);
            this.btnAppend1.TabIndex = 2;
            this.btnAppend1.Text = "Append...";
            this.btnAppend1.UseVisualStyleBackColor = true;
            this.btnAppend1.Click += new System.EventHandler(this.btnAppend1_Click);
            // 
            // btnReplace1
            // 
            this.btnReplace1.Location = new System.Drawing.Point(98, 57);
            this.btnReplace1.Name = "btnReplace1";
            this.btnReplace1.Size = new System.Drawing.Size(90, 33);
            this.btnReplace1.TabIndex = 1;
            this.btnReplace1.Text = "Replace...";
            this.btnReplace1.UseVisualStyleBackColor = true;
            this.btnReplace1.Click += new System.EventHandler(this.btnReplace1_Click);
            // 
            // btnExport1
            // 
            this.btnExport1.Location = new System.Drawing.Point(11, 57);
            this.btnExport1.Name = "btnExport1";
            this.btnExport1.Size = new System.Drawing.Size(75, 33);
            this.btnExport1.TabIndex = 0;
            this.btnExport1.Text = "Export...";
            this.btnExport1.UseVisualStyleBackColor = true;
            this.btnExport1.Click += new System.EventHandler(this.btnExport1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox3.Controls.Add(this.txtLength2);
            this.groupBox3.Controls.Add(this.txtOffset2);
            this.groupBox3.Controls.Add(this.btnAppend2);
            this.groupBox3.Controls.Add(this.btnReplace2);
            this.groupBox3.Controls.Add(this.btnExport2);
            this.groupBox3.Location = new System.Drawing.Point(294, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(285, 100);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Block 2 (Height Data 2)";
            // 
            // txtLength2
            // 
            this.txtLength2.Location = new System.Drawing.Point(179, 29);
            this.txtLength2.Name = "txtLength2";
            this.txtLength2.ReadOnly = true;
            this.txtLength2.Size = new System.Drawing.Size(81, 22);
            this.txtLength2.TabIndex = 6;
            // 
            // txtOffset2
            // 
            this.txtOffset2.Location = new System.Drawing.Point(21, 29);
            this.txtOffset2.Name = "txtOffset2";
            this.txtOffset2.ReadOnly = true;
            this.txtOffset2.Size = new System.Drawing.Size(123, 22);
            this.txtOffset2.TabIndex = 5;
            // 
            // btnAppend2
            // 
            this.btnAppend2.Location = new System.Drawing.Point(200, 57);
            this.btnAppend2.Name = "btnAppend2";
            this.btnAppend2.Size = new System.Drawing.Size(75, 33);
            this.btnAppend2.TabIndex = 2;
            this.btnAppend2.Text = "Append...";
            this.btnAppend2.UseVisualStyleBackColor = true;
            this.btnAppend2.Click += new System.EventHandler(this.btnAppend2_Click);
            // 
            // btnReplace2
            // 
            this.btnReplace2.Location = new System.Drawing.Point(98, 57);
            this.btnReplace2.Name = "btnReplace2";
            this.btnReplace2.Size = new System.Drawing.Size(90, 33);
            this.btnReplace2.TabIndex = 1;
            this.btnReplace2.Text = "Replace...";
            this.btnReplace2.UseVisualStyleBackColor = true;
            this.btnReplace2.Click += new System.EventHandler(this.btnReplace2_Click);
            // 
            // btnExport2
            // 
            this.btnExport2.Location = new System.Drawing.Point(11, 57);
            this.btnExport2.Name = "btnExport2";
            this.btnExport2.Size = new System.Drawing.Size(75, 33);
            this.btnExport2.TabIndex = 0;
            this.btnExport2.Text = "Export...";
            this.btnExport2.UseVisualStyleBackColor = true;
            this.btnExport2.Click += new System.EventHandler(this.btnExport2_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox4.Controls.Add(this.txtLength3);
            this.groupBox4.Controls.Add(this.txtOffset3);
            this.groupBox4.Controls.Add(this.btnAppend3);
            this.groupBox4.Controls.Add(this.btnReplace3);
            this.groupBox4.Controls.Add(this.btnExport3);
            this.groupBox4.Location = new System.Drawing.Point(3, 109);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(285, 100);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Block 3 (Object Data)";
            // 
            // txtLength3
            // 
            this.txtLength3.Location = new System.Drawing.Point(181, 29);
            this.txtLength3.Name = "txtLength3";
            this.txtLength3.ReadOnly = true;
            this.txtLength3.Size = new System.Drawing.Size(81, 22);
            this.txtLength3.TabIndex = 6;
            // 
            // txtOffset3
            // 
            this.txtOffset3.Location = new System.Drawing.Point(23, 29);
            this.txtOffset3.Name = "txtOffset3";
            this.txtOffset3.ReadOnly = true;
            this.txtOffset3.Size = new System.Drawing.Size(123, 22);
            this.txtOffset3.TabIndex = 5;
            // 
            // btnAppend3
            // 
            this.btnAppend3.Location = new System.Drawing.Point(200, 57);
            this.btnAppend3.Name = "btnAppend3";
            this.btnAppend3.Size = new System.Drawing.Size(75, 33);
            this.btnAppend3.TabIndex = 2;
            this.btnAppend3.Text = "Append...";
            this.btnAppend3.UseVisualStyleBackColor = true;
            this.btnAppend3.Click += new System.EventHandler(this.btnAppend3_Click);
            // 
            // btnReplace3
            // 
            this.btnReplace3.Location = new System.Drawing.Point(98, 57);
            this.btnReplace3.Name = "btnReplace3";
            this.btnReplace3.Size = new System.Drawing.Size(90, 33);
            this.btnReplace3.TabIndex = 1;
            this.btnReplace3.Text = "Replace...";
            this.btnReplace3.UseVisualStyleBackColor = true;
            this.btnReplace3.Click += new System.EventHandler(this.btnReplace3_Click);
            // 
            // btnExport3
            // 
            this.btnExport3.Location = new System.Drawing.Point(11, 57);
            this.btnExport3.Name = "btnExport3";
            this.btnExport3.Size = new System.Drawing.Size(75, 33);
            this.btnExport3.TabIndex = 0;
            this.btnExport3.Text = "Export...";
            this.btnExport3.UseVisualStyleBackColor = true;
            this.btnExport3.Click += new System.EventHandler(this.btnExport3_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox5.Controls.Add(this.txtLength4);
            this.groupBox5.Controls.Add(this.txtOffset4);
            this.groupBox5.Controls.Add(this.btnAppend4);
            this.groupBox5.Controls.Add(this.btnReplace4);
            this.groupBox5.Controls.Add(this.btnExport4);
            this.groupBox5.Location = new System.Drawing.Point(294, 109);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(285, 100);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Block 4 (?)";
            // 
            // txtLength4
            // 
            this.txtLength4.Location = new System.Drawing.Point(179, 29);
            this.txtLength4.Name = "txtLength4";
            this.txtLength4.ReadOnly = true;
            this.txtLength4.Size = new System.Drawing.Size(81, 22);
            this.txtLength4.TabIndex = 6;
            // 
            // txtOffset4
            // 
            this.txtOffset4.Location = new System.Drawing.Point(21, 29);
            this.txtOffset4.Name = "txtOffset4";
            this.txtOffset4.ReadOnly = true;
            this.txtOffset4.Size = new System.Drawing.Size(123, 22);
            this.txtOffset4.TabIndex = 5;
            // 
            // btnAppend4
            // 
            this.btnAppend4.Location = new System.Drawing.Point(200, 57);
            this.btnAppend4.Name = "btnAppend4";
            this.btnAppend4.Size = new System.Drawing.Size(75, 33);
            this.btnAppend4.TabIndex = 2;
            this.btnAppend4.Text = "Append...";
            this.btnAppend4.UseVisualStyleBackColor = true;
            this.btnAppend4.Click += new System.EventHandler(this.btnAppend4_Click);
            // 
            // btnReplace4
            // 
            this.btnReplace4.Location = new System.Drawing.Point(98, 57);
            this.btnReplace4.Name = "btnReplace4";
            this.btnReplace4.Size = new System.Drawing.Size(90, 33);
            this.btnReplace4.TabIndex = 1;
            this.btnReplace4.Text = "Replace...";
            this.btnReplace4.UseVisualStyleBackColor = true;
            this.btnReplace4.Click += new System.EventHandler(this.btnReplace4_Click);
            // 
            // btnExport4
            // 
            this.btnExport4.Location = new System.Drawing.Point(11, 57);
            this.btnExport4.Name = "btnExport4";
            this.btnExport4.Size = new System.Drawing.Size(75, 33);
            this.btnExport4.TabIndex = 0;
            this.btnExport4.Text = "Export...";
            this.btnExport4.UseVisualStyleBackColor = true;
            this.btnExport4.Click += new System.EventHandler(this.btnExport4_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox6.Controls.Add(this.txtLength5);
            this.groupBox6.Controls.Add(this.txtOffset5);
            this.groupBox6.Controls.Add(this.btnAppend5);
            this.groupBox6.Controls.Add(this.btnReplace5);
            this.groupBox6.Controls.Add(this.btnExport5);
            this.groupBox6.Location = new System.Drawing.Point(3, 215);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(285, 100);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Block 5 (?)";
            // 
            // txtLength5
            // 
            this.txtLength5.Location = new System.Drawing.Point(181, 29);
            this.txtLength5.Name = "txtLength5";
            this.txtLength5.ReadOnly = true;
            this.txtLength5.Size = new System.Drawing.Size(81, 22);
            this.txtLength5.TabIndex = 6;
            // 
            // txtOffset5
            // 
            this.txtOffset5.Location = new System.Drawing.Point(23, 29);
            this.txtOffset5.Name = "txtOffset5";
            this.txtOffset5.ReadOnly = true;
            this.txtOffset5.Size = new System.Drawing.Size(123, 22);
            this.txtOffset5.TabIndex = 5;
            // 
            // btnAppend5
            // 
            this.btnAppend5.Location = new System.Drawing.Point(200, 57);
            this.btnAppend5.Name = "btnAppend5";
            this.btnAppend5.Size = new System.Drawing.Size(75, 33);
            this.btnAppend5.TabIndex = 2;
            this.btnAppend5.Text = "Append...";
            this.btnAppend5.UseVisualStyleBackColor = true;
            this.btnAppend5.Click += new System.EventHandler(this.btnAppend5_Click);
            // 
            // btnReplace5
            // 
            this.btnReplace5.Location = new System.Drawing.Point(98, 57);
            this.btnReplace5.Name = "btnReplace5";
            this.btnReplace5.Size = new System.Drawing.Size(90, 33);
            this.btnReplace5.TabIndex = 1;
            this.btnReplace5.Text = "Replace...";
            this.btnReplace5.UseVisualStyleBackColor = true;
            this.btnReplace5.Click += new System.EventHandler(this.btnReplace5_Click);
            // 
            // btnExport5
            // 
            this.btnExport5.Location = new System.Drawing.Point(11, 57);
            this.btnExport5.Name = "btnExport5";
            this.btnExport5.Size = new System.Drawing.Size(75, 33);
            this.btnExport5.TabIndex = 0;
            this.btnExport5.Text = "Export...";
            this.btnExport5.UseVisualStyleBackColor = true;
            this.btnExport5.Click += new System.EventHandler(this.btnExport5_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox7.Controls.Add(this.txtLength6);
            this.groupBox7.Controls.Add(this.txtOffset6);
            this.groupBox7.Controls.Add(this.btnAppend6);
            this.groupBox7.Controls.Add(this.btnReplace6);
            this.groupBox7.Controls.Add(this.btnExport6);
            this.groupBox7.Location = new System.Drawing.Point(294, 215);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(285, 100);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Block 6 (?)";
            // 
            // txtLength6
            // 
            this.txtLength6.Location = new System.Drawing.Point(179, 29);
            this.txtLength6.Name = "txtLength6";
            this.txtLength6.ReadOnly = true;
            this.txtLength6.Size = new System.Drawing.Size(81, 22);
            this.txtLength6.TabIndex = 6;
            // 
            // txtOffset6
            // 
            this.txtOffset6.Location = new System.Drawing.Point(21, 29);
            this.txtOffset6.Name = "txtOffset6";
            this.txtOffset6.ReadOnly = true;
            this.txtOffset6.Size = new System.Drawing.Size(123, 22);
            this.txtOffset6.TabIndex = 5;
            // 
            // btnAppend6
            // 
            this.btnAppend6.Location = new System.Drawing.Point(200, 57);
            this.btnAppend6.Name = "btnAppend6";
            this.btnAppend6.Size = new System.Drawing.Size(75, 33);
            this.btnAppend6.TabIndex = 2;
            this.btnAppend6.Text = "Append...";
            this.btnAppend6.UseVisualStyleBackColor = true;
            this.btnAppend6.Click += new System.EventHandler(this.btnAppend6_Click);
            // 
            // btnReplace6
            // 
            this.btnReplace6.Location = new System.Drawing.Point(98, 57);
            this.btnReplace6.Name = "btnReplace6";
            this.btnReplace6.Size = new System.Drawing.Size(90, 33);
            this.btnReplace6.TabIndex = 1;
            this.btnReplace6.Text = "Replace...";
            this.btnReplace6.UseVisualStyleBackColor = true;
            this.btnReplace6.Click += new System.EventHandler(this.btnReplace6_Click);
            // 
            // btnExport6
            // 
            this.btnExport6.Location = new System.Drawing.Point(11, 57);
            this.btnExport6.Name = "btnExport6";
            this.btnExport6.Size = new System.Drawing.Size(75, 33);
            this.btnExport6.TabIndex = 0;
            this.btnExport6.Text = "Export...";
            this.btnExport6.UseVisualStyleBackColor = true;
            this.btnExport6.Click += new System.EventHandler(this.btnExport6_Click);
            // 
            // groupBox8
            // 
            this.groupBox8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox8.Controls.Add(this.txtLength7);
            this.groupBox8.Controls.Add(this.txtOffset7);
            this.groupBox8.Controls.Add(this.btnAppend7);
            this.groupBox8.Controls.Add(this.btnReplace7);
            this.groupBox8.Controls.Add(this.btnExport7);
            this.groupBox8.Location = new System.Drawing.Point(3, 321);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(285, 100);
            this.groupBox8.TabIndex = 6;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Block 7 (Surface Map) [ENCODED]";
            // 
            // txtLength7
            // 
            this.txtLength7.Location = new System.Drawing.Point(181, 29);
            this.txtLength7.Name = "txtLength7";
            this.txtLength7.ReadOnly = true;
            this.txtLength7.Size = new System.Drawing.Size(81, 22);
            this.txtLength7.TabIndex = 6;
            // 
            // txtOffset7
            // 
            this.txtOffset7.Location = new System.Drawing.Point(23, 29);
            this.txtOffset7.Name = "txtOffset7";
            this.txtOffset7.ReadOnly = true;
            this.txtOffset7.Size = new System.Drawing.Size(123, 22);
            this.txtOffset7.TabIndex = 5;
            // 
            // btnAppend7
            // 
            this.btnAppend7.Location = new System.Drawing.Point(200, 57);
            this.btnAppend7.Name = "btnAppend7";
            this.btnAppend7.Size = new System.Drawing.Size(75, 33);
            this.btnAppend7.TabIndex = 2;
            this.btnAppend7.Text = "Append...";
            this.btnAppend7.UseVisualStyleBackColor = true;
            this.btnAppend7.Click += new System.EventHandler(this.btnAppend7_Click);
            // 
            // btnReplace7
            // 
            this.btnReplace7.Location = new System.Drawing.Point(98, 57);
            this.btnReplace7.Name = "btnReplace7";
            this.btnReplace7.Size = new System.Drawing.Size(90, 33);
            this.btnReplace7.TabIndex = 1;
            this.btnReplace7.Text = "Replace...";
            this.btnReplace7.UseVisualStyleBackColor = true;
            this.btnReplace7.Click += new System.EventHandler(this.btnReplace7_Click);
            // 
            // btnExport7
            // 
            this.btnExport7.Location = new System.Drawing.Point(11, 57);
            this.btnExport7.Name = "btnExport7";
            this.btnExport7.Size = new System.Drawing.Size(75, 33);
            this.btnExport7.TabIndex = 0;
            this.btnExport7.Text = "Export...";
            this.btnExport7.UseVisualStyleBackColor = true;
            this.btnExport7.Click += new System.EventHandler(this.btnExport7_Click);
            // 
            // btnDebugTable
            // 
            this.btnDebugTable.Location = new System.Drawing.Point(294, 321);
            this.btnDebugTable.Name = "btnDebugTable";
            this.btnDebugTable.Size = new System.Drawing.Size(125, 62);
            this.btnDebugTable.TabIndex = 7;
            this.btnDebugTable.Text = "DEBUG TABLE";
            this.btnDebugTable.UseVisualStyleBackColor = true;
            this.btnDebugTable.Visible = false;
            this.btnDebugTable.Click += new System.EventHandler(this.btnDebugTable_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "z64";
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Z64 File|*.z64";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "z64";
            this.saveFileDialog.Filter = "Z64 File|*.z64";
            // 
            // saveDataDialog
            // 
            this.saveDataDialog.DefaultExt = "bin";
            this.saveDataDialog.Filter = "Binary Files|*.bin";
            // 
            // openDataDialog
            // 
            this.openDataDialog.DefaultExt = "bin";
            this.openDataDialog.FileName = "openDataDialog";
            this.openDataDialog.Filter = "Binary Files|*.bin";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 529);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Text = "MainForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.flowLayoutPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbLevels;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAppend1;
        private System.Windows.Forms.Button btnReplace1;
        private System.Windows.Forms.Button btnExport1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnAppend2;
        private System.Windows.Forms.Button btnReplace2;
        private System.Windows.Forms.Button btnExport2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnAppend3;
        private System.Windows.Forms.Button btnReplace3;
        private System.Windows.Forms.Button btnExport3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnAppend4;
        private System.Windows.Forms.Button btnReplace4;
        private System.Windows.Forms.Button btnExport4;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnAppend5;
        private System.Windows.Forms.Button btnReplace5;
        private System.Windows.Forms.Button btnExport5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnAppend6;
        private System.Windows.Forms.Button btnReplace6;
        private System.Windows.Forms.Button btnExport6;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnAppend7;
        private System.Windows.Forms.Button btnReplace7;
        private System.Windows.Forms.Button btnExport7;
        private System.Windows.Forms.Button btnLoadRom;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.SaveFileDialog saveDataDialog;
        private System.Windows.Forms.OpenFileDialog openDataDialog;
        private System.Windows.Forms.TextBox txtOffset1;
        private System.Windows.Forms.TextBox txtLength1;
        private System.Windows.Forms.TextBox txtLength2;
        private System.Windows.Forms.TextBox txtOffset2;
        private System.Windows.Forms.TextBox txtLength3;
        private System.Windows.Forms.TextBox txtOffset3;
        private System.Windows.Forms.TextBox txtLength4;
        private System.Windows.Forms.TextBox txtOffset4;
        private System.Windows.Forms.TextBox txtLength5;
        private System.Windows.Forms.TextBox txtOffset5;
        private System.Windows.Forms.TextBox txtLength6;
        private System.Windows.Forms.TextBox txtOffset6;
        private System.Windows.Forms.TextBox txtLength7;
        private System.Windows.Forms.TextBox txtOffset7;
        private System.Windows.Forms.Label lblOrder;
        private System.Windows.Forms.ComboBox cbOrder;
        private System.Windows.Forms.Button btnDebugTable;
    }
}