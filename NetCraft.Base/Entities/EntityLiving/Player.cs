using NetCraft.Base.Entities.EntityLiving;
using NetCraft.Base.Worlds;
using NetCraft.Core.Network;
using NetCraft.Core.Packets;
using System.Collections.Generic;

namespace NetCraft.Base.Entities
{
    public class Player : IEntityLiving
    {
        public string Username { get; private set; }

        public bool IsDead { get; private set; }

        public List<ChunkPosition> LoadedChunk { get; private set; }

        public double PosX { get; private set; }
        public double PosY { get; private set; }
        public double PosZ { get; private set; }

        public int ChunkCoordX { get; private set; }
        public int ChunkCoordY { get; private set; }
        public int ChunkCoordZ { get; private set; }

        private Client _client;

        public Player(Client client, string username)
        {
            _client = client;
            Username = username;
            LoadedChunk = new List<ChunkPosition>();
            IsDead = false;
            PosX = 0.0;
            PosY = 0.0;
            PosZ = 0.0;
            ChunkCoordX = 0;
            ChunkCoordY = 0;
            ChunkCoordZ = 0;
        }

        public void SendPacket(IPacket packet)
        {
            _client.PendingPackets.Add(packet);
        }

        public void SetPosition(double x, double y, double z)
        {
            PosX = x;
            PosY = y;
            PosZ = z;
        }

        public void SetChunkPosition(int x, int y, int z)
        {
            ChunkCoordX = x;
            ChunkCoordY = y;
            ChunkCoordZ = z;
        }
    }
}
