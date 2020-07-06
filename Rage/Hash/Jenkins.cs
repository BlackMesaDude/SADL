using System;
using System.Collections.Generic;
using System.Text;

namespace SADL.Rage.Hash
{
    /// <summary>
    /// <see cref="Jenkins"/> is a class that derivers from the <b>Jenkins hashing functions</b>, these are a collection of non-cryptographic hash functions for multi-byte keys.
    /// </summary>
    public class Jenkins
    {
        /// <summary>
        /// Returns the multi-byte hash key of a single string key
        /// </summary>
        /// <param name="key">Key to be converted into multi-byte hash</param>
        /// <returns></returns>
        public static uint Hash(string key)
        {
            uint hash = 0;
            for (int i = 0; i < key.Length; ++i)
            {
                hash += key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return hash;
        }
    }
}
