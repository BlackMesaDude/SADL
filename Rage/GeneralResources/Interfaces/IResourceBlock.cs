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

namespace SADL.Rage.GeneralResources.Interfaces
{
    /// <summary>
    /// Contains definitions for a datablock that resides in the resource files
    /// </summary>
    public interface IResourceBlock 
    {
        /// <summary>
        /// Position of the <see cref="DataBlock" />
        /// </summary>
        long Position { get; set; }
        /// <summary>
        /// Length of the <see cref="DataBlock" />
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Reads specified data that resides in the resource file
        /// </summary>
        /// <param name="reader">The resource data reader</param>
        /// <param name="parameters">Defines the data that needs to be read</param>
        void Read(ResourceDataReader reader, params object[] parameters);
        /// <summary>
        /// Writes specified data that resides in the resource file
        /// </summary>
        /// <param name="reader">The resource data writer</param>
        /// <param name="parameters">Defines the data that needs to be written</param>
        void Write(ResourceDataWriter writer, params object[] parameters);
    }

    /// <summary>
    /// Contains definitions for a datablock segment that resides in the resource files
    /// </summary>
    public interface IResourceSystemBlock : IResourceBlock
    {
        /// <summary>
        /// Returns a list of DataBlocks that are a part of this segment
        /// </summary>
        Tuple<long, IResourceBlock>[] GetParts();

        /// <summary>
        /// Returns each DataBlock that are referenced by this block
        /// </summary>
        IResourceBlock[] GetReferences();
    }

    /// <summary>
    /// Contains definitions for a datablock segment type
    /// </summary>
    public interface IResourceXXSystemBlock : IResourceSystemBlock
    {
        IResourceSystemBlock GetType(ResourceDataReader reader, params object[] parameters);
    }

    /// <summary>
    /// Contains definitions for a datablock that resides in the graphical resource files
    /// </summary>
    public interface IResourceGraphicsBlock : IResourceBlock
    { 
        // TODO: Needs to be implemented 
    }
}