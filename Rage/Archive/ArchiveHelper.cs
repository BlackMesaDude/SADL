/**
 *  Copyright (c) 2020 francescomesianodev
 * 
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.
 **/

 using System;
 using System.IO;
 using System.Collections.Generic;

 namespace SADL.Rage 
 {
     /// <summary>
     /// Constains information of a single DataBlock such as the offset, length or tag that refers to it
     /// <summary>
     public class DataBlock 
     {
        /// <summary>
        /// Offset of the DataBlock
        /// </summary>
        public long Offset { get; set; }
        /// <summary>
        /// Length of the DataBlock
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// The tag that references the DataBlock
        /// </summary>
        public object Tag { get; set; }

         public DataBlock(long offset, long length, object tag)
         {
            this.Offset = offset;
            this.Length = length;
            this.Tag = tag;
         }
     }

    
    /// <summary>
    /// Defines a set of functions that help the manipulation of an archive
    /// </summary>
     public static class ArchiveHelper 
     {
        private const int BUFFER_SIZE = 16384;                                                                  // standard buffer size

        /// <summary>
        /// Finds the maximum length of a specified <see cref="DataBlock"/>
        /// </summary>
        /// <param name="blocks">DataBlock(s) that need to be checked</param>
        /// <param name="item">The item that defines the maximum length that a DataBlock needs</param>
        /// <returns>Returns the maximum length of the defined DataBlock</returns>
        public static long FindSpace(List<DataBlock> blocks, DataBlock item)
        {
            blocks.Sort((prev, next) => prev.Offset.CompareTo(next.Offset));                                    // lambda expression for comparing each element inside the given list

            DataBlock smallest = null;                                                                          // defines the smallest datablock
            for(int i = 0; i < blocks.Count; i++)
            {
                // if n block isn't equal to item and n block offset is greater then the item offset then make smallest datablock be equal to n block
                if((blocks[i] != item) && (blocks[i].Offset >= item.Offset))
                {
                    if(smallest == null)
                    {
                        smallest = blocks[i];
                    }
                    else 
                    {
                        if(blocks[i].Offset < smallest.Offset)
                        {
                            smallest = blocks[i];
                        }
                    }
                }
            }

            // if the smallest block is unfound (null) then return 0
            if(smallest == null)
                return 0;
            
            return smallest.Offset - item.Offset;                                                                  // returns the smallest offset decremented by the item offset
        }

        /// <summary>
        /// Finds a block of empty space of the specified length
        /// </summary>
        /// <param name="blocks">DataBlock(s) that need to be checked</param>
        /// <param name="space">The space that a block should have</param>
        /// <param name="size">The size or length that a block should have</param>
        /// <returns>Returns the offset where the empty space resides</returns>
        public static long FindOffset(List<DataBlock> blocks, long space, long size = 1)
        {
            // defines a temporary list that will contain processed datablocks
            List<DataBlock> temp = new List<DataBlock>();
            temp.AddRange(blocks);                                                                                 // sets the range of the list to the defined blocks list

            temp.Sort((prev, next) => prev.Offset.CompareTo(next.Offset));                                         // lambda expression for comparing each element inside the given list

            // if the temporary list has 0 elements than return 0
            if(temp.Count == 0)
                return 0;

            // allows to set the n datablock length to the minimum value between the original length and the next n block offset decremented by the original length
            for(int i = 0; i < temp.Count - 1; i++)
            {
                temp[i].Length = Math.Min(temp[i].Length, temp[i + 1].Offset - temp[i].Offset);
            }

            long offset = 0;                                                                                       // defines the new offset
            for(int i = 0; i < temp.Count; i++)
            {
                long len = temp[i].Offset - offset;                                                                // defines the length of the n block based on the original offset decremented by the new offset
                // if the length is greater than the space then return the new offset
                if(len >= space)
                    return offset;

                offset = temp[i].Offset + temp[i].Length;                                                          // set the offset value equal to the original offset added to the original length 
                offset = (long) Math.Ceiling((double) offset / (double) size) * size;                              // sets the offset value equal to the ceiling of the offset divided by the size then multiplied, again, by the size
            }

            return offset;                                                                                         // returns the new offset
        }

        /// <summary>
        /// Allows to move the bytes that reside in a stream from a initial offset to a destination offset with a specified length
        /// </summary>
        /// <param name="stream">Base stream that contains the entire bytestream</param>
        /// <param name="srcOffset">Initial offset where the data needs to be taken</param>
        /// <param name="desOffset">Destination offset where the data will be moved</param>
        /// <param name="length">The length of the data that needs to be moved</param>
        public static void MoveBytes(Stream stream, long srcOffset, long desOffset, long length)
        {
            byte[] buffer = new byte[BUFFER_SIZE];                                                                 // creates a byte array wich is size is based on the standard buffer size
            while(length > 0)
            {
                stream.Position = srcOffset;                                                                       // sets the stream position to the initial position
                int data = stream.Read(buffer, 0, (int) Math.Min(length, BUFFER_SIZE));                            // takes the data that needs to be moved based on a stream read of the buffer (at offset 0) wich the count is based on the defined length and standard buffer size minimum value

                stream.Position = desOffset;                                                                       // sets the stream position to the destination position
                stream.Write(buffer, 0, data);                                                                     // writes the data to the stream at the defined position

                // augments each offset based on the data value and decrements the length, again, based on the data value
                srcOffset += data;
                desOffset += data;
                length -= data;
            }
        }
     }
 }