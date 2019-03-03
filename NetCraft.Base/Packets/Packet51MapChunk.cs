using NetCraft.Core.Network;
using ICSharpCode.SharpZipLib.Zip.Compression;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet51MapChunk : IPacket
    {
        public int Size => 17 + Chunk?.Length ?? 0;

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int XSize { get; set; }
        public int YSize { get; set; }
        public int ZSize { get; set; }
        public byte[] Chunk { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            XPosition = stream.ReadInt32();
            YPosition = stream.ReadInt16();
            ZPosition = stream.ReadInt32();
            XSize = stream.ReadByte() + 1;
            YSize = stream.ReadByte() + 1;
            ZSize = stream.ReadByte() + 1;
            var chunkSize = stream.ReadInt32();
            var data = stream.ReadUInt8Array(chunkSize);
            Chunk = new byte[(XSize * YSize * ZSize * 5) / 2];
            Inflater inflater = new Inflater();
            inflater.SetInput(data);
            inflater.Inflate(Chunk);
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteInt32(XPosition);
            stream.WriteInt16((short)YPosition);
            stream.WriteInt32(ZPosition);
            stream.WriteByte((byte)(XSize - 1));
            stream.WriteByte((byte)(YSize - 1));
            stream.WriteByte((byte)(ZSize - 1));
            var chunk = new byte[(16 * 16 * 128 * 5) / 2];
            Deflater deflater = new Deflater(-1);
            deflater.SetInput(Chunk);
            deflater.Finish();
            deflater.Deflate(chunk);
            deflater.Flush();
            stream.WriteInt32(chunk.Length);
            stream.WriteUInt8Array(chunk, 0, chunk.Length);
        }
    }
}
