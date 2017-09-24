using System.Collections.Generic;
using NetCraft.Core.Network;
using NetCraft.Base.Worlds;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    class Packet60Explosion : IPacket
    {
        public int Size => 32 + DestroyedBlockPositions.Count * 3;

        public double ExplosionX { get; set; }
        public double ExplosionY { get; set; }
        public double ExplosionZ { get; set; }
        public float ExplosionSize { get; set; }
        public List<ChunkPosition> DestroyedBlockPositions { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            ExplosionX = stream.ReadDouble();
            ExplosionY = stream.ReadDouble();
            ExplosionZ = stream.ReadDouble();
            ExplosionSize = stream.ReadSingle();
            var blockCount = stream.ReadInt32();
            DestroyedBlockPositions = new List<ChunkPosition>(blockCount);
            var x = (int)ExplosionX;
            var y = (int)ExplosionY;
            var z = (int)ExplosionZ;
            for (int i = 0; i < blockCount; i++)
            {
                DestroyedBlockPositions.Add(new ChunkPosition
                {
                    X = stream.ReadByte() + x,
                    Y = stream.ReadByte() + y,
                    Z = stream.ReadByte() + z
                });
            }
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteDouble(ExplosionX);
            stream.WriteDouble(ExplosionY);
            stream.WriteDouble(ExplosionZ);
            stream.WriteSingle(ExplosionSize);
            stream.WriteInt32(DestroyedBlockPositions.Count);
            var x = (int)ExplosionX;
            var y = (int)ExplosionY;
            var z = (int)ExplosionZ;
            for (int i = 0; i < DestroyedBlockPositions.Count; i++)
            {
                stream.WriteByte((byte)(DestroyedBlockPositions[i].X - x));
                stream.WriteByte((byte)(DestroyedBlockPositions[i].Y - y));
                stream.WriteByte((byte)(DestroyedBlockPositions[i].Z - z));
            }
        }
    }
}
