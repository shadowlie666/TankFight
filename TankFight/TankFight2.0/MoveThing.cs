using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankFight2._0
{
    class MoveThing:GameObject
    {
        public enum Direction { Up, Down, Left, Right }
        public enum Tag { Enemy,My}
        public int Speed { get; set; }
        public Direction Dir { get; set; }
    }
}
