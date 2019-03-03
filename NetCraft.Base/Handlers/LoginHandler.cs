using NetCraft.Base.Entities;
using NetCraft.Base.Events;
using NetCraft.Base.Packets;
using NetCraft.Base.Worlds;
using NetCraft.Core.Network;
using NetCraft.Plugin;
using System;
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

            ChunkManager chunkManager = new ChunkManager();
            ChunkGeneratorSurface chunkGeneratorSurface = new ChunkGeneratorSurface(chunkManager);
            var world = new World("Bite", new Random().Next());
            Chunk chunk = new Chunk();
            chunkGeneratorSurface.PopulateChunk(world, chunk, new ChunkPosition(0, 0), false);

            player.SendPacket(new Packet1Login { ProtocolVersion = 17, Username = "", MapSeed = 0, Dimension = 0});
            player.SendPacket(new Packet13PlayerLookMove { XPosition = 4, YPosition = 135, ZPosition = 3, Stance = 256+1.6200000047683716D, OnGround = true, Pitch = 0, Yaw = 0 });
            player.SendPacket(new Packet50PreChunk { XPosition = 0, YPosition = 0, Mode = true });
            player.SendPacket(new Packet51MapChunk
            {
                XPosition = 0,
                YPosition = 0,
                ZPosition = 0,
                XSize = 16,
                YSize = 128,
                ZSize = 16,
                Chunk = chunkManager.GetChunkData(chunk)
            });
        }
    }
}
