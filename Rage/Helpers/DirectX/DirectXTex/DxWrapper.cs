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

using System.Runtime.InteropServices;

namespace SADL.Rage.Helpers.DirectX.DirectXTex
{
    /// <summary>
    /// Contains native library methods for compressing and decompressing a texture
    /// </summary>
    public class ImageCompressor 
    {
        /// <summary>
        /// Decompresses a texture based on its data, width, height and format
        /// </summary>
        /// <param name="data">Data to be decompressed</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="format">Format for the decompression</param>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern byte[] Decompress(byte[] data, int width, int height, int format);

        /// <summary>
        /// Compresses a texture based on its data, width, height and format
        /// </summary>
        /// <param name="data">Data to be compressed</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="format">Format for the compression</param>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern byte[] Compress(byte[] data, int width, int height, int format);
    }

    /// <summary>
    /// Contains native library methods for image conversion
    /// </summary>
    public class ImageConverter 
    {
        /// <summary>
        /// Converts a image from x format to y format
        /// </summary>
        /// <param name="data">Data to be converted</param>
        /// <param name="width">Width of the image</param>
        /// <param name="height">Height of the image</param>
        /// <param name="inputFormat">Initial format of the image</param>
        /// <param name="outputFormat">End format of the image</param>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern byte[] Convert(byte[] data, int width, int height, int inputFormat, int outputFormat);
    }

    /// <summary>
    /// Contains native library methods for general purpose image manipulation
    /// </summary>
    public class ImageStruct 
    {
        /// <summary>
        /// Gets the image pitch factor of a row
        /// </summary>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern int GetRowPitch();

        /// <summary>
        /// Gets the image pitch factor of the slice
        /// </summary>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern int GetSlicePitch();
    }

    /// <summary>
    /// Contains native library I\O methods for the DirectDraw Surface format
    /// </summary>
    public class DDSIO
    {
        /// <summary>
        /// Reads a <code>.dds</code> file
        /// </summary>
        /// <param name="fileName">The file to be read</param>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern ImageStruct ReadDDS(string fileName);

        /// <summary>
        /// Writes a image into a <code>.dds</code> file
        /// </summary>
        /// <param name="fileName">The file to be written</param>
        /// <param name="image">Image to be written in the file</param>
        [DllImport(@"Resources\Windows\x64\DirectXTex.dll")]
        public static extern void WriteDDS(string fileName, ImageStruct image);
    }
}