using FormalTankFight.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormalTankFight
{
    class MyTank:MoveThing
    {
        public bool IsMoving { get; set; }
        public int HP { get; set; }
        private int originX;
        private int originY;


        public MyTank(int x, int y, int speed)//构造方法
        {
            IsMoving = false;
            this.X = x;
            this.Y = y;
            this.Speed = speed;
            //要先设置图片，再设置方向，因为movething要取得图片的长宽是在dir里面取得的，
            //而取得长宽需要有图片，所以要在dir运行前就把图片准备好
            BitmapDown = Resources.MyTankDown;
            BitmapUp = Resources.MyTankUp;
            BitmapLeft = Resources.MyTankLeft;
            BitmapRight = Resources.MyTankRight;
            this.Dir = Direction.Up; //默认出生时方向向上
            this.HP =4;
            originX = x;
            originY = y;

        }

        public void TakeDamage()
        {
            HP--;

            if(HP<1)
            {
                X = originX;
                Y = originY;
                HP = 4;
            }
        }

        //这里的这个方法只需要调用别的类的对象，不需要调用别的类的方法，所以不需要使用静态方法
        public void KeyDown(KeyEventArgs args) 
        {
            switch(args.KeyCode) //判断按下的按键是什么,返回值是一个keys.类型
            {
                case Keys.W:
                    Dir = Direction.Up;
                    IsMoving = true;
                    break;
                case Keys.A:
                    Dir = Direction.Left;
                    IsMoving = true;
                    break;
                case Keys.S:
                    Dir = Direction.Down;
                    IsMoving = true;
                    break;
                case Keys.D:
                    Dir = Direction.Right;
                    IsMoving = true;
                    break;
                case Keys.Space:

                    Attack();
                    
                    
                    break;
            }
        }

        private void Attack() //保证子弹每次都能从坦克前进方向的正中间射出
        {
            SoundManger.PlayFire();
            int x = this.X;
            int y = this.Y;

            switch(Dir) //注意子弹的图片也是有大小的，不要把子弹当成一个点了
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

            GameObjectManager.CreateBullet(x, y, Tag.MyTank, Dir);
        }

        public void KeyUp(KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.W:
                    IsMoving = false;
                    break;
                case Keys.A:
                    IsMoving = false;
                    break;
                case Keys.S:
                    IsMoving = false;
                    break;
                case Keys.D:
                    IsMoving = false;
                    break;
            }
        }

        #region 碰撞检测
        public void MoveCheck()
        {
            //检查有没有超出窗体边界
            if (Dir == Direction.Up)
            {
                if (Y - Speed < 0)
                {
                    IsMoving = false; //当ismoveing为false时就不会移动了
                    return;
                }
            }
            //注意这里的构图，一个墙四张图片，是从左上角开始构造的，是以左上角为坐标顶点画图的
            //所以下移时判断有无超出边界要算上坦克自身的长度，不能只用左上角顶点判断
            else if (Dir == Direction.Down)
            {
                if (Y + Speed + Height > 450)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Left)
            {
                if (X - Speed < 0)
                {
                    IsMoving = false;
                    return;
                }
            }
            else if (Dir == Direction.Right)
            {
                if (X + Speed + Width > 450)
                {
                    IsMoving = false;
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
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedSteel(rect) != null) //检测是否与刚墙发生碰撞
            {
                IsMoving = false;
                return;
            }
            if (GameObjectManager.IsCollidedBoss(rect)) //检测是否与刚墙发生碰撞
            {
                IsMoving = false;
                return;
            }
        }
        #endregion

        public override void Update()
        {
            MoveCheck();
            Move();

            base.Update();
        }

        public void Move()
        {
            if (IsMoving == false)
                return;
            else
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
}  
