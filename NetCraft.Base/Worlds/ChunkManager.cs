using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Worlds
{
    public class ChunkManager : IChunkManager
    {
        public Chunk.ChunkBlockInformation GetBlockInformation(Chunk chunk, byte x, byte y, byte z)
        {
            return new Chunk.ChunkBlockInformation
            {
                Id = chunk.Blocks[x << 11 | z << 7 | y],
                Metadata = chunk.BlockMetadatas[x << 11 | z << 7 | y],
                Humidity = chunk.Humidity[(z << 4) + x],
                Temperature = chunk.Temperatures[(z << 4) + x],
            };
        }

        public byte[] GetChunkData(Chunk chunk)
        {
            byte[] data = new byte[(16 * 16 * 128 * 5) / 2];
            var offset = 0;

            Array.Copy(chunk.Blocks, 0, data, offset, chunk.Blocks.Length);
            offset += chunk.Blocks.Length;
            Array.Copy(chunk.BlockMetadatas, 0, data, offset, chunk.BlockMetadatas.Length);
            offset += chunk.BlockMetadatas.Length;
            /*Array.Copy(chunk.LightMap, 0, data, offset, chunk.LightMap.Length);
            offset += chunk.LightMap.Length;
            Array.Copy(skylightMap.data, 0, data, offset, skylightMap.data.length);
            offset += skylightMap.data.length;*/

            return data;
        }

        public byte GetBlockId(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.Blocks[x << 11 | z << 7 | y];
        }

        public byte GetBlockMetadata(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.BlockMetadatas[x << 11 | z << 7 | y];
        }

        public void SetHumidity(Chunk chunk, byte x, byte z, byte humidity)
        {
            chunk.Humidity[(z << 4) + x] = humidity;
        }

        public void SetTemperature(Chunk chunk, byte x, byte z, byte temperature)
        {
            chunk.Temperatures[(z << 4) + x] = temperature;
        }

        public void SetBlockId(Chunk chunk, byte x, byte y, byte z, byte id)
        {
            SetBlockIdAndMetadata(chunk, x, y, z, id, 0);
        }

        public void SetBlockIdAndMetadata(Chunk chunk, byte x, byte y, byte z, byte id, byte metadata)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
            var index = x << 11 | z << 7 | y;
            chunk.Blocks[index] = id;
            chunk.BlockMetadatas[index] = metadata;
        }

        public void Invalidate(Chunk chunk, byte y)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
        }

        public void Invalidate(Chunk chunk)
        {
            for (int i = 0; i < chunk.ChunkParts.Length; i++)
                chunk.ChunkParts[i].Invalidated = true;
        }

        private void SetSunlight(Chunk chunk, byte x, byte y, byte z, byte value)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
            var loc = x << 11 | z << 7 | y;
            chunk.LightMap[loc] = (byte)((chunk.LightMap[loc] & 0xf) | (value << 4));
        }

        private byte GetSunlight(Chunk chunk, byte x, byte y, byte z)
        {
            return (byte)((chunk.LightMap[x << 11 | z << 7 | y] >> 4) & 0xf);
        }

        private void SetTorchlight(Chunk chunk, byte x, byte y, byte z, byte value)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
            var loc = x << 11 | z << 7 | y;
            chunk.LightMap[loc] = (byte)((chunk.LightMap[loc] & 0xf0) | value);
        }

        private byte GetTorchlight(Chunk chunk, byte x, byte y, byte z)
        {
            return (byte)(chunk.LightMap[x << 11 | z << 7 | y] & 0xf);
        }
    }
}
