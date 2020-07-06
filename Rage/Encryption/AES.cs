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

using SADL.Rage.Encryption.Interfaces;
using System.Security.Cryptography;

namespace SADL.Rage.Encryption
{
    /// <summary>
    /// Defines a decription and decription solution for the AES algorithm used by RAGE.
    /// </summary>
    public class AES : IEncryption
    {
        /// <summary>
        /// Getter and Setter for the key
        /// </summary>
        public byte[] Key { get; set; }
        /// <summary>
        /// Getter and Setter for the current round
        /// </summary>
        public int Rounds { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="key">The buffer that is storing the key</param>
        /// <param name="round">The total of rounds to elaborate the key</param>
        public AES(byte[] key, int round = 1)
        {
            this.Key = key;
            this.Rounds = round;
        }

        /// <summary>
        /// Allows to decrypt AES with a buffer, key and rounds
        /// </summary>
        /// <param name="data">Bytestream that stores the data</param>
        /// <param name="key">Buffer that stores the AEB key</param>
        /// <param name="round">Total rounds to be done untile decryption</param>
        /// <returns></returns>
        public byte[] Decrypt(byte[] data, byte[] key, int round = 1)
        {
            Rijndael rijndael = Rijndael.Create();                                                              // creates Rijndael symmetric algorithm solution

            rijndael.KeySize = 256;                                                                             // sets the size of the key at 256 bits
            rijndael.Key = key;                                                                                 // sets the key as the given buffer

            rijndael.BlockSize = 128;                                                                           // sets the default block size at 128 bits

            rijndael.Mode = CipherMode.ECB;                                                                     // sets decryption mode to ECB wich decrypts and encrypts each block individually
            rijndael.Padding = PaddingMode.None;                                                                // sets padding to none because we dont need padding of the block

            byte[] buffer = (byte[]) data.Clone();                                                              // clones the bytestream to another buffer
            var lenght = data.Length - data.Length % 16;                                                        // defines the lenght of the data

            // if lenght is greater than 0 then decrypt
            if(lenght > 0)
            {
                var decryptor = rijndael.CreateDecryptor();                                                     // creates a Decryptor
                for(int i = 0; i < round; i++)                                                                                  
                {
                    decryptor.TransformBlock(buffer, 0, lenght, buffer, 0);                                     // using the created decryptor a transformation of the block will be done at offsets 0 I\O
                }
            }

            return buffer;                                                                                      // returns the processed buffer
        }

        /// <summary>
        /// Allows to encrypt AES with a buffer, key and rounds
        /// </summary>
        /// <param name="data">Bytestream that stores the data</param>
        /// <param name="key">Buffer that stores the AEB key</param>
        /// <param name="round">Total rounds to be done untile encryption</param>
        /// <returns></returns>
        public byte[] Encrypt(byte[] data, byte[] key, int round = 1)
        {
            Rijndael rijndael = Rijndael.Create();                                                              // creates Rijndael symmetric algorithm solution

            rijndael.KeySize = 256;                                                                             // sets the size of the key at 256 bits
            rijndael.Key = key;                                                                                 // sets the key as the given buffer                                  

            rijndael.BlockSize = 128;                                                                           // sets the default block size at 128 bits

            rijndael.Mode = CipherMode.ECB;                                                                     // sets decryption mode to ECB wich decrypts and encrypts each block individually
            rijndael.Padding = PaddingMode.None;                                                                // sets padding to none because we dont need padding of the block

            byte[] buffer = (byte[])data.Clone();                                                               // clones the bytestream to another buffer
            var lenght = data.Length - data.Length % 16;                                                        // defines the lenght of the data

            // if lenght is greater than 0 then encrypt
            if (lenght > 0)
            {
                var encryptor = rijndael.CreateEncryptor();                                                     // creates Encryptor
                for (int i = 0; i < round; i++)
                {
                    encryptor.TransformBlock(buffer, 0, lenght, buffer, 0);                                     // using the created encryptor a transformation of the block will be done at offsets 0 I\O
                }
            }

            return buffer;                                                                                      // returns processed buffer
        }
    }
}
