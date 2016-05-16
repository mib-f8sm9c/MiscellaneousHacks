using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Cereal64.Common.Utils;
using Cereal64.Microcodes.F3DEX.DataElements.Commands;
using Cereal64.Common.Rom;
using Cereal64.Microcodes.F3DEX.DataElements;
using Cereal64.Microcodes.F3DEX;
using Cereal64.Common.DataElements;

namespace MarioKartTestingTool
{
    public partial class Form1 : Form
    {
        //To do: verify rom, checksum fix, mi0 decode/encode, export data

        private const int MK64_COURSE_DATA_REFERENCE_TABLE_LOCATION = 0x122390;
        private const int MK64_COURSE_COUNT = 0x13;

        private const int MK64_TEXTURE_BANK_OFFSET = 0x641F70;

        private byte[] _romData;
        private List<CourseDataReferenceEntry> _courseInfo;


        public Form1()
        {
            InitializeComponent();

            N64DataElementFactory.AddN64ElementsFromAssembly(typeof(F3DEXCommandCollection).Assembly);//Add in the assembly string here
            N64ElementContainerFactory.AddN64ElementContainersFromAssembly(typeof(F3DEXCommandCollection).Assembly);//Add in the assembly string here

        }

        private void btnLoadRom_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _romData = File.ReadAllBytes(openFileDialog.FileName);

                ProcessRom();

                listBox.Items.Clear();

