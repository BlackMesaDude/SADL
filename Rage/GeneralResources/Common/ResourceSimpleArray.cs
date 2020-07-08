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
    /// Represents an array of <see cref="IResourceSystemBlock" />
    /// </summary>
    public class ResourceSimpleArray<T> : ListBase<T> where T : IResourceSystemBlock, new()
    {
        /// <summary>
        /// Gets the length of the data block
        /// </summary>
        public override long Length
        {
            get
            {
                long length = 0;
                for(int i = 0; i < Data.Count; i++)
                {
                    length = Data[i].Length;
                }
                return length;
            }
        }

        public ResourceSimpleArray()
        {
            Data = new List<T>();
        }

        /// <summary>
        /// Reads the DataBlock
        /// </summary>
        /// <param name="reader">The reader wich will help for reading the data</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            int numElements = Convert.ToInt32(parameters[0]);
            
            Data = new List<T>();
            for (int i = 0; i < numElements; i++)
            {
                T item = reader.ReadBlock<T>();
                Data.Add(item);
            }
        }

        /// <summary>
        /// Writes the DataBlock
        /// </summary>
        /// <param name="writer">The writer wich will help for writing the data</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            for(int i = 0; i < Data.Count; i++)
            {
                Data[i].Write(writer);
            }
        } 

        /// <summary>
        /// Returns a list of the parts in the DataBlock
        /// </summary>
        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            List<Tuple<long, IResourceBlock>> list = new List<Tuple<long, IResourceBlock>>();

            long length = 0;
            for(int i = 0; i < Data.Count; i++)
            {
                list.Add(new Tuple<long, IResourceBlock>(length, Data[i]));
                length += Data[i].Length;
            }

            return list.ToArray();
        }
    }   
}