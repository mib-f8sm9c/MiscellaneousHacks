using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

namespace StarRodSpriteCompatibilityConverter
{
    public class CI8Image
    {
        public CI8Palette Palette { get; set; }

        public Bitmap Image { get; private set; }

        public byte[] CI8Data { get; private set; }

        public string Name { get; private set; }

        public string FilePath { get; private set; }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public CI8Image(string name, string filePath, byte[] data, CI8Palette palette, int width, int height)
        {
            Name = name;
            FilePath = filePath;
            CI8Data = data;
            Palette = palette;
            Width = width;
            Height = height;
            UpdateImage();
        }

        public void UpdateImage()
        {
            //Convert CI8 to image
            if (Palette == null)
                Image = null;
            else
                Image = BinaryToCI8(CI8Data, Palette.Color, 0, Width, Height);
        }

        private Bitmap BinaryToCI8(byte[] imgData, Color[] palette, int paletteIndex, int width, int height)
        {
            //Pixel size is 1 byte
            if (width * height != imgData.Length)
                return null;

            if (palette == null || palette.Length < 1)
                return null;

            Bitmap bmp = new Bitmap(width, height);

            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int stride = data.Stride;
            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        int index = (i + j * width);

                        byte CI = imgData[index];

                        if (CI > palette.Length)
                            CI = 0;

                        ptr[(i * 4) + j * stride] = palette[CI].B;
                        ptr[(i * 4) + j * stride + 1] = palette[CI].G;
                        ptr[(i * 4) + j * stride + 2] = palette[CI].R;
                        ptr[(i * 4) + j * stride + 3] = palette[CI].A;
                    }
                }
            }
            bmp.UnlockBits(data);

            return bmp;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
