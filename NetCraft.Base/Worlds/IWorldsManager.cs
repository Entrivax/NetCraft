using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetCraft.Base.Worlds
{
    public interface IWorldsManager
    {
        List<World> Worlds { get; }

        void LoadWorld(String worldName);
        void UnloadWorld(World world);
    }
}