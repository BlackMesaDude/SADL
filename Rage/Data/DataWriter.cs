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

using SADL.Rage.Data.Formatting;

namespace SADL.Rage.Data
{
    /// <summary>
    /// Defines a set of functions for data writing purposes
    /// </summary>
    public class DataWriter
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
        public DataWriter(Stream stream, Endianess endianess = Endianess.LittleEndian)
        {
            this._stream = stream;
            this.Endianess = endianess;
        }

        /// <summary>
        /// Writes a single buffer into a stream
        /// </summary>
        /// <param name="value">The buffer for the stream</param>
        /// <param name="ignoreEndianess">Does it use endianess byte-order?</param>
        protected virtual void WriteToStream(byte[] value, bool ignoreEndianess = false)
        {
            // if endianess byte-order is used and it is set to BigEndian then clone the buffer, reverse it and write it back otherwise write the defined buffer
            if (!ignoreEndianess && (Endianess == Endianess.BigEndian))
            {
                byte[] buffer = (byte[]) value.Clone();

                Array.Reverse(buffer);
                _stream.Write(buffer, 0, buffer.Length);
            }
            else
            {
                _stream.Write(value, 0, value.Length);
            }
        }
        
        /// <summary>
        /// Writes a byte
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(byte value)
        {
            WriteToStream(new byte[] { value });
        }

        /// <summary>
        /// Writes a sequence of bytes
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(byte[] value)
        {
            WriteToStream(value, true);
        }

        /// <summary>
        /// Writes a signed 16-bit value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(short value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a signed 32-bit value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(int value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a signed 64-bit value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(long value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an unsigned 16-bit value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(ushort value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an unsigned 32-bit value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(uint value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes an unsigned 64-bit value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(ulong value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a single precision floating point value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(float value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a double precision floating point value
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(double value)
        {
            WriteToStream(BitConverter.GetBytes(value));
        }

        /// <summary>
        /// Writes a string
        /// </summary>
        /// <param name="value">The value that needs to be written</param>
        public void Write(string value)
        {
            /// TODO: needs to be optimized
            foreach (var c in value)
                Write((byte)c);
            Write((byte)0);
        }
    }
}