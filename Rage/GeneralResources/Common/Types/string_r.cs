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
    /// Represents a string that can be referenced in a resource structure
    /// </summary>
    public class string_r : ResourceSystemBlock
    {
        /// <summary>
        /// Gets the length of the string
        /// </summary>
        public override long Length
        {
            get { return Value.Length + 1; }
        }

        /// <summary>
        /// Gets or Sets the string value
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Reads a string value
        /// </summary>
        /// <param name="reader">Reader wich will help for reading the string</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters) => Value = reader.ReadString(); 

        /// <summary>
        /// Writes a string value
        /// </summary>
        /// <param name="writer">Writer wich will help for writing the string</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters) => writer.Write(Value);
        
        public static explicit operator string(string_r value)
        {
            return value.Value;
        }

        public static explicit operator string_r(string value)
        {
            string_r x = new string_r();
            x.Value = value;

            return x;
        }
    }
}