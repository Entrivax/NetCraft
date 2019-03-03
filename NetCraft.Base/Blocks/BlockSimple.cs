using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Blocks
{
    class BlockSimple : IBlock
    {
        public BlockSimple(string name, bool isOpaque)
        {
            IsOpaque = isOpaque;
            Name = name;
        }

        public bool IsOpaque { get; }
        public string Name { get; }
    }
}
