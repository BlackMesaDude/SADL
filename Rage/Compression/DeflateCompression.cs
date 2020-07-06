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
using System.IO.Compression;

namespace SADL.Rage.Compression
{
    /// <summary>
    /// Defines the Deflate compression algorithm functions that allow to compress and decompress bytes of data
    /// </summary>
    public class DeflateCompression : Interfaces.ICompression
    {
        /// <summary>
        /// Compresses bytes of data and returns the array of it
        /// </summary>
        /// <param name="data">The data that needs to be compressed</param>
        /// <returns>Returns the compressed data</returns>
        public byte[] Compress(byte[] data)
        {
            MemoryStream memStream = new MemoryStream(data);                                                        // defines a memory stream based on the byte buffer              
            DeflateStream defStream = new DeflateStream(memStream, CompressionMode.Compress);                       // defines the deflate stream based on the memory stream under compress mode

            defStream.Write(data, 0, data.Length);                                                                  // writes the data in the compressed stream at offset 0
            
            byte[] buffer = new byte[memStream.Length];                                                             // creates a new buffer based on the lenght of the memory stream

            memStream.Position = 0;                                                                                 // sets the memory stream position at 0
            memStream.Read(buffer, 0, (int) memStream.Length);                                                      // writes the buffer at offset 0

            defStream.Close();                                                                                      // closes the deflate stream

            return buffer;                                                                                          // returns the compressed buffer
        }

        /// <summary>
        /// Decompresses bytes of data with the specified length and returns the array of it
        /// </summary>
        /// <param name="data">The data that needs to be decompressed</param>
        /// <param name="length">The lenght of the data that needs to be decompressed</param>
        /// <returns>Returns the uncompressed data</returns>
        public byte[] Decompress(byte[] data, int lenght)
        {
            MemoryStream memStream = new MemoryStream(data);                                                        // defines a memory stream based on the byte buffer
            DeflateStream defStream = new DeflateStream(memStream, CompressionMode.Decompress);                     // defines the deflate stream based on the memory stream under decompression

            byte[] buffer = new byte[lenght];                                                                       // defines a new buffer based on the given lenght    

            defStream.Read(buffer, 0, lenght);                                                                      // reads the compressed buffer at offset 0
            defStream.Close();                                                                                      // closes the deflate stream

            return buffer;                                                                                          // returns the uncompressed buffer
        }
    }
}