using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.Common.Utils;
using System.IO;

namespace MarioKartTestingTool
{
    public static class TKMKTest
    {
        static int t1, t7, t8, t9, s0, s1, s3, s4, s6, s7, v0, v1;

        // 0x400 allocated on stack
        static ushort[] rgba_buf = new ushort[0x40];     // SP[000]-SP[07F] - buffer of last used 32 RGBA colors
        static ushort[] buffer80_u16 = new ushort[0x3F]; // SP[080]-SP[0FD]
        static ushort[] bufferFE_u16 = new ushort[0x3F]; // SP[0FE]-SP[17B]
                                       // SP[17C]-SP[19F] - preserved registers
        static int[]  channelCommandBuffer = new int[8];  // SP[1A0]-SP[1BF] - byte buffer
        static int[] channelPtrs = new int[8];       // SP[1C0]-SP[1DF] - 8 pointers to A0 data read from starting at offset 0xC
        static ushort[] channelCommandRemainingBits = new ushort[8];       // SP[1E0]-SP[1EF] - 8 u16s, related to some_ptrs
        static uint[] some_u32s = new uint[0x80];    // SP[200]-SP[3F0] - indexes used to initialize buffer80 and bufferFE

        static int channelCommandMode; //...0xXXXXXXYY : XXXXXX - unknown, YY- determines mode for the channel command process using a bit for each channel
        static bool[] channelCommandRepeatMode = new bool[8];
        static bool[] channelCommandRepeatFlag = new bool[8];

        static int masterCommandOffset;
        static int masterCommandRemainingBits = 0;
        static int masterCommandBuffer;
        static int masterCommandPtr;

        static TKMKHuffmanTreeNode treeHead;

        // this is needed to to logical shifts on signed data
        static int SRL(int val, int amount)
        {
           uint vU = (uint)val;
           vU >>= amount;
           return (int)vU;
        }

