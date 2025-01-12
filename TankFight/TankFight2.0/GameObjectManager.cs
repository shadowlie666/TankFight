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
    

    class GameObjectManager
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public static Point[] tankPosition = new Point[3];
        public static MoveThing.Direction enemyTankDir;
        public Flag flag;
        public static int count = 60;
        public static Random random = new Random();
        public static List<EnemyTank> enemyTankListTotal = new List<EnemyTank>();

        public static MyTank myTank;
        public static Bullet bullet;
        public static EnemyTank enemyTank;

        private static Object _lock = new Object();

        public static List<Bullet> bulletListTotal = new List<Bullet>(999);

        public static void TankPosition()
        {
            tankPosition[0].X = 2 * 30;
            tankPosition[0].Y = 0;
            tankPosition[1].X = 7 * 30;
            tankPosition[1].Y = 0;
            tankPosition[2].X = 12 * 30;
            tankPosition[2].Y = 0;
        }

        public static void Update()
        {
            foreach(NotMoveThing nm in NotMoveThing.wallListTotal)
            {
                NotMoveThing.Update(nm.X,nm.Y,Resources.wall);
            }
            foreach (NotMoveThing nm in NotMoveThing.steelListTotal)
            {
                NotMoveThing.Update(nm.X, nm.Y, Resources.steel);
            }
            foreach (NotMoveThing nm in NotMoveThing.bossListTotal)
            {
                NotMoveThing.Update(nm.X, nm.Y, Resources.Boss);
            }
            myTank.UpdateMytank();
            foreach (Bullet bu in bulletListTotal.ToArray())
            {
                bu.UpdateBullet();
            }
            InitEnemyTank();
            foreach (EnemyTank en in enemyTankListTotal.ToArray())
            {

                en.UpdateEnemyTank();
            }

        }

        public static void CreateMap()
        {
            NotMoveThing.CreateWall(1, 1, 3, Resources.wall);
            NotMoveThing.CreateWall(3, 1, 3, Resources.wall);
            NotMoveThing.CreateWall(6, 1, 2, Resources.wall);
            NotMoveThing.CreateWall(8, 1, 2, Resources.wall);
            NotMoveThing.CreateWall(11, 1, 3, Resources.wall);
            NotMoveThing.CreateWall(13, 1, 3, Resources.wall);
            

            NotMoveThing.CreateWall(1, 5, 1, Resources.wall);
            NotMoveThing.CreateWall(2, 5, 1, Resources.wall);
            NotMoveThing.CreateWall(3, 5, 1, Resources.wall);
            NotMoveThing.CreateWall(1, 5, 1, Resources.wall);
            NotMoveThing.CreateWall(5, 4, 2, Resources.wall);
            NotMoveThing.CreateWall(6, 4, 1, Resources.wall);
            NotMoveThing.CreateWall(7, 4, 1, Resources.wall);
            NotMoveThing.CreateWall(8, 4, 1, Resources.wall);
            NotMoveThing.CreateWall(9, 4, 2, Resources.wall);
            NotMoveThing.CreateWall(11, 5, 1, Resources.wall);
            NotMoveThing.CreateWall(12, 5, 1, Resources.wall);
            NotMoveThing.CreateWall(13, 5, 1, Resources.wall);

            NotMoveThing.CreateWall(1, 9, 1, Resources.wall);
            NotMoveThing.CreateWall(2, 9, 1, Resources.wall);
            NotMoveThing.CreateWall(3, 9, 1, Resources.wall);
            NotMoveThing.CreateWall(5, 9, 3, Resources.wall);
            NotMoveThing.CreateWall(6, 10, 1, Resources.wall);
            NotMoveThing.CreateWall(7, 9, 3, Resources.wall);
            NotMoveThing.CreateWall(8, 10, 1, Resources.wall);
            NotMoveThing.CreateWall(9, 9, 3, Resources.wall);
            NotMoveThing.CreateWall(11, 9, 1, Resources.wall);
            NotMoveThing.CreateWall(12, 9, 1, Resources.wall);
            NotMoveThing.CreateWall(13, 9, 1, Resources.wall);
            NotMoveThing.CreateWall(1, 11, 3, Resources.wall);
            NotMoveThing.CreateWall(3, 11, 3, Resources.wall);
            NotMoveThing.CreateWall(11, 11, 3, Resources.wall);
            NotMoveThing.CreateWall(13, 11, 3, Resources.wall);
            NotMoveThing.CreateWall(1, 11, 3, Resources.wall);
            NotMoveThing.CreateWall(5, 14, 1, Resources.wall);
            NotMoveThing.CreateWall(6, 13, 2, Resources.wall);
            NotMoveThing.CreateWall(7, 13, 1, Resources.wall);
            NotMoveThing.CreateWall(8, 13, 2, Resources.wall);
            NotMoveThing.CreateWall(9, 14, 1, Resources.wall);

            NotMoveThing.CreateSteel(0, 7, 1, Resources.steel);
            NotMoveThing.CreateSteel(1, 7, 1, Resources.steel);
            NotMoveThing.CreateSteel(6, 7, 1, Resources.steel);
            NotMoveThing.CreateSteel(7, 7, 1, Resources.steel);
            NotMoveThing.CreateSteel(8, 7, 1, Resources.steel);
            NotMoveThing.CreateSteel(13, 7, 1, Resources.steel);
            NotMoveThing.CreateSteel(14, 7, 1, Resources.steel);

            NotMoveThing.CreateBoss(7, 14, Resources.Boss);


        }


        public static void CreateMyTank()
        {
            myTank = new MyTank(7 * 30, 12 * 30, 3, MoveThing.Tag.My);
            
        }

        public static void CreateBullet(int x, int y, MoveThing.Direction direction,int width,int height,MoveThing.Tag tag)
        {
            int X = GetBulletX(direction, width, height,x);
            int Y = GetBulletY(direction, width, height, y);
            bullet = new Bullet(X, Y, 7, direction,Resources.BulletDown,Resources.BulletUp,Resources.BulletLeft,Resources.BulletRight,tag);
            bulletListTotal.Add(bullet);


        }

        public static int GetBulletX(MoveThing.Direction direction, int width, int height,int x)
        {
            int X = x;
            switch (direction)
            {
                case  MoveThing.Direction.Up:
                    break;
                case  MoveThing.Direction.Down:
                    break;
                case  MoveThing.Direction.Left:
                    X -= 13;
                    break;
                case  MoveThing.Direction.Right:
                    X = X + width - 9;
                    break;
            }
            return X;
        }

        public static int GetBulletY(MoveThing. Direction direction, int width, int height,int y)
        {
            int Y = y;
            switch (direction)
            {
                case MoveThing.Direction.Up:
                    Y -= 13;
                    break;
                case MoveThing.Direction.Down:
                    Y = Y + height - 9;
                    break;
                case MoveThing.Direction.Left:
                    break;
                case MoveThing.Direction.Right:
                    break;
            }
            return Y;
        }

        public static void InitEnemyTank()
        {
            if (count < 60)
            {
                count++;
            }
            else if (count == 60)
            {
                Point points = InitPosition();
                int i = random.Next(0, 4);
                switch (i)
                {
                    case 0:
                        CreateFastTank(points);
                        break;
                    case 1:
                        CreateSlowTank(points);
                        break;
                    case 2:
                        CreateYellowTank(points);
                        break;
                    case 3:
                        CreateGreenTank(points);
                        break;
                }
                count++;
            }
            else if (count > 60)
            {
                count = 0;
            }

        }



        public static Point InitPosition()
        {
            
            int i = random.Next(0, 3);

            return tankPosition[i];
        }

        public static void CreateFastTank(Point points)
        {
            enemyTank = new EnemyTank(points.X, points.Y, Resources.QuickDown,Resources.QuickUp,Resources.QuickLeft,Resources.QuickRight,5);
            enemyTankListTotal.Add(enemyTank);
        }

        public static void CreateSlowTank(Point points)
        {
            enemyTank = new EnemyTank(points.X, points.Y,  Resources.SlowDown,Resources.SlowUp,Resources.SlowLeft,Resources.SlowRight,1 );
            enemyTankListTotal.Add(enemyTank);
        }

        public static void CreateYellowTank(Point points)
        {
            enemyTank = new EnemyTank(points.X, points.Y, Resources.YellowDown,Resources.YellowUp,Resources.YellowLeft,Resources.YellowRight,3);
            enemyTankListTotal.Add(enemyTank);
        }

        public static void CreateGreenTank(Point points)
        {
            enemyTank = new EnemyTank(points.X, points.Y, Resources.GreenDown,Resources.GreenUp,Resources.GreenLeft,Resources.GreenRight,3);
            enemyTankListTotal.Add(enemyTank);
        }

        public static void KeyDown(KeyEventArgs e)
        {
            myTank.KeyDown1(e);
        }
        public static void KeyUp(KeyEventArgs e)
        {
            myTank.KeyUp1(e);
        }


    }
}
