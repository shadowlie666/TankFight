using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TankFIght_1.Properties; //所有的资源都是在properties这个类里面的，所以要使用素材就要使用这个命名空间

namespace TankFIght_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //this.StartPosition = FormStartPosition.CenterScreen; //把窗口生成位置定位在屏幕正中间

            //this.StartPosition = FormStartPosition.Manual;  手动设置窗口生成位置
            //this.Location = new Point(1000,500);

            


        }

        //双击form1打开窗口，右键属性选择事件，找到paint双击后自动生成此方法
        private void Form1_Paint(object sender, PaintEventArgs e) //绘制窗体时需要的方法,也就是画地图用的方法
        {
            Graphics g = this.CreateGraphics();  //创建一个图形对象

            #region 绘制线
            Pen p = new Pen(Color.Black);  //创建一个笔的对象，这个对象保存了黑色
            g.DrawLine(p, new Point(0, 0), new Point(100, 100)); //用黑色的笔画一条线，后面两个坐标是线的两个端点
                                                                 //注意在窗口里面坐标轴的原点在窗口的左上角，向右为x轴正方向，向下为y轴负方向
            #endregion 可以通过region把代码收起来，且可以像这样任意写注释

            g.DrawString("game", new Font("隶书", 20), new SolidBrush(Color.Red), new Point(100, 100));
            //绘制字符串的方法，第一个形参是你要输入的字符串内容，第二个形参是字体的样式与大小
            //第三个形参是字体的粗细与颜色，第四个形参是字体绘制的位置
            //补充粗细solidbrush实心刷，LinearGradientBrush梯度刷，HatchBrush阴影刷，TextureBrush纹理刷

            //Boss是图片名字，将鼠标移到boss上就可以看到他的数据类型是bitmap类型，点击bitmap可以看到他继承了image
            Image image = Properties.Resources.Boss; //创建一个boss对象
            g.DrawImage(image, new Point(200, 200)); //将这个boss对象绘制在200，200的位置上

            Bitmap bm = Properties.Resources.EXP1; //也可以使用bitmap创造对象，这个比较好，因为bitmap里面有一些好用的方法
            bm.MakeTransparent(Color.Black);//调用bitmap里面的这个方法，可以让图片中的指定颜色变透明


        }
    }
}
