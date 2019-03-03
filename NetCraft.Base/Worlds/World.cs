using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Worlds
{
    public class World
    {
        public ConcurrentBag<Chunk> ChunksToUnload { get; }
        public ConcurrentDictionary<ChunkPosition, Chunk[]> ChunkBlocks { get; }

        public string Name { get; }
        public long Seed { get; }
        public long Time { get; set; }

        public World(string name, long seed)
        {
            Name = name;
            Seed = seed;
            Time = 0;
            ChunkBlocks = new ConcurrentDictionary<ChunkPosition, Chunk[]>();
            ChunksToUnload = new ConcurrentBag<Chunk>();
        }
    }
}
