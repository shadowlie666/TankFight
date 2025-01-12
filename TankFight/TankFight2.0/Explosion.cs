using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankFight2._0.Properties;

namespace TankFight2._0
{

    class Explosion   //一秒播放完5张图片
    {
        int X;
        int Y;
        Bitmap bitmap;
        int count = 60;
        int xuhao = 0;

        public Explosion(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void DrawExplosion(int i)
        {
            switch(i)
            {
                case 0:
                    bitmap = Resources.EXP1;
                    bitmap.MakeTransparent(Color.Black);
                    GameFrameWork.g.DrawImage(bitmap, X, Y);
                    break;
                case 1:
                    bitmap = Resources.EXP2;
                    bitmap.MakeTransparent(Color.Black);
                    GameFrameWork.g.DrawImage(bitmap, X, Y);
                    break;
                case 2:
                    bitmap = Resources.EXP3;
                    bitmap.MakeTransparent(Color.Black);
                    GameFrameWork.g.DrawImage(bitmap, X, Y);
                    break;
                case 3:
                    bitmap = Resources.EXP4;
                    bitmap.MakeTransparent(Color.Black);
                    GameFrameWork.g.DrawImage(bitmap, X, Y);
                    break;
                case 4:
                    bitmap = Resources.EXP5;
                    bitmap.MakeTransparent(Color.Black);
                    GameFrameWork.g.DrawImage(bitmap, X, Y);
                    break;

            }
        }

        public void UpdateExplosion()
        {
            

            if(count<60)
            {
                count++;
            }
            else if(count == 60)
            {
                DrawExplosion(xuhao);
                xuhao++;
            }
            else if(count>60)
            {
                count = 0;
            }

            if(xuhao == 5)
            {
                xuhao = 0;
            }
            

        }

        
    }
}