        // a0[in]: pointer to TKMK00 data
        // a1[out]: pointer to output (1 byte per pixel)
        // a2[out]: pointer to output (RGBA16, 2 bytes per pixel)
        // a3[in]: RGBA color to set alpha to 0, values observed: 0x01, 0xBE
        public static void tkmk00decode(byte[] data, int tkmkOffset, out byte[] singleByteOutput, out byte[] doubleByteOutput, uint alphaFlagParam)  // 800405D0/0411D0
        {
            //TO DO: CLEAR THE BUFFERS BETWEEN DECODINGS

           uint offset;
           uint test_bits;
           int width, height;
           int col, row;
           int pixels;
           int alpha;
           uint i;
           ushort rgba0;
           ushort rgba1;
           byte red0, red1, green0, green1, blue0, blue1;
   
           width = ByteHelper.ReadUShort(data, tkmkOffset + 0x8);// read_u16_be(&tkmkOffset[0x8]);
           height = ByteHelper.ReadUShort(data, tkmkOffset + 0xA);//read_u16_be(&tkmkOffset[0xA]);
           alpha = (int)alphaFlagParam;
           channelCommandMode = data[tkmkOffset+0x6];
           for (int iii = 0; iii < 0x8; iii++) //Convert the channel command mode to bools
               channelCommandRepeatMode[iii] = (((channelCommandMode >> iii) & 0x1) != 0);

           pixels = width * height;
           for(int iCount = 0; iCount < rgba_buf.Length; iCount++)
               rgba_buf[iCount] = 0xFF;
            doubleByteOutput = new byte[2 * pixels];
            singleByteOutput = new byte[pixels];
            int doubleByteIndex = 0;
            int singleByteIndex = 0;
   
           for (i = 0; i < 8; i++)
           {
               offset = ByteHelper.ReadUInt(data, tkmkOffset + 0xC + (int)i * 4); //read_u32_be(&tkmkOffset[0xC + i*4]);
               if (0 == (channelCommandMode & (0x1 << (int)i)))
                   offset -= 4;
               else
                   offset--;
               channelPtrs[i] = tkmkOffset + (int)offset;
           }

            Array.Clear(channelCommandRemainingBits, 0, channelCommandRemainingBits.Length);

           //masterCommandOffset = 0x0; // no idea, used in proc_80040A60
           masterCommandBuffer = ByteHelper.ReadInt(data, tkmkOffset + 0x2C);// read_u32_be(&tkmkOffset[0x2C]); // used in proc_80040A60
           masterCommandRemainingBits = 0x20;
           masterCommandPtr = tkmkOffset + 0x2C;//ByteHelper.ReadInt(data, tkmkOffset + 0x30);//&tkmkOffset[0x30];
           uint val = 0x20;
           //SetUp5BitBuffers((uint)some_u32s.Length - 4, ref val, data); // recursive, init buffers???
           //treeHead = new TKMKHuffmanTreeNode();
           //treeHead.Value = 0x20;
           treeHead = NewNewSetUp5BitBuffers(val, data); // recursive, init buffers???
   
           t1 = v0;
           t7 = 0; //the last used rgba color
           StreamWriter writer = new StreamWriter("process.txt");
           for (row = 0; row != height; row++) {
              for (col = 0; col != width; col++) {
                  t9 = ByteHelper.ReadUShort(doubleByteOutput, doubleByteIndex);// ????????? read_u16_be(doubleByteOutput);
                //read the existing color of the byte

                 if (t9 == 0) { //Not set yet
                     v1 = singleByteOutput[singleByteIndex];
                 } else {
                     //Test to make sure that the curent color is not the alpha value with the incorrect alpha channel value
                    s3 = t9 & 0xFFFE; //All the color but the alpha channel
                    t7 = t9;
                    if (alpha != s3)
                    {
                        writer.WriteLine("UseExistingPixel");
                        goto goto_AdvanceToNextPixel;
                    }
                     //if alpha IS s3, then write it out
                    ByteHelper.WriteUShort((ushort)s3, doubleByteOutput, doubleByteIndex);// write_u16_be(doubleByteOutput, s3);
                    t7 = s3;
                    writer.WriteLine("RewriteAlphaPixel");
                    goto goto_AdvanceToNextPixel;
                 }
                 writer.Write("NewPixel-");
                 v1 += 1;
                 NewGetNextChannelCommand(data, v1, out v0); //Determine here if we use the previous color or a different one
         
                 if (v0 == 0) { //use last color here
                     ByteHelper.WriteUShort((ushort)t7, doubleByteOutput, doubleByteIndex);//write_u16_be(doubleByteOutput, t7);
                     writer.WriteLine("Previous color");
                    goto goto_AdvanceToNextPixel;
                 }
         
                 v1 = 1;
                 NewReadNextCommand(data, v1, out v0); //Check for creating new or re-using old RGBA value

                 if (v0 != 0)
                 { //Create new rgba value
                     writer.WriteLine("NewColor");
                     NewNewRetrieve5BitValue(data); //Read red component
            
                    s0 = s4; //store red component into s0
                    NewNewRetrieve5BitValue(data); //Read green component

                    s1 = s4; //store green component into s0
                    NewNewRetrieve5BitValue(data); //Read blue component and store into s0
            
                    rgba0 = 0; //pixel above this one
                    rgba1 = 0; //pixel before this one
                    if (row != 0) {
                        rgba0 = ByteHelper.ReadUShort(doubleByteOutput, doubleByteIndex - (width * 2));// read_u16_be(doubleByteOutput - (width * 2));
                        rgba1 = ByteHelper.ReadUShort(doubleByteOutput, doubleByteIndex - 2);//read_u16_be(doubleByteOutput - 2);
                    } else {
                       if (col != 0) {
                           rgba1 = ByteHelper.ReadUShort(doubleByteOutput, doubleByteIndex - 2);//read_u16_be(doubleByteOutput - 2);
                       }
                    }

                    green0 = (byte)((rgba0 & 0x7C0) >> 6);
                    green1 = (byte)((rgba1 & 0x7C0) >> 6);
                    t8 = (green0 + green1) / 2;
                    t9 = s0;
                    //proc_80040C94(); //Get the red component
                    ColorCombine(t8, ref t9);
                    s0 = t9;
            
                    v1 = t9 - t8;
                    red0 = (byte)((rgba0 & 0xF800) >> 11);
                    red1 = (byte)((rgba1 & 0xF800) >> 11);
                    t8 = v1 + (red0 + red1) / 2;
                    if (t8 >= 0x20) {
                       t8 = 0x1F;
                    } else if (t8 < 0) {
                       t8 = 0;
                    }
                    t9 = s1;
                    //proc_80040C94(); //Get the green component
                    ColorCombine(t8, ref t9);
                    s1 = t9;

                    blue0 = (byte)((rgba0 & 0x3E) >> 1);
                    blue1 = (byte)((rgba1 & 0x3E) >> 1);
                    t8 = v1 + (blue0 + blue1) / 2;
                    if (t8 >= 0x20) {
                       t8 = 0x1F;
                    } else if (t8 < 0) {
                       t8 = 0;
                    }
                    t9 = s4;
                    //proc_80040C94(); //Get the blue component
                    ColorCombine(t8, ref t9);

                    t7 = (s1 << 11) | (s0 << 6) | (t9 << 1); //Combine the color components into the ushort
                    if (t7 != alpha) {
                       t7 |= 0x1;
                    }
            
                    // insert new value by shifting others to right
                    for (i = (uint)rgba_buf.Length - 1; i > 0; i--) {
                       rgba_buf[i] = rgba_buf[i - 1];
                    }
                    rgba_buf[0] = (ushort)t7;
                 }
                 else
                 { //Use existing rgba value
                     writer.WriteLine("ExistingColor");
                    v1 = 6;
                    NewReadNextCommand(data, v1, out v0); //Read in the index of the rgba in the rgba buffer to use
                    t7 = rgba_buf[v0];
                    if (v0 != 0) {
                       for (i = (uint)v0; i > 0; i--) {
                          rgba_buf[i] = rgba_buf[i - 1];
                       }
                       rgba_buf[0] = (ushort)t7;
                    }
                 }

                  //Write the RGBA to the doubleByteIndex location
                 ByteHelper.WriteUShort((ushort)t7, doubleByteOutput, doubleByteIndex);//write_u16_be(doubleByteOutput, t7);

                 test_bits = 0; //stores flags marking the existence of pixels around the current pixel
                 if (col != 0) {
                    test_bits |= 0x01;
                 }
                 if (col < (width - 1)) {
                    test_bits |= 0x02;
                 } 
                 if (col < (width - 2)) {
                    test_bits |= 0x04;
                 }
                 if (row < (height - 1)) {
                    test_bits |= 0x08;
                 }
                 if (row < (height - 2)) {
                    test_bits |= 0x10;
                 }

                 if (0x2 == (test_bits & 0x2)) { //if a pixel exists to the right 1
                     singleByteOutput[singleByteIndex+1]++;
                 } 
                 if (0x4 == (test_bits & 0x4)) { //if a pixel exists to the right 2
                     singleByteOutput[singleByteIndex+2]++;
                 }
                 if (0x9 == (test_bits & 0x9))  { //if a pixel exists below 1 and to the left 1
                     singleByteOutput[singleByteIndex+width - 1]++; 
                 }
                 if (0x8 == (test_bits & 0x8)) { //if a pixel exists below 1
                     singleByteOutput[singleByteIndex+width]++;
                 }
                 if (0xA == (test_bits & 0xA)) { //if a pixel exists below 1 and right 1
                     singleByteOutput[singleByteIndex+width + 1]++;
                 }
                 if (0x10 == (test_bits & 0x10)) { //if a pixel exists 2 below this one
                     singleByteOutput[singleByteIndex+2 * width]++;
                 }
         
                 v1 = 1;
                 NewReadNextCommand(data, v1, out v0); //Check if we need to preload this color onto rows below this one
         
                 if (v0 != 0) {
                    int outOffset = 0;
                    s0 = width * 2; //Length of a row in bytes
                    s3 = t7 | 0x1; //Previous RGBA, with alpha forced on
            
                    do {
                       v1 = 2;
                       NewReadNextCommand(data, v1, out v0); //0 - advanced move, 1 - back one, 2 - no lateral move, 3 - forward one
                       if (v0 == 0) {
                          v1 = 1;
                          NewReadNextCommand(data, v1, out v0); //0 - stop advanced move, 1 - continue
  
                          if (v0 == 0) {
                             break;
                          } else {
                             v1 = 1;
                             NewReadNextCommand(data, v1, out v0); //0 - move back 2, 1 - move forward 2
                             outOffset += 4;
                             if (v0 == 0) {
                                outOffset -= 8;
                             }
                          }
                       } else if (v0 == 1) {
                          outOffset -= 2;
                       } else if (v0 == 3) {
                          outOffset += 2;
                       }
                       outOffset += s0; //Move down a row
                       ByteHelper.WriteUShort((ushort)s3, doubleByteOutput, doubleByteIndex + outOffset);//write_u16_be(out, s3);
                    } while (true);
                 }
        goto_AdvanceToNextPixel: //Advance the pointers one forward
                 singleByteIndex += 1;
                 doubleByteIndex += 2;
              }
           }
           writer.Close();
        }

