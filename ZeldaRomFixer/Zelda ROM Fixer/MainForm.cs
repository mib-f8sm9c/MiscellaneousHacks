using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

///OoT ROM N64 Compatibility Fix
///Author: mib_f8sm9c
///Version: 0.1
///Date: 6/28/15
///Description: This program will convert Ocarina of Time ROMs that don't work on the original
///              N64 hardware into ROMs that do. Specifically, it fixes a problem with overloading
///              the vertex buffer (loading 33 vertices into a buffer that only has space for 32) and
///              a bad number in the 0xE2 command (the last byte should be 78, not 7C).
///              No guarantees that this will definitely fix any ROM, but it should be functional for
///              most. Direct questions/comments to mib.f8sm9c@gmail.com
///              
///Note: Totally forgot this while writing up the description, but this fix is for ROMs that have new
///       3d models in them imported through Model2N64. Other methods of importing might not be compatible
///       with this program.

namespace ZeldaROMFixer
{
    public partial class MainForm : Form
    {
        private static string[] goodFiletypeExtensions = { ".ROM", ".N64", ".Z64" };

        private static byte[] dlistCommand = new byte[8];

        private static byte[] overloadedVertexBufferCommand = new byte[4];
        private static int currentOVBCIndex = 0;
        private static byte overloadedVertexBufferSlot = 0x40;
        private static byte doubleTriangleCommand = 0x06;
        private static byte singleTriangleCommand = 0x05;

        private static byte[] badE2Command = new byte[8];
        private static byte[] badE2Command2 = new byte[8];
        private static byte[] badE2Command3 = new byte[8];
        private static int currentBE2Index = 0;

        private static byte[] ROMData;

        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;

            textBox.Text = openFileDialog.FileName;

            btnFix.Enabled = true;
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            btnFix.Enabled = false;

            string fileType = Path.GetExtension(textBox.Text).ToUpper();

