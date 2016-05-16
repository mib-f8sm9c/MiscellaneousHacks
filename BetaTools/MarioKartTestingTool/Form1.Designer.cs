namespace MarioKartTestingTool
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
            this.btnLoadRom = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnExport = new System.Windows.Forms.Button();
            this.listBox = new System.Windows.Forms.ListBox();
            this.btnTestImportLevel = new System.Windows.Forms.Button();
            this.btnRender = new System.Windows.Forms.Button();
            this.btnTkmk = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadRom
            // 
            this.btnLoadRom.Location = new System.Drawing.Point(28, 12);
            this.btnLoadRom.Name = "btnLoadRom";
            this.btnLoadRom.Size = new System.Drawing.Size(111, 33);
            this.btnLoadRom.TabIndex = 0;
            this.btnLoadRom.Text = "Load Rom...";
            this.btnLoadRom.UseVisualStyleBackColor = true;
            this.btnLoadRom.Click += new System.EventHandler(this.btnLoadRom_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "Z64 Rom file|*.z64";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(333, 243);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(111, 33);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export...";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // listBox
            // 
            this.listBox.FormattingEnabled = true;
            this.listBox.Location = new System.Drawing.Point(12, 51);
            this.listBox.Name = "listBox";
            this.listBox.Size = new System.Drawing.Size(305, 225);
            this.listBox.TabIndex = 2;
            // 
            // btnTestImportLevel
            // 
            this.btnTestImportLevel.Location = new System.Drawing.Point(333, 51);
            this.btnTestImportLevel.Name = "btnTestImportLevel";
            this.btnTestImportLevel.Size = new System.Drawing.Size(111, 53);
            this.btnTestImportLevel.TabIndex = 3;
            this.btnTestImportLevel.Text = "Test Import Level";
            this.btnTestImportLevel.UseVisualStyleBackColor = true;
            this.btnTestImportLevel.Click += new System.EventHandler(this.btnTestImportLevel_Click);
            // 
            // btnRender
            // 
            this.btnRender.Location = new System.Drawing.Point(457, 243);
            this.btnRender.Name = "btnRender";
            this.btnRender.Size = new System.Drawing.Size(111, 33);
            this.btnRender.TabIndex = 4;
            this.btnRender.Text = "Render...";
            this.btnRender.UseVisualStyleBackColor = true;
            this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
            // 
            // btnTkmk
            // 
            this.btnTkmk.Location = new System.Drawing.Point(457, 51);
            this.btnTkmk.Name = "btnTkmk";
            this.btnTkmk.Size = new System.Drawing.Size(111, 53);
            this.btnTkmk.TabIndex = 5;
            this.btnTkmk.Text = "Test TKMKExport";
            this.btnTkmk.UseVisualStyleBackColor = true;
            this.btnTkmk.Click += new System.EventHandler(this.btnTkmk_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(323, 117);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(245, 110);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 288);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnTkmk);
            this.Controls.Add(this.btnRender);
            this.Controls.Add(this.btnTestImportLevel);
            this.Controls.Add(this.listBox);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnLoadRom);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLoadRom;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.ListBox listBox;
        private System.Windows.Forms.Button btnTestImportLevel;
        private System.Windows.Forms.Button btnRender;
        private System.Windows.Forms.Button btnTkmk;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

