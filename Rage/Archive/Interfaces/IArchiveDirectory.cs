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

namespace SADL.Rage.Archive.Interfaces
{
    /// <summary>
    /// <see cref="IArchive"/> defines a interface that aims to declare manipulation methods and objects for a directory of an archive
    /// </summary>
    public interface IArchiveDirectory
    {
        /// <summary>
        /// Getter and Setter for the directory name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Allows to get every internal directory of an archive
        /// </summary>
        /// <returns></returns>
        IArchiveDirectory[] GetInternalDirectories();
        /// <summary>
        /// Allows to get a specified internal directory of an archive
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IArchiveDirectory GetInternalDirectory(string name);

        /// <summary>
        /// Allows to create a directory inside an archvie
        /// </summary>
        /// <returns></returns>
        IArchiveDirectory CreateInternalDirectory();

        /// <summary>
        /// Allows to delete a specified <see cref="IArchiveDirectory"/> from an internal directory
        /// </summary>
        /// <param name="direcotry"></param>
        void DeleteInternalDirectory(IArchiveDirectory direcotry);

        /// <summary>
        /// Allows to get every file inside the directory
        /// </summary>
        /// <returns></returns>
        IArchiveFile[] GetInternalFiles();
        /// <summary>
        /// Allows to get a specified file inside the directory
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IArchiveFile[] GetInternalFile(string name);

        /// <summary>
        /// Allows to get every binary file inside the directory
        /// </summary>
        /// <returns></returns>
        IArchiveBinaryFile CreateInternalBinaryFile();
        /// <summary>
        /// Allows to create a binary file inside the directory
        /// </summary>
        /// <returns></returns>
        IArchiveResourceFile CreateInernalResourceFile();

        /// <summary>
        /// Allows to delete a <see cref="IArchiveFile"/> inside a directory
        /// </summary>
        /// <param name="file"></param>
        void DeleteFile(IArchiveFile file);
    }
}
