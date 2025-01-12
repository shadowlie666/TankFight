using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankFight2._0
{
    class GameFrameWork
    {
        public static Graphics g;

        public static void Start()
        {
            GameObjectManager.CreateMyTank();
            GameObjectManager.CreateMap();
            GameObjectManager.TankPosition();
        }

        public static void Update()
        {
            GameObjectManager.Update();
        }

        
    }
}
