using NetCraft.Base.Entities;
using NetCraft.Base.Events;
using NetCraft.Base.Packets;
using NetCraft.Core.Network;
using NetCraft.Plugin;
using System.Collections.Generic;

namespace NetCraft.Base.Handlers
{
    public static class LoginHandler
    {
        public static Player Handshake(Client client, Packet2Handshake packet, PluginManager pluginManager)
        {
            var player = new Player(client, packet.Username);

            var playerLoginEvent = new PlayerLoginEvent(player);
            pluginManager.BroadcastEvent(playerLoginEvent);
            if (playerLoginEvent.Cancelled)
            {
                player.SendPacket(new Packet255KickDisconnect(playerLoginEvent.Reason));
                return null;
            }
            player.SendPacket(new Packet2Handshake { Username = "-" }); // Send username with value of "-" tells to the client there is no authentication system
            return player;
        }

        public static void ConnectClient(Dictionary<Client, Player> players, Client client, Packet2Handshake packet, PluginManager pluginManager)
        {
            var player = Handshake(client, packet, pluginManager);
            if (player != null)
                players.Add(client, player);
        }

        public static void LoginPlayer(Player player, Client client, Packet1Login packet, PluginManager pluginManager)
        {
            //TO-DO Give Right Dimension Later
            //TO-DO Give Right ProtocolVersion (Player EntityID)
            player.SendPacket(new Packet1Login { ProtocolVersion = 17, Username = "", MapSeed = 0, Dimension = 0});
        }
    }
}
