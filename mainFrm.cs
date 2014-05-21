using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CG_Exp_2D
{
    public partial class mainFrm : Form
    {
        private bool polygonStart, polygonEnd;//多边形绘画开关
        private bool lineStart, lineEnd;//直线绘画开关
        private bool circleStart;//圆绘画开关

        
        private Canvas curCanvas;
        private Point prepClick, curClick;//上一次鼠标点击的位置； 当前鼠标点击的位置
        private Color curColor;//当前颜色
        private Point zero;//参考坐标系的原点

        public mainFrm()
        {
            InitializeComponent();
            zero.X = panel_workspace.Width / 2;
            zero.Y = panel_workspace.Height / 2;
            curCanvas = new Canvas(zero.X*2, zero.Y*2 , Color.White, "背景图层");
            curColor = Color.Black;
            panel_curColor.Refresh();
            switchOff();
        }

        /// <summary>
        /// 关闭所有的图元绘画开关
        /// </summary>
        private void switchOff()
        {
            polygonStart = false;
            polygonEnd = false;
            lineStart = false;
            lineEnd = false;
            circleStart = false;
        }

        /// <summary>
        /// 检查是否有绘画指令
        /// </summary>
        private void checkIfDraw()
        {
            string name;
            if (polygonStart == true)
            {
                if (polygonEnd == true)
                {
                    name = "多边形" + (curCanvas.CountPolygon+1).ToString();
                    if (curCanvas.drawLastEdge(curColor, name) == true)
                    {
                        listBox_graphics.Items.Add(name);
                        switchOff();
                    }
                    else
                    {
                        polygonEnd = false;
                    }
                }
                else
                {
                    curCanvas.drawPolygon(curClick, curColor);
                }
                panel_workspace.Refresh();
                return;
            }//end polygon

            if (lineStart == true)
            {
                if (lineEnd == true)
                {
                    name = "直线" + (curCanvas.CountLine+1).ToString();
                    curCanvas.drawLine_Bresenham(prepClick, curClick, curColor, name);
                    listBox_graphics.Items.Add(name);
                    switchOff();
                }
                else
                {
                    prepClick = curClick;
                    lineEnd = true;
                }
                panel_workspace.Refresh();
                return;
            }//end line

            if (circleStart == true)
            {
                try
                {
                    int R = int.Parse(textBox_circleRadius.Text);
                    name = "圆" + (curCanvas.CountCircle+1).ToString();
                    curCanvas.drawCircle_Bresenham(curClick, R, curColor, name);
                    listBox_graphics.Items.Add(name);
                    switchOff();
                    panel_workspace.Refresh();
                    return;
                }
                catch
                {
                    MessageBox.Show("请输入正确的半径数值（一个正整数）！");
                }
            }//end circle
        }

        private void mainFrm_Load(object sender, EventArgs e)
        {

        }

        //响应鼠标单击事件
        private void panel_workspace_MouseDown(object sender, MouseEventArgs e)
        {
            curClick = e.Location;
            curClick.X = curClick.X - zero.X;
            curClick.Y = zero.Y - curClick.Y;
            textBox_curCoords.Text = curClick.X.ToString() + ", " + curClick.Y.ToString();
            if (circleStart==true || lineStart==true || polygonStart==true) checkIfDraw();
        }

        //多边形绘制开始
        private void button_startDrawPolygon_Click(object sender, EventArgs e)
        {
            switchOff();
            curCanvas.CurPolygon = null;
            polygonStart = true;
        }

        //多边形绘制结束
        private void button_endDrawPolygon_Click(object sender, EventArgs e)
        {
            polygonEnd = true;
            checkIfDraw();
        }

        //绘制工作区
        private void panel_workspace_Paint(object sender, PaintEventArgs e)
        {
            //Pen pen = new Pen(Color.Black);
            Graphics g = e.Graphics;
            g.DrawImageUnscaled(curCanvas.Bmp, 0, 0);
            /*
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Hidden == false)
                {
                    g.DrawImageUnscaled(layers[i].Bmp, 0, 0);
                }
            }
            if (showSelection == true)
            {
                g.DrawImageUnscaled(selectionCanvas.Bmp, 0, 0);
            }
            */
            //释放资源
            g.Dispose();
            //pen.Dispose();
        }

        //选择颜色对话框, 将curColor赋值为选择的颜色
        private void button_chooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                curColor = dlg.Color;
                panel_curColor.Refresh();
            }
        }

        //直线绘制开始
        private void button_startDrawLine_Click(object sender, EventArgs e)
        {
            switchOff();
            lineStart = true;
        }

        //显示当前颜色的panel
        private void panel_curColor_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush sb = new SolidBrush(curColor);
            Graphics g = e.Graphics;
            g.FillRectangle(sb, 0, 0, 30, 30);
            g.Dispose();
            sb.Dispose();
        }

        //半径文本框获得焦点
        private void textBox_circleRadius_Enter(object sender, EventArgs e)
        {
            textBox_circleRadius.Text = "";
        }

        //半径文本框失去焦点
        private void textBox_circleRadius_Leave(object sender, EventArgs e)
        {
            textBox_circleRadius.Text = "请输入半径";
        }

        //开始绘制圆
        private void button_drawCircle_Click(object sender, EventArgs e)
        {
            circleStart = true;
        }

        private void button_setCoor_Click(object sender, EventArgs e)
        {
            curClick.X = int.Parse(textBox_X.Text);
            curClick.Y = int.Parse(textBox_Y.Text);
            textBox_curCoords.Text = curClick.X.ToString() + ", " + curClick.Y.ToString();
            checkIfDraw();
        }
    }
}
