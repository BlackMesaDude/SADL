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

using System.IO;

namespace SADL.Rage.GeneralResources.Interfaces
{
    /// <summary>
    /// Defines manipulation methods for a resource file
    /// </summary>
    public interface IResourceFile
    {
        /// <summary>
        /// Gets or Sets the system segment data
        /// </summary>
        byte[] SystemData { get; set; }
        /// <summary>
        /// Gets or Sets the graphical segment data
        /// </summary>
        byte[] GraphicsData { get; set; }
        /// <summary>
        /// Gets or Sets the version of the file
        /// </summary>
        int Version { get; set; }

        /// <summary>
        /// Loads a resource file
        /// </summary>
        /// <param name="stream">The stream that needs to be loaded</param>
        void Load(Stream stream);
        /// <summary>
        /// Loads a resource file
        /// </summary>
        /// <param name="fileName">The file that needs to be loaded</param>
        void Load(string fileName); 
        /// <summary>
        /// Saves a resource file
        /// </summary>
        /// <param name="stream">The stream that needs to be saved</param>
        void Save(Stream stream);
        /// <summary>
        /// Saves a resource file
        /// </summary>
        /// <param name="fileName">The file that needs to be saved</param>
        void Save(string fileName);
    }

    /// <summary>
    /// Defines a list of resource files of <see cref="IResourceBlock" />
    /// </summary>
    public interface IResourceFile<T> : IResourceFile where T : IResourceBlock, new()
    {
        /// <summary>
        /// Gets or Sets the <see cref="IResourceBlock" />
        /// </summary>
        T ResourceData { get; set; }
    }
}