using FormalTankFight.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{
    enum Tag //用于识别是我方的子弹还是敌人的子弹
    {
        MyTank,
        EnemyTank
    }

    class Bullet:MoveThing
    {
        public Tag Tag { get;set; }

        public bool IsDestory { get; set; }

        public Bullet(int x, int y, int speed, Direction dir, Tag tag)//构造方法
        {
            IsDestory = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapDown = Resources.BulletDown;
            BitmapUp = Resources.BulletUp;
            BitmapLeft = Resources.BulletLeft;
            BitmapRight = Resources.BulletRight;
            this.Dir = dir;
            this.Tag = tag;

            this.X -= Width/2;
            this.Y -= Height/2;
        }

      

        public void MoveCheck()
        {
            //超出窗体边界时销毁子弹
            if (Dir == Direction.Up)
            {
                if (Y + Height/2 -3< 0)//子弹中心高度为6
                {
                    IsDestory = true;
                    return;
                }
            }
            //注意这里的构图，一个墙四张图片，是从左上角开始构造的，是以左上角为坐标顶点画图的
            //所以下移时判断有无超出边界要算上坦克自身的长度，不能只用左上角顶点判断
            else if (Dir == Direction.Down)
            {
                if (Y + Height/2 +3 > 450)
                {
                    IsDestory = true;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X + Width / 2 - 3 < 0)
                {
                    IsDestory = true;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Width / 2 + 3 > 450)
                {
                    IsDestory = true;
                    return;
                }
            }

            //碰撞检测
            Rectangle rect = GetRectangle();
            //用于获取下一状态的位置，用下一状态的位置来进行碰撞检测

            rect.X = X + Width / 2 - 3;
            rect.Y = Y + Height / 2 - 3;
            rect.Width = 3;
            rect.Height = 3;

            int xExplosion = this.X + Width / 2;
            int yExplosion = this.Y + Height / 2;

            NotMoveThing wall = null;
            if ((wall = GameObjectManager.IsCollidedWall(rect)) != null) //与墙发生碰撞
            {
                IsDestory = true;
                GameObjectManager.DestoryWall(wall);
                GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                SoundManger.PlayBlast();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null) //与刚墙发生碰撞
            {
                IsDestory = true;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect)) //与Boss发生碰撞
            {
                GameFramework.ChangeToGameOver();
                SoundManger.PlayBlast();
                return;
            }

            if(Tag == Tag.MyTank) //子弹打到我的坦克
            {
                EnemyTank tank = null;
                if ((tank = GameObjectManager.IsCollidedEnemyTank(rect)) != null) 
                {
                    IsDestory = true;
                    GameObjectManager.DestoryTank(tank);
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    SoundManger.PlayHit();
                    return;
                }
            }
            else if(Tag == Tag.EnemyTank) //子弹打到敌方坦克
            {
                MyTank mytank = null;
                if((mytank = GameObjectManager.IsCollidedMytank(rect)) != null)
                {
                    IsDestory = true;
                    GameObjectManager.CreateExplosion(xExplosion, yExplosion);
                    SoundManger.PlayBlast();
                    mytank.TakeDamage();
                }
            }
        }

        public override void Update()
        {
            MoveCheck();
            Move();

            base.Update();
        }

        public void Move()
        {

            switch (Dir)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }

        }


    }
}
