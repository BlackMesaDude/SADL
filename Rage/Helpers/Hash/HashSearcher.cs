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
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;

using Global = SADL.Rage.Helpers.Constants;

namespace SADL.Rage.Helpers.Hash
{
    public static class HashSearcher
    {
        /// <summary>
        /// Searches an hash key in a stream based on the give alignment and length
        /// </summary>
        /// <param name="stream">Stream wich the check will be applied</param>
        /// <param name="hash">Byte array where the hash key is stored</param>
        /// <param name="aligment">Alligment factor used in the stream</param>
        /// <param name="length">Length of the hash key that needs to be found</param>
        /// <returns>Returns a byte array</return>
        public static byte[] SearchHash(Stream stream, byte[] hash, int alignment = 1, int length = 32)
        {
            return SearchHashes(stream, new List<byte[]> { hash }, alignment, length)[0];
        }

        /// <summary>
        /// Searches an hash key in a stream based on the give alignment and length
        /// </summary>
        /// <param name="stream">Stream wich the check will be applied</param>
        /// <param name="hashes">List of byte array(s) where the hash keys are stored</param>
        /// <param name="aligment">Alligment factor used in the stream</param>
        /// <param name="length">Length of the hash keys that needs to be found</param>
        /// <returns>Returns a bidimensional byte array</return>
        public static byte[][] SearchHashes(Stream stream, IList<byte[]> hashes, int alignment = 1, int length = 32)
        {
            byte[] buf = new byte[stream.Length];                                                                   // defines the buffer of the stream

            stream.Position = 0;                                                                                    // sets stream position to 0
            stream.Read(buf, 0, buf.Length);                                                                        // reads the buffer at offset 0

            byte[][] result = new byte[hashes.Count][];                                                             // defines a byte array that will store the result

            // parallel for loop that will search and compute a hash key
            Parallel.For(0, (int)(stream.Length / Global.HashData.BLOCK_LENGTH), (int k) => {                       
                byte[] tmp = new byte[length];                                                                      // temporary byte array that will store the result

                SHA1CryptoServiceProvider provider = new SHA1CryptoServiceProvider();                               // defines SHA1 function provider
                for (int i = 0; i < (Global.HashData.BLOCK_LENGTH / alignment); i++)
                {
                    int position = k * Global.HashData.BLOCK_LENGTH + i * alignment;                                // defines the new position for the stream

                    // if the position is greater and equal to the stream length then continue
                    if (position >= stream.Length)
                        continue;

                    for (int t = 0; t < length; t++)
                    {
                        // sets the n temp element equal to position + n buffer
                        tmp[t] = buf[position + t];
                    }

                    byte[] hash = provider.ComputeHash(tmp);                                                        // defines the computed hash
                    for (int j = 0; j < hashes.Count; j++)
                        if (hash.SequenceEqual(hashes[j]))
                            result[j] = (byte[])tmp.Clone();                                                        // clones the temporary result and assigns it to the result byte array
                }
            });

            return result;                                                                                          // returns result array
        }
    }
}