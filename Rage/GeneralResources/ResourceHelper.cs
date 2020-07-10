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
using System.Linq;
using System.Collections.Generic;

using Global = SADL.Rage.Helpers.Constants;

using SADL.Rage.GeneralResources.Interfaces;

namespace SADL.Rage.GeneralResources
{
    /// <summary>
    /// Contains functions and other resources that can help with the manipulation of a resource file
    /// </summary>
    public static class ResourceHelpers
    {
        // hashset(s) for system blocks, graphics blocks and processed blocks
        private static HashSet<IResourceBlock> _sysBlocks = new HashSet<IResourceBlock>(), 
                                               _gfxBlocks = new HashSet<IResourceBlock>(), 
                                               _processed = new HashSet<IResourceBlock>();

        // list for protected blocks
        private static IList<IResourceBlock> _prtBlocks = new List<IResourceBlock>();
        // stack for blocks
        private static Stack<IResourceBlock> _stkBlocks = new Stack<IResourceBlock>();
        // stack for sub-blocks
        private static Stack<IResourceSystemBlock> _stkSubBlocks = new Stack<IResourceSystemBlock>();

        /// <summary>
        /// Gets every block of a resource file
        /// </summary>
        /// <param name="root">Root element that indicates where to start the check</param>
        /// <param name="sys">System segment block list out</param>
        /// <param name="gfx">System segment block list out</param>
        public static void GetBlocks(IResourceBlock root, out IList<IResourceBlock> sys, out IList<IResourceBlock> gfx)
        {
            ClearData();                                                                                    // clears all data for a new usage

            // push to the stack and adds to the processed elements the root item
            _stkBlocks.Push(root);
            _processed.Add(root);

            // resource blocks for reference purporses
            IResourceBlock popBlock = null;
            IResourceBlock[] references = null;
            // while the stack isn't empty
            while(_stkBlocks.Count > 0)
            {
                popBlock = _stkBlocks.Pop();                                                                // removes the first block from the stack and gets the next one

                // if the block is null then continue
                if(popBlock == null)
                    continue;

                // checks the type of the popBlock, the difference here is that the type wil be checked from the inheritance tree
                if(popBlock is IResourceSystemBlock)
                {
                    // if the system segments doesnt contain a block similiar to a popBlock then add it
                    if(!_sysBlocks.Contains(popBlock))
                        _sysBlocks.Add(popBlock);

                    references = ((IResourceSystemBlock)popBlock).GetReferences();                          // sets the defined array to contain every reference of the popBlock
                    for(int i = 0; i < references.Length; i++)
                    {
                        // if processed doesn't contains n reference block then push it to the stack and add it to the references
                        if(!_processed.Contains(references[i]))
                        {
                            _stkBlocks.Push(references[i]);
                            _processed.Add(references[i]);
                        }
                    }

                    for(int i = 0; i < ((IResourceSystemBlock)popBlock).GetParts().Length; i++)
                    {
                        // pushes each part of the popBlock to the stack of sub blocks
                        _stkSubBlocks.Push((IResourceSystemBlock)((IResourceSystemBlock)popBlock).GetParts()[i]);
                    }

                    // while the sub block stack isn't empty
                    while(_stkSubBlocks.Count > 0)
                    {
                        IResourceSystemBlock subBlock = _stkSubBlocks.Pop();                                // creates a IResourceSystemBlock that will remove the first element of the sub block stack and get the next one
                        for(int i = 0; i < subBlock.GetReferences().Length; i++)
                        {
                            // if the processed list doesnt contain the n sub block reference then push it to the stack and add it to the processed list
                            if(!_processed.Contains(subBlock.GetReferences()[i]))
                            {
                                _stkBlocks.Push(subBlock.GetReferences()[i]);
                                _processed.Add(subBlock.GetReferences()[i]);
                            }
                        }

                        for(int i = 0; i < subBlock.GetParts().Length; i++)
                        {
                            // pushes each Item2 part of the subBlock to the stack of sub blocks 
                            _stkSubBlocks.Push((IResourceSystemBlock)subBlock.GetParts()[i].Item2); 
                        }

                        _prtBlocks.Add(subBlock);                                                            // adds the subBlock to the prtBlock list
                    }
                }
                else 
                {
                    // otherwise add the pop block to the graphics list
                    if(!_gfxBlocks.Contains(popBlock))
                        _gfxBlocks.Add(popBlock);
                }
            }

            /* from this part processed blocks will be checked and added to the corrispettive list */

            for(int i = 0; i < _prtBlocks.Count; i++)
            {
                if(_sysBlocks.Contains(_prtBlocks[i]))
                    _sysBlocks.Remove(_prtBlocks[i]);
            }

            sys = new List<IResourceBlock>();
            gfx = new List<IResourceBlock>();

            for(int i = 0; i < _sysBlocks.Count; i++)
            {
                sys.Add(_sysBlocks.ToList<IResourceBlock>()[i]);
            }

            for(int i = 0; i < _gfxBlocks.Count; i++)
            {
                gfx.Add(_gfxBlocks.ToList<IResourceBlock>()[i]);
            }
        }

