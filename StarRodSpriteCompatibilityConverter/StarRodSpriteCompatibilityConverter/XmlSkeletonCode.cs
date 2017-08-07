using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace StarRodSpriteCompatibilityConverter
{
    [XmlRoot("SpriteSheet")]
    public class SpritesheetSkeleton
    {
        [XmlAttribute("a")]
        public string a { get; set; }
        [XmlAttribute("b")]
        public string b { get; set; }

        [XmlArray("PaletteList"), XmlArrayItem(typeof(PaletteSkeleton), ElementName = "Palette")]
        public List<PaletteSkeleton> PaletteList { get; set; }
        [XmlArray("RasterList"), XmlArrayItem(typeof(RasterSkeleton), ElementName = "Raster")]
        public List<RasterSkeleton> RasterList { get; set; }
        [XmlArray("AnimationList"), XmlArrayItem(typeof(AnimationSkeleton), ElementName = "Animation")]
        public List<AnimationSkeleton> AnimationList { get; set; }
    }
    public class PaletteSkeleton
    {
        [XmlAttribute("id")]
        public string id { get; set; }
        [XmlAttribute("src")]
        public string src { get; set; }
    }
    public class RasterSkeleton
    {
        [XmlAttribute("id")]
        public string id { get; set; }
        [XmlAttribute("palette")]
        public string palette { get; set; }
        [XmlAttribute("src")]
        public string src { get; set; }
    }
    public class AnimationSkeleton
    {
        [XmlElement("Component")]
        public List<ComponentSkeleton> Components { get; set; }
    }
    public class ComponentSkeleton
    {
        [XmlAttribute("x")]
        public string x { get; set; }
        [XmlAttribute("y")]
        public string y { get; set; }
        [XmlAttribute("z")]
        public string z { get; set; }

        [XmlElement("Command")]
        public List<CommandSkeleton> Commands { get; set; }

        [XmlElement("Label")]
        public List<LabelSkeleton> Labels { get; set; }
    }
    public class CommandSkeleton
    {
        [XmlAttribute("val")]
        public string val { get; set; }
    }
    public class LabelSkeleton
    {
        [XmlAttribute("name")]
        public string name { get; set; }
        [XmlAttribute("pos")]
        public string pos { get; set; }
    }

}
