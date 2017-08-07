namespace StarRodSpriteCompatibilityConverter
{
    partial class Form1
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
            this.btnConvert = new System.Windows.Forms.Button();
            this.openSpritesheetDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnLoadSpriteSheet = new System.Windows.Forms.Button();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.gbSpriteSheet = new System.Windows.Forms.GroupBox();
            this.flpPalette = new System.Windows.Forms.FlowLayoutPanel();
            this.lblPalette = new System.Windows.Forms.Label();
            this.nudPalette = new System.Windows.Forms.NumericUpDown();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.pbImage = new System.Windows.Forms.PictureBox();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.cbCreatePalette = new System.Windows.Forms.CheckBox();
            this.lbImages = new System.Windows.Forms.ListBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openSpriteDialog = new System.Windows.Forms.OpenFileDialog();
            this.gbSpriteSheet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPalette)).BeginInit();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(272, 12);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(70, 42);
            this.btnConvert.TabIndex = 0;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Visible = false;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // openSpritesheetDialog
            // 
            this.openSpritesheetDialog.Filter = "Xml File|*.xml";
            // 
            // btnLoadSpriteSheet
            // 
            this.btnLoadSpriteSheet.Location = new System.Drawing.Point(12, 12);
            this.btnLoadSpriteSheet.Name = "btnLoadSpriteSheet";
            this.btnLoadSpriteSheet.Size = new System.Drawing.Size(179, 42);
            this.btnLoadSpriteSheet.TabIndex = 2;
            this.btnLoadSpriteSheet.Text = "Load Spritesheet...";
            this.btnLoadSpriteSheet.UseVisualStyleBackColor = true;
            this.btnLoadSpriteSheet.Click += new System.EventHandler(this.btnLoadSpriteSheet_Click);
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Location = new System.Drawing.Point(436, 12);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(179, 42);
            this.btnSaveChanges.TabIndex = 3;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // gbSpriteSheet
            // 
            this.gbSpriteSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSpriteSheet.Controls.Add(this.flpPalette);
            this.gbSpriteSheet.Controls.Add(this.lblPalette);
            this.gbSpriteSheet.Controls.Add(this.nudPalette);
            this.gbSpriteSheet.Controls.Add(this.pnlImage);
            this.gbSpriteSheet.Controls.Add(this.btnAddImage);
            this.gbSpriteSheet.Controls.Add(this.cbCreatePalette);
            this.gbSpriteSheet.Controls.Add(this.lbImages);
            this.gbSpriteSheet.Enabled = false;
            this.gbSpriteSheet.Location = new System.Drawing.Point(12, 60);
            this.gbSpriteSheet.Name = "gbSpriteSheet";
            this.gbSpriteSheet.Size = new System.Drawing.Size(603, 310);
            this.gbSpriteSheet.TabIndex = 4;
            this.gbSpriteSheet.TabStop = false;
            this.gbSpriteSheet.Text = "SpriteSheet";
            // 
            // flpPalette
            // 
            this.flpPalette.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPalette.Location = new System.Drawing.Point(75, 250);
            this.flpPalette.Name = "flpPalette";
            this.flpPalette.Size = new System.Drawing.Size(360, 54);
            this.flpPalette.TabIndex = 6;
            // 
            // lblPalette
            // 
            this.lblPalette.AutoSize = true;
            this.lblPalette.Location = new System.Drawing.Point(26, 252);
            this.lblPalette.Name = "lblPalette";
            this.lblPalette.Size = new System.Drawing.Size(43, 13);
            this.lblPalette.TabIndex = 5;
            this.lblPalette.Text = "Palette:";
            // 
            // nudPalette
            // 
            this.nudPalette.Location = new System.Drawing.Point(20, 268);
            this.nudPalette.Name = "nudPalette";
            this.nudPalette.Size = new System.Drawing.Size(49, 20);
            this.nudPalette.TabIndex = 4;
            this.nudPalette.ValueChanged += new System.EventHandler(this.nudPalette_ValueChanged);
            // 
            // pnlImage
            // 
            this.pnlImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlImage.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pnlImage.Controls.Add(this.pbImage);
            this.pnlImage.Location = new System.Drawing.Point(6, 19);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Size = new System.Drawing.Size(429, 225);
            this.pnlImage.TabIndex = 3;
            // 
            // pbImage
            // 
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(0, 0);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(429, 225);
            this.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pbImage.TabIndex = 0;
            this.pbImage.TabStop = false;
            // 
            // btnAddImage
            // 
            this.btnAddImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddImage.Location = new System.Drawing.Point(522, 251);
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.Size = new System.Drawing.Size(75, 46);
            this.btnAddImage.TabIndex = 1;
            this.btnAddImage.Text = "Add Images...";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // cbCreatePalette
            // 
            this.cbCreatePalette.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCreatePalette.AutoSize = true;
            this.cbCreatePalette.Checked = true;
            this.cbCreatePalette.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbCreatePalette.Location = new System.Drawing.Point(441, 254);
            this.cbCreatePalette.Name = "cbCreatePalette";
            this.cbCreatePalette.Size = new System.Drawing.Size(85, 43);
            this.cbCreatePalette.TabIndex = 2;
            this.cbCreatePalette.Text = "Create New \r\nPalette For\r\nImages\r\n";
            this.cbCreatePalette.UseVisualStyleBackColor = true;
            // 
            // lbImages
            // 
            this.lbImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbImages.FormattingEnabled = true;
            this.lbImages.Location = new System.Drawing.Point(441, 19);
            this.lbImages.Name = "lbImages";
            this.lbImages.Size = new System.Drawing.Size(156, 225);
            this.lbImages.TabIndex = 0;
            this.lbImages.SelectedIndexChanged += new System.EventHandler(this.lbImages_SelectedIndexChanged);
            // 
            // openSpriteDialog
            // 
            this.openSpriteDialog.Filter = "All Files|*.*";
            this.openSpriteDialog.Multiselect = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 382);
            this.Controls.Add(this.gbSpriteSheet);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.btnLoadSpriteSheet);
            this.Controls.Add(this.btnConvert);
            this.Name = "Form1";
            this.Text = "Star Rod Sprite Sheet Image Manager";
            this.gbSpriteSheet.ResumeLayout(false);
            this.gbSpriteSheet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPalette)).EndInit();
            this.pnlImage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.OpenFileDialog openSpritesheetDialog;
        private System.Windows.Forms.Button btnLoadSpriteSheet;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.GroupBox gbSpriteSheet;
        private System.Windows.Forms.ListBox lbImages;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.CheckBox cbCreatePalette;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox pbImage;
        private System.Windows.Forms.Label lblPalette;
        private System.Windows.Forms.NumericUpDown nudPalette;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.FlowLayoutPanel flpPalette;
        private System.Windows.Forms.OpenFileDialog openSpriteDialog;
    }
}

