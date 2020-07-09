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

 namespace SADL.Rage.GeneralResources.Common.x64
 {
    /// <summary>
    /// Represents a x64 array of <see cref="IResourceSystemBlock" />
    /// </summary>
     public class ResourceSimpleArray64<T> : ListBase<ResourceSimpleArray<T>> where T : IResourceSystemBlock, new()
     {
        /// <summary>
        /// Gets the length of the data block
        /// </summary>
        public override long Length
        {
            get
            {
                long len = 8 * Data.Count;
                for(int i = 0; i < Data.Count; i++)
                {
                    len += Data[i].Length;
                }
                return len;
            }
        }

        public ResourceSimpleArray64()
        {
            Data = new List<ResourceSimpleArray<T>>();
        }

        public List<ulong> ptr_list;

        /// <summary>
        /// Reads the data block
        /// </summary>
        /// <param name="reader">Reader wich will help for reading the block</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            ResourceSimpleArray<SADL.Rage.GeneralResources.Common.Types.uint_r> num = (ResourceSimpleArray<SADL.Rage.GeneralResources.Common.Types.uint_r>)parameters[1];

            ptr_list = new List<ulong>();
            for (int i = 0; i < num.Count; i++)
                ptr_list.Add(reader.ReadUInt64());

            for (int i = 0; i < num.Count; i++)
            {
                ResourceSimpleArray<T> block = reader.ReadBlockAt<ResourceSimpleArray<T>>(ptr_list[i], (uint)num[i]);
                Data.Add(block);
            }
        }

        /// <summary>
        /// Writes the data block
        /// </summary>
        /// <param name="writer">Writer wich will help for writing the block</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            ptr_list = new List<ulong>();
            for(int i = 0; i < Data.Count; i++)
                ptr_list.Add((ulong)Data[i].Position);
            for(int i = 0; i < ptr_list.Count; i++)
                writer.Write(ptr_list[i]);
            for(int i = 0; i < Data.Count; i++)
                Data[i].Write(writer);
        }

        /// <summary>
        /// Gets a list of references of this array
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            List<IResourceBlock> children = new List<IResourceBlock>();
            return children.ToArray();
        }

        /// <summary>
        /// Gets the parts of this array
        /// </summary>
        public override Tuple<long, IResourceBlock>[] GetParts()
        {
            List<Tuple<long, IResourceBlock>> children = new List<Tuple<long, IResourceBlock>>();

            if (Data != null)
            {
                long len = 8 * Data.Count;
                for(int i = 0; i < Data.Count; i++)
                {
                    children.Add(new Tuple<long, IResourceBlock>(len, Data[i]));
                    len += Data[i].Length;
                }
            }
      
            return children.ToArray();
        }
     }
 }