using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Cereal64.Common.Rom;
using Cereal64.Microcodes.F3DZEX;
using Cereal64.VisObj64.Data.OpenGL.Wrappers.F3DZEX;
using Cereal64.Microcodes.F3DZEX.DataElements;

namespace F3DEX2Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //load the file
                byte[] filedata = File.ReadAllBytes(openFileDialog1.FileName);

                RomProject.Instance.AddRomFile(new RomFile("dumpFile", 0, new Cereal64.Common.DataElements.UnknownData(0, filedata)));
                RomProject.Instance.AddDmaProfile(new DmaProfile("Default"));
                DmaSegment seg =new DmaSegment();
                seg.File = RomProject.Instance.Files[0];
                seg.FileStartOffset = 0;
                seg.FileEndOffset = seg.File.FileLength;
                seg.RamSegment = 0x80;
                seg.RamStartOffset = 0;
                DmaSegment seg2 = new DmaSegment();
                seg2.File = RomProject.Instance.Files[0];
                seg2.FileStartOffset = 0x0;// 0x310000;
                seg2.FileEndOffset = seg.File.FileLength;
                seg2.RamSegment = 0x02;
                seg2.RamStartOffset = 0x0;
                RomProject.Instance.SelectedDmaProfile.RamSegments.Add(0x80, new List<DmaSegment>() { seg });
                RomProject.Instance.SelectedDmaProfile.RamSegments.Add(0x02, new List<DmaSegment>() { seg2 });
                
                //perfect!

                MessageBox.Show("File loaded successfully!");
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int offset = (int)hexNumericUpDown1.Value;

            F3DZEXReaderPackage package = F3DZEXReader.ReadF3DZEXAt(RomProject.Instance.Files[0], offset);
            F3DZEXCommandCollection commands = (F3DZEXCommandCollection)package.Elements[RomProject.Instance.Files[0]].FirstOrDefault(
                c => c.FileOffset == offset && c is F3DZEXCommandCollection);

            openGLControl1.GraphicsCollections.Clear();
            openGLControl1.GraphicsCollections.Add(VO64F3DZEXReader.ReadCommands(commands, 0));

            openGLControl1.RefreshGraphics();

        }
    }
}
