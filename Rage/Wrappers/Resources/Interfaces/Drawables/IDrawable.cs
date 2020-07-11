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

namespace SADL.Rage.Wrappers.Resources.Interfaces.Drawables
{
    /// <summary>
    /// Represents a drawable list
    /// </summary>
    public interface IDrawableList : IList<IDrawable>
    {
        // TODO: needs to be implemented
    }

    /// <summary>
    /// Represents a drawable
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Gets the name of the drawable
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets the shader group of the drawable
        /// </summary>
        IShaderGroup ShaderGroup { get; }
    }
}