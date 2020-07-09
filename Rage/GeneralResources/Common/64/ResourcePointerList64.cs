using System.Collections.Generic;

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources.Common.x64
{
    /// <summary>
    /// Represents a x64 pointer list of <see cref="ResourceSystemBlock" />
    /// </summary>
    public class ResourcePointerList64<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        /// <summary>
        /// Gets the lenght of the pointer list
        /// </summary>
        public override long Length { get { return 16; } }
    
        /// <summary>
        /// Pointer entries
        /// </summary>
        public ulong EntriesPointer;
        
        /// <summary>
        /// Pointer item count and capacity
        /// </summary>
        public ushort EntriesCount, EntriesCapacity;
        
        /// <summary>
        /// Items contained in the list
        /// </summary>
        public ResourcePointerArray64<T> Entries;

        /// <summary>
        /// Reads the pointer item
        /// </summary>
        /// <param name="reader">The reader wich will help for reading the pointer</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt16();
            this.EntriesCapacity = reader.ReadUInt16();
            reader.Position += 4;

            this.Entries = reader.ReadBlockAt<ResourcePointerArray64<T>>(
                this.EntriesPointer,
                this.EntriesCount
            );
        }

        /// <summary>
        /// Writes the pointer item
        /// </summary>
        /// <param name="reader">The writer wich will help for writing the pointer</param>
        /// <param name="parameters">The paramaters for additional data grabbing</param>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            this.EntriesPointer = (ulong)(this.Entries != null ? this.Entries.Position : 0);
            this.EntriesCount = (ushort)(this.Entries != null ? this.Entries.Count : 0);
            this.EntriesCapacity = (ushort)(this.Entries != null ? this.Entries.Count : 0);

            writer.Write(EntriesPointer);
            writer.Write(EntriesCount);
            writer.Write(EntriesCapacity);
            writer.Write((uint)0x0000000);
        }

        /// <summary>
        /// Gets a list of reference to this list
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            List<IResourceBlock> list = new List<IResourceBlock>();
            if (Entries != null) list.Add(Entries);
            return list.ToArray();
        }
    }
}