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

namespace SADL.Rage.GeneralResources
{
    public static class ResourceHelpers
    {
        // TODO: needs to be reviewed

        private const int SKIP_SIZE = 64;
        private const int ALIGN_SIZE = 64;

        public static void GetBlocks(IResourceBlock rootBlock, out IList<IResourceBlock> sys, out IList<IResourceBlock> gfx)
        {
            HashSet<IResourceBlock> systemBlocks = new HashSet<IResourceBlock>();
            HashSet<IResourceBlock> graphicBlocks = new HashSet<IResourceBlock>();
            IList<IResourceBlock> protectedBlocks = new List<IResourceBlock>();

            Stack<IResourceBlock> stack = new Stack<IResourceBlock>();
            stack.Push(rootBlock);

            HashSet<IResourceBlock> processed = new HashSet<IResourceBlock>();
            processed.Add(rootBlock);

            while (stack.Count > 0)
            {
                IResourceBlock block = stack.Pop();
                if (block == null)
                    continue;

                if (block is IResourceSystemBlock)
                {
                    if (!systemBlocks.Contains(block))
                        systemBlocks.Add(block);

                    IResourceBlock[] references = ((IResourceSystemBlock)block).GetReferences();
                    for(int i = 0; i < references.Length; i++)
                    {
                        if (!processed.Contains(references[i]))
                        {
                            stack.Push(references[i]);
                            processed.Add(references[i]);
                        } 
                    }

                    Stack<IResourceSystemBlock> subs = new Stack<IResourceSystemBlock>();
                    foreach (var part in ((IResourceSystemBlock)block).GetParts())
                        subs.Push((IResourceSystemBlock)part.Item2);

                    while (subs.Count > 0)
                    {
                        IResourceSystemBlock sub = subs.Pop();

                        foreach (var x in sub.GetReferences())
                            if (!processed.Contains(x))
                            {
                                stack.Push(x);
                                processed.Add(x);
                            }
                        foreach (var x in sub.GetParts())
                            subs.Push((IResourceSystemBlock)x.Item2);

                        protectedBlocks.Add(sub);
                    }
                }
                else
                {
                    if (!graphicBlocks.Contains(block))
                        graphicBlocks.Add(block);
                }
            }

            foreach (var q in protectedBlocks)
                if (systemBlocks.Contains(q))
                    systemBlocks.Remove(q);

            sys = new List<IResourceBlock>();
            foreach (var s in systemBlocks)
                sys.Add(s);
            gfx = new List<IResourceBlock>();
            foreach (var s in graphicBlocks)
                gfx.Add(s);
        }

        public static void AssignPositions(IList<IResourceBlock> blocks, uint basePosition, ref int pageSize, out int pageCount)
        {
            long largestBlockSize = 0;
            foreach (var block in blocks)
            {
                if (largestBlockSize < block.Length)
                    largestBlockSize = block.Length;
            }

            long currentPageSize = 0x2000;
            while (currentPageSize < largestBlockSize)
                currentPageSize *= 2;

            long currentPageCount;
            long currentPosition;
            while (true)
            {
                currentPageCount = 0;
                currentPosition = 0;

                foreach (var block in blocks)
                    block.Position = -1;

                foreach (var block in blocks)
                {
                    if (block.Position != -1)
                        throw new Exception("A position of -1 is not possible!");

                    long maxSpace = currentPageCount * currentPageSize - currentPosition;
                    if (maxSpace < (block.Length + SKIP_SIZE))
                    {
                        currentPageCount++;
                        currentPosition = currentPageSize * (currentPageCount - 1);
                    }

                    block.Position = basePosition + currentPosition;
                    currentPosition += block.Length + SKIP_SIZE;

                    if ((currentPosition % ALIGN_SIZE) != 0)
                        currentPosition += (ALIGN_SIZE - (currentPosition % ALIGN_SIZE));
                }

                if (currentPageCount < 128)
                    break;

                currentPageSize *= 2;
            }

            pageSize = (int)currentPageSize;
            pageCount = (int)currentPageCount;
        }
    }
}