        // inputs: a0, a3, v1, t0
        // outputs: a0, a3, t0, t8, t9, v0
        // Reads in the in_ptr int and progresses through the flags. V0 output size is v1 number of bits
        static void ReadNextCommand(byte[] data) // 80040A60/041660
        {
            //v1 is between 31 and 0 (for the bitshifting!)
            uint to_be_offset;
            to_be_offset = (uint)(masterCommandOffset + v1);
            t8 = 0x20 - v1;
            v0 = (int)((uint)masterCommandBuffer >> t8);//SRL(some_flags, t8); // v0 = t0 >> t8;
            if (to_be_offset < 0x21)
            {
                if (to_be_offset != 0x20)
                {
                    masterCommandBuffer <<= v1;
                    masterCommandOffset += v1;
                    return;
                }
                else
                {
                    masterCommandBuffer = ByteHelper.ReadInt(data, masterCommandPtr);// read_u32_be(in_ptr);
                    masterCommandOffset = 0;
                    masterCommandPtr += 4;
                    return;
                }
            }
            else
            {
                to_be_offset = 0x40;
                masterCommandBuffer = ByteHelper.ReadInt(data, masterCommandPtr);//read_u32_be(in_ptr);
                to_be_offset -= (uint)v1;
                to_be_offset -= (uint)masterCommandOffset;
                masterCommandOffset -= t8;
                t8 = (int)((uint)masterCommandBuffer >> (int)to_be_offset);// some_flags >> (int)to_be_offset;//SRL(some_flags, this_offset); // t8 = t0 >> t9;
                v0 |= t8; //could be =, since v0 is 0 at this point
                masterCommandPtr += 4;
                masterCommandBuffer <<= masterCommandOffset;
                return;
            }
        }

