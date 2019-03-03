using System;

namespace NetCraft.Base.Worlds
{
    public struct ChunkPosition : IEquatable<ChunkPosition>
    {
        public int X { get; private set; }
        public int Z { get; private set; }

        public ChunkPosition(int x, int z)
        {
            X = x;
            Z = z;
        }

        public override bool Equals(object obj)
        {
            var other = (ChunkPosition)obj;
            return other.X == X && other.Z == Z;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() * 399 ^ Z.GetHashCode();
        }

        public bool Equals(ChunkPosition other)
        {
            return Equals((object)other);
        }
    }
}
