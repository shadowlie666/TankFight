using FormalTankFight.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormalTankFight
{
    class EnemyTank:MoveThing
    {
        public int ChangeDirSpeed { get; set; }
        private int ChangeDirCount = 0;
        public int attackSpeed { get; set; }
        private int attackCount = 0;
        Random r = new Random();

         public EnemyTank(int x, int y, int speed,Bitmap bmpDown,Bitmap bmpUp,Bitmap bmpLeft,Bitmap bmpRight)//构造方法
        {
            
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            //要先设置图片，再设置方向，因为movething要取得图片的长宽是在dir里面取得的，
            //而取得长宽需要有图片，所以要在dir运行前就把图片准备好
            BitmapDown = bmpDown;
            BitmapUp = bmpUp;
            BitmapLeft = bmpLeft;
            BitmapRight = bmpRight;
            this.Dir = Direction.Down;
            this.attackSpeed = 60;
            this.ChangeDirSpeed = 70;

        }
        public void MoveCheck()
        {
            //检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    ChangeDirection(); 
                    return;
                }
            }
            //注意这里的构图，一个墙四张图片，是从左上角开始构造的，是以左上角为坐标顶点画图的
            //所以下移时判断有无超出边界要算上坦克自身的长度，不能只用左上角顶点判断
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    ChangeDirection(); ;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    ChangeDirection();
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    ChangeDirection();
                    return;
                }
            }

            //碰撞检测
            Rectangle rect = GetRectangle();
            //用于获取下一状态的位置，用下一状态的位置来进行碰撞检测
            switch (Dir)
            {
                case Direction.Up:
                    rect.Y -= Speed;
                    break;
                case Direction.Down:
                    rect.Y += Speed;
                    break;
                case Direction.Left:
                    rect.X -= Speed;
                    break;
                case Direction.Right:
                    rect.X += Speed;
                    break;
            }

            if (GameObjectManager.IsCollidedWall(rect) != null) //检测是否与墙发生碰撞
            {
                ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null) //检测是否与刚墙发生碰撞
            {
                ChangeDirection();
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect)) //检测是否与刚墙发生碰撞
            {
                ChangeDirection();
                return;
            }
        }

        private void Attack() //保证子弹每次都能从坦克前进方向的正中间射出
        {
            int x = this.X;
            int y = this.Y;

            switch (Dir) //注意子弹的图片也是有大小的，不要把子弹当成一个点了
            {
                case Direction.Up:
                    x = x + Width / 2; //width是长，height是高
                    break;
                case Direction.Down:
                    x = x + Width / 2;
                    y += Height;
                    break;
                case Direction.Left:
                    y = y + Height / 2;
                    break;
                case Direction.Right:
                    x += Width;
                    y = y + Height / 2;
                    break;

            }

            GameObjectManager.CreateBullet(x, y, Tag.EnemyTank, Dir);
        }

        public override void Update()
        {
            MoveCheck();
            Move();
            AttackCheck();
            AutoChangeDirection();

            base.Update();
        }



        private void AttackCheck()
        {
            attackCount++;
            if(attackCount<attackSpeed)
            {
                return;
            }
            else
            {
                Attack();
                attackCount = 0;
            }
        }

        private void AutoChangeDirection() //随机转向
        {
            ChangeDirCount++;
            if(ChangeDirCount<ChangeDirSpeed)
            {
                return;
            }
            else
            {
                ChangeDirection();
                ChangeDirCount = 0;
            }
        }

        private void ChangeDirection()
        {
           while(true)
            {
                Direction dir = (Direction)r.Next(0, 4); //结构体类型变量也是类似数组的，结构体里面的变量也可以通过下标访问
                if(dir == Dir) //判断是否与当前方向一致，不一致才可以换方向
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
