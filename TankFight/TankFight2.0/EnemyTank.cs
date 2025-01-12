using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TankFight2._0.Properties;

namespace TankFight2._0
{
    enum Flag { Fast, Slow, Yellow, Green}

    class EnemyTank:MoveThing
    {
        public Bitmap BitmapDown;
        public Bitmap BitmapUp; 
        public Bitmap BitmapLeft; 
        public Bitmap BitmapRight;

        Random random = new Random();
        private int changeDirCount = 0;
        private int attackCount = 30;
        public Bullet bullet;
        public Tag tag;

        public EnemyTank(int x,int y,Bitmap bitmapDown,Bitmap bitmapUp, Bitmap bitmapLeft, Bitmap bitmapRight, int speed)
        {
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            BitmapUp = bitmapUp;
            BitmapDown = bitmapDown;
            BitmapLeft = bitmapLeft;
            BitmapRight = bitmapRight;
            this.Dir = Direction.Down;
            tag = Tag.Enemy;
            this.Image = bitmapDown;
        }



        public  void UpdateEnemyTank()
        {
            MoveCheck();
            MoveEnemyTank();
            Attack();
            AutoChangeDirection();
        }

        public int GetWidth(Bitmap bitmap)
        {
            return bitmap.Width;
        }

        public int GetHeight(Bitmap bitmap)
        {
            return bitmap.Height;
        }
        
        public void AutoChangeDirection()
        {
            if(changeDirCount<60)
            {
                changeDirCount++;
            }
            else if(changeDirCount == 60)
            {
                RandomChangeDirection();
                changeDirCount++;
            }
            else if(changeDirCount>60)
            {
                changeDirCount = 0;
            }
        }

        public void Attack()
        {
            if(attackCount<60)
            {
                attackCount++;
            }
            else if(attackCount==60)
            {
                GameObjectManager.CreateBullet(X, Y, Dir, GetWidth(Image), GetHeight(Image),Tag.Enemy);
                //bullet = new Bullet(X, Y, 7, Dir, GetWidth(Image), GetHeight(Image));
                //GameObjectManager.bulletListTotal.Add(bullet);
                attackCount++;
            }
            else if(attackCount>60)
            {
                attackCount = 0;
            }
        }

        public void RandomChangeDirection()
        {  
            while(true)
            {
                int i = random.Next(0, 4);
                Direction dir = (Direction)i;
                if (Dir == dir)
                {
                    continue;
                }
                else
                {
                    Dir = dir;
                    break;
                }
            }

            MoveCheck();
        }
        

        public bool IsCollidedWall()
        {
            int X1 = 0;
            int Y1 = 0;
            int width = GetWidth(Image);
            int height = GetHeight(Image);
            
                //获取下一状态的xy坐标
                if (Dir == Direction.Up)
                {
                    X1 = X;
                    Y1 = Y - Speed;
                }
                else if (Dir == Direction.Down)
                {
                    X1 = X;
                    Y1 = Y + Speed;
                }
                else if (Dir == Direction.Left)
                {
                    X1 = X - Speed;
                    Y1 = Y;
                }
                else if (Dir == Direction.Right)
                {
                    X1 = X + Speed;
                    Y1 = Y;
                }
                Rectangle enemytank = new Rectangle(X1, Y1, width, height);
                //看有没有和墙发生碰撞
                foreach (NotMoveThing wm in NotMoveThing.wallListTotal)
                {
                    Rectangle wall = new Rectangle(wm.X, wm.Y, wm.Width, wm.Height);

                    if (enemytank.IntersectsWith(wall))
                    {
                        return true;
                    }
                }
            return false;
        }

        public bool IsCollidedSteel()
        {
            int X1 = 0;
            int Y1 = 0;
            int width = GetWidth(Image);
            int height = GetHeight(Image);

            //获取下一状态的xy坐标
            if (Dir == Direction.Up)
            {
                X1 = X;
                Y1 = Y - Speed;
            }
            else if (Dir == Direction.Down)
            {
                X1 = X;
                Y1 = Y + Speed;
            }
            else if (Dir == Direction.Left)
            {
                X1 = X - Speed;
                Y1 = Y;
            }
            else if (Dir == Direction.Right)
            {
                X1 = X + Speed;
                Y1 = Y;
            }
            Rectangle enemytank = new Rectangle(X1, Y1, width, height);
                //看有没有和墙发生碰撞
                foreach (NotMoveThing sm in NotMoveThing.steelListTotal)
                {
                    Rectangle steel = new Rectangle(sm.X, sm.Y, sm.Width, sm.Height);

                    if (enemytank.IntersectsWith(steel))
                    {
                        return true;
                    }
                }
            
            return false;
        }

        public  void MoveCheck()
        {
                //不超出边界的检查
                switch (Dir)
                {
                    case Direction.Up:
                        if (Y - Speed < 0)
                        {
                            RandomChangeDirection();
                        }
                        break;
                    case Direction.Down:
                        int height1 = GetHeight(Image);
                        if (Y + height1 + Speed > 450)
                        {
                            RandomChangeDirection();
                        }
                        break;
                    case Direction.Left:
                        if (X - Speed < 0)
                        {
                            RandomChangeDirection();
                        }
                        break;
                    case Direction.Right:
                        int width1 = GetWidth(Image);
                        if (X + width1 + Speed > 450)
                        {
                            RandomChangeDirection();
                        }
                        break;
                }
                //碰撞检测
                if (IsCollidedWall())
                {
                    RandomChangeDirection();
                    return;
                }
                if (IsCollidedSteel())
                {
                    RandomChangeDirection();
                    return;
                }
        }

        public Bitmap GetImage()
        {
            Bitmap bitmap = null;
            switch(Dir)
            {
                case Direction.Up:
                    bitmap = BitmapUp;
                    break;
                case Direction.Down:
                    bitmap = BitmapDown;
                    break;
                case Direction.Left:
                    bitmap = BitmapLeft;
                    break;
                case Direction.Right:
                    bitmap = BitmapRight;
                    break;
            }
            return bitmap;
        }

        public  void MoveEnemyTank()
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
                GameFrameWork.g.DrawImage(GetImage(), X,Y);
            
        }
    }
}
