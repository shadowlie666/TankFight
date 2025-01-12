using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TankFight2._0.Properties;

namespace TankFight2._0
{
    class Bullet : MoveThing //中心4*4大小子弹
    {
        Direction bulletDir;
        Bitmap bitmap;
        int tankwidth;
        int tankheight;
        public bool isDestory { get; set; }
        Explosion exp;
        EnemyTank enemyBullet = new EnemyTank(0, 0, Resources.Boss, Resources.Boss, Resources.Boss, Resources.Boss, 1);
        public Bitmap BitmapDown;
        public Bitmap BitmapUp;
        public Bitmap BitmapLeft;
        public Bitmap BitmapRight;
        public Tag Tag;


        public Bullet(int x,int y,int speed,Direction dir, Bitmap bitmapDown, Bitmap bitmapUp, Bitmap bitmapLeft, Bitmap bitmapRight,Tag tag)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            bulletDir = dir;
            BitmapUp = bitmapUp;
            BitmapDown = bitmapDown;
            BitmapLeft = bitmapLeft;
            BitmapRight = bitmapRight;
            Tag = tag;

        }

        public void UpdateBullet()
        {
            
            MoveCheck();
            MoveBullet();
        }

       

        public NotMoveThing IsCollidedWall()
        {
            int X1 = X + 9 ;
            int Y1 = Y + 9;
            int width = 4;
            int height = 4;
            //获取下一状态的xy坐标
            if (bulletDir == Direction.Up)
            {
                Y1 = Y1 - Speed;
            }
            else if (bulletDir == Direction.Down)
            {
                Y1 = Y1 + Speed;
            }
            else if (bulletDir == Direction.Left)
            {
                X1 = X1 - Speed;
            }
            else if (bulletDir == Direction.Right)
            {
                X1 = X1 + Speed;
            }
            Rectangle bullet = new Rectangle(X1, Y1, width, height);
            //看有没有和墙发生碰撞
            foreach (NotMoveThing wm in NotMoveThing.wallListTotal)
            {
                Rectangle wall = new Rectangle(wm.X, wm.Y, wm.Width, wm.Height);

                if (bullet.IntersectsWith(wall))
                {
                    return wm;
                }
            }
            return null;
        }

        public NotMoveThing IsCollidedSteel()
        {
            int X1 = X ;
            int Y1 = Y ;
            int width = 4;
            int height = 4;
            //获取下一状态的xy坐标
            if (bulletDir == Direction.Up)
            {
                Y1 = Y1 - Speed;
            }
            else if (bulletDir == Direction.Down)
            {
                Y1 = Y1 + Speed;
            }
            else if (bulletDir == Direction.Left)
            {
                X1 = X1 - Speed;
            }
            else if (bulletDir == Direction.Right)
            {
                X1 = X1 + Speed;
            }
            Rectangle bullet = new Rectangle(X1, Y1, width, height);
            //看有没有和墙发生碰撞
            foreach (NotMoveThing sm in NotMoveThing.steelListTotal)
            {
                Rectangle steel = new Rectangle(sm.X, sm.Y, sm.Width, sm.Height);

                if (bullet.IntersectsWith(steel))
                {
                    return sm;
                }
            }
            return null;
        }

        public EnemyTank IsCollidedEnemyTank()
        {
            int X1 = X;
            int Y1 = Y;
            int width = 4;
            int height = 4;
            //获取下一状态的xy坐标
            if (bulletDir == Direction.Up)
            {
                Y1 = Y1 - Speed;
            }
            else if (bulletDir == Direction.Down)
            {
                Y1 = Y1 + Speed;
            }
            else if (bulletDir == Direction.Left)
            {
                X1 = X1 - Speed;
            }
            else if (bulletDir == Direction.Right)
            {
                X1 = X1 + Speed;
            }

            Rectangle bullet = new Rectangle(X1, Y1, width, height);
            //看有没有和墙发生碰撞
            foreach (EnemyTank en in GameObjectManager.enemyTankListTotal)
            {
                Rectangle enemy = new Rectangle(en.X, en.Y, en.Width, en.Height);

                if (bullet.IntersectsWith(enemy))
                {
                    if(Tag == Tag.My)
                    {
                        return en;
                    }
                    
                }
            }
            return null;
        }

        public void DestoryBullet(NotMoveThing wall)
        {
            GameObjectManager.bulletListTotal.Remove(GameObjectManager.bullet);
            GameObjectManager.bulletListTotal.Remove(enemyBullet.bullet);
            NotMoveThing.wallListTotal.Remove(wall);
        }

        public void MoveCheck()
        {
            //不超出边界的检查
            switch (bulletDir)
            {
                case Direction.Up:
                    if (Y + 9 - Speed < 0)
                    {
                        GameObjectManager.bulletListTotal.Remove(GameObjectManager.bullet);
                        GameObjectManager.bulletListTotal.Remove(enemyBullet.bullet);
                    }
                    break;
                case Direction.Down:
                    if (Y + 13 + Speed > 450)
                    {
                        GameObjectManager.bulletListTotal.Remove(GameObjectManager.bullet);
                        GameObjectManager.bulletListTotal.Remove(enemyBullet.bullet);
                    }
                    break;
                case Direction.Left:
                    if (X + 9 - Speed < 0)
                    {
                        GameObjectManager.bulletListTotal.Remove(GameObjectManager.bullet);
                        GameObjectManager.bulletListTotal.Remove(enemyBullet.bullet);
                    }
                    break;
                case Direction.Right:
                    if (X + 13 + Speed > 450)
                    {
                        GameObjectManager.bulletListTotal.Remove(GameObjectManager.bullet);
                        GameObjectManager.bulletListTotal.Remove(enemyBullet.bullet);
                    }
                    break;
            }
            //与墙的碰撞检测
            NotMoveThing wall = IsCollidedWall();
            if (wall != null)
            {
                DestoryBullet(wall);
                exp = new Explosion(X, Y);
                exp.UpdateExplosion();
            }
            NotMoveThing steel = IsCollidedSteel();
            if (steel != null)
            {
                GameObjectManager.bulletListTotal.Remove(GameObjectManager.bullet);
            }
            EnemyTank enemyTank = IsCollidedEnemyTank();
            if(enemyTank != null)
            {
                GameObjectManager.enemyTankListTotal.Remove(enemyTank);
            }
        }

        

        public void MoveBullet()
        {
            switch (bulletDir)
            {
                case Direction.Up:
                    bitmap = BitmapUp;
                    bitmap.MakeTransparent(Color.Black);
                    Y -= Speed;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    bitmap.MakeTransparent(Color.Black);
                    Y += Speed;
                    break;
                case Direction.Left:
                    bitmap = BitmapLeft;
                    bitmap.MakeTransparent(Color.Black);
                    X -= Speed;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    bitmap.MakeTransparent(Color.Black);
                    X += Speed;
                    break;
            }
            GameFrameWork.g.DrawImage(bitmap, X, Y);
        }


    }
}
