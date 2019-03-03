using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Worlds
{
    public interface IWorldManager
    {
        /// <summary>
        /// Provide a chunk at chunk position and generate or load it if it is not loaded
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns>The chunk at provided position</returns>
        Chunk GetChunkAt(World world, int x, int z);

        /// <summary>
        /// Get chunk from world block position and return null if it is not loaded
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns>The chunk at provided position or null if not loaded</returns>
        Chunk GetChunkAtWorldCoordinates(World world, int x, int z);

        /// <summary>
        /// Get block id at provided world block position from loaded chunks only
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>The block id</returns>
        byte GetBlockIdAtForCurrentlyLoadedChunks(World world, int x, int y, int z);

        /// <summary>
        /// Get block id at provided world block position and generate or load chunk if not loaded
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns>The block id</returns>
        byte GetBlockIdAt(World world, int x, int y, int z);

        /// <summary>
        /// Get sibling chunks from chunk position
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="chunkPosition">The chunk position</param>
        /// <returns>4 chunks in array</returns>
        Chunk[] GetSiblingChunks(World world, ChunkPosition chunkPosition);

        /// <summary>
        /// Set block id at given world block position (start chunk generation/loading if chunk is not loaded)
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="blockId"></param>
        void SetBlockIdAt(World world, int x, int y, int z, byte blockId);

        /// <summary>
        /// Set block id and its metadata at given world block position (start chunk generation/loading if chunk is not loaded)
        /// </summary>
        /// <param name="world"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="blockId"></param>
        /// <param name="metadata"></param>
        void SetBlockIdAndMetadataAt(World world, int x, int y, int z, byte blockId, byte metadata);

        /// <summary>
        /// Return a list of loaded chunks and their position
        /// </summary>
        /// <param name="world">The world</param>
        /// <returns>A list of loaded chunks and their position</returns>
        List<Tuple<ChunkPosition, Chunk>> GetLoadedChunks(World world);

        /// <summary>
        /// Mark a chunk at chunk position to be unloaded
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        void SetChunkToUnload(World world, int x, int z);

        /// <summary>
        /// Get chunk at provided chunk position
        /// </summary>
        /// <param name="world">The world</param>
        /// <param name="x"></param>
        /// <param name="z"></param>
        /// <returns>The chunk at provided chunk position or null if the chunk is not loaded</returns>
        Chunk DirectGetChunk(World world, int x, int z);

        /// <summary>
        /// Unload all chunks marked for unload
        /// </summary>
        /// <param name="world">The world</param>
        void UnloadChunks(World world);

        /// <summary>
        /// Unload every chunks and unload them
        /// </summary>
        /// <param name="world">The world</param>
        void Clean(World world);
    }
}
