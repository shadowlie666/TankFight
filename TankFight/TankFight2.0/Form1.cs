using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankFight2._0
{
    public partial class Form1 : Form
    {
        private Thread t;
        private static Graphics windowMap;
        private static Bitmap tempMap;
        

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            windowMap = this.CreateGraphics();
            tempMap = new Bitmap(450,450);
            Graphics middleMap = Graphics.FromImage(tempMap);
            GameFrameWork.g = middleMap;


            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();

        }

        private static void GameMainThread()
        {
             int sleepTime = 1000 / 60;

             GameFrameWork.Start();

            while(true)
            {
                GameFrameWork.g.Clear(Color.Black);
                GameFrameWork.Update();
                windowMap.DrawImage(tempMap, 0, 0);
                Thread.Sleep(sleepTime);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyDown(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            GameObjectManager.KeyUp(e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

