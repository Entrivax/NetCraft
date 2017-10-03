using NetCraft.Plugin;
using System;
using NetCraft.Network;
using NetCraft.Base.Packets;
using System.Collections.Generic;
using NetCraft.Core.Network;
using NetCraft.Base.Entities;
using NetCraft.Base.Handlers;
using NetCraft.Base.Events;
using NetCraft.Logging;

namespace NetCraft.Base
{
    public class Base : IPlugin
    {
        private Dictionary<Client, Player> _players;

        public string Author => "Entrivax";
        public string Description => "Base implementation of NetCraft";
        public string Name => "NetCraft Base";
        public string Version => "0.0.1";

        private Logger _logger;

        public void Load(Server server)
        {
            _logger = server.GetLogger(this);
            _logger.Info("Loading");

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
                _logger.Info($"Player {_players[client].Username} disconnected");
                _players.Remove(client);
            }
        }

        [EventListener]
        public void OnLogin(PlayerLoginEvent playerLoginEvent)
        {
            _logger.Info($"{playerLoginEvent.Player.Username} is trying to connect");
            playerLoginEvent.Cancel("Server not fully implemented");
        }

        public void Unload(Server server)
        {
            _logger.Info("Unloading");
        }
    }
}
