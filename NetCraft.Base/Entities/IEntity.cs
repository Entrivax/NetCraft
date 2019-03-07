using NetCraft.Base.Worlds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCraft.Base.Entities
{
    interface IEntity
    {
        bool IsDead { get; }

        double PosX { get; }
        double PosY { get; }
        double PosZ { get; }

        int ChunkCoordX { get; }
        int ChunkCoordY { get; }
        int ChunkCoordZ { get; }

        void SetPosition(double x, double y, double z);
    }
}
