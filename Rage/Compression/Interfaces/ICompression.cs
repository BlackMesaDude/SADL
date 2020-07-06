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

namespace SADL.Rage.Compression.Interfaces
{
    /// <summary>
    /// Defines methods for the compression algorithm
    /// </summary>
    public interface ICompression 
    {
        /// <summary>
        /// Defines a method for data compression
        /// </summary>
        /// <param name="data">The bytes that need to be compressed</param>
        /// <returns>Returns compressed bytes</returns>
        byte[] Compress(byte[] data);

        /// <summary>
        /// Defines a method for data decompression
        /// </summary>
        /// <param name="data">The bytes that need to be uncompressed</param>
        /// <param name="length">The length of the data that needs to be uncompressed</param>
        /// <returns>Returns uncompressed bytes based on the given length</returns>
        byte[] Decompress(byte[] data, int length);
    }
}