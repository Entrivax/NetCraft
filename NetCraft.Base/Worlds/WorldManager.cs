using NetCraft.Base.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Worlds
{
    class WorldManager : IWorldManager
    {
        private readonly IBlocksProvider _blocksProvider;
        private readonly IChunkManager _chunkManager;
        private readonly IChunkGenerator _chunkGenerator;

        public WorldManager(IBlocksProvider blocksProvider, IChunkManager chunkManager, IChunkGenerator chunkGenerator)
        {
            _blocksProvider = blocksProvider;
            _chunkManager = chunkManager;
            _chunkGenerator = chunkGenerator;
        }

        public Chunk GetChunkAt(World world, int x, int z)
        {
            return ProvideChunk(world, x, z);
        }

        public Chunk GetChunkAtWorldCoordinates(World world, int x, int z)
        {
            return DirectGetChunk(world, x >> 4, z >> 4);
        }

        public byte GetBlockIdAtForCurrentlyLoadedChunks(World world, int x, int y, int z)
        {
            var x4 = x >> 4;
            var z4 = z >> 4;
            var chunk = DirectGetChunk(world, x4, z4);
            return chunk == null ? (byte)0 : _chunkManager.GetBlockId(chunk, (byte)(x - (x4 << 4)), (byte)y, (byte)(z - (z4 << 4)));
        }

        public byte GetBlockIdAt(World world, int x, int y, int z)
        {
            var x4 = x >> 4;
            var z4 = z >> 4;
            var chunk = ProvideChunk(world, x4, z4);
            return _chunkManager.GetBlockId(chunk, (byte)(x - (x4 << 4)), (byte)y, (byte)(z - (z4 << 4)));
        }

        public Chunk[] GetSiblingChunks(World world, ChunkPosition chunkPosition)
        {
            var chunks = new Chunk[4];
            chunks[0] = DirectGetChunk(world, chunkPosition.X - 1, chunkPosition.Z);
            chunks[1] = DirectGetChunk(world, chunkPosition.X, chunkPosition.Z + 1);
            chunks[2] = DirectGetChunk(world, chunkPosition.X + 1, chunkPosition.Z);
            chunks[3] = DirectGetChunk(world, chunkPosition.X, chunkPosition.Z - 1);
            return chunks;
        }

        public void SetBlockIdAt(World world, int x, int y, int z, byte blockId)
        {
            SetBlockIdAndMetadataAt(world, x, y, z, blockId, 0);
        }

        public void SetBlockIdAndMetadataAt(World world, int x, int y, int z, byte blockId, byte metadata)
        {
            var x40 = x >> 4;
            var z40 = z >> 4;
            var by = (byte)y;
            var chunk0 = ProvideChunk(world, x40, z40);
            _chunkManager.SetBlockIdAndMetadata(chunk0, (byte)(x & 0xF), (byte)y, (byte)(z & 0xF), blockId, metadata);
            _chunkManager.Invalidate(chunk0, by);
            if (y > 0)
                _chunkManager.Invalidate(chunk0, (byte)(y - 1));
            if (y < 255)
                _chunkManager.Invalidate(chunk0, (byte)(y + 1));

            var x41 = (x - 1) >> 4;
            var x42 = (x + 1) >> 4;
            if (x41 != x40)
                _chunkManager.Invalidate(ProvideChunk(world, x41, z40), by);
            else if (x42 != x40)
                _chunkManager.Invalidate(ProvideChunk(world, x42, z40), by);

            var z41 = (z - 1) >> 4;
            var z42 = (z + 1) >> 4;
            if (z41 != z40)
                _chunkManager.Invalidate(ProvideChunk(world, x40, z41), by);
            else if (z42 != z40)
                _chunkManager.Invalidate(ProvideChunk(world, x40, z42), by);
        }

        private Chunk ProvideChunk(World world, int x, int z)
        {
            var position = new ChunkPosition(x, z);

            var chunkBlockPosition = new ChunkPosition((int)Math.Floor(x / 8f), (int)Math.Floor(z / 8f));
            Chunk[] chunkBlock;
            if (!world.ChunkBlocks.TryGetValue(chunkBlockPosition, out chunkBlock))
            {
                chunkBlock = new Chunk[8 * 8];
                world.ChunkBlocks.TryAdd(chunkBlockPosition, chunkBlock);
            }
            var chunkIndex = (((x & 7) << 3) + (z & 7));
            if (chunkBlock[chunkIndex] != null)
                return chunkBlock[chunkIndex];

            var newChunk = new Chunk();
            _chunkGenerator.PopulateChunk(world, newChunk, position, false);
            chunkBlock[chunkIndex] = newChunk;

            var chunk = DirectGetChunk(world, x - 1, z);
            if (chunk != null)
                _chunkManager.Invalidate(chunk);
            chunk = DirectGetChunk(world, x + 1, z);
            if (chunk != null)
                _chunkManager.Invalidate(chunk);
            chunk = DirectGetChunk(world, x, z - 1);
            if (chunk != null)
                _chunkManager.Invalidate(chunk);
            chunk = DirectGetChunk(world, x, z + 1);
            if (chunk != null)
                _chunkManager.Invalidate(chunk);
            return newChunk;
        }

        public List<Tuple<ChunkPosition, Chunk>> GetLoadedChunks(World world)
        {
            var chunks = new List<Tuple<ChunkPosition, Chunk>>();
            foreach (var chunkBlock in world.ChunkBlocks)
            {
                var chunkBlockPosition = chunkBlock.Key;
                var currentChunks = chunkBlock.Value;
                for (int i = 0; i < currentChunks.Length; i++)
                {
                    if (currentChunks[i] != null)
                    {
                        chunks.Add(new Tuple<ChunkPosition, Chunk>(
                            new ChunkPosition(
                                (chunkBlockPosition.X << 3) + (((i >> 3) & 7)),
                                (chunkBlockPosition.Z << 3) + (i & 7)
                            ), currentChunks[i]));
                    }
                }
            }
            return chunks;
        }

        public void SetChunkToUnload(World world, int x, int z)
        {
            var chunkBlockPosition = new ChunkPosition((int)Math.Floor(x / 8f), (int)Math.Floor(z / 8f));
            Chunk[] chunkBlock;
            if (world.ChunkBlocks.TryGetValue(chunkBlockPosition, out chunkBlock))
            {
                var chunkIndex = (((x & 7) << 3) + (z & 7));
                if (chunkBlock[chunkIndex] != null)
                {
                    world.ChunksToUnload.Add(chunkBlock[chunkIndex]);
                    chunkBlock[chunkIndex] = null;

                    for (int i = 0; i < chunkBlock.Length; i++)
                        if (chunkBlock[i] != null)
                            return;
                    Chunk[] tmp;
                    world.ChunkBlocks.TryRemove(chunkBlockPosition, out tmp);
                }
            }
        }

        public Chunk DirectGetChunk(World world, int x, int z)
        {
            var chunkBlockPosition = new ChunkPosition((int)Math.Floor(x / 8f), (int)Math.Floor(z / 8f));
            Chunk[] chunkBlock;
            if (world.ChunkBlocks.TryGetValue(chunkBlockPosition, out chunkBlock))
            {
                var chunkIndex = (((x & 7) << 3) + (z & 7));
                if (chunkBlock[chunkIndex] != null)
                    return chunkBlock[chunkIndex];
            }
            return null;
        }

        public void UnloadChunks(World world)
        {
            while (world.ChunksToUnload.Count > 0)
            {
                Chunk chunk;
                if (!world.ChunksToUnload.TryTake(out chunk))
                    break;
                //_chunkManager.Unload(chunk);
            }
        }

        public void Clean(World world)
        {
            foreach (var c in world.ChunkBlocks)
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        SetChunkToUnload(world, c.Key.X * 8 + i, c.Key.Z * 8 + j);
            }
            UnloadChunks(world);
        }
    }
}
