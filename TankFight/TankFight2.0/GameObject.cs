using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankFight2._0
{
    class GameObject
    {
        public  int X { get; set; }
        public  int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Bitmap Image { get; set; }
        private static MyTank myTank ;

        public virtual void Update()
        {
            GameFrameWork.Update();
        }


    }
}
