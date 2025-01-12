using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{

    
    enum Direction
    {
        Up,Down,Left,Right
    }

    class MoveThing:GameObject
    {

        //加载坦克移动方向的图片
        public Bitmap BitmapUp { get; set; }
        public Bitmap BitmapDown { get; set; }
        public Bitmap BitmapLeft { get; set; }
        public Bitmap BitmapRight { get; set; }
        
        //设置坦克移动速度
        public int Speed { get; set; }

        private Object _lock = new Object();//Object为任意类型，就是可以是类对象函数对象或一般对象


        //因为movething每个元素有四张图片，坦克在不同方向时，他的长宽不一样（坦克不是正方形）
        //所以坦克的长宽要根据其运动的方向来决定
        private Direction dir;
        public Direction Dir 
        {
            get
            {
                return dir;
            } 
            set
            {
                dir = value;
                Bitmap bmp = null;
               
                    switch (dir) //先通过dir获取当前图片，再找图片的长宽
                    {
                        case Direction.Up:
                            bmp = BitmapUp;
                            break;
                        case Direction.Down:
                            bmp = BitmapDown;
                            break;
                        case Direction.Left:
                            bmp = BitmapLeft;
                            break;
                        case Direction.Right:
                            bmp = BitmapRight;
                            break;
                    }
               lock(_lock)
                {
                    Width = bmp.Width;
                    Height = bmp.Height;
                }
                
            }
        } 

       
        
        protected override Image GetImage()//获取movething的图片
        {

            Bitmap bitmap = null;
            switch(Dir) //movething的东西需要根据方向来判断返回那个图片
            {
                case Direction.Up: 
                    bitmap =  BitmapUp; //不直接返回图片，需要设置图片透明度
                    break; //终止switch
                case Direction.Down: 
                    bitmap  = BitmapDown;
                    break;
                case Direction.Left: 
                    bitmap  = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap =  BitmapRight;
                    break;
            }

            bitmap.MakeTransparent(Color.Black); //设置图片透明度
            return bitmap;
        }

        public override void Drawself()
        {
            lock(_lock)
            {
                base.Drawself();
            }
            
        }
    }
}
