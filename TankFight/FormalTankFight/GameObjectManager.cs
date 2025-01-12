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
    //这里的人为坐标是30像素一格，实际坐标的图片是15像素一格，所以人为坐标的（0，0）里有四张图片，实际坐标是（15，15）
    class GameObjectManager //绘制地图
    {
        //下面那个方法里面的walllist只是返回每一小块区域的墙的实际位置和图片，
        //要创造一个total集合用于存储所有的墙的实际位置和图片，先把你要把墙构造的人为位置和图片和total传递过去
        //然后这个方法会根据你设定的人为位置返回墙的实际位置到total里面并存储起来
        private static List<NotMoveThing> WallListTotal = new List<NotMoveThing>();
        private static List<NotMoveThing> SteelListTotal = new List<NotMoveThing>();
        private static NotMoveThing Boss;
        private static MyTank myTank; //静态变量相当于全局变量，其他类也可以使用

        private static List<EnemyTank> tanklist = new List<EnemyTank>();
        private static List<Bullet> bulletList = new List<Bullet>();

        private static int enemyBornSpeed = 120; //敌人生成速度，每秒一个
        private static int enemyBorncount = 120; //计数器，每一帧加一，到60的时候就是一秒，就会生成一个敌人
        private static Point[] points = new Point[3]; //c#提供的point方法可以保存点的坐标

        private static List<Explosion> expList = new List<Explosion>();


        public static void Start()
        {
            //敌方坦克生成位置固定有如下三种位置
            points[0].X = 0;
            points[0].Y = 0;
            points[1].X = 7 * 30;
            points[1].Y = 0;
            points[2].X = 14 * 30;
            points[2].Y = 0;
        }

        public static void Update()
        {
            foreach (NotMoveThing nm in WallListTotal)//根据walltotal里面存储的所有位置来画墙
            {
                nm.Update();
            }
            foreach (NotMoveThing nm in SteelListTotal)
            {
                nm.Update();
            }
            foreach (EnemyTank tank in tanklist)
            {
                tank.Update();
            }
            CheckAndDestoryBullet();
            foreach (Bullet bullet in bulletList)
            {
                bullet.Update();
            }
            CheckAndDestroyExplosion();
            foreach(Explosion exp in expList)
            {
                exp.Update();
            }
            

            Boss.Update();

            myTank.Update();

            EnemyBorn();
        }

        //避免在刷新的时候更改刷新时需要调用的数组，所以这里把需要删除的子弹统一存在一个集合中，线程运行完了再删
        private static void CheckAndDestoryBullet()
        {
            List<Bullet> needToDestory = new List<Bullet>();
            //先保存需要删除的子弹，不能直接删除，如果在其他方法遍历bulletlist的时候删除其中的元素，会报错
            foreach (Bullet bullet in bulletList) 
            {
                if(bullet.IsDestory == true)
                {
                    needToDestory.Add(bullet);
                }
            }
            foreach(Bullet bullet in needToDestory)//再遍历数组获取需要删除的子弹
            {
                bulletList.Remove(bullet);
            }
        }

        private static void CheckAndDestroyExplosion()
        {
            List<Explosion> needToDestory = new List<Explosion>();
            foreach (Explosion exp in expList) //先保存需要删除的爆炸效果
            {
                if (exp.isNeedDestory == true)
                {
                    needToDestory.Add(exp);
                }
            }
            foreach (Explosion exp in needToDestory)//再遍历数组获取需要删除的爆炸效果
            {
                expList.Remove(exp);
            }
        }

        public static void CreateBullet(int x, int y, Tag tag, Direction dir)
        {
            Bullet bullet = new Bullet(x, y, 7, dir, tag);

            bulletList.Add(bullet);


        }

        private static void EnemyBorn() //在三个固定位置上随机选择一个位置，然后随机生成一种坦克
        {
            enemyBorncount++;
            if (enemyBorncount < enemyBornSpeed)
                return;
            else
            {
                SoundManger.PlayAdd();
                Random rd = new Random();
                int index = rd.Next(0, 3); //随机生成012三个数中的一个
                Point position = points[index]; //保存敌人生成的位置
                int enemyType = rd.Next(1, 5);
                switch (enemyType)
                {
                    case 1:
                        CreateEnemyTank1(position.X, position.Y);
                        break;
                    case 2:
                        CreateEnemyTank2(position.X, position.Y);
                        break;
                    case 3:
                        CreateEnemyTank3(position.X, position.Y);
                        break;
                    case 4:
                        CreateEnemyTank4(position.X, position.Y);
                        break;

                }

            }
            enemyBorncount = 0;
        }

        //创建四个方法用于创建敌方的四种坦克
        private static void CreateEnemyTank1(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GrayUp, Resources.GrayDown, Resources.GrayLeft, Resources.GrayRight);
            tanklist.Add(tank);
        }
        private static void CreateEnemyTank2(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 2, Resources.GreenUp, Resources.GreenDown, Resources.GreenLeft, Resources.GreenRight);
            tanklist.Add(tank);
        }
        private static void CreateEnemyTank3(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 4, Resources.QuickUp, Resources.QuickDown, Resources.QuickLeft, Resources.QuickRight);
            tanklist.Add(tank);
        }
        private static void CreateEnemyTank4(int x, int y)
        {
            EnemyTank tank = new EnemyTank(x, y, 1, Resources.SlowUp, Resources.SlowDown, Resources.SlowLeft, Resources.SlowRight);
            tanklist.Add(tank);
        }

        public static void CreateExplosion(int x,int y)
        {
            Explosion exp = new Explosion(x, y);
            expList.Add(exp);
        }

        //public static void Drawmap() 
        //{



        //}

        //public static void DrawMyTank()
        //{

        //}

        public static void DestoryWall(NotMoveThing wall)
        {
            WallListTotal.Remove(wall);
        }

        public static void DestoryTank(EnemyTank tank)
        {
            tanklist.Remove(tank);
        }

        public static MyTank IsCollidedMytank(Rectangle rt)
        {
            if(myTank.GetRectangle().IntersectsWith(rt))
            {
                return myTank;
            }
            else
            {
                return null;
            }
        }

        public static NotMoveThing IsCollidedWall(Rectangle rt) //与墙进行碰撞检测
        {
            foreach (NotMoveThing wall in WallListTotal)
            {
                //intersectswith方法用于判断两个矩形是否相交，若相交了就是发生碰撞
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;    //若相交就返回与他碰撞的墙,后续将这个墙销毁
                }
            }
            return null; //若不相交就返回null
        }

        public static NotMoveThing IsCollidedSteel(Rectangle rt) //与刚墙进行碰撞检测
        {
            foreach (NotMoveThing wall in SteelListTotal)
            {
                if (wall.GetRectangle().IntersectsWith(rt))
                {
                    return wall;
                }
            }
            return null;
        }

        public static bool IsCollidedBoss(Rectangle rt) //与boss做碰撞检测
        {
            return Boss.GetRectangle().IntersectsWith(rt);
        }

        public static EnemyTank IsCollidedEnemyTank(Rectangle rt) //与刚墙进行碰撞检测
        {
            foreach (EnemyTank tank in tanklist)
            {
                if (tank.GetRectangle().IntersectsWith(rt))
                {
                    return tank;
                }
            }
            return null;
        }

        public static void CreateMyTank()
        {
            int x = 5 * 30;
            int y = 14 * 30;
            myTank = new MyTank(x, y, 2);
        }

        public static void CreateMap() //使用静态方法方便调用
        {
            CreateWall(1, 1, 5, Resources.wall, WallListTotal);//按人为定下的（1，1）坐标开始，往下构造五个墙
            CreateWall(3, 1, 5, Resources.wall, WallListTotal);
            CreateWall(5, 1, 4, Resources.wall, WallListTotal);
            CreateWall(7, 1, 3, Resources.wall, WallListTotal);
            CreateWall(9, 1, 4, Resources.wall, WallListTotal);
            CreateWall(11, 1, 5, Resources.wall, WallListTotal);
            CreateWall(13, 1, 5, Resources.wall, WallListTotal);

            CreateWall(7, 5, 1, Resources.steel, SteelListTotal);

            CreateWall(2, 7, 1, Resources.wall, WallListTotal);
            CreateWall(3, 7, 1, Resources.wall, WallListTotal);
            CreateWall(4, 7, 1, Resources.wall, WallListTotal);
            CreateWall(6, 7, 1, Resources.wall, WallListTotal);
            CreateWall(7, 6, 2, Resources.wall, WallListTotal);
            CreateWall(8, 7, 1, Resources.wall, WallListTotal);
            CreateWall(10, 7, 1, Resources.wall, WallListTotal);
            CreateWall(11, 7, 1, Resources.wall, WallListTotal);
            CreateWall(12, 7, 1, Resources.wall, WallListTotal);

            CreateWall(1, 9, 5, Resources.wall, WallListTotal);
            CreateWall(0, 7, 1, Resources.steel, SteelListTotal);
            CreateWall(14, 7, 1, Resources.steel, SteelListTotal);
            CreateWall(3, 9, 5, Resources.wall, WallListTotal);
            CreateWall(5, 9, 3, Resources.wall, WallListTotal);
            CreateWall(9, 9, 3, Resources.wall, WallListTotal);
            CreateWall(6, 10, 1, Resources.wall, WallListTotal);
            CreateWall(7, 10, 2, Resources.wall, WallListTotal);
            CreateWall(8, 10, 1, Resources.wall, WallListTotal);
            CreateWall(11, 9, 5, Resources.wall, WallListTotal);
            CreateWall(13, 9, 5, Resources.wall, WallListTotal);

            CreateWall(6, 13, 2, Resources.wall, WallListTotal);
            CreateWall(7, 13, 1, Resources.wall, WallListTotal);
            CreateWall(8, 13, 2, Resources.wall, WallListTotal);
            CreateBoss(7, 14, Resources.Boss, Boss);
        }

        //用于保存每一小块区域的所有墙的位置和图片
        private static void CreateWall(int x, int y, int count, Image image, List<NotMoveThing> walllist)
        {//xy表第几格，count是那一列有几个
            int XPosition = x * 30; //地图中一格墙是四张图片构成的，一张图片是15*15像素
            int YPosition = y * 30;
            for (int i = YPosition; i < YPosition + count * 30; i += 15) //根据墙在第几格，一列有几个墙来创造墙
            {
                NotMoveThing wall1 = new NotMoveThing(XPosition, i, image); //保存墙的位置和图片
                NotMoveThing wall2 = new NotMoveThing(XPosition + 15, i, image);
                walllist.Add(wall1); //把每张图片的位置存到walllist集合里
                walllist.Add(wall2);
            }
        }

        private static void CreateBoss(int x, int y, Image image, NotMoveThing list)
        {
            int XPosition = x * 30;
            int YPosition = y * 30;
            Boss = new NotMoveThing(XPosition, YPosition, image);

        }

        public static void KeyDown(KeyEventArgs args)
        {
            myTank.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            myTank.KeyUp(args);
        }
    }
}
