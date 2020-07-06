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
using System.Collections.Generic;
using System.Text;

namespace SADL.Rage.Encryption.Interfaces
{
    /// <summary>
    /// Defines a interface that aims to implement encryption and decription methods
    /// </summary>
    public interface IEncryption
    {
        /// <summary>
        /// Allows to decrypt a bytestream
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        byte[] Decrypt(byte[] data, byte[] key, int round = 1);

        /// <summary>
        /// Allows to encrypt a bytestream
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        byte[] Encrypt(byte[] data, byte[] key, int round = 1);
    }
}
