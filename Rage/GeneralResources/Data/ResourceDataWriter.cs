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

using SADL.Rage.Data.Formatting;
using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources
{
    public class ResourceDataWriter : SADL.Rage.Data.DataWriter
    {
        private const long SYSTEM_BASE = 0x50000000;                                                // standard system segment base index                                                        
        private const long GRAPHICS_BASE = 0x60000000;                                              // standard graphical segment base index

        private Stream _sysStream, _grpStream;                                                      // defines a stream for the system segment and graphic segment

        /// <summary>
        /// Gets the last length of the writer
        /// <summary>
        public override long Length { get { return -1; } }

        /// <summary>
        /// Gets or Sets the position of the writer
        /// <summary>
        public override long Position { get; set; }

        /// <param name="systemStream">The system stream that takes care of the block segments</param>
        /// <param name="graphicStream">The graphic stream that takes care of the graphical block segments</param>
        /// <param name="endianess">The byte-order that should be used</param>
        public ResourceDataWriter(Stream systemStream, Stream graphicsStream, Endianess endianess = Endianess.LittleEndian) : base((Stream)null, endianess)
        {
            this._sysStream = systemStream;
            this._grpStream = graphicsStream;
        }

        /// <summary>
        /// Writes data to a stream
        /// </summary>
        /// <param name="value">The data that should be written</param>
        /// <param name="ignoreEndianess">Is byte-order enabled?</param>
        protected override void WriteToStream(byte[] value, bool ignoreEndianess = true)
        {
            // This handles the system segment wich is triggered when the position hits the SYSTEM_BASE index
            if ((Position & SYSTEM_BASE) == SYSTEM_BASE)
            {
                _sysStream.Position = Position & ~SYSTEM_BASE;                                          // the position of the stream wich is based on the SYSTEM_BASE

                // if endianess is enabled and set to big endian then the buffer will be cloned and reversed to avoid any problems with the original source
                if (!ignoreEndianess && (Endianess == Endianess.BigEndian))
                {
                    byte[] buffer = (byte[]) value.Clone();                                             // clones the given buffer to another
                    Array.Reverse(buffer);                                                              // reverses the cloned buffer
                    _sysStream.Write(buffer, 0, buffer.Length);                                         // writes the cloned buffer
                }
                else
                {
                    _sysStream.Write(value, 0, value.Length);                                           // writes the given buffer
                }

                Position = _sysStream.Position | SYSTEM_BASE;                                           // resets the stream position                                        
            }

            // this handles the graphical segment wich is triggered when the position hits the GRAPHICS_BASE index
            if ((Position & GRAPHICS_BASE) == GRAPHICS_BASE)
            {
                _grpStream.Position = Position & ~GRAPHICS_BASE;                                        // the position of the stream wich is based on the GRAPHICS_BASE

                // if endianess is enabled and set to big endian then the buffer will be cloned and reversed to avoid any problems with the original source
                if (!ignoreEndianess && (Endianess == Endianess.BigEndian))
                {
                    byte[] buffer = (byte[]) value.Clone();                                             // clones the given buffer to another
                    Array.Reverse(buffer);                                                              // reverses the cloned buffer
                    _sysStream.Write(buffer, 0, buffer.Length);                                         // writes the cloned buffer
                }
                else
                {
                    _sysStream.Write(value, 0, value.Length);                                           // writes the given buffer
                }

                Position = _grpStream.Position | GRAPHICS_BASE;                                         // resets the stream position
            }

            throw new Exception("Unable to write, the position is invalid!");
        }

        /// <summary>
        /// Writes a single block
        /// </summary>
        /// <param name="vakue">The block that should be written</param>
        public void WriteBlock(IResourceBlock value)
        {
            value.Write(this);
        }
    }
}