        static void NewReadNextCommand(byte[] data, int commandBitSize, out int commandValue) // 80040A60/041660
        {
            commandValue = 0; //clear the command value
            for (; commandBitSize > 0; commandBitSize--) //For each bit in the commandBitSize
            {
                if (masterCommandRemainingBits == 0) //If the end of the current masterCommandBuffer has been reached
                {
                    //Load the next byte and progress the pointer/reset the remaining bits
                    masterCommandPtr += 4;
                    masterCommandBuffer = ByteHelper.ReadInt(data, masterCommandPtr);
                    masterCommandRemainingBits = 0x20;
                }

                //Append the next bit in the masterCommandBuffer to the end of the commandValue
                commandValue = ((commandValue << 1) | ((masterCommandBuffer >> (masterCommandRemainingBits - 1)) & 0x1));

                masterCommandRemainingBits--; //Advance to the next bit
            }
        }

        // inputs: t2, v1(Points at one of the 8 offsets)
        // outputs: t8, t9, s6, s7, v0
        //This is looking to be the same as the previous one, just for the multiple-pointers group. Outputs 1 bit all the time
        static void GetNextChannelCommand(byte[] data) // 80040AC8/0416C8
        {
            //NOTE: the 0th some_ptrs refers to the one used exclusively by proc_80040BC0 to set up the buffers. 1-7 refer to
            //      the values in the singleByteOutput (0-6)

           int s6ptr;
           t8 = channelCommandMode >> v1; // t8 = t2 >> v1;
           t9 = t8 & 0x1; //Tells if the header byte is on or off
           s7 = channelCommandRemainingBits[v1]; //Contains the bits remaining in the command list. Ranges 0x19 -> 0x00
           if (t9 == 0) { //If the header byte is off, it returns the next bit in the command list (same as the master commands)
               s6ptr = channelPtrs[v1];
              if (s7 == 0) {
                  s6ptr += 4; //Load next byte
                 s7 = 0x20;
                 channelPtrs[v1] = s6ptr;
              }
              t9 = ByteHelper.ReadInt(data, s6ptr); //Read the current byte
              s7 -= 1;
              channelCommandRemainingBits[v1] = (ushort)s7;
              v0 = SRL(t9, s7); // v0 = t9 >> s7;
              v0 &= 0x1;
              return; //single bit
           }
            //Else if the header byte is on - Another flag determines if it will repeat the loaded byte (flag is off) or if it will
            // load bytes just like the other command loaders. The remaining bits will be more than a single byte's worth.
            // First byte of a new grouping defines whether the flag is flipped or not, and also contains the # of bytes remaining
           s6ptr = channelPtrs[v1];
           if (s7 == 0) { //if it's at the end of its line
               s7 = ByteHelper.ReadByte(data, s6ptr); //This byte either flips on the flag (doesn't repeat )
              v0 = 0x100;
              v0 <<= v1;
              if ((s7 & 0x80) == 0x00) { //If the first bit is on, turn off the flag
                 v0 = ~v0;
                 s7 += 3;
                 channelCommandMode &= v0; //Turns off a header flag
              }
              else //first bit is off, turn on the flag
              {
                 s7 &= 0x7F;
                 s7 += 1;
                 channelCommandMode |= v0; //Turns on a header flag
              }
              v0 = ByteHelper.ReadByte(data, s6ptr+1); //Load the next byte
              s6ptr += 2;
              s7 <<= 3;
              channelCommandBuffer[v1] = (byte)v0; //push a byte onto the byte buffer
              channelPtrs[v1] = s6ptr;
           }
           v0 = channelCommandBuffer[v1];
           s7 -= 1;
           channelCommandRemainingBits[v1] = (ushort)s7;
           t8 = s7 & 0x7;
           v0 = SRL(v0, t8); // v0 >>= t8;
           v0 &= 0x1;
           if (t8 == 0 && s7 != 0) { //If s7 somehow ends up above 0x7
              t8 = 0x100;
              s7 = t8 << v1;
              s7 &= channelCommandMode;
              if (s7 != 0) {
                  s7 = ByteHelper.ReadByte(data, s6ptr);
                 s6ptr += 1;
                 channelCommandBuffer[v1] = (byte)s7;
                 channelPtrs[v1] = s6ptr;
              }
           }
           return;
        }

