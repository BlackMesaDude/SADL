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
using System.Text;
using System.Collections.Generic;

using SADL.Rage.Data.Formatting;

namespace SADL.Rage.Data
{
    /// <summary>
    /// Defines a set of functions for data reading purposes
    /// </summary>
    public class DataReader
    {
        private Stream _stream;
        
        /// <summary>
        /// Gets or sets the endianess of the underlying stream
        /// </summary>
        public Endianess Endianess { get; set; }

        /// <summary>
        /// Gets the length of the underlying stream
        /// </summary>
        public virtual long Length { get { return _stream.Length; } }

        /// <summary>
        /// Gets or sets the position within the underlying stream
        /// </summary>
        public virtual long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        /// <param name="stream"> The target stream </param>
        /// <param name="endianess"> The type of byte-order </param>
        public DataReader(Stream stream, Endianess endianess = Endianess.LittleEndian)
        {
            this._stream = stream;
            this.Endianess = endianess;
        }

        /// <summary>
        /// Reads a single buffer from a stream
        /// </summary>
        /// <param name="count">The lenght of the buffer</param>
        /// <param name="ignoreEndianess">Does it use endianess byte-order?</param>
        protected virtual byte[] ReadFromStream(int count, bool ignoreEndianess = false)
        {
            byte[] buffer = new byte[count];                                                            // defines the buffer that will be taken from the stream
            _stream.Read(buffer, 0, count);                                                             // reads the buffer at offset 0 with the specified length

            // if endianess is enabled and its using BigEndian then reverse the buffer
            if (!ignoreEndianess && (Endianess == Endianess.BigEndian))
            {
                Array.Reverse(buffer);
            }

            return buffer;                                                                              // returns the taken buffer
        }

        /// <summary>
        /// Reads a single byte
        /// </summary>
        public byte ReadByte()
        {
            return ReadFromStream(1)[0];
        }

        /// <summary>
        /// Reads a sequence of bytes
        /// </summary>
        /// <param name="count">The value that needs to be read</param>
        public byte[] ReadBytes(int count)
        {
            return ReadFromStream(count, true);
        }

        /// <summary>
        /// Reads a signed 16-bit value
        /// </summary>
        public short ReadInt16()
        {
            return BitConverter.ToInt16(ReadFromStream(2), 0);
        }

        /// <summary>
        /// Reads a signed 32-bit value
        /// </summary>
        public int ReadInt32()
        {
            return BitConverter.ToInt32(ReadFromStream(4), 0);
        }

        /// <summary>
        /// Reads a signed 64-bit value
        /// </summary>
        public long ReadInt64()
        {
            return BitConverter.ToInt64(ReadFromStream(8), 0);
        }

        /// <summary>
        /// Reads an unsigned 16-bit value
        /// </summary>
        public ushort ReadUInt16()
        {
            return BitConverter.ToUInt16(ReadFromStream(2), 0);
        }

        /// <summary>
        /// Reads an unsigned 32-bit value
        /// </summary>
        public uint ReadUInt32()
        {
            return BitConverter.ToUInt32(ReadFromStream(4), 0);
        }

        /// <summary>
        /// Reads an unsigned 64-bit value
        /// </summary>
        public ulong ReadUInt64()
        {
            return BitConverter.ToUInt64(ReadFromStream(8), 0);
        }

        /// <summary>
        /// Reads a single precision floating point value
        /// </summary>
        public float ReadSingle()
        {
            return BitConverter.ToSingle(ReadFromStream(4), 0);
        }

        /// <summary>
        /// Reads a double precision floating point value
        /// </summary>
        public double ReadDouble()
        {
            return BitConverter.ToDouble(ReadFromStream(8), 0);
        }

        /// <summary>
        /// Reads a string
        /// </summary>
        public string ReadString()
        {
            var bytes = new List<byte>();                                                               // a list of bytes that depend on the string                                                
            var temp = ReadFromStream(1)[0];                                                            // reads the buffer from the stream
            while (temp != 0)
            {
                bytes.Add(temp);                                                                        // adds the temporary readden buffer to the byte list
                temp = ReadFromStream(1)[0];                                                            // reads the next buffer
            }

            return Encoding.UTF8.GetString(bytes.ToArray());                                            // returns the string under UTF-8 encoding
        }
    }
}