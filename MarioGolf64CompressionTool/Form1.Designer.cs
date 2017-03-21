namespace MarioGolf64CompressionTool
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
            this.hnudOffset = new Cereal64.Common.Controls.HexNumericUpDown();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.gbCompress = new System.Windows.Forms.GroupBox();
            this.gbDecompress = new System.Windows.Forms.GroupBox();
            this.lblDecOffset = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblDecFile = new System.Windows.Forms.Label();
            this.btnDecompress = new System.Windows.Forms.Button();
            this.btnCompress = new System.Windows.Forms.Button();
            this.btnLocateDec = new System.Windows.Forms.Button();
            this.txtDecFile = new System.Windows.Forms.TextBox();
            this.txtComFile = new System.Windows.Forms.TextBox();
            this.btnLocateCom = new System.Windows.Forms.Button();
            this.lblComFile = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.gbCompress.SuspendLayout();
            this.gbDecompress.SuspendLayout();
            this.SuspendLayout();
            // 
            // hnudOffset
            // 
            this.hnudOffset.Location = new System.Drawing.Point(26, 125);
            this.hnudOffset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.hnudOffset.Maximum = new decimal(new int[] {
            -1,
            0,
            0,
            0});
            this.hnudOffset.Minimum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.hnudOffset.Name = "hnudOffset";
            this.hnudOffset.Size = new System.Drawing.Size(156, 32);
            this.hnudOffset.State = Cereal64.Common.Controls.HexNumericUpDown.BaseState.hex;
            this.hnudOffset.TabIndex = 0;
            this.hnudOffset.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.gbDecompress);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.gbCompress);
            this.splitContainer.Size = new System.Drawing.Size(554, 249);
            this.splitContainer.SplitterDistance = 269;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 1;
            // 
            // gbCompress
            // 
            this.gbCompress.Controls.Add(this.txtComFile);
            this.gbCompress.Controls.Add(this.btnLocateCom);
            this.gbCompress.Controls.Add(this.lblComFile);
            this.gbCompress.Controls.Add(this.btnCompress);
            this.gbCompress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCompress.Location = new System.Drawing.Point(0, 0);
            this.gbCompress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbCompress.Name = "gbCompress";
            this.gbCompress.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbCompress.Size = new System.Drawing.Size(280, 249);
            this.gbCompress.TabIndex = 0;
            this.gbCompress.TabStop = false;
            this.gbCompress.Text = "Compress";
            // 
            // gbDecompress
            // 
            this.gbDecompress.Controls.Add(this.txtDecFile);
            this.gbDecompress.Controls.Add(this.btnLocateDec);
            this.gbDecompress.Controls.Add(this.btnDecompress);
            this.gbDecompress.Controls.Add(this.lblDecFile);
            this.gbDecompress.Controls.Add(this.lblDecOffset);
            this.gbDecompress.Controls.Add(this.hnudOffset);
            this.gbDecompress.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDecompress.Location = new System.Drawing.Point(0, 0);
            this.gbDecompress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDecompress.Name = "gbDecompress";
            this.gbDecompress.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDecompress.Size = new System.Drawing.Size(269, 249);
            this.gbDecompress.TabIndex = 0;
            this.gbDecompress.TabStop = false;
            this.gbDecompress.Text = "Decompress";
            // 
            // lblDecOffset
            // 
            this.lblDecOffset.AutoSize = true;
            this.lblDecOffset.Location = new System.Drawing.Point(25, 104);
            this.lblDecOffset.Name = "lblDecOffset";
            this.lblDecOffset.Size = new System.Drawing.Size(80, 17);
            this.lblDecOffset.TabIndex = 1;
            this.lblDecOffset.Text = "Data Offset";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.Filter = "All files|*.*";
            // 
            // lblDecFile
            // 
            this.lblDecFile.AutoSize = true;
            this.lblDecFile.Location = new System.Drawing.Point(23, 32);
            this.lblDecFile.Name = "lblDecFile";
            this.lblDecFile.Size = new System.Drawing.Size(113, 17);
            this.lblDecFile.TabIndex = 2;
            this.lblDecFile.Text = "Compressed File";
            // 
            // btnDecompress
            // 
            this.btnDecompress.Enabled = false;
            this.btnDecompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDecompress.Location = new System.Drawing.Point(61, 179);
            this.btnDecompress.Name = "btnDecompress";
            this.btnDecompress.Size = new System.Drawing.Size(145, 58);
            this.btnDecompress.TabIndex = 3;
            this.btnDecompress.Text = "Decompress";
            this.btnDecompress.UseVisualStyleBackColor = true;
            this.btnDecompress.Click += new System.EventHandler(this.btnDecompress_Click);
            // 
            // btnCompress
            // 
            this.btnCompress.Enabled = false;
            this.btnCompress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCompress.Location = new System.Drawing.Point(75, 179);
            this.btnCompress.Name = "btnCompress";
            this.btnCompress.Size = new System.Drawing.Size(145, 58);
            this.btnCompress.TabIndex = 4;
            this.btnCompress.Text = "Compress";
            this.btnCompress.UseVisualStyleBackColor = true;
            this.btnCompress.Click += new System.EventHandler(this.btnCompress_Click);
            // 
            // btnLocateDec
            // 
            this.btnLocateDec.Location = new System.Drawing.Point(144, 29);
            this.btnLocateDec.Name = "btnLocateDec";
            this.btnLocateDec.Size = new System.Drawing.Size(75, 23);
            this.btnLocateDec.TabIndex = 4;
            this.btnLocateDec.Text = "Locate...";
            this.btnLocateDec.UseVisualStyleBackColor = true;
            this.btnLocateDec.Click += new System.EventHandler(this.btnLocateDec_Click);
            // 
            // txtDecFile
            // 
            this.txtDecFile.Location = new System.Drawing.Point(26, 63);
            this.txtDecFile.Name = "txtDecFile";
            this.txtDecFile.ReadOnly = true;
            this.txtDecFile.Size = new System.Drawing.Size(215, 23);
            this.txtDecFile.TabIndex = 5;
            // 
            // txtComFile
            // 
            this.txtComFile.Location = new System.Drawing.Point(27, 63);
            this.txtComFile.Name = "txtComFile";
            this.txtComFile.ReadOnly = true;
            this.txtComFile.Size = new System.Drawing.Size(215, 23);
            this.txtComFile.TabIndex = 8;
            // 
            // btnLocateCom
            // 
            this.btnLocateCom.Location = new System.Drawing.Point(159, 29);
            this.btnLocateCom.Name = "btnLocateCom";
            this.btnLocateCom.Size = new System.Drawing.Size(75, 23);
            this.btnLocateCom.TabIndex = 7;
            this.btnLocateCom.Text = "Locate...";
            this.btnLocateCom.UseVisualStyleBackColor = true;
            this.btnLocateCom.Click += new System.EventHandler(this.btnLocateCom_Click);
            // 
            // lblComFile
            // 
            this.lblComFile.AutoSize = true;
            this.lblComFile.Location = new System.Drawing.Point(24, 32);
            this.lblComFile.Name = "lblComFile";
            this.lblComFile.Size = new System.Drawing.Size(129, 17);
            this.lblComFile.TabIndex = 6;
            this.lblComFile.Text = "Decompressed File";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 249);
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Mario Golf 64 Compression Tool";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.gbCompress.ResumeLayout(false);
            this.gbCompress.PerformLayout();
            this.gbDecompress.ResumeLayout(false);
            this.gbDecompress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Cereal64.Common.Controls.HexNumericUpDown hnudOffset;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox gbDecompress;
        private System.Windows.Forms.GroupBox gbCompress;
        private System.Windows.Forms.Label lblDecOffset;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblDecFile;
        private System.Windows.Forms.Button btnDecompress;
        private System.Windows.Forms.Button btnCompress;
        private System.Windows.Forms.Button btnLocateDec;
        private System.Windows.Forms.TextBox txtDecFile;
        private System.Windows.Forms.TextBox txtComFile;
        private System.Windows.Forms.Button btnLocateCom;
        private System.Windows.Forms.Label lblComFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

