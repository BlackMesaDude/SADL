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

namespace SADL.Rage.Archive.Interfaces
{
    /// <summary>
    /// Defines methods for a file inside an archive
    /// </summary>
    public interface IArchiveFile
    {
        /// <summary>
        /// Getter and Setter for the file name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Getter for the file size
        /// </summary>
        long Size { get; }

        /// <summary>
        /// Allows to import a file based on its name
        /// </summary>
        /// <param name="name"></param>
        void Import(string name);
        /// <summary>
        /// Allows to import a file based on its bytestream
        /// </summary>
        /// <param name="stream"></param>
        void Import(Stream stream);

        /// <summary>
        /// Allows to export a file based on its file name
        /// </summary>
        /// <param name="name"></param>
        void Export(string name);
        /// <summary>
        /// Allows to export a file based on its file stream
        /// </summary>
        /// <param name="stream"></param>
        void Export(Stream stream);
    }

    /// <summary>
    /// <see cref="IArchive"/> defines a interface that aims to declare manipulation methods and objects for a binary file
    /// </summary>
    public interface IArchiveBinaryFile : IArchiveFile
    {
        /// <summary>
        /// Is the file under encryption?
        /// </summary>
        bool IsUnderEncryption { get; set; }
        /// <summary>
        /// Is the file under compression?
        /// </summary>
        bool IsUnderCompression { get; set; }

        /// <summary>
        /// Defines the compression size of the file if it is actually under compression, of course.
        /// </summary>
        long CompressedSize { get; }

        /// <summary>
        /// Allows to get the file stream
        /// </summary>
        /// <returns></returns>
        Stream GetStream();
    }

    public interface IArchiveResourceFile : IArchiveFile 
    {
        /// TODO: needs interface declaration
    }
}