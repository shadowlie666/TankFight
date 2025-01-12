using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankFight2._0
{
    class NotMoveThing:GameObject
    {
        

        public static List<NotMoveThing> wallListTotal = new List<NotMoveThing>();
        public static List<NotMoveThing> steelListTotal = new List<NotMoveThing>();
        public static List<NotMoveThing> bossListTotal = new List<NotMoveThing>();

        public NotMoveThing(int x,int y,Bitmap bitmap,int width,int height)
        {
            this.X = x;
            this.Y = y;
            this.Image = bitmap;
            this.Width = width;
            this.Height = height;
        }

        //public static int GetWidth(Bitmap bitmap)
        //{
        //    return bitmap.Width;
        //}

        //public static int GetHeight(Bitmap bitmap)
        //{
        //    return bitmap.Height;
        //}

        public static void Update(int x,int y,Bitmap bitmap)
        {
            GameFrameWork.g.DrawImage(bitmap, x, y);
        }

        public static void CreateWall(int x, int y, int count,Bitmap bitmap)
        {
            int X1 = x*30;
            int Y1 = y*30;
            int width;
            int height;

            for(int i=X1; i<X1+30; i+=15)
            {
                for(int j=Y1; j<Y1+30*count;j+=15)
                {
                    width = bitmap.Width;
                    height = bitmap.Height;
                    GameFrameWork.g.DrawImage(bitmap, i, j);
                    NotMoveThing wall1 = new NotMoveThing(i, j, bitmap,width,height);
                    wallListTotal.Add(wall1);
                }
            }
        }

        public static void CreateSteel(int x, int y, int count, Bitmap bitmap)
        {
            int X1 = x * 30;
            int Y1 = y * 30;
            int width;
            int height;

            for (int i = X1; i < X1 + 30; i += 15)
            {
                for (int j = Y1; j < Y1 + 30 * count; j += 15)
                {
                    width = bitmap.Width;
                    height = bitmap.Height;
                    GameFrameWork.g.DrawImage(bitmap, i, j);
                    NotMoveThing steel1 = new NotMoveThing(i, j, bitmap, width, height);
                    steelListTotal.Add(steel1);
                }
            }
        }

        public static void CreateBoss(int x,int y,Bitmap bitmap)
        {
            int X1 = x * 30;
            int Y1 = y * 30;
            int width = bitmap.Width;
            int height = bitmap.Height;
            GameFrameWork.g.DrawImage(bitmap, X1, Y1);
            NotMoveThing boss = new NotMoveThing(X1,Y1,bitmap,width,height);
            bossListTotal.Add(boss);

        }
    }
}
