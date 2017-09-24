using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Entities
{
    public class Player
    {
        public string Username { get; private set; }

        private Client _client;

        public Player(Client client, string username)
        {
            _client = client;
            Username = username;
        }

        public void SendPacket(IPacket packet)
        {
            _client.PendingPackets.Add(packet);
        }
    }
}
