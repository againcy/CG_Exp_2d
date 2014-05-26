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
        private bool rectangleStart, rectangleEnd;//矩形绘画开关

        
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
            rectangleStart = false;
            rectangleEnd = false;
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
                    panel_workspace.Refresh();
                }
                else
                {
                    prepClick = curClick;
                    lineEnd = true;
                }
                
                return;
            }//end line

            if (circleStart == true)
            {
                try
                {
                    int R = int.Parse(textBox_circleRadius.Text);
                    if (R == 0)
                    {
                        MessageBox.Show("请输入正确的半径数值（一个正整数）！");
                        return;
                    }
                    else
                    {
                        name = "圆" + (curCanvas.CountCircle + 1).ToString();
                        curCanvas.drawCircle_Bresenham(curClick, R, curColor, name);
                        listBox_graphics.Items.Add(name);
                        switchOff();
                        panel_workspace.Refresh();
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("请输入正确的半径数值（一个正整数）！");
                }
            }//end circle

            if (rectangleStart == true)
            {
                if (rectangleEnd==true)
                {
                     name = "矩形" + (curCanvas.CountRectangle + 1).ToString();
                     curCanvas.drawRectangle(prepClick,curClick,curColor,name);
                     listBox_graphics.Items.Add(name);
                     switchOff();
                     panel_workspace.Refresh();
                }
                else
                {
                    prepClick = curClick;
                    rectangleEnd = true;
                }
                return;
               
            }//end rectangle
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
            if (circleStart == true || lineStart == true || polygonStart == true || rectangleStart == true) checkIfDraw();
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
            
            Graphics g = e.Graphics;
            
            g.DrawImageUnscaled(curCanvas.Bmp, 0, 0);
            /*Pen pen = new Pen(Color.Black);
            g.DrawLine(pen, new Point(zero.X, 0), new Point(zero.X, 600));
            g.DrawLine(pen, new Point(0, zero.Y), new Point(600, zero.Y));*/
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

        //设置输入的坐标为当前坐标
        private void button_setCoor_Click(object sender, EventArgs e)
        {
            try
            {
                curClick.X = int.Parse(textBox_X.Text);
                curClick.Y = int.Parse(textBox_Y.Text);
            }
            catch (Exception)
            {
            }
            textBox_curCoords.Text = curClick.X.ToString() + ", " + curClick.Y.ToString();
            checkIfDraw();
        }

        //开始绘制矩形
        private void button_drawRectangle_Click(object sender, EventArgs e)
        {
            switchOff();
            rectangleStart = true;
        }

        //开始裁剪线段
        private void button_clipLines_Click(object sender, EventArgs e)
        {
            curCanvas.clipLines_usingCurRectangle();
            panel_workspace.Refresh();
        }

        //图元列表中的选项改变时
        private void listBox_graphics_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                string name = listBox_graphics.SelectedItem.ToString();

                if (curCanvas.changeCurPrimitive(name) != null)
                {
                    textBox_chooseItem.Text = name;
                }
                else
                {
                    MessageBox.Show("找不到该图元！");
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        private void button_output_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "BMP图片(*.bmp)|*.bmp";
            dlg.FilterIndex = 1;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string filePath = dlg.FileName;
                curCanvas.Bmp.Save(filePath);
                
            }
        }

        private void button_clipPolygon_Click(object sender, EventArgs e)
        {
            if (curCanvas.CurPolygon == null)
            {
                MessageBox.Show("请选择作为裁剪窗口的多边形！");
                return;
            }
            curCanvas.clipPolygons_usingCurPolygon();
            panel_workspace.Refresh();
        }

        private void button_findIntersections_Click(object sender, EventArgs e)
        {
            string tmp = curCanvas.findIntersections();
            if (tmp == null) return;
            else
            {
                if (tmp == "") MessageBox.Show("无交点！");
                else
                {
                    panel_workspace.Refresh();
                    MessageBox.Show(tmp);
                }
            }
        }

        private void button_checkInPolygon_Click(object sender, EventArgs e)
        {
            curCanvas.checkInPolygon(curClick);
        }

        private void button_clearCanvas_Click(object sender, EventArgs e)
        {
            curCanvas.clearCanvas();
            listBox_graphics.Items.Clear();
            panel_workspace.Refresh();
        }
    }
}
