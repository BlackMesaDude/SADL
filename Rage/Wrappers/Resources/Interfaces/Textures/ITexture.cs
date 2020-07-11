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

using System.Collections.Generic;

namespace SADL.Rage.Wrappers.Resources.Interfaces.Textures
{
    /// <summary>
    /// Defines available texture formats
    /// </summary>
    public enum TextureFormat
    {
        D3DFMT_A8R8G8B8 = 21,
        D3DFMT_A1R5G5B5 = 25,
        D3DFMT_A8 = 28,
        D3DFMT_A8B8G8R8 = 32,
        D3DFMT_L8 = 50,

        // fourCC
        D3DFMT_DXT1 = 0x31545844,
        D3DFMT_DXT3 = 0x33545844,
        D3DFMT_DXT5 = 0x35545844,
        D3DFMT_ATI1 = 0x31495441,
        D3DFMT_ATI2 = 0x32495441,
        D3DFMT_BC7 = 0x20374342,

        UNKNOWN
    }

    /// <summary>
    /// Represents a texture list
    /// </summary>
    public interface ITextureList : IList<ITexture> { }

    /// <summary>
    /// Represents a texture
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Gets or sets the name of the texture
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the width of the texture
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the texture
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the number of mipmaps of the texture
        /// </summary>
        int MipMapLevels { get; }

        /// <summary>
        /// Gets the compression format of the texture
        /// </summary>
        TextureFormat Format { get; }

        /// <summary>
        /// Factor that defines padding and memory layout
        /// </summary>
        int Stride { get; }

        /// <summary>
        /// Gets the texture data
        /// </summary>
        byte[] GetTextureData();

        /// <summary>
        /// Gets the texture data of the specified MipMap level
        /// </summary>
        /// <param name="mipMapLevel">MipMap level of the texture</param>
        byte[] GetTextureData(int mipMapLevel);

        /// <summary>
        /// Sets the texture data
        /// </summary>
        /// <param name="data">Data to be set</param>
        void SetTextureData(byte[] data);

        /// <summary>
        /// Sets the texture data of the specified MipMap level
        /// </summary>
        /// <param name="data">Data to be set</param>
        /// <param name="mipMapLevel">MipMap level for the data</param>
        void SetTextureData(byte[] data, int mipMapLevel);

        /// <summary>
        /// Resets all the data of the texture
        /// </summary>
        /// <param name="width">New image width</param>
        /// <param name="height">New image height</param>
        /// <param name="mipMapLevels">New MipMap level</param>
        /// <param name="stride">Stride factor</param>
        /// <param name="Format">New image format</param>
        void Reset(
            int width,
            int height,
            int mipMapLevels,
            int stride,
            TextureFormat Format);        
    }
}