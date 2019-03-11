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
                Metadata = (byte)chunk.BlockMetadatas.GetNibble(x, y, z),
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
            Array.Copy(chunk.BlockMetadatas.Data, 0, data, offset, chunk.BlockMetadatas.Data.Length);
            offset += chunk.BlockMetadatas.Data.Length;
            Array.Copy(chunk.LightMap.Data, 0, data, offset, chunk.LightMap.Data.Length);
            offset += chunk.LightMap.Data.Length;
            Array.Copy(chunk.SunLightMap.Data, 0, data, offset, chunk.SunLightMap.Data.Length);
            offset += chunk.SunLightMap.Data.Length;

            return data;
        }

        public byte GetBlockId(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.Blocks[x << 11 | z << 7 | y];
        }

        public byte GetBlockMetadata(Chunk chunk, byte x, byte y, byte z)
        {
            return (byte)chunk.BlockMetadatas.GetNibble(x, y, z);
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
            chunk.BlockMetadatas.SetNibble(x, y, z, metadata);
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

        public void SetSunlight(Chunk chunk, byte x, byte y, byte z, byte value)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
            chunk.SunLightMap.SetNibble(x, y, z, (byte)(value & 0xf));
        }

        private byte GetSunlight(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.SunLightMap.GetNibble(x, y, z);
        }

        private void SetTorchlight(Chunk chunk, byte x, byte y, byte z, byte value)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
            chunk.LightMap.SetNibble(x, y, z, (byte)(value & 0xf));
        }

        private byte GetTorchlight(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.LightMap.GetNibble(x, y, z);
        }
    }
}
