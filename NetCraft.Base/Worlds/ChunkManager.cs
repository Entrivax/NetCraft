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
                Id = chunk.Blocks[(y << 8) + x + (z << 4)],
                Metadata = chunk.BlockMetadatas[(y << 8) + x + (z << 4)],
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
            /*Array.Copy(data.data, 0, abyte0, offset, data.data.length);
            offset += data.data.length;
            System.arraycopy(blocklightMap.data, 0, abyte0, offset, blocklightMap.data.length);
            offset += blocklightMap.data.length;
            System.arraycopy(skylightMap.data, 0, abyte0, offset, skylightMap.data.length);
            offset += skylightMap.data.length;*/

            return data;
        }

        public byte GetBlockId(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.Blocks[(y << 8) + x + (z << 4)];
        }

        public byte GetBlockMetadata(Chunk chunk, byte x, byte y, byte z)
        {
            return chunk.BlockMetadatas[(y << 8) + x + (z << 4)];
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
            var index = (y << 8) + x + (z << 4);
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
            var loc = (y << 8) + x + (z << 4);
            chunk.LightMap[loc] = (byte)((chunk.LightMap[loc] & 0xf) | (value << 4));
        }

        private byte GetSunlight(Chunk chunk, byte x, byte y, byte z)
        {
            return (byte)((chunk.LightMap[(y << 8) + x + (z << 4)] >> 4) & 0xf);
        }

        private void SetTorchlight(Chunk chunk, byte x, byte y, byte z, byte value)
        {
            chunk.ChunkParts[y >> 4].Invalidated = true;
            var loc = (y << 8) + x + (z << 4);
            chunk.LightMap[loc] = (byte)((chunk.LightMap[loc] & 0xf0) | value);
        }

        private byte GetTorchlight(Chunk chunk, byte x, byte y, byte z)
        {
            return (byte)(chunk.LightMap[(y << 8) + x + (z << 4)] & 0xf);
        }
    }
}
