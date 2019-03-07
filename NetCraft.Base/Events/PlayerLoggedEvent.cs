using NetCraft.Base.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NetCraft.Base.Events
{
    public class PlayerLoggedEvent
    {
        public Player Player { get; private set; }

        public PlayerLoggedEvent(Player player)
        {
            Player = player;
        }
    }
}