﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.DataElements;
using System.ComponentModel;
using Cereal64.Common.Utils;

namespace MarioKartTestingTool
{
    public class CourseDataReferenceEntry : N64DataElement
    {
        public enum CourseDataID
        {
            MarioRaceway = 0x0,
            ChocoMountain,
            BowsersCastle,
            BansheeBoardwalk,
            YoshiValley,
            FrappeSnowland,
            KoopaTroopaBeach,
            RoyalRaceway,
            LuigiRaceway,
            MooMooFarm,
            ToadsTurnpike,
            KalimariDesert,
            SherbertLand,
            RainbowRoad,
            WarioStadium,
            BlockFort,
            Skyscraper,
            DoubleDeck,
            DKsJungleParkway,
            BigDonut
        }

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Start of the display list MIO0 block (segment 6)")]
        public int DisplayListBlockStart { get { return _displayListBlockStart; } set { _displayListBlockStart = value; } }
        private int _displayListBlockStart;


        [CategoryAttribute("Course Data"),
        DescriptionAttribute("End of the display list MIO0 block (segment 6)")]
        public int DisplayListBlockEnd { get { return _displayListBlockEnd; } set { _displayListBlockEnd = value; } }
        private int _displayListBlockEnd;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Start of the vertex/compressed display list MIO0 block (segment 4)")]
        public int VertexBlockStart { get { return _vertexBlockStart; } set { _vertexBlockStart = value; } }
        private int _vertexBlockStart;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("End of the vertex/compressed display MIO0 block (segment 7)")]
        public int VertexBlockEnd { get { return _vertexBlockEnd; } set { _vertexBlockEnd = value; } }
        private int _vertexBlockEnd;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Start of the texture MIO0 block (segment 9)")]
        public int TextureBlockStart { get { return _textureBlockStart; } set { _textureBlockStart = value; } }
        private int _textureBlockStart;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("End of the texture MIO0 block (segment 9)")]
        public int TextureBlockEnd { get { return _textureBlockEnd; } set { _textureBlockEnd = value; } }
        private int _textureBlockEnd;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Segment the display lists are stored in")]
        public byte Segment { get { return _segment; } set { _segment = value; } }
        private byte _segment;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Number of vertices in the vertex file")]
        public int VertexCount { get { return _vertexCount; } set { _vertexCount = value; } }
        private int _vertexCount;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Offset in the segment for the display lists")]
        public int DisplayListOffset { get { return _displayListOffset; } set { _displayListOffset = value; } }
        private int _displayListOffset;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Size of the display list block")]
        public int DisplayListSize { get { return _displayListSize; } set { _displayListSize = value; } }
        private int _displayListSize;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Segment the textures are stored in")]
        public byte TextureSegment { get { return _textureSegment; } set { _textureSegment = value; } }
        private byte _textureSegment;

        [CategoryAttribute("Course Data"),
        DescriptionAttribute("Unknown 2 - Either 0000 or 0001")]
        public ushort Unknown2 { get { return _unknown2; } set { _unknown2 = value; } }
        private ushort _unknown2;

        public CourseDataID CourseID;

        public CourseDataReferenceEntry(int offset, byte[] data, int courseID)
            : base(offset, data)
        {
            CourseID = (CourseDataID)courseID;
        }

        public override byte[] RawData
        {
            get
            {
                return ByteHelper.CombineIntoBytes(
                    _displayListBlockStart,
                    _displayListBlockEnd,
                    _vertexBlockStart,
                    _vertexBlockEnd,
                    _textureBlockStart,
                    _textureBlockEnd,
                    _segment << 24, //calculates as int
                    _vertexCount,
                    _displayListOffset,
                    _displayListSize,
                    _textureSegment << 24, //calculates as int
                    _unknown2 << 16); //calculates as int
            }
            set
            {
                if (value.Length != 0x30)
                    return;

                _displayListBlockStart = ByteHelper.ReadInt(value, 0x0);
                _displayListBlockEnd = ByteHelper.ReadInt(value, 0x4);
                _vertexBlockStart = ByteHelper.ReadInt(value, 0x8);
                _vertexBlockEnd = ByteHelper.ReadInt(value, 0xC);
                _textureBlockStart = ByteHelper.ReadInt(value, 0x10);
                _textureBlockEnd = ByteHelper.ReadInt(value, 0x14);
                _segment = ByteHelper.ReadByte(value, 0x18);
                _vertexCount = ByteHelper.ReadInt(value, 0x1C);
                _displayListOffset = ByteHelper.ReadInt(value, 0x20);
                _displayListSize = ByteHelper.ReadInt(value, 0x24);
                _textureSegment = ByteHelper.ReadByte(value, 0x28);
                _unknown2 = ByteHelper.ReadUShort(value, 0x2C);
            }
        }

        public override int RawDataSize
        {
            get { return 0x30; }
        }

        public override string ToString()
        {
            return CourseID.ToString();
        }
    }
}
