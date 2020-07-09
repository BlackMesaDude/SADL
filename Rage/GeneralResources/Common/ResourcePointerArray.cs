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

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources.Common
{
    /// <summary>
    /// Represents a pointer array of <see cref="ResourceSystemBlock" />
    /// </summary>
    public class ResourcePointerArray<T> : ResourceSystemBlock, IList<T> where T : IResourceSystemBlock, new()
    {
        /// <summary>
        /// Gets the lenght of the pointer array
        /// </summary>
        public override long Length { get { return 4 * data_items.Count; } }

        /// <summary>
        /// Pointers of this array
        /// </summary>
        public List<uint> data_pointers;

        /// <summary>
        /// Items of this array
        /// </summary>
        public List<T> data_items;

        /// <summary>
        /// Reads the pointer item
        /// </summary>
        /// <param name="reader">The reader wich will help for reading the pointer</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int numElements = Convert.ToInt32(parameters[0]);

            data_pointers = new List<uint>();
            for (int i = 0; i < numElements; i++)
            {
                data_pointers.Add(reader.ReadUInt32());
            }

            data_items = new List<T>();
            for (int i = 0; i < numElements; i++)
            {
                data_items.Add(
                    reader.ReadBlockAt<T>(data_pointers[i])
                    );
            }
        }

        /// <summary>
        /// Writes the pointer item
        /// </summary>
        /// <param name="reader">The writer wich will help for writing the pointer</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            data_pointers = new List<uint>();
            for(int i = 0; i < data_items.Count; i++)
            {
                if(data_items[i] != null)
                {
                    data_pointers.Add((uint) data_items[i].Position);
                }
                else
                {
                    data_pointers.Add((uint) 0);
                }
            }

            for(int i = 0; i < data_pointers.Count; i++)
            {
                writer.Write(data_pointers[i]);
            }
        }

        /// <summary>
        /// Returns a list of the references in the array
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            List<IResourceBlock> list = new List<IResourceBlock>();

            for(int i = 0; i < data_items.Count; i++)
            {
                list.Add(data_items[i]);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Finds the index of a specified item
        /// </summary>
        /// <param name="item">Item where the index should be taken</param>
        public int IndexOf(T item)
        {
            return data_items.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item with a specified index
        /// </summary>
        /// <param name="index">Index wich the item will be inserted</param>
        /// <param name="item">Item that needs to be insterted</param>
        public void Insert(int index, T item)
        {
            data_items.Insert(index, item);
        }

        /// <summary>
        /// Removes an item based on the index
        /// </summary>
        /// <param name="index">Item index to be removed</param>
        public void RemoveAt(int index)
        {
            data_items.RemoveAt(index);
        }

        /// <summary>
        /// Gets or Sets an item
        /// </summary>
        public T this[int index]
        {
            get
            {
                return data_items[index];
            }
            set
            {
                data_items[index] = value;
            }
        }

        /// <summary>
        /// Adds an item
        /// </summary>
        /// <param name="item">Item to be added</param>
        public void Add(T item)
        {
            data_items.Add(item);
        }

        /// <summary>
        /// Clears a list
        /// </summary>
        public void Clear()
        {
            data_items.Clear();
        }

        /// <summary>
        /// Conditional operator '==' checks if an item is contained in the array
        /// </summary>
        /// <param name="item">Item to be checked</param>
        public bool Contains(T item)
        {
            return data_items.Contains(item);
        }

        /// <summary>
        /// Copies an array to a specified index
        /// </summary>
        /// <param name="array">Array to be copied</param>
        /// <param name="arrayIndex">Index where the array needs to be copied</param>
        public void CopyTo(T[] array, int arrayIndex) { data_items.CopyTo(array, arrayIndex); }
        
        /// <summary> 
        /// Gets how many items are in the array
        /// </summary>
        public int Count { get { return data_items.Count; } }

        /// <summary>
        /// Is the array readable only?
        /// </summary>
        public bool IsReadOnly { get { return false; } }

        /// <summary>
        /// Removes an item from the array
        /// </summary>
        /// <param name="item">Item to be removed</param>
        public bool Remove(T item)
        {
            return data_items.Remove(item);
        }

        /// <summary>
        /// Gets array enumerator
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return data_items.GetEnumerator();
        }

        /// <summary>
        /// Gets array enumerator
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return data_items.GetEnumerator();
        }
    }   
}