        static void NewGetNextChannelCommand(byte[] data, int channelIndex, out int commandValue) // 80040AC8/0416C8
        {
            //NOTE: the 0th some_ptrs refers to the one used exclusively by proc_80040BC0 to set up the buffers. 1-7 refer to
            //      the values in the singleByteOutput (0-6)

            commandValue = 0; //clear the command value

            if (!channelCommandRepeatMode[channelIndex])
            {
                //No repeat mode, treat it like normal

                if (channelCommandRemainingBits[channelIndex] == 0) //If the end of the current masterCommandBuffer has been reached
                {
                    //Load the next byte and progress the pointer/reset the remaining bits
                    channelPtrs[channelIndex] += 4;
                    channelCommandBuffer[channelIndex] = ByteHelper.ReadInt(data, channelPtrs[channelIndex]);
                    channelCommandRemainingBits[channelIndex] = 0x20;
                }

                //Append the next bit in the masterCommandBuffer to the end of the commandValue
                commandValue = ((channelCommandBuffer[channelIndex] >> (channelCommandRemainingBits[channelIndex] - 1)) & 0x1);
                channelCommandRemainingBits[channelIndex]--; //Advance to the next bit
            }
            else
            {
                //Repeat mode
                if (channelCommandRemainingBits[channelIndex] == 0) //out of data
                {
                    channelPtrs[channelIndex]++;
                    byte repeatInfo = ByteHelper.ReadByte(data, channelPtrs[channelIndex]);

                    channelCommandRepeatFlag[channelIndex] = ((repeatInfo & 0x80) != 0);

                    if (channelCommandRepeatFlag[channelIndex]) //true, read like normal
                    {
                        channelCommandRemainingBits[channelIndex] = (ushort)(((repeatInfo & 0x7F) + 1) * 8);
                    }
                    else //false, repeat current 
                    {
                        channelCommandRemainingBits[channelIndex] = (ushort)((repeatInfo + 3) * 8);
                    }

                    channelPtrs[channelIndex]++;
                    channelCommandBuffer[channelIndex] = ByteHelper.ReadByte(data, channelPtrs[channelIndex]);
                }

                int byteWrappedRemainingBits = (channelCommandRemainingBits[channelIndex] & 0x7);
                if (byteWrappedRemainingBits == 0)
                    byteWrappedRemainingBits = 8;

                commandValue = ((channelCommandBuffer[channelIndex] >> (byteWrappedRemainingBits - 1)) & 0x1);
                channelCommandRemainingBits[channelIndex]--; //Advance to the next bit

                byteWrappedRemainingBits--;

                if (byteWrappedRemainingBits == 0 && channelCommandRemainingBits[channelIndex] > 0)
                {
                    if (channelCommandRepeatFlag[channelIndex]) //Not repeat mode
                    {
                        channelPtrs[channelIndex]++;
                        channelCommandBuffer[channelIndex] = ByteHelper.ReadByte(data, channelPtrs[channelIndex]);
                    }
                }
            }
        }

