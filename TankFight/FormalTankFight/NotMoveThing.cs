using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{
    class NotMoveThing:GameObject
    {
        private Image img;
        public Image Img //需要进行碰撞检测，所以在传递图片的时候要保存每张图片的大小
        {
            get
            {
                return img; //注意对get，set方法编辑后，系统就不会自动创建Img变量，需要你自己创建返回值变量
            }
            set 
            {
                img = value;
                Width = img.Width;
                Height = img.Height;
            }
        }

        protected override Image GetImage()
        {
            return Img;
        }

        public NotMoveThing(int x, int y, Image image)//接收构造地图时的xy和图片，交给gameobject一起控制其他地形的绘制
        {
            this.X = x;
            this.Y = y;
            this.Img = image;
        }
    }
}
