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

using System.Collections;
using System.Collections.Generic;

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources.Common
{
    public abstract class ListBase<T> : ResourceSystemBlock, IList<T> where T : IResourceSystemBlock, new()
    {
        /// <summary>
        /// Gets or Sets the data in the ListBase
        /// </summary>
        public List<T> Data { get; set; }

        /// <summary>
        /// Gets or Sets n element in the ListBase
        /// </summary>
        public T this[int index]
        {
            get { return Data[index]; }
            set { Data[index] = value; }
        }

        /// <summary>
        /// Gets how many element are in the ListBase
        /// </summary>
        public int Count { get { return Data.Count; } }

        /// <summary>
        /// Is it only readable?
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        public ListBase()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// Adds an item in the list
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Add(T item)
        {
            Data.Add(item);
        }

        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            Data.Clear();
        }

        /// <summary>
        /// Conditional operator '==' for an item that resides in the list
        /// </summary>
        /// <param name="item">Item to be checked</param>
        public bool Contains(T item)
        {
            return Data.Contains(item);
        }

        /// <summary>
        /// Copies an item to an array
        /// </summary>
        /// <param name="array">Array where the item will be copied</param>
        /// <param name="arrayIndex">Index of the item to be copied</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Data.CopyTo(array, arrayIndex);
        }
        
        /// <summary>
        /// Gets the enumerator
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        /// <summary>
        /// Gets the index value of an item
        /// </summary>
        /// <param name="item">Item where the index should be taken</param>
        public int IndexOf(T item)
        {
            return Data.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item to the list after a specified index
        /// </summary>
        /// <param name="index">Index where the item should be placed</param>
        /// <param name="item">Item that should be inserted</param>
        public void Insert(int index, T item)
        {
            Data.Insert(index, item);
        }

        /// <summary>
        /// Removes an item to the list
        /// </summary>
        /// <param name="item">Item that should be removed</param>
        public bool Remove(T item)
        {
            return Data.Remove(item);
        }

        /// <summary>
        /// Removes an item to the list based on the given idex
        /// </summary>
        /// <param name="index">Item that should be removed by index</param>
        public void RemoveAt(int index)
        {
            Data.RemoveAt(index);
        }

        /// <summary>
        /// Gets the current enumerator
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}