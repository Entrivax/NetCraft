using NetCraft.Base.Entities;
using NetCraft.Base.Packets;
using NetCraft.Core.Network;
using System;
using System.Collections.Generic;

namespace NetCraft.Base.Handlers
{
    public static class LoginHandler
    {
        public static Player Handshake(Client client, Packet2Handshake packet)
        {
            var player = new Player(packet.Username);

            client.PendingPackets.Add(new Packet2Handshake { Username = "-" }); // Send username with value of "-" tells to the client there is no authentication system
            return player;
        }

        public static void ConnectClient(Dictionary<Client, Player> players, Client client, Packet2Handshake packet)
        {
            var player = Handshake(client, packet);
            Console.WriteLine($"Player {player.Username} connecting");
            players.Add(client, player);
        }
    }
}
