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

namespace SADL.Rage.Helpers 
{
    /// <summary>
    /// Contains costant variables wich will be used elsewhere for an easy organization
    /// </summary>
    public static class Constants 
    {
        /// <summary>
        /// Defines constant values used by the defined buffers
        /// </summary>
        public readonly ref struct BufferData 
        {
            /// <summary>
            /// Standard size of a single buffer
            /// </summary>
            public const int BUFFER_SIZE = 16384;
        }

        /// <summary>
        /// Defines hex address for each, known, memory segment
        /// </summary>
        public readonly ref struct SegmentAddress
        {
            /// <summary>
            /// Relative address for the system memory segment
            /// </summary>
            public const long SYSTEM_SEGMENT = 0x50000000;
            /// <summary>
            /// Relative address for the graphical memory segment
            /// </summary>
            public const long GRAPHIC_SEGMENT = 0x60000000;
        }

        /// <summary>
        /// Defines the size values for a resource
        /// </summary>
        public readonly ref struct ResourceSize 
        {
            /// <summary>
            /// The size used to skip a certain length in a memory block that resides in a resource file
            /// </summary>
            public const int SKIP_SIZE = 64;
            /// <summary>
            /// The alignment size used to align a certain data with the memory block that resides in a resource file
            /// </summary>
            public const int ALIGN_SIZE = 64;
        }

        /// <summary>
        /// Defines paging values used to access resource files
        /// </summary>
        public readonly ref struct PagingValues 
        {
            /// <summary>
            /// The default size of a page
            /// </summary>
            public const long DEFAULT_PAGE_SIZE = 0x2000;
            /// <summary>
            /// The maximum count of pages
            /// </summary>
            public const int MAXIMUM_PAGE_COUNT = 128;
        }

        /// <summary>
        /// Defines the size values for a resource
        /// </summary>
        public readonly ref struct EncryptionData 
        {
            /// <summary>
            /// Standard key size
            /// </summary>
            public const int KEY_SIZE = 256;
            /// <summary>
            /// Standard block size
            /// </summary>
            public const int BLOCK_SIZE = 128;

            /// <summary>
            /// Defines the chiper mode used by the encryption algorithm
            /// </summary>
            public const System.Security.Cryptography.CipherMode CHIPER_MODE = System.Security.Cryptography.CipherMode.ECB;
            /// <summary>
            /// Defines the padding mode used by the encryption algorithm
            /// </summary>
            public const System.Security.Cryptography.PaddingMode PADDING_MODE = System.Security.Cryptography.PaddingMode.None;
        }

        /// <summary>
        /// Defines the values used by hashing functions
        /// </summary>
        public readonly ref struct HashData
        {
            /// <summary>
            /// Default block length of an hash key
            /// </summary>
            public const int BLOCK_LENGTH = 1048576;
        }
    }
}