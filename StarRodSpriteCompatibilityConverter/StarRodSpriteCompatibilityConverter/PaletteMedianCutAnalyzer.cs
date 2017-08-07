using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StarRodSpriteCompatibilityConverter
{
    public class PaletteMedianCutAnalyzer
    {
        private byte _minRed, _maxRed, _minBlue, _maxBlue, _minGreen, _maxGreen;
        private List<Color> _opaqueColors, _transparentColors;

        public PaletteMedianCutAnalyzer()
        {
            _minRed = 0xFF;
            _minBlue = 0xFF;
            _minGreen = 0xFF;

            _maxRed = 0x00;
            _maxBlue = 0x00;
            _maxGreen = 0x00;

            _opaqueColors = new List<Color>();
            _transparentColors = new List<Color>();
        }

        public void AddColor(Color color)
        {
            if (color.A == 0)
            {
                //Add to transparent
                if (!_transparentColors.Contains(color))
                    _transparentColors.Add(color);
            }
            else
            {
                if (!_opaqueColors.Contains(color))
                {
                    _opaqueColors.Add(color);
                    _minRed = (byte)Math.Min(color.R, _minRed);
                    _minGreen = (byte)Math.Min(color.G, _minGreen);
                    _minBlue = (byte)Math.Min(color.B, _minBlue);

                    _maxRed = (byte)Math.Min(color.R, _maxRed);
                    _maxGreen = (byte)Math.Min(color.G, _maxGreen);
                    _maxBlue = (byte)Math.Min(color.B, _maxBlue);
                }
            }
        }

        public Color[] GetPalette(int paletteSize)
        {
            Color[] colors = new Color[paletteSize];

            ColorBox[] boxes = new ColorBox[paletteSize];

            int boxIndex = 0;
            ColorBox box;
            int startIndex = 0;

            if (_transparentColors.Count > 0)
            {
                box = new ColorBox(0);
                foreach (Color color in _transparentColors)
                    box.Colors.Add(color);
                box.ShrinkToFit();
                boxes[boxIndex] = box;
                boxIndex++;
                startIndex++;
            }

            //Handle the rest of the boxes
            box = new ColorBox(0);
            foreach (Color color in _opaqueColors)
                box.Colors.Add(color);
            box.ShrinkToFit();
            boxes[boxIndex] = box;
            boxIndex++;
            while (boxIndex < paletteSize)
            {
                int longestEdge = -1;
                int longestIndex = startIndex;
                for (int i = startIndex; i < boxIndex; i++)
                {
                    if (boxes[i].LongestSide > longestEdge)
                    {
                        longestEdge = boxes[i].LongestSide;
                        longestIndex = i;
                    }
                }

                Tuple<ColorBox, ColorBox> newBoxes = boxes[longestIndex].SplitBox();

                boxes[longestIndex] = newBoxes.Item1;
                boxes[boxIndex] = newBoxes.Item2;

                boxIndex++;
            }

            for (int i = 0; i < boxes.Length; i++)
                colors[i] = boxes[i].GetCentroidColor();

            return colors;
        }

        private struct ColorBox
        {
            public byte MinRed;
            public byte MaxRed;
            public byte MinBlue;
            public byte MaxBlue;
            public byte MinGreen;
            public byte MaxGreen;

            public List<Color> Colors;

            public ColorBox(int nothing = 0)
            {
                MinRed = 0xFF;
                MinBlue = 0xFF;
                MinGreen = 0xFF;

                MaxRed = 0x00;
                MaxGreen = 0x00;
                MaxBlue = 0x00;

                Colors = new List<Color>();
            }

            public ColorBox(byte minR, byte maxR, byte minB, byte maxB, byte minG, byte maxG)
            {
                MinRed = minR;
                MinBlue = minB;
                MinGreen = minG;

                MaxRed = maxR;
                MaxGreen = maxG;
                MaxBlue = maxB;

                Colors = new List<Color>();
            }

            public bool CanContainColor(Color color)
            {
                if (color.R < MinRed || color.R > MaxRed)
                    return false;
                if (color.B < MinBlue || color.B > MaxBlue)
                    return false;
                if (color.G < MinGreen || color.G > MaxGreen)
                    return false;

                return true;
            }

            public byte LongestSide
            {
                get
                {
                    if (MaxRed < MinRed || MaxBlue < MinBlue || MaxGreen < MinGreen) //Invalid box
                        return 0;

                    return (byte)Math.Max(Math.Max(MaxRed - MinRed, MaxGreen - MinGreen), MaxBlue - MinBlue);
                }
            }

            public Tuple<ColorBox, ColorBox> SplitBox()
            {
                byte redLength = (byte)(MaxRed - MinRed);
                byte blueLength = (byte)(MaxBlue - MinBlue);
                byte greenLength = (byte)(MaxGreen - MinGreen);

                ColorBox cb1, cb2;

                if (redLength >= blueLength && redLength >= greenLength)
                {
                    cb1 = new ColorBox(MinRed, (byte)(MinRed + (redLength / 2)), MinBlue, MaxBlue, MinGreen, MaxGreen);
                    cb2 = new ColorBox((byte)(MinRed + (redLength / 2)), MaxRed, MinBlue, MaxBlue, MinGreen, MaxGreen);
                }
                else if (blueLength >= redLength && blueLength >= greenLength)
                {
                    cb1 = new ColorBox(MinRed, MaxRed, MinBlue, (byte)(MinBlue + (blueLength / 2)), MinGreen, MaxGreen);
                    cb2 = new ColorBox(MinRed, MaxRed, (byte)(MinBlue + (blueLength / 2)), MaxBlue, MinGreen, MaxGreen);
                }
                else
                {
                    cb1 = new ColorBox(MinRed, MaxRed, MinBlue, MaxBlue, MinGreen, (byte)(MinGreen + (greenLength / 2)));
                    cb2 = new ColorBox(MinRed, MaxRed, MinBlue, MaxBlue, (byte)(MinGreen + (greenLength / 2)), MaxGreen);
                }

                foreach (Color color in Colors)
                {
                    if (cb1.CanContainColor(color))
                        cb1.Colors.Add(color);
                    else
                        cb2.Colors.Add(color);
                }

                cb1.ShrinkToFit();
                cb2.ShrinkToFit();

                return new Tuple<ColorBox, ColorBox>(cb1, cb2);
            }

            public Color GetCentroidColor()
            {
                int red = 0, blue = 0, green = 0, alpha = 0;
                foreach (Color color in Colors)
                {
                    red += color.R;
                    green += color.G;
                    blue += color.B;
                    alpha += color.A;
                }

                if (Colors.Count > 0)
                {
                    red = (int)Math.Round(red / (double)Colors.Count);
                    blue = (int)Math.Round(blue / (double)Colors.Count);
                    green = (int)Math.Round(green / (double)Colors.Count);
                    alpha = alpha > 0 ? 255 : 0;
                }

                return Color.FromArgb(alpha, red, green, blue);
            }

            public void ShrinkToFit()
            {
                //Compress the box
                MinRed = 0xFF;
                MinGreen = 0xFF;
                MinBlue = 0xFF;

                MaxRed = 0x00;
                MaxGreen = 0x00;
                MaxBlue = 0x00;

                foreach (Color color in Colors)
                {
                    MinRed = (byte)Math.Min(color.R, MinRed);
                    MinGreen = (byte)Math.Min(color.G, MinGreen);
                    MinBlue = (byte)Math.Min(color.B, MinBlue);

                    MaxRed = (byte)Math.Max(color.R, MaxRed);
                    MaxGreen = (byte)Math.Max(color.G, MaxGreen);
                    MaxBlue = (byte)Math.Max(color.B, MaxBlue);
                }
            }
        }
    }

}
