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

namespace SADL.Rage.Wrappers.Resources.Interfaces
{
    /// <summary>
    /// Defines a drawbale dictionary
    /// </summary>
    public interface IDrawableDictionary
    {
        /// <summary>
        /// Gets or Sets the drawable item list
        /// </summary>
        IDrawableList Drawables { get; set; }

        /// <summary>
        /// Gets the hashcode of the specified drawable
        /// </summary>
        /// <param name="index">Index of the drawable</param>
        uint GetHash(int index);
    }
}