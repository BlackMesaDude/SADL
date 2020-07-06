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

namespace SADL.Rage.Data.Streams
{
    /// <summary>
    /// Delegate that gets the offset of the buffer
    /// </summary>
    public delegate long GetOffsetDelegate();
    /// <summary>
    /// Delegate that gets the length of the buffer
    /// </summary>
    public delegate long GetLengthDelegate();
    /// <summary>
    /// Delegate that sets the length of the buffer
    /// </summary>
    /// <param name="length">The new length for the buffer</param>
    public delegate void SetLengthDelegate(long length);

    /// <summary>
    /// Defines a partial stream that allows to write and read buffers with 'custom' delegates
    /// </summary>
    public class PartialStream : Stream 
    {
        private Stream _stream;

        private GetOffsetDelegate getOffsetDelegate;
        private GetLengthDelegate getLengthDelegate;
        private SetLengthDelegate setLengthDelegate;

        private long _posRelative;

        /// <summary>
        /// Gets a value indicating whether the stream supports seeking. 
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the stream supports reading. 
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return _stream.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the stream supports writing. 
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return _stream.CanWrite;
            }
        }

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        public override long Length
        {
            get
            {
                return getLengthDelegate();
            }
        }

        /// <summary>
        /// Gets or sets the position within the stream.
        /// </summary>
        public override long Position
        {
            get
            {
                return _posRelative;
            }
            set
            {
                if (Position > Length)
                    SetLength(Position);                
                _posRelative = value;
            }
        }

        /// <param name="stream">Target stream</param>
        /// <param name="getOffset">Offset delegate</param>
        /// <param name="getLength">Length delegate</param>
        /// <param name="setLength">Length setter delegate</param>
        public PartialStream(Stream stream, GetOffsetDelegate getOffset, GetLengthDelegate getLength, SetLengthDelegate setLength)
        {
            this._stream = stream;

            this.getOffsetDelegate = getOffset;
            this.getLengthDelegate = getLength;
            this.setLengthDelegate = setLength;
        }

        /// <summary>
        /// Reads a buffer inside the stream
        /// </summary>
        /// <param name="buffer">Buffer that needs to be readden</param>
        /// <param name="offset">Offset where the buffer needs to be readden</param>
        /// <param name="count">Length of the buffer that needs to be readden</param>
        /// <returns>Returns the readden buffer</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            long origin = _stream.Position;                                                             // defines the original position

            int maxCount = (int) (getLengthDelegate() - _posRelative);                                  // defines the max length 
            int newcount = Math.Min(count, maxCount);                                                   // defines the new length

            _stream.Position = getOffsetDelegate() + _posRelative;                                      // sets the stream position based on the relative position and the delegate

            int data = _stream.Read(buffer, offset, newcount);                                          // reads the data at the specified offset and length
            _posRelative += data;                                                                       // augments the relative position based on the data

            _stream.Position = origin;                                                                  // resets the stream position

            return data;                                                                                // returns the readdend data
        }

        /// <summary>
        /// Writes a buffer inside the stream
        /// </summary>
        /// <param name="buffer">Buffer that needs to be written</param>
        /// <param name="offset">Offset where the buffer needs to be written</param>
        /// <param name="count">Length of the buffer that needs to be written</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            var origin = _stream.Position;                                                              // defines the original position

            long newlen = _posRelative + count;                                                         // defines the new lenght
            // if the lenght is greater than the original lenght then use the delegate
            if (newlen > Length)
                setLengthDelegate(newlen);

            int maxCount = (int)(getLengthDelegate() - _posRelative);                                   // defines the max length
            int newcount = Math.Min(count, maxCount);                                                   // defines the new length

            _stream.Position = getOffsetDelegate() + _posRelative;                                      // sets the stream position based on the relative position and the delegate

            _stream.Write(buffer, offset, count);                                                       // writes the buffer at the given offset and length
            _posRelative += count;                                                                      // augments the relative position based on the length

            _stream.Position = origin;                                                                  // resets the stream position
        }

        /// <summary>
        /// Finds the position of the stream based on a offset and origin
        /// </summary>
        /// <param name="offset">Offset value of the new position</param>
        /// <param name="origin">Origin of the position</param>
        /// <returns>Returns the position value</param>
        public override long Seek(long offset, SeekOrigin origin)
        {
            // checks each origin type (from start, current, to end) and defines the relative position
            switch (origin)
            {
                case SeekOrigin.Begin:
                {
                    _posRelative = offset;
                    break;
                }
                case SeekOrigin.Current:
                {
                    _posRelative += offset;
                    break;
                }
                case SeekOrigin.End:
                {
                    _posRelative = getLengthDelegate() + offset;
                    break;
                }
            }

            return _posRelative;
        }

        /// <summary>
        /// Sets the legth of the stream
        /// </summary>
        /// <param name="value">Lenght of the stream</param>
        public override void SetLength(long value)
        {
            setLengthDelegate(value);
        }

        /// <summary>
        /// Flushesh the data that is contained in the stream
        /// </summary>
        public override void Flush()
        {
            _stream.Flush();
        }
    }
}