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

using System.IO;

using SADL.Rage.Data;

namespace SADL.GrandTheftAuto.Encryption.Helpers
{
    /// <summary>
    /// Allows to read or write NG keys and tables
    /// </summary>
    public class CryptoIO
    {
        /// <summary>
        /// Reads NG keys from a file
        /// </summary>
        /// <param name="fileName">Path to the file that needs to be readden</param>
        public static byte[][] ReadNGKeys(string fileName)
        {
            byte[][] result = new byte[101][];                                                      // defines the array that later will contain processed data

            FileStream fileStream = new FileStream(fileName, FileMode.Open);                        // stream for opening the file
            DataReader dataReader = new DataReader(fileStream);                                     // reader for reading the data taken from the stream

            for(int i = 0; i < 101; i++)
            {
                // n result will have the value of the readden 272 sequence of bytes
                result[i] = dataReader.ReadBytes(272);
            }

            fileStream.Close();                                                                     // closes the stream

            return result;                                                                          // returns the processed result
        }

        /// <summary>
        /// Writes NG keys to a file
        /// </summary>
        /// <param name="fileName">Path to the file that needs to be readden</param>
        /// <param name="data">Keys that needs to be written to the file</param>
        public static void WriteNGKeys(string fileName, byte[][] data)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);                      // stream for creating the file
            DataWriter dataWriter = new DataWriter(fileStream);                                     // writer for writing in the stream

            for (int i = 0; i < 101; i++)
            {
                dataWriter.Write(data[i]);                                                          // writes the data into the stream
            }

            fileStream.Close();                                                                     // closes the stream
        }

        /// <summary>
        /// Reads NG tables from a file
        /// </summary>
        /// <param name="fileName">Path to the file that needs to be readden</param>
        public static uint[][][] ReadNGTables(string fileName)
        {
            uint[][][] result = new uint[17][][];                                                   // defines the array that later will be used for the processed result

            FileStream fileStream = new FileStream(fileName, FileMode.Open);                        // stream for opening the file
            DataReader dataReader = new DataReader(fileStream);                                     // reader for reading the data inside the stream

            // iterates trough 17 rounds
            for (int i = 0; i < 17; i++)
            {
                result[i] = new uint[16][];
                // iterates trough 16 bytes
                for (int j = 0; j < 16; j++)
                {
                    result[i][j] = new uint[256];
                    // iterates trough 256 entries
                    for (int k = 0; k < 256; k++)
                    {
                        // x y z result will be the readden UINT32 value
                        result[i][j][k] = dataReader.ReadUInt32();                  
                    }
                }
            }
            
            fileStream.Close();                                                                     // closes the stream

            return result;                                                                          // returns the processed result
        }

        /// <summary>
        /// Writes NG tables from a file
        /// </summary>
        /// <param name="fileName">Path to the file that needs to be readden</param>
        /// <param name="data">Tables that needs to be written to the file</param>
        public static void WriteNGTables(string fileName, uint[][][] data)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Create);                      // stream for creating the file
            DataWriter dataWriter = new DataWriter(fileStream);                                     // writer for writing the data inside the stream

            // iterates trough 17 rounds
            for (int i = 0; i < 17; i++)
            {
                // iterates trough 16 bytes
                for (int j = 0; j < 16; j++)
                {
                    // iterates trough 256 entries
                    for (int k = 0; k < 256; k++)
                    {
                        // x y z result will be the written to the stream
                        dataWriter.Write(data[i][j][k]);
                    }
                }
            }

            fileStream.Close();                                                                     // closes the stream
        }
    }
}