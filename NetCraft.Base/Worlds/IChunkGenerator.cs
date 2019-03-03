namespace NetCraft.Base.Worlds
{
    interface IChunkGenerator
    {
        void PopulateChunk(World world, Chunk chunk, ChunkPosition position, bool parallelComputing);
    }
}