        // inputs: s3, s4
        // outputs: v0, v1, s0, s1, s3, s4
        //Only runs once, at the beginning, but is recursive. Sets up the buffers.
        static void SetUp5BitBuffers(uint u32idx, ref uint val, byte[] data) // 80040BC0/0417C0
        {
           u32idx--;
           v1 = 0;
           NewGetNextChannelCommand(data, v1, out v0); //Grabs the buffer set up information

           if (v0 != 0) {
              uint idx;
              some_u32s[u32idx] = val;
              val++;
              SetUp5BitBuffers(u32idx, ref val, data);
              idx = some_u32s[u32idx];
              buffer80_u16[idx] = (ushort)v0;
              SetUp5BitBuffers(u32idx, ref val, data);
              idx = some_u32s[u32idx];
              u32idx++;
              s6 = (int)idx;
              bufferFE_u16[idx] = (ushort)v0;
              v0 = s6;
              return;
           } else {
              s0 = 0;
           }
           s1 = 5;
           do {
              v1 = 0;
              NewGetNextChannelCommand(data, v1, out v0);
              s0 = v0 + s0 * 2; //basically bitshifts s0 to the left and adds v0, aka it's loading 5 bytes straight from the 0th some_ptr
              s1 -= 1;
           } while (s1 != 0);
           u32idx++;
           v0 = s0;
           return;
        }

        public class TKMKHuffmanTreeNode
        {
            public TKMKHuffmanTreeNode Left;
            public TKMKHuffmanTreeNode Right;
            public int Value;

            public bool IsEndNode
            { get { return Left == null && Right == null; } }
        }

        static void NewSetUp5BitBuffers(ref uint val, byte[] data) // 80040BC0/0417C0
        {
            uint startingVal = val;

            v1 = 0;
            NewGetNextChannelCommand(data, v1, out v0); //Grabs the buffer set up information

            if (v0 != 0)
            {
                val++;
                NewSetUp5BitBuffers(ref val, data);
                buffer80_u16[startingVal] = (ushort)v0;
                NewSetUp5BitBuffers(ref val, data);
                bufferFE_u16[startingVal] = (ushort)v0;
                v0 = (int)startingVal;
                return;
            }

            s0 = 0;
            s1 = 5;
            do
            {
                v1 = 0;
                NewGetNextChannelCommand(data, v1, out v0);
                s0 = s0 * 2 + v0; //basically bitshifts s0 to the left and adds v0, aka it's loading 5 bytes straight from the 0th some_ptr
                s1 -= 1;
            } while (s1 != 0);

            v0 = s0;
            return;
        }

        static TKMKHuffmanTreeNode NewNewSetUp5BitBuffers(uint val, byte[] data) // 80040BC0/0417C0
        {
            TKMKHuffmanTreeNode newTree = new TKMKHuffmanTreeNode();
            newTree.Value = (int)val;
            //uint startingVal = val;

            NewGetNextChannelCommand(data, 0, out v0); //Grabs the buffer set up information

            if (v0 != 0)
            {
                val++;
                TKMKHuffmanTreeNode left = NewNewSetUp5BitBuffers(val, data);
                //buffer80_u16[startingVal] = (ushort)v0;
                TKMKHuffmanTreeNode right = NewNewSetUp5BitBuffers(val, data);
                //bufferFE_u16[startingVal] = (ushort)v0;
                //v0 = (int)startingVal;
                newTree.Left = left;
                newTree.Right = right;
                return newTree;
            }

            s0 = 0;
            s1 = 5;
            do
            {
                NewGetNextChannelCommand(data, 0, out v0);
                s0 = s0 * 2 + v0; //basically bitshifts s0 to the left and adds v0, aka it's loading 5 bytes straight from the 0th some_ptr
                s1 -= 1;
            } while (s1 != 0);

            newTree.Value = s0;
            return newTree;
        }