                foreach (CourseDataReferenceEntry entry in _courseInfo)
                {
                    listBox.Items.Add(entry);
                }
            }
        }

        private void ProcessRom()
        {
            //Start reading in the stage information
            _courseInfo = new List<CourseDataReferenceEntry>();

            for (int i = 0; i < MK64_COURSE_COUNT; i++)
            {
                int offset = MK64_COURSE_DATA_REFERENCE_TABLE_LOCATION + i * 0x30;
                byte[] bytes = new byte[0x30];
                Array.Copy(_romData, offset, bytes, 0, 0x30);

                _courseInfo.Add(new CourseDataReferenceEntry(offset, bytes, i));
            }
        }

        private CourseDataReferenceEntry SelectedCourse
        {
            get
            {
                return listBox.SelectedItem as CourseDataReferenceEntry;
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(SelectedCourse == null)
                return;

            //Take the blocks, and export them
            byte[] displayListBlock = new byte[SelectedCourse.DisplayListBlockEnd - SelectedCourse.DisplayListBlockStart];
            Array.Copy(_romData, SelectedCourse.DisplayListBlockStart,
                displayListBlock, 0, displayListBlock.Length);
            int vertexEndPackedDLStartOffset = SelectedCourse.DisplayListOffset & 0x00FFFFFF;
            byte[] vertexBlock = new byte[vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart,
                vertexBlock, 0, vertexBlock.Length);
            byte[] packedBlock = new byte[(SelectedCourse.VertexBlockEnd - SelectedCourse.VertexBlockStart) - vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart + vertexEndPackedDLStartOffset,
                packedBlock, 0, packedBlock.Length);
            byte[] textureBlock = new byte[SelectedCourse.TextureBlockEnd - SelectedCourse.TextureBlockStart];
            Array.Copy(_romData, SelectedCourse.TextureBlockStart,
                textureBlock, 0, textureBlock.Length);

            string path = Path.GetDirectoryName(openFileDialog.FileName);

            string filePath = Path.Combine(path, "rawdisplaylists.bin");
            File.WriteAllBytes(filePath, displayListBlock);

            filePath = Path.Combine(path, "rawvertices.bin");
            File.WriteAllBytes(filePath, vertexBlock);

            filePath = Path.Combine(path, "rawpacked.bin");
            File.WriteAllBytes(filePath, packedBlock);

            filePath = Path.Combine(path, "rawtextures.bin");
            File.WriteAllBytes(filePath, textureBlock);

            filePath = Path.Combine(path, "displaylists.bin");
            File.WriteAllBytes(filePath, Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock));

            filePath = Path.Combine(path, "vertices.bin");
            File.WriteAllBytes(filePath, Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock));

            //filePath = Path.Combine(path, "textures.bin");
            //File.WriteAllBytes(filePath, Cereal64.Common.Utils.Encoding.MIO0.Decode(textureBlock));

            List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(packedBlock.ToList());
            byte[] bytes = new byte[commands.Sum(c => c.RawDataSize)];
            int offset = 0;
            foreach(F3DEXCommand command in commands)
            {
                Array.Copy(command.RawData, 0, bytes, offset, command.RawDataSize);
                offset += command.RawDataSize;
            }
            filePath = Path.Combine(path, "packed.bin");
            File.WriteAllBytes(filePath, bytes);
        }

        private void btnTestImportLevel_Click(object sender, EventArgs e)
        {
            //Step one: decode the selected level MIO0 encoded blocks
            //2: Re-encode the blocks, attach to the end of the rom, fix the table offsets
            //3: Resize rom, fix CRC, save as new one

            if (SelectedCourse == null)
                return;

            byte[] newRomData = new byte[0x1000000];
            Array.Copy(_romData, 0, newRomData, 0, _romData.Length);
            _romData = newRomData;

            //Take the blocks, and export them
            byte[] displayListBlock = new byte[SelectedCourse.DisplayListBlockEnd - SelectedCourse.DisplayListBlockStart];
            Array.Copy(_romData, SelectedCourse.DisplayListBlockStart,
                displayListBlock, 0, displayListBlock.Length);

            byte[] vertexBlock = new byte[SelectedCourse.VertexBlockEnd - SelectedCourse.VertexBlockStart];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart,
                vertexBlock, 0, vertexBlock.Length);

            byte[] textureBlock = new byte[SelectedCourse.TextureBlockEnd - SelectedCourse.TextureBlockStart];
            Array.Copy(_romData, SelectedCourse.TextureBlockStart,
                textureBlock, 0, textureBlock.Length);

            byte[] displayDecoded = Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock);
            byte[] displayRecoded = Cereal64.Common.Utils.Encoding.MIO0.EncodeAsRaw(displayDecoded);
            byte[] vertexDecoded = Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock);
            byte[] vertexRecoded = Cereal64.Common.Utils.Encoding.MIO0.EncodeAsRaw(vertexDecoded);

            //0xBE9160 - 0xBFFFFF
            int position = 0xBE9160;
            if (position + displayRecoded.Length < 0xC00000)
            {
                Array.Copy(displayRecoded, 0, _romData, position, displayRecoded.Length);
                SelectedCourse.DisplayListBlockStart = position;
                position += displayRecoded.Length;
                SelectedCourse.DisplayListBlockEnd = position;

                if (position % 0x10 != 0)
                    position += 0x10 - (position % 0x10);

            if (position + vertexBlock.Length < 0xC00000)
                {
                    Array.Copy(vertexBlock, 0, _romData, position, vertexBlock.Length);
                    SelectedCourse.VertexBlockStart = position;
                    position += vertexBlock.Length;
                    SelectedCourse.VertexBlockEnd = position;

                 //   Array.Copy(textureBlock, 0, _romData, position, textureBlock.Length);
                  //  SelectedCourse.TextureBlockStart = position;
                  //  position += textureBlock.Length;
                  //  SelectedCourse.TextureBlockEnd = position;
                }
            }

            Array.Copy(SelectedCourse.RawData, 0, _romData, SelectedCourse.FileOffset, SelectedCourse.RawDataSize);

            if (N64Sums.FixChecksum(_romData))
            {
                File.WriteAllBytes(openFileDialog.FileName + "new.z64", _romData);
            }
        }

        private void btnRender_Click(object sender, EventArgs e)
        {
            if (SelectedCourse == null)
                return;

            for (; RomProject.Instance.Files.Count > 0;)
                RomProject.Instance.RemoveRomFile(RomProject.Instance.Files[0]);

            for (; RomProject.Instance.DMAProfiles.Count > 0; )
                RomProject.Instance.RemoveDmaProfile(RomProject.Instance.DMAProfiles[0]);

            //Take the blocks, and export them
            byte[] displayListBlock = new byte[SelectedCourse.DisplayListBlockEnd - SelectedCourse.DisplayListBlockStart];
            Array.Copy(_romData, SelectedCourse.DisplayListBlockStart,
                displayListBlock, 0, displayListBlock.Length);
            int vertexEndPackedDLStartOffset = SelectedCourse.DisplayListOffset & 0x00FFFFFF;
            byte[] vertexBlock = new byte[vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart,
                vertexBlock, 0, vertexBlock.Length);
            byte[] packedBlock = new byte[(SelectedCourse.VertexBlockEnd - SelectedCourse.VertexBlockStart) - vertexEndPackedDLStartOffset];
            Array.Copy(_romData, SelectedCourse.VertexBlockStart + vertexEndPackedDLStartOffset,
                packedBlock, 0, packedBlock.Length);
            byte[] textureBlock = new byte[SelectedCourse.TextureBlockEnd - SelectedCourse.TextureBlockStart];
            Array.Copy(_romData, SelectedCourse.TextureBlockStart,
                textureBlock, 0, textureBlock.Length);



            byte[] decodedDLData = Cereal64.Common.Utils.Encoding.MIO0.Decode(displayListBlock);

            List<Vertex> vertices = VertexPacker.BytesToVertices(Cereal64.Common.Utils.Encoding.MIO0.Decode(vertexBlock).ToList());
            VertexCollection vertCollection = new VertexCollection(0x00, vertices);
            byte[] vertsData = vertCollection.RawData;

            List<F3DEXCommand> commands = F3DEXPacker.BytesToCommands(packedBlock.ToList());
            F3DEXCommandCollection commandColl = new F3DEXCommandCollection(0x00, commands);
            byte[] commandsData = commandColl.RawData;

            List<TextureMIORef> textureSegPointers = ReadTextureBank(textureBlock);

            byte[] textureSegData = new byte[textureSegPointers.Sum(t => t.DecompressedSize)];
            int bytePointer = 0;
            for (int i = 0; i < textureSegPointers.Count; i++)
            {
                byte[] tempHolder = new byte[textureSegPointers[i].CompressedSize];
                Array.Copy(_romData, (textureSegPointers[i].RomOffset & 0x00FFFFFF) + MK64_TEXTURE_BANK_OFFSET,
                    tempHolder, 0, textureSegPointers[i].CompressedSize);
                byte[] decompressed = Cereal64.Common.Utils.Encoding.MIO0.Decode(tempHolder);
                Array.Copy(decompressed, 0, textureSegData, bytePointer, decompressed.Length);
                bytePointer += decompressed.Length;
            }

            //Use the F3DEXReader here
            RomProject.Instance.AddRomFile(new RomFile("Verts", 1, new Cereal64.Common.DataElements.UnknownData(0x00, vertsData)));
            RomProject.Instance.Files[0].FileLength = vertsData.Length;
            RomProject.Instance.AddRomFile(new RomFile("PackedDLs", 2, new Cereal64.Common.DataElements.UnknownData(0x00, commandsData)));
            RomProject.Instance.Files[1].FileLength = commandsData.Length;
            RomProject.Instance.AddRomFile(new RomFile("Textures", 3, new Cereal64.Common.DataElements.UnknownData(0x00, textureSegData)));
            RomProject.Instance.Files[2].FileLength = textureSegData.Length;

            DmaProfile profile = new DmaProfile("Levelviewer");
            DmaSegment segment = new DmaSegment();
            segment.File = RomProject.Instance.Files[0];
            segment.RamSegment = 0x04;
            segment.RamStartOffset = 0x00;
            segment.FileStartOffset = 0x00;
            segment.FileEndOffset = segment.File.FileLength;
            segment.TagInfo = "Vertices";
            profile.AddDmaSegment(0x04, segment);
            segment = new DmaSegment();
            segment.File = RomProject.Instance.Files[1];
            segment.RamSegment = 0x07;
            segment.RamStartOffset = 0x00;
            segment.FileStartOffset = 0x00;
            segment.FileEndOffset = segment.File.FileLength;
            segment.TagInfo = "PackedDLs";
            profile.AddDmaSegment(0x07, segment);
            segment = new DmaSegment();
            segment.File = RomProject.Instance.Files[2];
            segment.RamSegment = 0x05;
            segment.RamStartOffset = 0x00;
            segment.FileStartOffset = 0x00;
            segment.FileEndOffset = segment.File.FileLength;
            segment.TagInfo = "Textures";
            profile.AddDmaSegment(0x05, segment);
            RomProject.Instance.AddDmaProfile(profile);
            DmaManager.Instance.AddNewDmaProfile(profile);

            F3DEXReaderPackage package = F3DEXReader.ReadF3DEXAt(RomProject.Instance.Files[1], 0x00);
            F3DEXReaderPackage newPackage = package;
            newPackage = null;

            if (package.Elements[RomProject.Instance.Files[1]][0] is F3DEXCommandCollection)
            {
                OpenGLForm glForm = new OpenGLForm();
                glForm.Show();
                glForm.SetCommands((F3DEXCommandCollection)package.Elements[RomProject.Instance.Files[1]][0]);
            }
        }

        private List<TextureMIORef> ReadTextureBank(byte[] texturePointers)
        {
            List<TextureMIORef> output = new List<TextureMIORef>();
            byte[] tempArray = new byte[0x10];

            for (int i = 0; i < texturePointers.Length / 0x10; i++)
            {
                Array.Copy(texturePointers, i * 0x10, tempArray, 0, 0x10);
                TextureMIORef refText = new TextureMIORef(i * 0x10, tempArray);
                if (refText.RomOffset == 0x00000000)
                    break;

                output.Add(refText);
            }

            return output;
        }

        private void btnTkmk_Click(object sender, EventArgs e)
        {
            byte[] imageData;
            imageData = TKMK00Encoder.Decode(_romData, 0x8094C0, 0x01);
            Bitmap bmp = TextureConversion.BinaryToRGBA16(imageData, 320, 240);
            pictureBox1.Image = bmp;
            //return;
            File.WriteAllBytes("decode1.bin", imageData);
            //byte[] newdata = TextureConversion.RGBA16ToBinary((Bitmap)Bitmap.FromFile("diddyKong.png"));
            byte[] outData = TKMK00Encoder.Encode(imageData, 320, 240, 0x01); //hardcoded repeat mask/alpha color
            File.WriteAllBytes("newTest.bin", outData);
            imageData = TKMK00Encoder.Decode(outData, 0, 0xBE);
            File.WriteAllBytes("decode2.bin", imageData);
            //Bitmap bmp = TextureConversion.BinaryToRGBA16(imageData, 64, 54);
            //pictureBox1.Image = bmp;

            Array.Copy(outData, 0, _romData, 0x8094C0, outData.Length);

            File.WriteAllBytes(openFileDialog.FileName + "Added.z64", _romData);
        }
    }
}
