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

using Global = SADL.Rage.Helpers.Constants;

using SADL.Rage.Data.Formatting;
using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources
{
    public class ResourceDataReader : SADL.Rage.Data.DataReader
    {
        private Stream _sysStream, _grpStream;                                                          // defines a stream for the system segment and graphic segment

        // Dictionary that contains every segment readden from this reader
        private Dictionary<long, List<IResourceBlock>> blockPool;

        /// <summary>
        /// Gets the standard lenght of the current stream
        /// </summary>
        public override long Length { get { return -1; } }

        /// <summary>
        /// Gets or sets the position within the current stream
        /// </summary>
        public override long Position { get; set; }

        /// <param name="systemStream">The system stream that takes care of the block segments</param>
        /// <param name="graphicsStream">The graphic stream that takes care of the graphical block segments</param>
        /// <param name="endianess">The byte-order that should be used</param>
        public ResourceDataReader(Stream systemStream, Stream graphicsStream, Endianess endianess = Endianess.LittleEndian) : base((Stream)null, endianess)
        {
            this._sysStream = systemStream;
            this._grpStream = graphicsStream;
            this.blockPool = new Dictionary<long, List<IResourceBlock>>();
        }

        /// <summary>
        /// Reads data from a stream
        /// </summary>
        /// <param name="count">The length of the data that should be read</param>
        /// <param name="ignoreEndianess">Is byte-order enabled?</param>
        protected override byte[] ReadFromStream(int count, bool ignoreEndianess = false)
        {
            // This handles the system segment wich is triggered when the position hits the SYSTEM_BASE index
            if ((Position & Global.SegmentAddress.SYSTEM_SEGMENT) == Global.SegmentAddress.SYSTEM_SEGMENT)
            {
                _sysStream.Position = Position & ~Global.SegmentAddress.SYSTEM_SEGMENT;                 // the position of the stream wich is based on the SYSTEM_BASE

                byte[] buffer = new byte[count];                                                        // buffer wich stores the readden data
                _sysStream.Read(buffer, 0, count);                                                      // reads the data to the buffer at offset 0

                // if endianess is enabled and set to big endian then the buffer will be reversed
                if (!ignoreEndianess && (Endianess == Endianess.BigEndian))
                {
                    Array.Reverse(buffer);
                }

                Position = _sysStream.Position | Global.SegmentAddress.SYSTEM_SEGMENT;                  // resets the stream position

                return buffer;                                                                          // returns the processed buffer
            }

            // this handles the graphical segment wich is triggered when the position hits the GRAPHICS_BASE index
            if ((Position & Global.SegmentAddress.GRAPHIC_SEGMENT) == Global.SegmentAddress.GRAPHIC_SEGMENT)
            {
                _grpStream.Position = Position & ~Global.SegmentAddress.GRAPHIC_SEGMENT;                // the position of the stream wich is based on the GRAPHICS_BASE

                byte[] buffer = new byte[count];                                                        // buffer wich stores the readden data
                _grpStream.Read(buffer, 0, count);                                                      // reads the data to the buffer at offset 0

                // if endianess is enabled and set to big endian then the buffer will be reversed
                if (!ignoreEndianess && (Endianess == Endianess.BigEndian))
                {
                    Array.Reverse(buffer);
                }

                Position = _grpStream.Position | Global.SegmentAddress.GRAPHIC_SEGMENT;                 // resets the stream position

                return buffer;                                                                          // returns the processed buffer
            }
            throw new Exception("Unable to read, the position is invalid!");
        }

        /// <summary>
        /// Reads a single block
        /// </summary>
        /// <param name="parameters">The type of data that should be readden</param>
        public T ReadBlock<T>(params object[] parameters) where T : IResourceBlock, new()
        {
            if (blockPool.ContainsKey(Position))
            {
                var blocks = blockPool[Position];
                for(int i = 0; i < blocks.Count; i++)
                {
                    if(blocks[i] is T)
                    {
                        Position += blocks[i].Length;

                        return (T) blocks[i];
                    }
                }
            }

            T result = new T();

            if (result is IResourceXXSystemBlock)
                result = (T)((IResourceXXSystemBlock)result).GetType(this, parameters);

            if (blockPool.ContainsKey(Position))
            {
                blockPool[Position].Add(result);
            }
            else
            {
                var blocks = new List<IResourceBlock>();
                blocks.Add(result);
                blockPool.Add(Position, blocks);
            }

            long classPosition = Position;            
            result.Read(this, parameters);
            result.Position = classPosition;

            return (T) result;
        }

        public T ReadBlockAt<T>(ulong position, params object[] parameters) where T : IResourceBlock, new()
        {
            if (position != 0)
            {
                var positionBackup = Position;

                Position = (long)position;
                var result = ReadBlock<T>(parameters);
                Position = positionBackup;

                return result;
            }
            else
            {
                return default(T);
            }
        }
    }
}