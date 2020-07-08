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

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources
{
    /// <summary>
    /// Defines abstract methods for a resource block that resides in the system memory segment
    /// </summary>
    public abstract class ResourceSystemBlock : IResourceSystemBlock
    {
        private long position;

        /// <summary>
        /// Gets or sets the position of the data block
        /// </summary>
        public virtual long Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                for(int i = 0; i < GetParts().Length; i++)
                {
                    GetParts()[i].Item2.Position = value + GetParts()[i].Item1;
                }
            }
        }

        /// <summary>
        /// Gets the length of the data block
        /// </summary>
        public abstract long Length { get; }

        /// <summary>
        /// Reads the data block
        /// </summary>
        public abstract void Read(ResourceDataReader reader, params object[] parameters);

        /// <summary>
        /// Writes the data block
        /// </summary>
        public abstract void Write(ResourceDataWriter writer, params object[] parameters);

        /// <summary>
        /// Returns a list of data blocks that are part of this block
        /// </summary>
        public virtual Tuple<long, IResourceBlock>[] GetParts() { return new Tuple<long, IResourceBlock>[0]; }

        /// <summary>
        /// Returns the list of data blocks that are referenced by this block
        /// </summary>
        public virtual IResourceBlock[] GetReferences() { return new IResourceBlock[0]; }
    }

    /// <summary>
    /// Represents the type of a data block of the system segment in a resource file
    /// </summary>
    public abstract class ResourecTypedSystemBlock : ResourceSystemBlock, IResourceXXSystemBlock
    {
        /// <summary>
        /// Gets the type of the system block
        /// </summary>
        /// <param name="reader">The reader wich will help for reading the block</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public abstract IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters);
    }

    /// <summary>
    /// Represents a data block of the graphics segmenet in a resource file
    /// </summary>
    public abstract class ResourceGraphicsBlock : IResourceGraphicsBlock
    {
        /// <summary>
        /// Gets or sets the position of the data block
        /// </summary>
        public virtual long Position { get; set; }

        /// <summary>
        /// Gets the length of the data block
        /// </summary>
        public abstract long Length { get; }

        /// <summary>
        /// Reads a block
        /// </summary>
        /// <param name="reader">The reader wich will help for reading the block</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public abstract void Read(ResourceDataReader reader, params object[] parameters);

        /// <summary>
        /// Writes a block
        /// </summary>
        /// <param name="writer">The writer wich will help for writing the block</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public abstract void Write(ResourceDataWriter writer, params object[] parameters);
    }
}