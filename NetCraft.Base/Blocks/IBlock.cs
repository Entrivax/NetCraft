using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Blocks
{
    public interface IBlock
    {
        bool IsOpaque { get; }

        string Name { get; }
    }
}