        /// <summary>
        /// Assigns a position for paging purposes
        /// </summary>
        /// <param name="blocks">The blocks list for the paging</param>
        /// <param name="basePosition">The base position</param>
        /// <param name="pageSize">The page size</param>
        /// <param name="pageCount">The page count</param>
        public static void AssignPositions(IList<IResourceBlock> blocks, uint basePosition, ref int pageSize, out int pageCount)
        {
            long largestBlockSize = 0;                                                                          // defines a long variable that will store the largest block size
            for(int i = 0; i < blocks.Count; i++)
            {
                // if largest block is less than n block size than assign n block size value to the largestBlockSize
                if(largestBlockSize < blocks[i].Length)
                    largestBlockSize = blocks[i].Length;
            }

            long currentPageSize = Global.PagingValues.DEFAULT_PAGE_SIZE;                                       // defines the current page size starting from the default page size
            // if the current page size is less than the largest block size than multipli the page size by 2
            while (currentPageSize < largestBlockSize)
                currentPageSize *= 2;

            long currentPageCount, currentPosition;                                                             // defines current count and position
            while (true)
            {
                currentPageCount = 0;
                currentPosition = 0;

                // for each block set the position to -1                
                for(int i = 0; i < blocks.Count; i++)
                {
                    blocks[i].Position = -1;
                }

                for(int i = 0; i < blocks.Count; i++)
                {
                    // if the position of n block isn't -1 then throw an exception and continue
                    if(blocks[i].Position != -1) 
                        throw new Exception("n block position at -1 is not available!");

                    long maxSpace = currentPageCount * currentPageSize - currentPosition;                       // defines the maximum space
                    // if the maximum space is less than the length of n block + default skip size than augment page count and assign the new position
                    if (maxSpace < (blocks[i].Length + Global.ResourceSize.SKIP_SIZE))
                    {
                        currentPageCount++;
                        currentPosition = currentPageSize * (currentPageCount - 1);
                    }

                    blocks[i].Position = basePosition + currentPosition;                                        // new n block position 
                    currentPosition += blocks[i].Length + Global.ResourceSize.SKIP_SIZE;                        // augments the new position based on the n block length and default skip size value

                    // if the factor of current position and default align size value is not zeor then augment current position with the new value
                    if ((currentPosition % Global.ResourceSize.ALIGN_SIZE) != 0)
                        currentPosition += (Global.ResourceSize.ALIGN_SIZE - (currentPosition % Global.ResourceSize.ALIGN_SIZE));
                }

                // if the current page exceeds the maximum page count then stop
                if (currentPageCount < Global.PagingValues.MAXIMUM_PAGE_COUNT)
                    break;

                currentPageSize *= 2;                                                                            // augments the page size to two
            }

            pageSize = (int)currentPageSize;                                                                     // assigns the current page size to the default value
            pageCount = (int)currentPageCount;                                                                   // assigns the current page count to the default value
        }

        // clears each stack and hashset
        private static void ClearData()
        {
                _prtBlocks.Clear();
                
                _stkBlocks.Clear();

                _stkBlocks.Clear();

                _sysBlocks.Clear();

                _gfxBlocks.Clear();

                _processed.Clear();
        }
    }
}