        // inputs: t1
        // outputs: s4
        //Only called when creating new rgba value. follows the maze of the color buffers to return the correct value
        static void Retrieve5BitValue(byte[] data) // 80040C54/041854
        {
            s4 = t1;
            while (s4 >= 0x20)
            {
                v1 = 0;
                NewGetNextChannelCommand(data, v1, out v0);
                if (v0 == 0)
                {
                    s4 = buffer80_u16[s4];
                }
                else
                {
                    s4 = bufferFE_u16[s4];
                }
            }
        }

        // inputs: t1
        // outputs: s4
        //Only called when creating new rgba value. follows the maze of the color buffers to return the correct value
        static void NewNewRetrieve5BitValue(byte[] data) // 80040C54/041854
        {
            TKMKHuffmanTreeNode currentNode = treeHead;
            //s4 = t1;
            while (currentNode.Value >= 0x20 && !currentNode.IsEndNode)
            {
                v1 = 0;
                NewGetNextChannelCommand(data, v1, out v0);
                if (v0 == 0)
                {
                    //s4 = buffer80_u16[s4];
                    currentNode = currentNode.Left;
                }
                else
                {
                    //s4 = bufferFE_u16[s4];
                    currentNode = currentNode.Right;
                }
            }
            s4 = currentNode.Value;
        }

        // inputs: t8 (last color component, 5 bits), t9 (command from proc_80040C54 call)
        // outputs: t9 (new color component)
        // Only called when creating new rgba value
        static void proc_80040C94() // 80040C94/041894
        {
           if (t8 >= 0x10) {
              v0 = (0x1F - t8) * 2;
              if (v0 < t9) {
                 v0 = 0x1F;
                 t9 = v0 - t9;
                 return;
              } else {
                 v0 = t9 & 0x1;
              }
              t9 = SRL(t9, 1); // t9 >>= 1;
              if (v0 != 0) {
                 t9 += t8 + 1;
                 return;
              } else {
                 t9 = t8 - t9;
                 return;
              }
           } else {
              v0 = t8 << 1;
           }
           if (v0 >= t9) { 
              v0 = t9 & 0x1; //Last bit of t9 is there to determine if t9 is added or subracted from t8?
              t9 = SRL(t9, 1); // t9 >>= 1;
              if (v0 != 0) {
                 t9 += t8 + 1;
                 return;
              } else {
                 t9 = t8 - t9;
                 return;
              }
           }
           return;
        }

        static void ColorCombine(int oldColor, ref int newColor) // 80040C94/041894
        {
            if (oldColor >= 0x10)
            {
                int inversedOldColor = (0x1F - oldColor) << 1;
                if (inversedOldColor < newColor)
                {
                    newColor = 0x1F - newColor;
                    return;
                }
            }
            else
            {
                int boostedOldColor = oldColor << 1;
                if (boostedOldColor < newColor)
                {
                    return;
                }
            }

            bool addToOld = (newColor & 0x1) != 0; //Last bit of t9 is there to determine if t9 is added or subracted from t8?
            newColor = newColor >> 1; // t9 >>= 1;
            if (addToOld)
                newColor += oldColor + 1;
            else
                newColor = oldColor - newColor;

            return;
        }

        static void NewColorCombine(int predictedColor, ref int newColor) // 80040C94/041894
        {
            //First off, see if our numbers could possibly go over 0x1F or below 0x00 if we do the differential method. If they could,
            // then just use the new color.

            if (predictedColor >= 0x10) //Check if it'll go above 0x1F
            {
                int remainingSpace = (0x1F - predictedColor);
                int incrementDecrementValue = newColor >> 1;
                bool isAdding = (newColor & 0x1) != 0;
                if (remainingSpace < incrementDecrementValue || (remainingSpace == incrementDecrementValue && isAdding)) //Since an extra 1 is used when adding
                {
                    newColor = 0x1F - newColor;
                    return;
                }
            }
            else
            {
                //int boostedOldColor = predictedColor << 1;
                int incrementDecrementValue = newColor >> 1;
                bool isAdding = (newColor & 0x1) != 0;
                if (predictedColor < incrementDecrementValue || (predictedColor == incrementDecrementValue && isAdding))
                {
                    return;
                }
            }

            bool addToOld = (newColor & 0x1) != 0; //Last bit of t9 is there to determine if t9 is added or subracted from t8?
            newColor = newColor >> 1; // t9 >>= 1;
            if (addToOld)
                newColor += predictedColor + 1;
            else
                newColor = predictedColor - newColor;

            return;
        }
    }
}
