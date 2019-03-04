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


            server.PacketManager.RegisterPacketHandler<Packet0KeepAlive>(0, (client, packet) => {
                _players[client].SendPacket(new Packet0KeepAlive());
            });

            server.PacketManager.RegisterPacketHandler<Packet1Login>(1, (client, packet) => {
                LoginHandler.LoginPlayer(_players[client], client, packet, server.PluginManager);
            });

            server.PacketManager.RegisterPacketHandler<Packet2Handshake>(2, (client, packet) => {
                LoginHandler.ConnectClient(_players, client, packet, server.PluginManager);
            });

            server.PacketManager.RegisterPacketHandler<Packet10Flying>(10, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet11PlayerPosition>(11, (client, packet) => {
                _players[client].SendPacket(new Packet0KeepAlive());
            });

            server.PacketManager.RegisterPacketHandler<Packet12PlayerLook>(12, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet13PlayerLookMove>(13, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet14BlockDig>(14, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet18Animation>(18, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet100OpenWindow>(100, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet101CloseWindow>(101, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet102WindowClick>(102, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet103SetSlot>(103, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet104WindowItems>(104, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet105UpdateProgressBar>(105, (client, packet) => {

            });

            server.PacketManager.RegisterPacketHandler<Packet106Transaction>(106, (client, packet) => {

            });

            server.PacketManager.RegisterPacketId<Packet0KeepAlive>(0);
            server.PacketManager.RegisterPacketId<Packet1Login>(1);
            server.PacketManager.RegisterPacketId<Packet2Handshake>(2);
            server.PacketManager.RegisterPacketId<Packet4UpdateTime>(4);
            server.PacketManager.RegisterPacketId<Packet10Flying>(10);
            server.PacketManager.RegisterPacketId<Packet12PlayerLook>(12);
            server.PacketManager.RegisterPacketId<Packet13PlayerLookMove>(13);
            server.PacketManager.RegisterPacketId<Packet14BlockDig>(14);
            server.PacketManager.RegisterPacketId<Packet18Animation>(18);
            server.PacketManager.RegisterPacketId<Packet50PreChunk>(50);
            server.PacketManager.RegisterPacketId<Packet51MapChunk>(51);
            server.PacketManager.RegisterPacketId<Packet100OpenWindow>(100);
            server.PacketManager.RegisterPacketId<Packet101CloseWindow>(101);
            server.PacketManager.RegisterPacketId<Packet102WindowClick>(102);
            server.PacketManager.RegisterPacketId<Packet103SetSlot>(103);
            server.PacketManager.RegisterPacketId<Packet104WindowItems>(104);
            server.PacketManager.RegisterPacketId<Packet105UpdateProgressBar>(105);
            server.PacketManager.RegisterPacketId<Packet106Transaction>(106);
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
            //playerLoginEvent.Cancel("Server not fully implemented");
        }


        public void Unload(Server server)
        {
            _logger.Info("Unloading");
        }
    }
}
