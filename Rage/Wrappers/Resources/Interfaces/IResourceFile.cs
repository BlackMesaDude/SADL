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

namespace SADL.Rage.Wrappers.Resources.Interfaces
{
    /// <summary>
    /// Represents a resource file
    /// </summary>
    public interface IResourceFile
    {
        /// <summary>
        /// Loads the resource from a file
        /// </summary>
        /// <param name="fileName">Path to the file</param>
        void Load(string fileName);

        /// <summary>
        /// Loads the resource from a stream
        /// </summary>
        /// <param name="stream">Target stream where the resource needs to be loaded</param>
        void Load(Stream stream);

        /// <summary>
        /// Saves the resource to a file
        /// </summary>
        /// <param name="fileName">Path to the file</param>
        void Save(string fileName);

        /// <summary>
        /// Saves the resource to a stream
        /// </summary>
        /// <param name="stream">Target stream where the resource needs to be saved</param>
        void Save(Stream stream);
    }
}