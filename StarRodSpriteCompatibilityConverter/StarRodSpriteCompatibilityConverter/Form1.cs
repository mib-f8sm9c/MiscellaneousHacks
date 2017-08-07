using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DamienG.Security.Cryptography;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace StarRodSpriteCompatibilityConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private byte[] HEADER = new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };

        private string SpritesheetXmlPath;

        private SpritesheetSkeleton LoadedSpritesheet;

        public int SpritesheetA;
        public int SpritesheetB;

        private List<CI8Palette> SpritesheetPalettes;
        private List<CI8Image> SpritesheetImages;

        private void btnLoadSpriteSheet_Click(object sender, EventArgs e)
        {
            //Load in the spritesheet, convert to a bunch of palettes and images.
            // This will remove the xsi/xsd namespaces from serialization

            if (openSpritesheetDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SpritesheetXmlPath = openSpritesheetDialog.FileName;

                gbSpriteSheet.Enabled = false;
                btnSaveChanges.Enabled = false;

                using (XmlTextReader reader = new XmlTextReader(SpritesheetXmlPath))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(SpritesheetSkeleton));
                    LoadedSpritesheet = (SpritesheetSkeleton)ser.Deserialize(reader);
                }

                SpritesheetA = Convert.ToInt32(LoadedSpritesheet.a, 16);
                SpritesheetB = Convert.ToInt32(LoadedSpritesheet.b, 16);

                SpritesheetPalettes = new List<CI8Palette>();
                SpritesheetImages = new List<CI8Image>();

                //Load up the palettes
                foreach (PaletteSkeleton skel in LoadedSpritesheet.PaletteList)
                {
                    byte[] pngData = File.ReadAllBytes(Path.Combine(Path.GetDirectoryName(SpritesheetXmlPath), skel.src));

                    //Need to iterate through the PNGChunks, until you find the right one.
                    int pngOffset = 0xC;
                    byte[] colorData = null;
                    byte[] transColor = null;
                    while (pngOffset < pngData.Length)
                    {
                        string chunkname = ASCIIEncoding.ASCII.GetString(pngData, pngOffset, 4);
                        byte[] lengthBytes = new byte[4];
                        Array.Copy(pngData, pngOffset - 4, lengthBytes, 0, 4);
                        int chunkLength = BitConverter.ToInt32(lengthBytes.Reverse().ToArray(), 0);
                        if (chunkname == "PLTE")
                        {
                            colorData = new byte[chunkLength];
                            Array.Copy(pngData, pngOffset + 4, colorData, 0, colorData.Length);
                            if (transColor != null)
                                break;
                        }
                        else if (chunkname == "tRNS")
                        {
                            transColor = new byte[chunkLength];
                            Array.Copy(pngData, pngOffset + 4, transColor, 0, transColor.Length);
                            if (colorData != null)
                                break;
                        }

                        pngOffset += 12 + chunkLength;
                    }

                    int colorCount = 16; //CI4 format
                    Color[] colors = new Color[colorCount];

                    for (int i = 0; i < colorCount; i++)
                    {
                        if(transColor == null)
                            colors[i] = Color.FromArgb(0xFF, colorData[3 * i],
                                colorData[3 * i + 1], colorData[3 * i + 2]);
                        else
                            colors[i] = Color.FromArgb(transColor[i], colorData[3 * i],
                                colorData[3 * i + 1], colorData[3 * i + 2]);
                    }

                    SpritesheetPalettes.Add(new CI8Palette(skel.id, skel.src, colors));
                }

                //Load up the palettes
                foreach (RasterSkeleton skel in LoadedSpritesheet.RasterList)
                {
                    CI8Palette pal = SpritesheetPalettes.FirstOrDefault(p => p.Name == skel.palette);

                    if (pal == null)
                    {
                        MessageBox.Show("Error! Could not find palette for raster " + skel.id + "! Invalid Spritesheet!");
                        throw new Exception();
                    }

                    Bitmap bmp;
                    using (var bmpTemp = new Bitmap(Path.Combine(Path.GetDirectoryName(SpritesheetXmlPath), skel.src)))
                    {
                        bmp = (Bitmap)(new Bitmap(bmpTemp));
                    }

                    byte[] data = CI8ToBinary(bmp, pal.Color);

                    SpritesheetImages.Add(new CI8Image(skel.id, skel.src, data, pal, bmp.Width, bmp.Height));

                    SpritesheetImages.Last().UpdateImage();
                }

                //Enable the things that need enabling
                btnSaveChanges.Enabled = true;
                gbSpriteSheet.Enabled = true;
            
                //Add the images to the listbox
                lbImages.Items.Clear();
                foreach (CI8Image img in SpritesheetImages)
                {
                    lbImages.Items.Add(img);
                }

                if (lbImages.Items.Count > 0)
                    lbImages.SelectedIndex = 0;

            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            //Save all the images out to the directory, go ahead and don't duplicate existing ones
            Dictionary<CI8Palette, byte[]> FirstImgPerPalette = new Dictionary<CI8Palette,byte[]>();

            foreach (CI8Image img in SpritesheetImages)
            {
                byte[] pngData = ConvertCI8ToPNG(img, img.Palette);
                File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(SpritesheetXmlPath), img.FilePath), pngData);
                if (!FirstImgPerPalette.ContainsKey(img.Palette))
                    FirstImgPerPalette[img.Palette] = pngData;
            }

            //Palettes
            foreach (CI8Palette palette in SpritesheetPalettes)
            {
                if (FirstImgPerPalette.ContainsKey(palette))
                {
                    byte[] palettePNGData = FirstImgPerPalette[palette];
                    File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(SpritesheetXmlPath), palette.FilePath), palettePNGData);
                }
                else
                {
                    byte[] palettePNGData = ConvertCI8ToPNG(null, palette);
                    File.WriteAllBytes(Path.Combine(Path.GetDirectoryName(SpritesheetXmlPath), palette.FilePath), palettePNGData);
                }
            }

            //Match up the spritesheet skeleton to the images/palettes
            LoadedSpritesheet.a = SpritesheetA.ToString("X");
            LoadedSpritesheet.b = SpritesheetB.ToString("X");
            LoadedSpritesheet.RasterList = new List<RasterSkeleton>();
            LoadedSpritesheet.PaletteList = new List<PaletteSkeleton>();
            foreach (CI8Image img in SpritesheetImages)
            {
                RasterSkeleton raster = new RasterSkeleton();
                raster.id = img.Name;
                raster.src = img.FilePath;
                raster.palette = img.Palette.Name;
                LoadedSpritesheet.RasterList.Add(raster);
            }
            foreach (CI8Palette pal in SpritesheetPalettes)
            {
                PaletteSkeleton palette = new PaletteSkeleton();
                palette.id = pal.Name;
                palette.src = pal.FilePath;
                LoadedSpritesheet.PaletteList.Add(palette);
            }

            //Save the spritesheet
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer ser = new XmlSerializer(typeof(SpritesheetSkeleton));
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter writer = XmlWriter.Create(SpritesheetXmlPath, settings);
            ser.Serialize(writer, LoadedSpritesheet, ns);  // Inform the XmlSerializerNamespaces here
        }

        private void lbImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbImages.SelectedItem == null)
            {
                pbImage.Image = null;
                nudPalette.Enabled = false;
            }
            else
            {
                CI8Image img = (CI8Image)lbImages.SelectedItem;

                int paletteIndex = SpritesheetPalettes.IndexOf(img.Palette);

                nudPalette.ValueChanged -= nudPalette_ValueChanged;
                nudPalette.Value = paletteIndex;
                nudPalette.ValueChanged += nudPalette_ValueChanged;

                pbImage.Image = img.Image;

                nudPalette.Enabled = true;
                UpdatePaletteEdit();
            }
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            //Here's the turkey, gents. All you gotta do is fill this out, then test the whole caboodle a few times, then you're good.

            //Openfiledalog for multiple files
            if (openSpriteDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<CI8Image> images = new List<CI8Image>();

                if (cbCreatePalette.Checked)
                {
                    //If palette needs to be made:
                    //For each one, load them up and add up the palettes, create the new palette
                    //Convert each one to a CI4 format using the new palette

                    List<Bitmap> bmps = new List<Bitmap>();

                    foreach (string fileName in openSpriteDialog.FileNames)
                    {
                        using (var bmpTemp = new Bitmap(fileName))
                        {
                            bmps.Add((Bitmap)(new Bitmap(bmpTemp)));
                        }
                    }

                    Color[] colors = GetPalette(bmps);

                    string paletteName = SpritesheetPalettes.Count.ToString("X2");
                    CI8Palette palette = new CI8Palette(paletteName, "Palette_" + paletteName + ".png", colors);

                    SpritesheetPalettes.Add(palette);

                    for (int i = 0; i < bmps.Count; i++)
                    {
                        string imageName = (SpritesheetImages.Count + images.Count).ToString("X2");
                        images.Add(new CI8Image(imageName, "Raster_" + imageName + ".png", CI8ToBinary(bmps[i], palette.Color),
                        palette, bmps[i].Width, bmps[i].Height));
                    }
                }
                else
                {
                    //If palette needs to be used:
                    //For each one, convert it using the old palette

                    CI8Palette palette = SpritesheetPalettes[(int)nudPalette.Value];

                    foreach (string fileName in openSpriteDialog.FileNames)
                    {
                        Bitmap bmp;
                        using (var bmpTemp = new Bitmap(fileName))
                        {
                            bmp = (Bitmap)(new Bitmap(bmpTemp));
                        }

                        string imageName = (SpritesheetImages.Count + images.Count).ToString("X2");
                        images.Add(new CI8Image(imageName, "Raster_" + imageName + ".png",
                            CI8ToBinary(bmp, palette.Color), palette, bmp.Width, bmp.Height));
                    }
                }

                //Add them to the spritesheet images list (and the new palette if applicable)
                foreach (CI8Image image in images)
                    SpritesheetImages.Add(image);

                //update the listbox, move the cursor to the first new added image
                int newImageIndex = lbImages.Items.Count;
                foreach (CI8Image image in images)
                    lbImages.Items.Add(image);

                if (images.Count > 0)
                    lbImages.SelectedIndex = newImageIndex;

            }
        }

        private void nudPalette_ValueChanged(object sender, EventArgs e)
        {
            if(nudPalette.Value >= SpritesheetPalettes.Count)
            {
                nudPalette.Value = SpritesheetPalettes.Count - 1;
            }
            else if (nudPalette.Value < 0)
            {
                nudPalette.Value = 0;
            }
            else
            {
                CI8Image img = (CI8Image)lbImages.SelectedItem;
                CI8Palette palette = SpritesheetPalettes[(int)nudPalette.Value];
                img.Palette = palette;
                img.UpdateImage();
                pbImage.Image = img.Image;
                UpdatePaletteEdit();
            }
        }

        private void UpdatePaletteEdit()
        {
            flpPalette.Controls.Clear();
            CI8Palette palette = SpritesheetPalettes[(int)nudPalette.Value];
            for (int i = 0; i < palette.Color.Length; i++)
            {
                Button btn = new Button();
                btn.Width = 16;
                btn.Height = 16;
                btn.Text = string.Empty;
                btn.BackColor = palette.Color[i];
                btn.Click += ColorEditClick;
                btn.FlatStyle = FlatStyle.Flat;
                flpPalette.Controls.Add(btn);
            }
        }

        private void ColorEditClick(object sender, EventArgs args)
        {
            int colorIndex = flpPalette.Controls.IndexOf((Control)sender);
            CI8Palette palette = SpritesheetPalettes[(int)nudPalette.Value];
            colorDialog1.Color = palette.Color[colorIndex];
            if (colorDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                palette.Color[colorIndex] = colorDialog1.Color;
                foreach (CI8Image img in SpritesheetImages)
                {
                    if (img.Palette == palette)
                        img.UpdateImage();
                }
                pbImage.Image = ((CI8Image)lbImages.SelectedItem).Image;
            }
        }

        private byte[] ConvertCI8ToPNG(CI8Image img, CI8Palette palette)
        {
            byte[] compresseddata;
            int width = (img == null ? 4 : img.Width);
            int height = (img == null ? 4 : img.Height);
            if(img == null)
                compresseddata = CompressData(new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 }, palette.Color, width, height);
            else
                compresseddata = CompressData(img.CI8Data, palette.Color, width, height);

            //ToDo: Write the palette determiner, and allow it to resize palettes if needed.
            //       Copy from existing code if necessary.

            //Also you need to get the filtering and the compressing working. The specs are online.

            //Write the data
            List<byte[]> byteArrays = new List<byte[]>();

            byteArrays.Add(HEADER);

            //Header
            byte[] chunkData = new byte[0xD];
            Array.Copy(BitConverter.GetBytes(width).Reverse().ToArray(), 0, chunkData, 0, 4);
            Array.Copy(BitConverter.GetBytes(height).Reverse().ToArray(), 0, chunkData, 4, 4);
            chunkData[0x8] = 0x08;
            chunkData[0x9] = 0x03;
            byteArrays.Add((new PNGChunk("IHDR", chunkData)).GetAsBytes());

            //Palette
            chunkData = new byte[256 * 3];
            for (int i = 0; i < 256; i++)
            {
                if (i < palette.Color.Length)
                {
                    chunkData[i * 3] = palette.Color[i].R;
                    chunkData[i * 3 + 1] = palette.Color[i].G;
                    chunkData[i * 3 + 2] = palette.Color[i].B;
                }
                else
                {
                    chunkData[i * 3] = 0;
                    chunkData[i * 3 + 1] = 0;
                    chunkData[i * 3 + 2] = 0;
                }
            }
            byteArrays.Add((new PNGChunk("PLTE", chunkData)).GetAsBytes());

            //Transparency
            chunkData = new byte[256];
            for (int i = 0; i < 256; i++)
            {
                if (i < palette.Color.Length)
                    chunkData[i] = palette.Color[i].A;
                else
                    chunkData[i] = 0;
            }

            byteArrays.Add((new PNGChunk("tRNS", chunkData)).GetAsBytes());

            //Data
            byteArrays.Add((new PNGChunk("IDAT", compresseddata)).GetAsBytes());

            //End
            byteArrays.Add((new PNGChunk("IEND", new byte[0])).GetAsBytes());


            //Now sum up all the byte arrays and create an output
            byte[] output = new byte[byteArrays.Sum(b => b.Length)];

            int outputOffset = 0;
            foreach (byte[] bytes in byteArrays)
            {
                Array.Copy(bytes, 0, output, outputOffset, bytes.Length);
                outputOffset += bytes.Length;
            }

            return output;
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //{
            //    Bitmap bmp = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);

            //    //Analyze the data
            //    Color[] palette = GetPalette(bmp);

            //    byte[] compresseddata = CompressData(bmp, palette);

            //    //ToDo: Write the palette determiner, and allow it to resize palettes if needed.
            //    //       Copy from existing code if necessary.

            //    //Also you need to get the filtering and the compressing working. The specs are online.

            //    //Write the data
            //    List<byte[]> byteArrays = new List<byte[]>();

            //    byteArrays.Add(HEADER);

            //    //Header
            //    byte[] chunkData = new byte[0xD];
            //    Array.Copy(BitConverter.GetBytes(bmp.Width).Reverse().ToArray(), 0, chunkData, 0, 4);
            //    Array.Copy(BitConverter.GetBytes(bmp.Height).Reverse().ToArray(), 0, chunkData, 4, 4);
            //    chunkData[0x8] = 0x08;
            //    chunkData[0x9] = 0x03;
            //    byteArrays.Add((new PNGChunk("IHDR", chunkData)).GetAsBytes());

            //    //Palette
            //    chunkData = new byte[palette.Length * 3];
            //    for(int i = 0; i < palette.Length; i++)
            //    {
            //        chunkData[i * 3] = palette[i].R;
            //        chunkData[i * 3 + 1] = palette[i].G;
            //        chunkData[i * 3 + 2] = palette[i].B;
            //    }
            //    byteArrays.Add((new PNGChunk("PLTE", chunkData)).GetAsBytes());


            //    //Transparency
            //    chunkData = new byte[palette.Length];
            //    for (int i = 0; i < palette.Length; i++)
            //    {
            //        chunkData[i] = palette[i].A;
            //    }
            //    byteArrays.Add((new PNGChunk("tRNS", chunkData)).GetAsBytes());

            //    //Data
            //    byteArrays.Add((new PNGChunk("IDAT", compresseddata)).GetAsBytes());

            //    //End
            //    byteArrays.Add((new PNGChunk("IEND", new byte[0])).GetAsBytes());


            //    //Now sum up all the byte arrays and create an output
            //    byte[] output = new byte[byteArrays.Sum(b => b.Length)];

            //    int outputOffset = 0;
            //    foreach (byte[] bytes in byteArrays)
            //    {
            //        Array.Copy(bytes, 0, output, outputOffset, bytes.Length);
            //        outputOffset += bytes.Length;
            //    }

            //    string newPath = Path.Combine(Path.GetDirectoryName(openFileDialog1.FileName),
            //        Path.GetFileNameWithoutExtension(openFileDialog1.FileName) + "-fixed.png");

            //    File.WriteAllBytes(newPath, output);
            //}
        }

        private Color[] GetPalette(List<Bitmap> images)
        {
            int colorCount = 0x10; //max 0x0F

            //This can take a long time (not optimized), so be careful
            List<Color> colors = new List<Color>();
            foreach (Bitmap image in images)
            {
                BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                int stride = data.Stride;
                unsafe
                {
                    byte* ptr = (byte*)data.Scan0;
                    for (int j = 0; j < image.Height; j++)
                    {
                        for (int i = 0; i < image.Width; i++)
                        {
                            Color pixel = Color.FromArgb((ptr[(i * 4) + j * stride]) |
                                            (ptr[(i * 4) + j * stride + 1] << 8) |
                                            (ptr[(i * 4) + j * stride + 2] << 16) |
                                            (ptr[(i * 4) + j * stride + 3] << 24));
                            if (!colors.Contains(pixel))
                                colors.Add(pixel);
                        }
                    }
                }
                image.UnlockBits(data);
            }

            PaletteMedianCutAnalyzer analyzer = new PaletteMedianCutAnalyzer();
            foreach (Color color in colors)
                analyzer.AddColor(color);

            //Now make the new palette
            return analyzer.GetPalette(colorCount);
        }

        private byte[] CompressData(byte[] ciData, Color[] palette, int width, int height)
        {
            //First, we need to take the bmp and convert it to CI8.
            //byte[] ciData = CI8ToBinary(bmp, palette);

            byte[] filteredCIData = new byte[ciData.Length + height];
            for (int i = 0; i < height; i++)
            {
                filteredCIData[i * (width + 1)] = 0; //filter type
                Array.Copy(ciData, i * width, filteredCIData, i * (width + 1) + 1, width);
            }
            ciData = filteredCIData;

            byte[] newData;

            //Then run the compressor on it!
            using(MemoryStream inStream = new MemoryStream(ciData))
            {
                using (CompressingStream cStream = new CompressingStream(inStream))
                {
                    byte[] output = new byte[ciData.Length * 2]; //Done like this since small datasets can compress larger
                    cStream.Read(output, 0, ciData.Length);

                    newData = new byte[cStream.Length + 2];
                    newData[0] = 0x78;
                    newData[1] = 0xDA;
                    Array.Copy(output, 0, newData, 2, newData.Length - 2);
                }
            }

            return newData;
        }

        private byte[] CI8ToBinary(Bitmap bmp, Color[] palette)
        {
            //Pixel size is 1 byte
            if (bmp == null)
                return null;

            if (palette == null || palette.Length < 1)
                return null;

            byte[] imgData = new byte[bmp.Width * bmp.Height];

            int[] paletteIDs = new int[palette.Length];
            for (int k = 0; k < palette.Length; k++)
            {
                paletteIDs[k] = palette[k].ToArgb();
            }

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int stride = data.Stride;
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        int index = i + j * bmp.Width;

                        int pixelID = ((ptr[(i * 4) + j * stride]) |
                                        (ptr[(i * 4) + j * stride + 1] << 8) |
                                        (ptr[(i * 4) + j * stride + 2] << 16) |
                                        (ptr[(i * 4) + j * stride + 3] << 24));
                        Color pixel = Color.FromArgb(pixelID);

                        byte palIndex = 0x00;
                        bool foundExactMatch = false;

                        double closestDist = double.MaxValue;
                        byte closestIndex = 0;

                        for (int p = 0; p < paletteIDs.Length; p++)
                        {
                            if (paletteIDs[p] == pixelID)
                            {
                                palIndex = (byte)p;
                                foundExactMatch = true;
                                break;
                            }
                            else
                            {
                                //Get the dist to the color, and keep track of which is the best representation
                                double dist = ColorDistanceFrom(pixel, palette[p]);

                                if (dist < closestDist)
                                {
                                    closestDist = dist;
                                    closestIndex = (byte)p;
                                }
                            }
                        }

                        if (foundExactMatch)
                            imgData[index] = palIndex;
                        else
                            imgData[index] = closestIndex;
                    }
                }
            }
            bmp.UnlockBits(data);

            return imgData;
        }

        private double ColorDistanceFrom(Color color, Color comparisonColor)
        {
            double dist = Math.Pow(color.R - comparisonColor.R, 2) +
                                    Math.Pow(color.G - comparisonColor.G, 2) +
                                     Math.Pow(color.B - comparisonColor.B, 2);

            if ((color.A == 0 && comparisonColor.A != 0) ||
                (color.A != 0 && comparisonColor.A == 0))
                dist += 1000000000; //Just make it a worst choice if alphas don't line up

            return dist;
        }

        private class PNGChunk
        {
            private byte[] _data;
            private string _name;

            public PNGChunk(string type, byte[] data)
            {
                _name = type;
                _data = data;
            }

            public byte[] GetAsBytes()
            {
                byte[] data = new byte[_name.Length + _data.Length + 8];
                data[0] = (byte)((_data.Length >> 0x18) & 0xFF);
                data[1] = (byte)((_data.Length >> 0x10) & 0xFF);
                data[2] = (byte)((_data.Length >> 0x8) & 0xFF);
                data[3] = (byte)(_data.Length & 0xFF);

                for (int i = 0; i < _name.Length; i++)
                {
                    data[4 + i] = (byte)_name[i];
                }

                int startData = 4 + _name.Length;
                for (int i = 0; i < _data.Length; i++)
                {
                    data[startData + i] = _data[i];
                }

                Crc32 crc32 = new Crc32();

                Array.Copy(crc32.ComputeHash(data, 4, data.Length - 8), 0, data, data.Length - 4, 4);

                return data;

            }
        }

    }

}
