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

namespace SADL.Rage.Archive.Interfaces
{
    /// <summary>
    /// <see cref="IArchive"/> defines a interface that aims to declare manipulation methods and objects for an archive
    /// </summary>
    interface IArchive : IDisposable
    {
        /// <summary>
        /// Getter for the external root archive.
        /// </summary>
        IArchiveDirectory ExternalRoot { get; }

        /// <summary>
        /// Method for buffer clearance, this allows to clear every buffer for the specified archive wich causes data under the buffer to be writter to the device.
        /// </summary>
        void FlushDirectory();
    }
}
