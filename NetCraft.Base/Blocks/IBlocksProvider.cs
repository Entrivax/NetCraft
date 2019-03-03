using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Blocks
{
    public interface IBlocksProvider
    {
        IBlock GetBlockForId(byte id);

        void RegisterBlock(byte id, IBlock block);
    }
}
