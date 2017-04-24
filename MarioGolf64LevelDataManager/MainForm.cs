using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MarioGolf64LevelDataManager
{
    public partial class MainForm : Form
    {
        private const int LEVEL_COUNT = 145;
        private const int TABLE_OFFSET = 0xE473F0;
        private int NewDataOffset = 0x171F540;

        private List<TableEntry> _tableEntries;
        private byte[] _romData;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoadRom_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _romData = File.ReadAllBytes(openFileDialog.FileName);
                saveFileDialog.FileName = openFileDialog.FileName;
                ReadTableData();
            }
        }

        private void ReadTableData()
        {
            _tableEntries = new List<TableEntry>();
            List<Pointer> pointers = new List<Pointer>();

            int index = 0;

            for (int i = 0; i < LEVEL_COUNT * 7; i += 7)
            {
                pointers.Clear();
                byte[] data = new byte[4];
                for (int j = 0; j < 7; j++)
                {
                    Array.Copy(_romData, TABLE_OFFSET + (i + j) * 0x8, data, 0, 4);
                    uint length = BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
                    Array.Copy(_romData, TABLE_OFFSET + (i + j) * 0x8 + 4, data, 0, 4);
                    uint offset = BitConverter.ToUInt32(data.Reverse().ToArray(), 0);
                    pointers.Add(new Pointer(length, offset));
                }

                _tableEntries.Add(new TableEntry(CourseNames.NameList[index], index, pointers[0], pointers[1], pointers[2], pointers[3], pointers[4], pointers[5], pointers[6]));
                index++;
            }

            cbOrder.SelectedIndex = 0;
            cbOrder.Enabled = true;
            cbLevels.Enabled = true;
            flowLayoutPanel.Enabled = true;
        }

        private void cbOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableEntry selectedEntry = SelectedEntry;
            cbLevels.Items.Clear();
            switch (cbOrder.SelectedIndex)
            {
                case 0: //cup
                    foreach (int i in CourseNames.OrderedIndices)
                    {
                        cbLevels.Items.Add(_tableEntries[i]);
                    }
                    break;
                case 1: //table
                    foreach (TableEntry entry in _tableEntries)
                    {
                        cbLevels.Items.Add(entry);
                    }
                    break;
            }
            if (selectedEntry == null)
                cbLevels.SelectedIndex = 0;
            else
                cbLevels.SelectedIndex = _tableEntries.IndexOf(selectedEntry);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            ApplyTableEntries();
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllBytes(saveFileDialog.FileName, _romData);
            }
        }

        private void ApplyTableEntries()
        {
            for (int i = 0; i < _tableEntries.Count; i++)
            {
                Array.Copy(_tableEntries[i].HeightData1.GetAsBytes(), 0, _romData, TABLE_OFFSET + i * 7 * 8, 8);
                Array.Copy(_tableEntries[i].HeightData2.GetAsBytes(), 0, _romData, TABLE_OFFSET + (i * 7 + 1) * 8, 8);
                Array.Copy(_tableEntries[i].ObjectList.GetAsBytes(), 0, _romData, TABLE_OFFSET + (i * 7 + 2) * 8, 8);
                Array.Copy(_tableEntries[i].Pointer4.GetAsBytes(), 0, _romData, TABLE_OFFSET + (i * 7 + 3) * 8, 8);
                Array.Copy(_tableEntries[i].Pointer5.GetAsBytes(), 0, _romData, TABLE_OFFSET + (i * 7 + 4) * 8, 8);
                Array.Copy(_tableEntries[i].Pointer6.GetAsBytes(), 0, _romData, TABLE_OFFSET + (i * 7 + 5) * 8, 8);
                Array.Copy(_tableEntries[i].SurfaceMap.GetAsBytes(), 0, _romData, TABLE_OFFSET + (i * 7 + 6) * 8, 8);
            }
        }

        private TableEntry SelectedEntry { get { return (TableEntry)cbLevels.SelectedItem; } }

        private void cbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStats();
        }

        private void RefreshStats()
        {
            txtOffset1.Text = string.Format("0x{0:X}", SelectedEntry.HeightData1.Offset + TABLE_OFFSET);
            txtLength1.Text = string.Format("0x{0:X}", SelectedEntry.HeightData1.Length);

            txtOffset2.Text = string.Format("0x{0:X}", SelectedEntry.HeightData2.Offset + TABLE_OFFSET);
            txtLength2.Text = string.Format("0x{0:X}", SelectedEntry.HeightData2.Length);

            txtOffset3.Text = string.Format("0x{0:X}", SelectedEntry.ObjectList.Offset + TABLE_OFFSET);
            txtLength3.Text = string.Format("0x{0:X}", SelectedEntry.ObjectList.Length);

            txtOffset4.Text = string.Format("0x{0:X}", SelectedEntry.Pointer4.Offset + TABLE_OFFSET);
            txtLength4.Text = string.Format("0x{0:X}", SelectedEntry.Pointer4.Length);

            txtOffset5.Text = string.Format("0x{0:X}", SelectedEntry.Pointer5.Offset + TABLE_OFFSET);
            txtLength5.Text = string.Format("0x{0:X}", SelectedEntry.Pointer5.Length);

            txtOffset6.Text = string.Format("0x{0:X}", SelectedEntry.Pointer6.Offset + TABLE_OFFSET);
            txtLength6.Text = string.Format("0x{0:X}", SelectedEntry.Pointer6.Length);

            txtOffset7.Text = string.Format("0x{0:X}", SelectedEntry.SurfaceMap.Offset + TABLE_OFFSET);
            txtLength7.Text = string.Format("0x{0:X}", SelectedEntry.SurfaceMap.Length);
        }

        private void btnDebugTable_Click(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("table.txt"))
            {
                writer.WriteLine("^ Index ^ Course ^ Height Data 1 Offset ^ Height Data 1 Length ^ Height Data 2 Offset ^ Height Data 2 Length ^ Object List Offset ^ Object List Length ^ Block 4 Offset ^ Block 4 Length ^ Block 5 Offset ^ Block 5 Length ^ Block 6 Offset ^ Block 6 Length ^ Surface Map Offset ^ Surface Map Length ^");
                foreach (TableEntry entry in _tableEntries)
                {
                    writer.WriteLine(string.Format("| {0} | {1} | 0x{2:X} | 0x{3:X} | 0x{4:X} | 0x{5:X} | 0x{6:X} | 0x{7:X} | 0x{8:X} | 0x{9:X} | 0x{10:X} | 0x{11:X} | 0x{12:X} | 0x{13:X} | 0x{14:X} | 0x{15:X} |",
                        entry.Index,
                        entry.CourseName,
                        entry.HeightData1.Offset,
                        entry.HeightData1.Length,
                        entry.HeightData2.Offset,
                        entry.HeightData2.Length,
                        entry.ObjectList.Offset,
                        entry.ObjectList.Length,
                        entry.Pointer4.Offset,
                        entry.Pointer4.Length,
                        entry.Pointer5.Offset,
                        entry.Pointer5.Length,
                        entry.Pointer6.Offset,
                        entry.Pointer6.Length,
                        entry.SurfaceMap.Offset,
                        entry.SurfaceMap.Length));
                }
            }
        }

        private void btnExport1_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.HeightData1.Length];
                Array.Copy(_romData, SelectedEntry.HeightData1.Offset + TABLE_OFFSET, data, 0, SelectedEntry.HeightData1.Length);
                File.WriteAllBytes(saveDataDialog.FileName, data);
            }
        }

        private void btnReplace1_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                if (data.Length > SelectedEntry.HeightData1.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.HeightData1.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.HeightData1.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend1_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.HeightData1.Length = (uint)data.Length;
                SelectedEntry.HeightData1.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

        private void btnExport2_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.HeightData2.Length];
                Array.Copy(_romData, SelectedEntry.HeightData2.Offset + TABLE_OFFSET, data, 0, SelectedEntry.HeightData2.Length);
                File.WriteAllBytes(saveDataDialog.FileName, data);
            }
        }

        private void btnReplace2_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                if (data.Length > SelectedEntry.HeightData2.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.HeightData2.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.HeightData2.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend2_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.HeightData2.Length = (uint)data.Length;
                SelectedEntry.HeightData2.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

        private void btnExport3_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.ObjectList.Length];
                Array.Copy(_romData, SelectedEntry.ObjectList.Offset + TABLE_OFFSET, data, 0, SelectedEntry.ObjectList.Length);
                File.WriteAllBytes(saveDataDialog.FileName, data);
            }
        }

        private void btnReplace3_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                if (data.Length > SelectedEntry.ObjectList.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.ObjectList.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.ObjectList.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend3_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.ObjectList.Length = (uint)data.Length;
                SelectedEntry.ObjectList.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

        private void btnExport4_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.Pointer4.Length];
                Array.Copy(_romData, SelectedEntry.Pointer4.Offset + TABLE_OFFSET, data, 0, SelectedEntry.Pointer4.Length);
                File.WriteAllBytes(saveDataDialog.FileName, data);
            }
        }

        private void btnReplace4_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                if (data.Length > SelectedEntry.Pointer4.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.Pointer4.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.Pointer4.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend4_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.Pointer4.Length = (uint)data.Length;
                SelectedEntry.Pointer4.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

        private void btnExport5_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.Pointer5.Length];
                Array.Copy(_romData, SelectedEntry.Pointer5.Offset + TABLE_OFFSET, data, 0, SelectedEntry.Pointer5.Length);
                File.WriteAllBytes(saveDataDialog.FileName, data);
            }
        }

        private void btnReplace5_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                if (data.Length > SelectedEntry.Pointer5.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.Pointer5.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.Pointer5.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend5_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.Pointer5.Length = (uint)data.Length;
                SelectedEntry.Pointer5.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

        private void btnExport6_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.Pointer6.Length];
                Array.Copy(_romData, SelectedEntry.Pointer6.Offset + TABLE_OFFSET, data, 0, SelectedEntry.Pointer6.Length);
                File.WriteAllBytes(saveDataDialog.FileName, data);
            }
        }

        private void btnReplace6_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                if (data.Length > SelectedEntry.Pointer6.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.Pointer6.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.Pointer6.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend6_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = File.ReadAllBytes(openDataDialog.FileName);
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.Pointer6.Length = (uint)data.Length;
                SelectedEntry.Pointer6.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

        private void btnExport7_Click(object sender, EventArgs e)
        {
            if (saveDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = new byte[SelectedEntry.SurfaceMap.Length];
                Array.Copy(_romData, SelectedEntry.SurfaceMap.Offset + TABLE_OFFSET, data, 0, SelectedEntry.SurfaceMap.Length);
                File.WriteAllBytes(saveDataDialog.FileName, MarioGolf64CompressionTool.MarioGolf64Compression.DecompressGolfHalfword(data, 0, 0));
            }
        }

        private void btnReplace7_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = MarioGolf64CompressionTool.MarioGolf64Compression.CompressGolfHalfword(File.ReadAllBytes(openDataDialog.FileName));
                if (data.Length > SelectedEntry.SurfaceMap.Length)
                {
                    MessageBox.Show("Error: Input data is too long, cannot replace existing data! Try appending.", "Error", MessageBoxButtons.OK);
                    return;
                }
                Array.Copy(data, 0, _romData, SelectedEntry.SurfaceMap.Offset + TABLE_OFFSET, data.Length);
                SelectedEntry.SurfaceMap.Length = (uint)data.Length;
                RefreshStats();
            }
        }

        private void btnAppend7_Click(object sender, EventArgs e)
        {
            if (openDataDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] data = MarioGolf64CompressionTool.MarioGolf64Compression.CompressGolfHalfword(File.ReadAllBytes(openDataDialog.FileName));
                Array.Copy(data, 0, _romData, NewDataOffset, data.Length);
                SelectedEntry.SurfaceMap.Length = (uint)data.Length;
                SelectedEntry.SurfaceMap.Offset = (uint)NewDataOffset - TABLE_OFFSET;
                NewDataOffset += data.Length;
                RefreshStats();
            }
        }

    }

    internal class Pointer
    {
        public uint Length;
        public uint Offset;

        public Pointer(uint length, uint offset)
        {
            Length = length;
            Offset = offset;
        }

        public byte[] GetAsBytes()
        {
            byte[] bytes = new byte[8];
            byte[] length = BitConverter.GetBytes(Length).Reverse().ToArray();
            byte[] offset = BitConverter.GetBytes(Offset).Reverse().ToArray();

            Array.Copy(length, bytes, 4);
            Array.Copy(offset, 0, bytes, 4, 4);

            return bytes;
        }
    }

    internal class TableEntry
    {
        public string CourseName;
        public int Index;
        
        public Pointer HeightData1;
        public Pointer HeightData2;
        public Pointer ObjectList;
        public Pointer Pointer4;
        public Pointer Pointer5;
        public Pointer Pointer6;
        public Pointer SurfaceMap;

        public TableEntry(string courseName, int index, Pointer p1, Pointer p2, Pointer p3, Pointer p4, Pointer p5, Pointer p6, Pointer p7)
        {
            CourseName = courseName;
            Index = index;
            HeightData1 = p1;
            HeightData2 = p2;
            ObjectList = p3;
            Pointer4 = p4;
            Pointer5 = p5;
            Pointer6 = p6;
            SurfaceMap = p7;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", Index, CourseName);
        }
    }
}
