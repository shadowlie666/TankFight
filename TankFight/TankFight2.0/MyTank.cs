using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankFight2._0.Properties;

namespace TankFight2._0
{
    class MyTank : MoveThing
    {
        

        public Direction direction;
        public bool isMoving { get; set; }
        public Bitmap bitmap;
        public Tag Tag;
        

        public MyTank(int x, int y,  int speed, Tag tag)
        {
            isMoving = false;
            this.X = x;
            this.Y = y;
            //this.Width= width;
            //this.Height = height;
            //this.Image = bitmap;
            this.Speed = speed;
            direction = Direction.Up;
            bitmap = Resources.MyTankUp;
            Tag = tag;
        }

        public void UpdateMytank()
        {
            MoveCheck();
            MoveMyTank();
        }



        public int GetMyTankWidth(Direction dir, Bitmap bitmap)
        {
            int width = 0;
            switch(dir)
            {
                case Direction.Up:
                    width = bitmap.Width;
                    break;
                case Direction.Down:
                    width = bitmap.Width;
                    break;
                case Direction.Left:
                    width = bitmap.Width;
                    break;
                case Direction.Right:
                    width = bitmap.Width;
                    break;
            }
            return width;
        }

        public int GetMyTankHeight(Direction dir, Bitmap bitmap)
        {
            int height = 0;
            switch (dir)
            {
                case Direction.Up:
                    height = bitmap.Height;
                    break;
                case Direction.Down:
                    height = bitmap.Height;
                    break;
                case Direction.Left:
                    height = bitmap.Height;
                    break;
                case Direction.Right:
                    height = bitmap.Height;
                    break;
            }
            return height;
        }

        public bool IsCollidedWall()
        {
            int X1 = 0;
            int Y1 = 0;

            //获取下一状态的xy坐标
            if(direction == Direction.Up)
            {
                X1 = X;
                Y1 = Y - Speed;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            else if(direction == Direction.Down)
            {
                X1 = X;
                Y1 = Y + Speed;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            else if(direction == Direction.Left)
            {
                X1 = X - Speed;
                Y1 = Y;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            else if (direction == Direction.Right)
            {
                X1 = X + Speed;
                Y1 = Y;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            Rectangle mytank = new Rectangle(X1, Y1, Width, Height);
            //看有没有和墙发生碰撞
            foreach (NotMoveThing wm in NotMoveThing.wallListTotal)
            {
                Rectangle wall = new Rectangle(wm.X, wm.Y, wm.Width, wm.Height);

                if (mytank.IntersectsWith(wall))
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

            //获取下一状态的xy坐标
            if (direction == Direction.Up)
            {
                X1 = X;
                Y1 = Y - Speed;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            else if (direction == Direction.Down)
            {
                X1 = X;
                Y1 = Y + Speed;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            else if (direction == Direction.Left)
            {
                X1 = X - Speed;
                Y1 = Y;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            else if (direction == Direction.Right)
            {
                X1 = X + Speed;
                Y1 = Y;
                Width = GetMyTankWidth(direction, bitmap);
                Height = GetMyTankHeight(direction, bitmap);
            }
            Rectangle mytank = new Rectangle(X1, Y1, Width, Height);
            foreach (NotMoveThing sm in NotMoveThing.steelListTotal)
            {
                Rectangle steel = new Rectangle(sm.X, sm.Y, sm.Width, sm.Height);

                if (mytank.IntersectsWith(steel))
                {
                    return true;
                }
            }
            return false;
        }

        public void MoveCheck()
        {
            //不超出边界的检查
            switch(direction)
            {
                case Direction.Up:
                    if(Y-Speed<0)
                    {
                        isMoving = false;
                    }
                    break;
                case Direction.Down:
                    int height1 = GetMyTankHeight(Direction.Down, bitmap);
                    if(Y+height1+Speed>450)
                    {
                        isMoving = false;
                    }
                    break;
                case Direction.Left:
                    if(X-Speed<0)
                    {
                        isMoving = false;
                    }
                    break;
                case Direction.Right:
                    int width1 = GetMyTankWidth(Direction.Right, bitmap);
                    if(X+width1+Speed>450)
                    {
                        isMoving = false;
                    }
                    break;
            }
            //碰撞检测
            if(IsCollidedWall())
            {
                isMoving = false;
            }
            if(IsCollidedSteel())
            {
                isMoving = false;
            }
        }

   
 

        public void MoveMyTank()
        {
           
            GameFrameWork.g.DrawImage(bitmap, X, Y);
            if (isMoving == true)
            {
                switch (direction)
                {
                    case Direction.Up:
                        Y -= Speed;
                        bitmap = Resources.MyTankUp;
                        bitmap.MakeTransparent(Color.Black);
                        break;
                    case Direction.Down:
                        Y += Speed;
                        bitmap = Resources.MyTankDown;
                        bitmap.MakeTransparent(Color.Black);
                        break;
                    case Direction.Left:
                        X -= Speed;
                        bitmap = Resources.MyTankLeft;
                        bitmap.MakeTransparent(Color.Black);
                        break;
                    case Direction.Right:
                        X += Speed;
                        bitmap = Resources.MyTankRight;
                        bitmap.MakeTransparent(Color.Black);
                        break;
                }
            }
            else if (isMoving == false)
                return;
            
        }


        public void KeyDown1(KeyEventArgs key)
        {
            switch (key.KeyCode)
            {
                case Keys.W:
                    isMoving = true;
                    direction = Direction.Up;
                    break;
                case Keys.S:
                    isMoving = true;
                    direction = Direction.Down;
                    break;
                case Keys.A:
                    isMoving = true;
                    direction = Direction.Left;
                    break;
                case Keys.D:
                    isMoving = true;
                    direction = Direction.Right;
                    break;
                case Keys.Space:
                    GameObjectManager.CreateBullet(X,Y,direction, GetMyTankWidth(direction, bitmap), GetMyTankWidth(direction, bitmap),Tag.My);
                    break;
            }
        }

        

        public void KeyUp1(KeyEventArgs key)
        {
            switch (key.KeyCode)
            {
                case Keys.W:
                    isMoving = false;
                    break;
                case Keys.S:
                    isMoving = false;
                    break;
                case Keys.A:
                    isMoving = false;
                    break;
                case Keys.D:
                    isMoving = false;
                    break;

            }
        }


    }
}
