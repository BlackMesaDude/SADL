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
    /// Represents a ushort that can be referenced in a resource structure
    /// </summary>
    public class ushort_r : ResourceSystemBlock
    {
        /// <summary>
        /// Gets the length of the ushort
        /// </summary>
        public override long Length { get { return 2; } }

        /// <summary>
        /// Gets or Sets the ushort value
        /// </summary>
        public ushort Value { get; set; }

        /// <summary>
        /// Reads a ushort value
        /// </summary>
        /// <param name="reader">Reader wich will help for reading the ushort</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters) => Value = reader.ReadUInt16();

        /// <summary>
        /// Writes a ushort value
        /// </summary>
        /// <param name="writer">Writer wich will help for writing the ushort</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters) => writer.Write(Value);
        
        public static explicit operator ushort(ushort_r value)
        {
            return value.Value;
        }

        public static explicit operator ushort_r(ushort value)
        {
            ushort_r x = new ushort_r();
            x.Value = value;

            return x;
        }
    }
}