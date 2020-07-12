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
    /// Helper for directx texture conversion
    /// </summary>
    public static class TextureConvert
    {
        /// <summary>
        /// Converts data from A8 format to RGBA
        /// </summary>
        /// <param name="data">Data of the image</param>
        /// <param name="width">Width of the image</param>
        /// <param name="heigth">Height of the image</param>
        public static byte[] MakeRGBAFromA8(byte[] data, int width, int height)
        {
            return DirectX.DirectXTex.ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_A8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        /// <summary>
        /// Converts data from A8B8G8R8 format to RGBA
        /// </summary>
        /// <param name="data">Data of the image</param>
        /// <param name="width">Width of the image</param>
        /// <param name="heigth">Height of the image</param>
        public static byte[] MakeRGBAFromA8B8G8R8(byte[] data, int width, int height)
        {
            return DirectX.DirectXTex.ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        /// <summary>
        /// Converts data from A8R8G8B8 format to RGBA
        /// </summary>
        /// <param name="data">Data of the image</param>
        /// <param name="width">Width of the image</param>
        /// <param name="heigth">Height of the image</param>
        public static byte[] MakeRGBAFromA8R8G8B8(byte[] data, int width, int height)
        {
            return DirectX.DirectXTex.ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        /// <summary>
        /// Converts data from L8 format to ARGB
        /// </summary>
        /// <param name="data">Data of the image</param>
        /// <param name="width">Width of the image</param>
        /// <param name="heigth">Height of the image</param>
        public static byte[] MakeARGBFromL8(byte[] data, int width, int height)
        {
            return DirectX.DirectXTex.ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_R8_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }

        /// <summary>
        /// Converts data from A1R5G5B5 format to ARGB
        /// </summary>
        /// <param name="data">Data of the image</param>
        /// <param name="width">Width of the image</param>
        /// <param name="heigth">Height of the image</param>
        public static byte[] MakeARGBFromA1R5G5B5(byte[] data, int width, int height)
        {
            return DirectX.DirectXTex.ImageConverter.Convert(data, width, height, (int)DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM, (int)DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM);
        }
    }
}