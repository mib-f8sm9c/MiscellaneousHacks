using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MarioGolf64CompressionTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLocateDec_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtDecFile.Text = openFileDialog.FileName;
                btnDecompress.Enabled = true;
            }
            else
            {
                txtDecFile.Text = string.Empty;
                btnDecompress.Enabled = false;
            }
        }

        private void btnLocateCom_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtComFile.Text = openFileDialog.FileName;
                btnCompress.Enabled = true;
            }
            else
            {
                txtComFile.Text = string.Empty;
                btnCompress.Enabled = false;
            }
        }

        private void btnDecompress_Click(object sender, EventArgs e)
        {
            if(!File.Exists(txtDecFile.Text))
            {
                MessageBox.Show("File doesn't exist!");
                return;
            }

            byte[] data = File.ReadAllBytes(txtDecFile.Text);
            int pointer = (int)hnudOffset.Value;

            byte[] output = MarioGolf64Compression.DecompressGolfHalfword(data, pointer, 0);

            if (output == null || output.Length == 0)
            {
                MessageBox.Show("Error during decompression!");
                return;
            }
            else
            {
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllBytes(Path.ChangeExtension(saveFileDialog.FileName, ".bin"), output);
                }
            }
        }

        private void btnCompress_Click(object sender, EventArgs e)
        {
            if (!File.Exists(txtComFile.Text))
            {
                MessageBox.Show("File doesn't exist!");
                return;
            }

            byte[] data = File.ReadAllBytes(txtComFile.Text);

            byte[] output = MarioGolf64Compression.CompressGolfHalfword(data);

            if (output == null || output.Length == 0)
            {
                MessageBox.Show("Error during compression!");
                return;
            }
            else
            {
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllBytes(Path.ChangeExtension(saveFileDialog.FileName, ".bin"), output);
                }
            }
        }

    }
}
