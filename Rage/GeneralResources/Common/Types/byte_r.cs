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

namespace SADL.Rage.GeneralResources.Common.Types
{
    /// <summary>
    /// Represents a byte that can be referenced in a resource structure
    /// </summary>
    public class byte_r : ResourceSystemBlock
    {
        /// <summary>
        /// Gets the length of the byte
        /// </summary>
        public override long Length { get { return 1; } }

        /// <summary>
        /// Gets or Sets the byte value
        /// </summary>
        public byte Value { get; set; }

        /// <summary>
        /// Reads a byte value
        /// </summary>
        /// <param name="reader">Reader wich will help for reading the byte</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            Value = reader.ReadByte();
        }

        /// <summary>
        /// Writes a byte value
        /// </summary>
        /// <param name="writer">Writer wich will help for writing the byte</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            writer.Write(Value);
        }

        public static explicit operator byte (byte_r value)
        {
            return value.Value;
        }

        public static explicit operator byte_r(byte value)
        {
            byte_r x = new byte_r();
            x.Value = value;

            return x;
        }
    }
}