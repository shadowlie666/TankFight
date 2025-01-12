using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{
    abstract class GameObject
    {
        

        public int X { get; set; } //相当于get{return x}，set{value = x}
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        //用于获得坦克图片的一个抽象方法，让每个子类坦克去重写这个方法，因为不同坦克的图片不一样
        protected abstract Image GetImage();
      

        public virtual void Drawself()
        {
            Graphics g = GameFramework.g;

            g.DrawImage(GetImage(), X, Y); //在画布上画坦克
        }

        public virtual void Update()
        {
            Drawself();
        }

        public Rectangle GetRectangle()
        {
            Rectangle rectangle = new Rectangle(X, Y, Width, Height);
            return rectangle;
        }
    }
}
