using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace StarRodSpriteCompatibilityConverter
{
    public class CI8Palette
    {
        public Color[] Color { get; private set; }

        public string Name { get; private set; }

        public string FilePath { get; private set; }

        public CI8Palette(string name, string filePath, Color[] color)
        {
            Name = name;
            FilePath = filePath;
            Color = color;
        }
    }

}
