﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Worlds
{
    public class Chunk
    {
        public byte[] Blocks { get; }
        public byte[] BlockMetadatas { get; }
        public byte[] Temperatures { get; }
        public byte[] Humidity { get; }
        public byte[] LightMap { get; }
        public ChunkPart[] ChunkParts { get; }

        public Chunk()
        {
            Blocks = new byte[16 * 16 * 128];
            BlockMetadatas = new byte[16 * 16 * 128];
            Temperatures = new byte[16 * 16];
            Humidity = new byte[16 * 16];
            var chunkPartsNumber = 128 / 16;
            ChunkParts = new ChunkPart[chunkPartsNumber];
            for (int i = 0; i < chunkPartsNumber; i++)
            {
                ChunkParts[i] = new ChunkPart();
            }
        }

        public struct ChunkBlockInformation
        {
            public byte Humidity { get; set; }
            public byte Temperature { get; set; }
            public byte Id { get; set; }
            public byte Metadata { get; set; }
        }

        public class ChunkPart
        {
            public bool Invalidated { get; set; }
        }
    }
}