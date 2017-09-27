using NetCraft.Plugin;
using System;
using NetCraft.Network;
using NetCraft.Base.Packets;
using System.Collections.Generic;
using NetCraft.Core.Network;
using NetCraft.Base.Entities;
using NetCraft.Base.Handlers;
using NetCraft.Base.Events;

namespace NetCraft.Base
{
    public class Base : IPlugin
    {
        private Dictionary<Client, Player> _players;

        public void Load(Server server)
        {
            Console.WriteLine("Loading NetCraft server base");

            _players = new Dictionary<Client, Player>();

            server.PacketManager.RegisterPacketHandler<Packet2Handshake>(2, (client, packet) => {
                LoginHandler.ConnectClient(_players, client, packet, server.PluginManager);
            });
            server.PacketManager.RegisterPacketId<Packet2Handshake>(2);
            server.PacketManager.RegisterPacketId<Packet255KickDisconnect>(255);

            server.OnDisconnect += OnClientDisconnect;
            server.PluginManager.RegisterEventHandler(this);
        }

        private void OnClientDisconnect(object sender, Client client)
        {
            if (_players.ContainsKey(client))
            {
                Console.WriteLine($"Player {_players[client].Username} disconnected");
                _players.Remove(client);
            }
        }

        [EventListener(typeof(PlayerLoginEvent))]
        public void OnLogin(PlayerLoginEvent playerLoginEvent)
        {
            Console.WriteLine($"{playerLoginEvent.Player.Username} is trying to connect");
            playerLoginEvent.Cancel("Server not fully implemented");
        }

        public void Unload(Server server)
        {
            Console.WriteLine("Unloading NetCraft server base");
        }
    }
}
