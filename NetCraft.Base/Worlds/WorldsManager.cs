using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetCraft.Base.Worlds
{
    class WorldsManager : IWorldsManager
    {
        public List<World> Worlds { get; private set; }

        public WorldsManager()
        {
            Worlds = new List<World>();
        }

        public void LoadWorld(string worldName)
        {
            Worlds.Add(new World(worldName, new Random().Next()));
        }

        public void UnloadWorld(World world)
        {
            throw new NotImplementedException();
        }
    }
}