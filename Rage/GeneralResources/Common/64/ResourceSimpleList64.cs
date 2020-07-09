using System.Collections.Generic;

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources.Common.x64
{
    public class ResourceSimpleList64<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long Length { get { return 16; } }

        public ulong EntriesPointer;
        public ushort EntriesCount, EntriesCapacity;

        public ResourceSimpleArray<T> Entries;

        /// <summary>
        /// Reads the pointer
        /// </summary>
        /// <param name="reader">Reader wich will help for reading the pointer</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt16();
            this.EntriesCapacity = reader.ReadUInt16();
            reader.Position += 4;

            this.Entries = reader.ReadBlockAt<ResourceSimpleArray<T>>(
                this.EntriesPointer, 
                this.EntriesCount
            );
        }

        /// <summary>
        /// Writes the pointer
        /// </summary>
        /// <param name="writer">Writer wich will help for writing the pointer</param>
        /// <param name="parameters">Additional parameters for data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            this.EntriesPointer = (ulong)(this.Entries != null ? this.Entries.Position : 0);
            this.EntriesCount = (ushort)(this.Entries != null ? this.Entries.Count : 0);
            this.EntriesCapacity = (ushort)(this.Entries != null ? this.Entries.Count : 0);

            writer.Write(this.EntriesPointer);
            writer.Write(this.EntriesCount);
            writer.Write(this.EntriesCapacity);
            writer.Write((uint)0x00000000);
        }

        /// <summary>
        /// Gets a list of references of this list
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            List<IResourceBlock> list = new List<IResourceBlock>();
            if (Entries != null) list.Add(Entries);
            return list.ToArray();
        }
    }
}