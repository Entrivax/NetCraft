using NetCraft.Core.Network;
using NetCraft.Core.Packets;

namespace NetCraft.Base.Packets
{
    public class Packet27Position : IPacket
    {
        public int Size => 18;

        public float StrafeMovement { get; set; }
        public float ForwardMovement { get; set; }
        public bool IsSneaking { get; set; }
        public bool IsInJump { get; set; }
        public float PitchRotation { get; set; }
        public float YawRotation { get; set; }

        public void ReadPacketData(JavaDataStream stream)
        {
            StrafeMovement = stream.ReadSingle();
            ForwardMovement = stream.ReadSingle();
            PitchRotation = stream.ReadSingle();
            YawRotation = stream.ReadSingle();
            IsSneaking = stream.ReadBoolean();
            IsInJump = stream.ReadBoolean();
        }

        public void WritePacketData(JavaDataStream stream)
        {
            stream.WriteSingle(StrafeMovement);
            stream.WriteSingle(ForwardMovement);
            stream.WriteSingle(PitchRotation);
            stream.WriteSingle(YawRotation);
            stream.WriteBoolean(IsSneaking);
            stream.WriteBoolean(IsInJump);
        }
    }
}
