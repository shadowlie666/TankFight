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

namespace FormalTankFight
{
    public partial class Form1 : Form
    {
        private Thread t; //因为form1类里面的t控制了游戏框架的构造，其他方法也需要用这个变量，所以要拿出来作为类变量
        private static Graphics windowG; //窗口画布
        private static Bitmap tempBmp; //为解决闪屏问题，设置了临时图片

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; //将窗体设置在屏幕中间

            windowG = this.CreateGraphics();

            tempBmp = new Bitmap(450, 450);
            Graphics bmpG = Graphics.FromImage(tempBmp); //所有对bmpG的画都会画在tempBmp上
            //通过fromimage方法可以创造一张图片作为画布，这个画布可以和其他graphics变量一样使用所有功能，
            //但其最后呈现出来的图片不会在window窗口上显示
            GameFramework.g = bmpG;


            t = new Thread(new ThreadStart(GameMainThread));
            t.Start();

           

        }

        private static void GameMainThread()
        {
            //GameFrameWork
            GameFramework.Start(); //开始运行游戏框架

            int sleeptime = 1000 / 60;  //这里1000的单位是ms

            while(true)
            {
                //给临时图片tempbmp刷底画图片
                GameFramework.g.Clear(Color.Black);//由于这个是静态方法，不能访问此类的私有成员，要通过其他类访问

                GameFramework.Update(); //绘制每一帧的画面

                windowG.DrawImage(tempBmp, 0, 0); //把画好的图片覆盖到窗体上

                Thread.Sleep(sleeptime); //每调用一次绘制方法，休息1/60秒，那么就是每秒60次绘制了，游戏就变成60帧了
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //在关闭主线程的同时，也要让其他线程关闭，否则项目就无法结束运行
            //双击form1会弹出窗口，然后右键窗口属性，选择事件，找到formclosed,双击即可自动生成函数
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
    }
}
