using FormalTankFight.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{
    class Explosion : GameObject
    {
        private int playSpeed = 1;//想要每两帧播放一个爆炸图片
        private int playCount = 0; //每次update刷新的时候都让计数器加1，找到计数器的值和每次刷新时应播放数组内哪张图片的关系
        private int index = 0;

        public bool isNeedDestory { get; set; }

        private Bitmap[] bmpArray = new Bitmap[]
        {
            Resources.EXP1,
            Resources.EXP2,
            Resources.EXP3,
             Resources.EXP4,
             Resources.EXP5
           
        };

        public Explosion(int x, int y)
        {
            foreach(Bitmap bmp in bmpArray)
            {
                bmp.MakeTransparent(Color.Black);
            }
            this.X = x - bmpArray[0].Width / 2;
            this.Y = y - bmpArray[0].Height / 2;
            isNeedDestory = false;
        }

        protected override Image GetImage()
        {
            if(index>4)
            {
                return bmpArray[4];
            }
            
            return bmpArray[index];
        }

        public override void Update()//每次刷新时计数器加一，同时返回当前需要播放数组内的哪张图片
        {
            playCount++;
            int index = (playCount - 1) / playSpeed;

            if(index>4)
            {
                isNeedDestory = true;
            }

            base.Update();
        }
    }
}