            if (!goodFiletypeExtensions.Contains(fileType))
            {
                MessageBox.Show("Incorrect filetype, try a different file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnFix.Enabled = true;
                return;
            }

            InitializeDListArrays();

            //Steps here:
            //1. Load in the ROM
            //2. Step through the ROM, find the matching lines and fix em!
            //3. Write back out with a new filename

            try
            {
                ROMData = File.ReadAllBytes(textBox.Text);
            }
            catch
            {
                MessageBox.Show("Cannot open file specified. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                btnFix.Enabled = true; 
                return;
            }

            //Actually, this'll be dirty/slow, but I'll go ahead and just do 2 loop throughs of the
            // data, once for the vertex buffer overload, and once for the E2 commands. Just to be
            // lazy and simple.
            bool verticesLoaded = false;

            int badVertexCount = 0;
            int badE2CommandCount = 0;

            try
            {
                //Check for the vertex buffer overload here
                for (int i = 0; i < ROMData.Length; i++)
                {
                    //Quick rundown: There are 2 modes here: first we need to catch the overloaded
                    //                vertex and fix it to not load it. Then we need to find the
                    //                triangle drawn with the overloaded buffer and then get rid of it.
                    if (!verticesLoaded)
                    {
                        if (ROMData[i] == overloadedVertexBufferCommand[currentOVBCIndex])
                        {
                            currentOVBCIndex++;

                            if (currentOVBCIndex >= overloadedVertexBufferCommand.Length)
                            {
                                //Yeah, you hit it! Time to fix this overloaded vertex buffer
                                ROMData[i - 1] = 0x00;
                                ROMData[i] = 0x40;
                                currentOVBCIndex = 0;

                                badVertexCount++;

                                verticesLoaded = true;
                                i += 4; //Go to the end of the vertices loading command
                            }
                        }
                        else if (ROMData[i] == overloadedVertexBufferCommand[0]) //Catch this if there's an errant 0x01 that'd throw things off
                        {
                            currentOVBCIndex = 1;
                        }
                        else
                        {
                            currentOVBCIndex = 0;
                        }
                    }
                    else
                    {
                        while (verticesLoaded)
                        {
                            //Load up data
                            if (ROMData[i] == doubleTriangleCommand)
                            {
                                Array.Copy(ROMData, i, dlistCommand, 0, 8);
                                bool firstTriangleBad = false, secondTriangleBad = false;
                                for (int j = 1; j < 4; j++)
                                {
                                    if (ROMData[i + j] == overloadedVertexBufferSlot)
                                    {
                                        firstTriangleBad = true;
                                        break;
                                    }
                                }
                                for (int j = 5; j < 8; j++)
                                {
                                    if (ROMData[i + j] == overloadedVertexBufferSlot)
                                    {
                                        secondTriangleBad = true;
                                        break;
                                    }
                                }

                                if (firstTriangleBad && secondTriangleBad)
                                {
                                    //Get rid of both triangles
                                    for (int j = 0; j < 8; j++)
                                    {
                                        ROMData[i + j] = 0x00;
                                    }
                                }
                                else if (!firstTriangleBad && secondTriangleBad)
                                {
                                    //Get rid of second triangle
                                    ROMData[i] = singleTriangleCommand;
                                    for (int j = 4; j < 8; j++)
                                    {
                                        ROMData[i + j] = 0x00;
                                    }
                                }
                                else if (firstTriangleBad && !secondTriangleBad)
                                {
                                    //Overwrite first triangle with second
                                    ROMData[i] = singleTriangleCommand;
                                    ROMData[i + 4] = 0x00;
                                    for (int j = 1; j < 4; j++)
                                    {
                                        ROMData[i + j] = ROMData[i + j + 4];
                                        ROMData[i + j + 4] = 0x00;
                                    }
                                }

                                i += 8;
                            }
                            else if (ROMData[i] == singleTriangleCommand)
                            {
                                Array.Copy(ROMData, i, dlistCommand, 0, 4);
                                bool firstTriangleBad = false;
                                for (int j = 1; j < 4; j++)
                                {
                                    if (ROMData[i + j] == overloadedVertexBufferSlot)
                                    {
                                        firstTriangleBad = true;
                                        break;
                                    }
                                }

                                if (firstTriangleBad)
                                {
                                    for (int j = 0; j < 4; j++)
                                    {
                                        ROMData[i + j] = 0x00;
                                    }
                                }

                                i += 4;
                            }
                            else
                            {
                                verticesLoaded = false;
                            }
                        }
                    }
                }

                //Check for the E2 commands
                for (int i = 0; i < ROMData.Length; i++)
                {
                    //By doing it this way, we could end up with a mistaken value 
                    // (like a mixing of the second and third commands), but I kinda
                    // doubt it'll actually happen. Even if it does, the C on the end
                    // needs to be an 8.
                    if (ROMData[i] == badE2Command[currentBE2Index] ||
                        ROMData[i] == badE2Command2[currentBE2Index] ||
                        ROMData[i] == badE2Command3[currentBE2Index])
                    {
                        currentBE2Index++;

                        if (currentBE2Index >= badE2Command.Length)
                        {
                            //Yeah, you hit it! Time to fix this E2 command
                            if (ROMData[i] == badE2Command[7])
                                ROMData[i] = 0x78;
                            else if (ROMData[i] == badE2Command2[7])
                                ROMData[i] = 0x78;
                            else if (ROMData[i] == badE2Command3[7])
                                ROMData[i] = 0xD8;
                            currentBE2Index = 0;
                            badE2CommandCount++;
                        }
                    }
                    else if (ROMData[i] == badE2Command[0]) //Catch this if there's an errant 0xE2 that'd throw things off
                    {
                        currentBE2Index = 1;
                    }
                    else
                    {
                        currentBE2Index = 0;
                    }

                }
            }
            catch (Exception err)
            {
                if (MessageBox.Show("Error encountered, create error log?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Yes)
                {
                    File.WriteAllText(Path.Combine(Path.GetDirectoryName(textBox.Text), "Error.txt"), err.Message);
                }

                btnFix.Enabled = true;
                return;
            }

            string newFileName = Path.GetFileNameWithoutExtension(textBox.Text) + " - Fix" + fileType;

            File.WriteAllBytes(newFileName, ROMData);


            MessageBox.Show(string.Format("ROM file fixed!\n{0} vertex errors fixed\n{1} E2 commands fixed", badVertexCount, badE2CommandCount), "Success");

            btnFix.Enabled = true; 
        }

        private static void InitializeDListArrays()
        {
            overloadedVertexBufferCommand[0] = 0x01;
            overloadedVertexBufferCommand[1] = 0x02;
            overloadedVertexBufferCommand[2] = 0x10;
            overloadedVertexBufferCommand[3] = 0x42;

            badE2Command[0] = 0xE2;
            badE2Command[1] = 0x00;
            badE2Command[2] = 0x00;
            badE2Command[3] = 0x1C;
            badE2Command[4] = 0xC8;
            badE2Command[5] = 0x11;
            badE2Command[6] = 0x30;
            badE2Command[7] = 0x7C;

            badE2Command2[0] = 0xE2;
            badE2Command2[1] = 0x00;
            badE2Command2[2] = 0x00;
            badE2Command2[3] = 0x1C;
            badE2Command2[4] = 0xC8;
            badE2Command2[5] = 0x10;
            badE2Command2[6] = 0x30;
            badE2Command2[7] = 0x7C;

            badE2Command3[0] = 0xE2;
            badE2Command3[1] = 0x00;
            badE2Command3[2] = 0x00;
            badE2Command3[3] = 0x1C;
            badE2Command3[4] = 0xC8;
            badE2Command3[5] = 0x10;
            badE2Command3[6] = 0x49;
            badE2Command3[7] = 0xDC;
        }
    }
}
