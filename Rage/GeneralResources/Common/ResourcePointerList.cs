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

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources.Common
{
    /// <summary>
    /// Represents a pointer array of <see cref="ResourceSystemBlock" />
    /// </summary>
    public class ResourcePointerList<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        /// <summary>
        /// Gets the lenght of the pointer list
        /// </summary>
        public override long Length { get { return 8; } }

        /// <summary>
        /// Pointer id
        /// </summary>
        public uint DataPointer;
        /// <summary>
        /// First pointer data and second pointer data
        /// </summary>
        public ushort first, second;

        /// <summary>
        /// Items contained in the list
        /// </summary>
        public ResourcePointerArray<T> data_items;
        
        /// <summary>
        /// Reads the pointer item
        /// </summary>
        /// <param name="reader">The reader wich will help for reading the pointer</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            this.DataPointer = reader.ReadUInt32();
            this.first       = reader.ReadUInt16();
            this.second      = reader.ReadUInt16();

            this.data_items = reader.ReadBlockAt<ResourcePointerArray<T>>(
                this.DataPointer, 
                this.first
            );
        }

        /// <summary>
        /// Writes the pointer item
        /// </summary>
        /// <param name="reader">The writer wich will help for writing the pointer</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            this.DataPointer = (uint)   data_items.Position;
            this.first       = (ushort) data_items.Count;
            this.second      = (ushort) data_items.Count;

            writer.Write(DataPointer);
            writer.Write(first);
            writer.Write(second);
        }
    }   
}