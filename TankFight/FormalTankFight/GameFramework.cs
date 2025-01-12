using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormalTankFight
{
    enum GameState //控制游戏状态，当boss死了就要从running切换为gameover状态，停止里面的坦克移动
    {
        Running,
        GameOver
    }

    class GameFramework
    {
        public static Graphics g;
        private static GameState gameState = GameState.Running;

        public static void Start()
        {
            SoundManger.InitSound();
            GameObjectManager.Start();
            GameObjectManager.CreateMap(); //地图元素的位置只需要初始化一次即可
            GameObjectManager.CreateMyTank();
            SoundManger.PlayStart();
        }

        public static void Update()
        {
            //FPS帧率，用于绘制每秒画面帧率
            //GameObjectManager.Drawmap();//地图内的元素需要每一帧都重新画一次
            //GameObjectManager.DrawMyTank();
            if(gameState == GameState.Running)
            {
                GameObjectManager.Update();
            }
            else if(gameState == GameState.GameOver)
            {
                GameOverUpdate();
            }
           
            //框架更新，让画地图的方法在update里面，让他在刷新的同时画地图，不用定义画地图和刷新地图两个方法了
        }

        private static void GameOverUpdate()
        {
           

            int x = 450 / 2 - Properties.Resources.GameOver.Width / 2;
            int y = 450 / 2 - Properties.Resources.GameOver.Height / 2;
            g.DrawImage(Properties.Resources.GameOver, x, y);
        }

        public static void ChangeToGameOver()
        {
            gameState = GameState.GameOver;
        }

        public static void KeyDown(KeyEventArgs args)
        {
            GameObjectManager.KeyDown(args);
        }
        public static void KeyUp(KeyEventArgs args)
        {
            GameObjectManager.KeyUp(args);
        }
    }
}
