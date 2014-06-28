using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CG_Tools;

namespace CG_Exp_2D
{
    /// <summary>
    /// 计时器，秒表
    /// </summary>
    class Timer
    {
        private DateTime startTime,curTime;
        private bool started;

        /// <summary>
        /// 开始
        /// </summary>
        public void start()
        {
            curTime = new DateTime();
            startTime = new DateTime();
            startTime = DateTime.Now;
            started = true;
        }

        /// <summary>
        /// 计时
        /// </summary>
        /// <returns>从开始到现在经过的时间，以秒为单位</returns>
        public double count()
        {
            if (started == true)
            {
                curTime = DateTime.Now;
                return (curTime - startTime).TotalSeconds;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 停止计时
        /// </summary>
        /// <returns>从开始到现在经过的时间，以秒为单位</returns>
        public double stop()
        {
            double ret = count();
            //待添加
            started = false;
            return ret;
        }
    }

    /// <summary>
    /// 画布类
    /// </summary>
    class Canvas
    {
        private Point zero; //坐标系的(0,0)在panel中的位置

        private CG_Line curLine;
        private CG_Circle curCircle;
        private CG_Bezier curBezier;
        private LinkedList<Point> tmpPoints;

        public CG_Polygon CurPolygon
        {
            set
            {
                curPolygon = value;
            }
            get
            {
                return curPolygon;
            }
        }
        private CG_Polygon curPolygon;

        public CG_Rectangle CurRectangle
        {
            set
            {
                curRectangle = value;
            }
            get
            {
                return curRectangle;
            }
        }
        private CG_Rectangle curRectangle;

        /// <summary>
        /// 当前图层中直线的数量
        /// </summary>
        public int CountLine
        {
            get
            {
                return listLine.Count;
            }
        }

        /// <summary>
        /// 当前图层中圆的数量
        /// </summary>
        public int CountCircle
        {
            get
            {
                return listCircle.Count;
            }
        }

        /// <summary>
        /// 当前图层中多边形的数量
        /// </summary>
        public int CountPolygon
        {
            get
            {
                return listPolygon.Count;
            }
        }

        /// <summary>
        /// 当前图层中矩形的数量
        /// </summary>
        public int CountRectangle
        {
            get
            {
                return listRectangle.Count;
            }
        }

        private LinkedList<CG_Line> listLine;//直线列表
        private LinkedList<CG_Circle> listCircle;//圆列表
        private LinkedList<CG_Polygon> listPolygon;//多边形列表
        private LinkedList<CG_Rectangle> listRectangle;//矩形列表
        private Timer timer;//秒表

        /// <summary>
        /// 图层的名字
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        private string name;

        /// <summary>
        /// 图层默认背景色
        /// </summary>
        public Color BGColor
        {
            get
            {
                return bgColor;
            }
            set
            {
                bgColor = value;
            }
        }
        private Color bgColor;

        /// <summary>
        /// 图层是否锁定（无法删除）
        /// </summary>
        public bool Locked
        {
            get
            {
                return locked;
            }
            set
            {
                locked = value;
            }
        }
        private bool locked;

        /// <summary>
        /// 图层是否隐藏
        /// </summary>
        public bool Hidden
        {
            get
            {
                return hidden;
            }
            set
            {
                hidden = value;
            }
        }
        private bool hidden;
        /// <summary>
        /// 主绘图区
        /// </summary>
        public Bitmap Bmp
        {
            get
            {
                return bmp;
            }
            set
            {
                bmp = value;
            }
        }
        private Bitmap bmp;//表示要画到panel控件中的图

        /// <summary>
        /// 构造函数，输入画布的宽度和高度
        /// </summary>
        /// <param name="width">画布的宽度</param>
        /// <param name="height">画布的高度</param>
        /// <param name="bg">背景色</param>
        public Canvas(int width,int height,Color bg, string str)
        {
            bmp = new Bitmap(width, height);
            zero.X = width / 2;
            zero.Y = height / 2;
            timer = new Timer();
            bgColor = bg;//默认为透明色
            locked = false;
            hidden = false;
            name = str;
            clearCanvas();
            tmpPoints = new LinkedList<Point>();
            listLine = new LinkedList<CG_Line>();
            listPolygon = new LinkedList<CG_Polygon>();
            listCircle = new LinkedList<CG_Circle>();
            listRectangle = new LinkedList<CG_Rectangle>();
        }

        /// <summary>
        /// 清空画布，使用图层默认背景色填充
        /// </summary>
        public void clearCanvas()
        {
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                    bmp.SetPixel(i, j, bgColor);
            if (listCircle!=null) listCircle.Clear();
            if (listLine != null) listLine.Clear();
            if (listPolygon != null) listPolygon.Clear();
            if (listRectangle != null) listRectangle.Clear();
        }

        /// <summary>
        /// 填充时，判断点是否可以在画布上，包含边界判断
        /// </summary>
        /// <param name="p">点在参考坐标系中的位置</param>
        /// <param name="model">种子点的颜色</param>
        /// <returns>true:可以; false:不可以</returns>
        private bool canPaintInCanvas(Point p, Color model)
        {
            int x, y;//实际画布中的位置
            x = p.X + zero.X;
            y = zero.Y - p.Y;
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
            {
                if (bmp.GetPixel(x, y).ToArgb() == model.ToArgb()) return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// 对于参照坐标系的点，得出画布中的位置
        /// </summary>
        /// <param name="p">参考坐标系中的坐标</param>
        /// <param name="c">颜色</param>
        private bool drawOnCanvas(Point p, Color c)
        {
            int x, y;//实际画布中的位置
            x = p.X + zero.X;
            y = zero.Y - p.Y;
            if (x >= 0 && x < bmp.Width && y >= 0 && y < bmp.Height)
            {
                bmp.SetPixel(x, y, c);
                return true;
            }
            else return false;
        }

        /// <summary>
        /// 根据名称改变当前curXXX所指向的图元，返回值表示所选中的图元类型
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>line直线 circle圆 polygon多边形 rectangle矩形</returns>
        public string changeCurPrimitive(string name)
        {
            //todo
            LinkedListNode<CG_Line> curL = listLine.First;
            while (curL != null)
            {
                if (curL.Value.Name == name)
                {
                    curLine = curL.Value;
                    return "line";
                }
                curL = curL.Next;
            }

            LinkedListNode<CG_Circle> curC = listCircle.First;
            while (curC != null)
            {
                if (curC.Value.Name == name)
                {
                    curCircle = curC.Value;
                    return "circle";
                }
                curC = curC.Next;
            }

            LinkedListNode<CG_Polygon> curPol = listPolygon.First;
            while (curPol != null)
            {
                if (curPol.Value.Name == name)
                {
                    curPolygon = curPol.Value;
                    return "polygon";
                }
                curPol = curPol.Next;
            }

            LinkedListNode<CG_Rectangle> curRec = listRectangle.First;
            while (curRec != null)
            {
                if (curRec.Value.Name == name)
                {
                    curRectangle = curRec.Value;
                    return "rectangle";
                }
                curRec = curRec.Next;
            }
            return null;
        }

        /// <summary>
        /// 对当前输入的直线，使用Bresenham算法画出图像
        /// </summary>
        /// <param name="pStart">直线起点</param>
        /// <param name="pEnd">直线终点</param>
        /// <param name="color">直线颜色</param>
        public void drawLine_Bresenham(Point pStart, Point pEnd, Color color)
        {
            CG_Line newLine = new CG_Line(pStart, pEnd, color);
            foreach (Point cur in newLine.getPoints())
            {
                drawOnCanvas(cur, color);
            }
        }

        /// <summary>
        /// 对当前输入的直线，在直线列表中创建图元，并画出来
        /// </summary>
        /// <param name="pStart">直线起点</param>
        /// <param name="pEnd">直线终点</param>
        /// <param name="color">直线颜色</param>
        /// <param name="name">直线的名称</param>
        public void drawLine_Bresenham(Point pStart, Point pEnd, Color color, string name)
        {
            CG_Line newLine = new CG_Line(pStart, pEnd, color);
            newLine.Name = name;
            listLine.AddLast(newLine);
            curLine = newLine;
            drawLine_Bresenham(pStart, pEnd, color);
        }

        /// <summary>
        /// 对当前输入的参数使用bresenham算法画圆
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="R">半径</param>
        /// <param name="color">颜色</param>
        public void drawCircle_Bresenham(Point center, int R, Color color)
        {
            CG_Circle newCircle = new CG_Circle(center, R, color);
            foreach (Point cur in newCircle.getPoints())
            {
                drawOnCanvas(cur, color);
            }
        }

        /// <summary>
        /// 对当前输入的参数，在圆列表中创建图元并画出来
        /// </summary>
        /// <param name="center">圆心坐标</param>
        /// <param name="R">半径</param>
        /// <param name="color">颜色</param>
        /// <param name="name">名称</param>
        public void drawCircle_Bresenham(Point center, int R, Color color, string name)
        {
            CG_Circle newCircle = new CG_Circle(center, R, color);
            newCircle.Name = name;
            listCircle.AddLast(newCircle);
            curCircle = newCircle;
            drawCircle_Bresenham(center, R, color);
        }

        /// <summary>
        /// 根据顶点列表创建多边形，并绘制出来
        /// </summary>
        /// <param name="vertice">顶点集合</param>
        /// <param name="color">颜色</param>
        public void createPolygon(Point[] vertice, Color color)
        {
            CG_Polygon newPolygon= null;
            int count = 0;
            foreach (Point v in vertice)
            {
                count++;
                if (count == 1) newPolygon = new CG_Polygon(v, color);
                else
                {
                    newPolygon.addVertex(v);
                }
            }
            if (count<3)
            {
                MessageBox.Show("多边形顶点数不能小于3！");
                return;
            }
            else
            {
                Point head = newPolygon.getVertex();
                Point tail = newPolygon.getVertex();
                Point prep = head;
                while (tail != head)
                {
                    drawLine_Bresenham(prep, tail, color);
                    prep = tail;
                    tail = newPolygon.getVertex();
                }
                drawLine_Bresenham(prep, tail, color);
            }
        }

        /// <summary>
        /// 生成多边形的同时将其画出来
        /// </summary>
        /// <param name="vertex">最新要插入的顶点</param>
        /// <param name="color">当前边的颜色</param>
        public void drawPolygon(Point vertex, Color color)
        {
            if (curPolygon == null) curPolygon = new CG_Polygon(vertex, color);
            else
            {
                if (curPolygon.nVertex > 0)
                {
                    Point prep = curPolygon.getLast();
                    if (curPolygon.addVertex(vertex) == true)
                    {
                        drawLine_Bresenham(prep, vertex, color);
                    }
                    else
                    {
                        MessageBox.Show("加入的顶点不合法！");
                    }
                }
                else
                {
                    curPolygon.addVertex(vertex);
                }
            }
        }

        /// <summary>
        /// 对于已有的多边形，直接将其按顶点顺序绘制出来
        /// </summary>
        /// <param name="polygon">多边形</param>
        public void drawPolygon(CG_Polygon polygon)
        {
            if (polygon.nVertex < 3) return;
            Point first, prep, cur;
            first = polygon.getVertex();
            prep = first;
            cur = polygon.getVertex();
            while (cur != first)
            {
                drawLine_Bresenham(prep, cur, polygon.Color);
                prep = cur;
                cur = polygon.getVertex();
            }
            drawLine_Bresenham(prep, cur, polygon.Color);
        }

        /// <summary>
        /// 画多边形的最后一条边
        /// </summary>
        /// <param name="color">当前边的颜色</param>
        /// <param name="name">名称</param>
        /// <return>true:成功构成多边形; false:多边形自相交或顶点过少</return>
        public bool drawLastEdge(Color color, string name)
        {
            if (curPolygon.nVertex >= 3)
            {
                if (curPolygon.isSelfIntersect() == true)
                {
                    MessageBox.Show("多边形自相交！");
                    return false;
                }
                else
                {
                    
                    Point v1 = curPolygon.getLast();
                    Point v2 = curPolygon.getFirst();
                    curPolygon.changeVertexOrder();
                    drawLine_Bresenham(v1, v2, color);
                    curPolygon.Name = name;
                    listPolygon.AddLast(curPolygon);
                    return true;
                }
            }
            else
            {
                MessageBox.Show("顶点数不足3个，无法构成多边形！");
                return false;
            }
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="loc">左上角坐标</param>
        /// <param name="diagonal">右下角坐标</param>
        /// <param name="color">颜色</param>
        public void drawRectangle(Point loc, Point diagonal, Color color)
        {
            Point right_top = new Point(diagonal.X, loc.Y);//右上顶点
            Point left_bottom = new Point(loc.X, diagonal.Y);//左下顶点
            drawLine_Bresenham(loc, right_top, color);
            drawLine_Bresenham(right_top, diagonal, color);
            drawLine_Bresenham(diagonal, left_bottom, color);
            drawLine_Bresenham(loc, left_bottom, color);
        }

        /// <summary>
        /// 绘制矩形
        /// </summary>
        /// <param name="loc">左上角坐标</param>
        /// <param name="diagonal">右下角坐标</param>
        /// <param name="color">颜色</param>
        /// <param name="name">矩形名字</param>
        public void drawRectangle(Point loc, Point diagonal, Color color, string name)
        {
            CG_Rectangle newRec = new CG_Rectangle(loc, diagonal);
            newRec.Name = name;
            listRectangle.AddLast(newRec);
            curRectangle = newRec;
            drawRectangle(loc, diagonal, color);
            
        }

        /// <summary>
        /// 递归填充当前点
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="color">待填充的颜色</param>
        /// <param name="connective">连通性(只接受4或8)</param>
        public void recursiveFill(Point start, Color color, int connective)
        {
            if (connective != 4 && connective != 8)
            {
                MessageBox.Show("连通性请输入4或8！");
                return;
            }
            //记录时间
            timer.start();
            //判断点是否加入过队列
            bool[,] addedQueue = new bool[bmp.Width + 5, bmp.Height + 5];
            for (int i = 0; i < bmp.Width + 5; i++)
                for (int j = 0; j < bmp.Height + 5; j++) addedQueue[i, j] = false;
            Queue<Point> pointList = new Queue<Point>();

            pointList.Enqueue(start);
            //int connective = 8;//连通性
            Point cur, next;
            Color model = bmp.GetPixel(start.X + zero.X, zero.Y - start.Y);
            int max = 0;//记录队列最长长度
            while (pointList.Count > 0)
            {
                if (pointList.Count > max) max = pointList.Count;
                cur = pointList.Dequeue();
                if (canPaintInCanvas(cur, model) == false)
                {
                    continue;
                }
                drawOnCanvas(cur, color);
                next = cur;
                if (connective == 8)
                {
                    for (int i = 0; i < connective; i++)
                    {
                        switch (i)
                        {
                            case 0: next.X++; break;//右
                            case 1: next.Y++; break;//右上
                            case 2: next.X--; break;//上
                            case 3: next.X--; break;//左上
                            case 4: next.Y--; break;//左
                            case 5: next.Y--; break;//左下
                            case 6: next.X++; break;//下
                            case 7: next.X++; break;//右下
                        };
                        if (canPaintInCanvas(next, model) == true && addedQueue[next.X + zero.X, zero.Y - next.Y] == false)
                        {
                            pointList.Enqueue(next);
                            addedQueue[next.X + zero.X, zero.Y - next.Y] = true;
                        }
                    }
                }
                if (connective == 4)
                {
                    for (int i = 0; i < connective; i++)
                    {
                        switch (i)
                        {
                            case 0: next.X++; break;//右
                            case 1: next.X--; next.Y++; break;//上
                            case 2: next.Y--; next.Y--; break;//下
                            case 3: next.Y++; next.X--; break;//左
                        };
                        if (canPaintInCanvas(next, model) == true && addedQueue[next.X + zero.X, zero.Y - next.Y] == false)
                        {
                            pointList.Enqueue(next);
                            addedQueue[next.X + zero.X, zero.Y - next.Y] = true;
                        }
                    }
                }
            }
            MessageBox.Show("填充完毕，队列最大长度为:" + max.ToString() + "\n用时:" + timer.stop().ToString("f3")+"秒");
        }

        /// <summary>
        /// 对当前点采用扫描线转换填充为color颜色
        /// </summary>
        /// <param name="start">起始点</param>
        /// <param name="color">待填充的颜色</param>
        public void scanFill(Point start, Color color)
        {
            //记录时间
            timer.start();

            //创建队列
            Queue<Point> pointList = new Queue<Point>();
            //判断点是否加入过队列
            bool[,] addedQueue = new bool[bmp.Width + 5, bmp.Height + 5];
            for (int i = 0; i < bmp.Width + 5; i++)
                for (int j = 0; j < bmp.Height + 5; j++) addedQueue[i, j] = false;
            //起点作为种子点加入队列
            pointList.Enqueue(start);
            Point cur, seed, left, right;
            bool upBlocked, downBlocked;//上下两行确定种子点时是否遇到不能填充的点，即每一行中，一段连续的可填充的点只需要一个种子点进入队列
            Color model = bmp.GetPixel(start.X + zero.X, zero.Y - start.Y);
            int max = 0;
            while (pointList.Count > 0)
            {
                if (pointList.Count > max) max = pointList.Count;
                seed = pointList.Dequeue();
                drawOnCanvas(seed, color);
                //向左扫描
                left = seed;
                left.X--;
                while (canPaintInCanvas(left, model) == true)
                {
                    drawOnCanvas(left, color);
                    addedQueue[left.X + zero.X, zero.Y - left.Y] = true;
                    left.X--;
                }
                left.X++;
                //向右扫描
                right = seed;
                right.X++;
                while (canPaintInCanvas(right, model) == true)
                {
                    drawOnCanvas(right, color);
                    addedQueue[right.X + zero.X, zero.Y - right.Y] = true;
                    right.X++;
                }
                right.X--;
                //确定这一行上下行的种子点
                cur = left;
                upBlocked = true;
                downBlocked = true;
                Point next = new Point();
                while (cur.X <= right.X)
                {
                    //上一行
                    next.X = cur.X;
                    next.Y = cur.Y + 1;
                    if (canPaintInCanvas(next, model) == true)
                    {
                        if (upBlocked == true)
                        {
                            upBlocked = false;
                            if (addedQueue[next.X + zero.X, zero.Y - next.Y] == false)
                            {
                                pointList.Enqueue(next);
                                addedQueue[next.X + zero.X, zero.Y - next.Y] = true;
                            }
                        }
                    }
                    else
                    {
                        upBlocked = true;
                    }
                    //下一行
                    next.X = cur.X;
                    next.Y = cur.Y - 1;
                    if (canPaintInCanvas(next, model) == true)
                    {
                        if (downBlocked == true)
                        {
                            downBlocked = false;
                            if (addedQueue[next.X + zero.X, zero.Y - next.Y] == false)
                            {
                                pointList.Enqueue(next);
                                addedQueue[next.X + zero.X, zero.Y - next.Y] = true;
                            }
                        }
                    }
                    else
                    {
                        downBlocked = true;
                    }
                    cur.X++;
                }
            }
            MessageBox.Show("填充完毕，队列最大长度为:" + max.ToString() + "\n用时:" + timer.stop().ToString("f3")+"秒");
        }

        /// <summary>
        /// 获取以当前点为种子点的相同（相似）颜色区域（魔棒工具），采用4连通算法
        /// </summary>
        /// <param name="start">种子点</param>
        /// <returns>一张与本图层大小相同的bmp图像，选区为深蓝色(DarkBlue)，其余为默认透明色</returns>
        public Bitmap getSelection(Point start)
        {
            Bitmap selection = new Bitmap(this.bmp.Width,this.bmp.Height);
            //判断点是否加入过队列
            bool[,] addedQueue = new bool[bmp.Width + 5, bmp.Height + 5];
            for (int i = 0; i < bmp.Width + 5; i++)
                for (int j = 0; j < bmp.Height + 5; j++) addedQueue[i, j] = false;
            Queue<Point> pointList = new Queue<Point>();
            pointList.Enqueue(start);
            Point cur, next;
            Color model = bmp.GetPixel(start.X + zero.X, zero.Y - start.Y);
            int max = 0;//记录队列最长长度
            while (pointList.Count > 0)
            {
                if (pointList.Count > max) max = pointList.Count;
                cur = pointList.Dequeue();
                if (canPaintInCanvas(cur, model) == false)
                {
                    continue;
                }
                selection.SetPixel(cur.X + zero.X, zero.Y - cur.Y, Color.DarkBlue);
                next = cur;
                for (int i = 0; i < 4; i++)
                {
                    switch (i)
                    {
                        case 0: next.X++; break;//右
                        case 1: next.X--; next.Y++; break;//上
                        case 2: next.Y--; next.Y--; break;//下
                        case 3: next.Y++; next.X--; break;//左
                    };
                    if (canPaintInCanvas(next, model) == true && addedQueue[next.X + zero.X, zero.Y - next.Y] == false)
                    {
                        pointList.Enqueue(next);
                        addedQueue[next.X + zero.X, zero.Y - next.Y] = true;
                    }
                }
            }
            return selection;
        }

        /// <summary>
        /// 填充选区为指定颜色
        /// </summary>
        /// <param name="selection">选区</param>
        /// <param name="color">待填充的颜色</param>
        public void fillSelection(Bitmap selection, Color color)
        {
            for (int i=0;i<selection.Width;i++)
                for (int j = 0; j < selection.Height; j++)
                {
                    if (selection.GetPixel(i, j).ToArgb() == Color.DarkBlue.ToArgb())
                    {
                        bmp.SetPixel(i, j, color);
                    }
                }
        }

        /// <summary>
        /// 图像填充
        /// </summary>
        /// <param name="selection">选区</param>
        /// <param name="image">待填充的图像</param>
        public void imageFill(Bitmap selection, Bitmap image)
        {
            int i,j;
            //记录选区
            bool[,] canPaint = new bool[bmp.Width + 5, bmp.Height + 5];
            for (i = 0; i < bmp.Width + 5; i++)
                for (j = 0; j < bmp.Height + 5; j++) canPaint[i, j] = false;
            //获取选区
            for (i=0;i<selection.Width;i++)
                for (j = 0; j < selection.Height; j++)
                {
                    if (selection.GetPixel(i, j).ToArgb() == Color.DarkBlue.ToArgb())
                    {
                        canPaint[i, j] = true;
                    }
                }
            
            //填充了图像的背景图层（范围大于选区）
            Bitmap tmp = new Bitmap(bmp.Width, bmp.Height);
            //获取左上角的一个选区点
            for (i = 0; i < bmp.Width; i++)
            {
                for (j = 0; j < bmp.Height; j++)
                {
                    if (canPaint[i, j] == true)
                    {
                        break;
                    }
                }
                if (j < bmp.Height) break;
            }
            //填充背景图层

            while (i < bmp.Width)
            {
                j = 0;
                while (j < bmp.Height)
                {
                    for (int x = 0; x < image.Width; x++)
                        for (int y = 0; y < image.Height; y++)
                        {
                            if (i + x < tmp.Width && j + y < tmp.Height)
                            {
                                tmp.SetPixel(i + x, j + y, image.GetPixel(x, y));
                            }
                        }
                    j = j + image.Height;
                }
                i = i + image.Width;
            }
            //遍历在选区内的点，将bmp中的像素值替换为背景图层
            for (i = 0; i < bmp.Width; i++)
                for (j = 0; j < bmp.Height; j++)
                {
                    if (canPaint[i, j] == true) bmp.SetPixel(i, j, tmp.GetPixel(i, j));
                }
        }

        /// <summary>
        /// 使用Liang-Barsky算法裁剪线段，裁剪窗口为矩形
        /// </summary>
        /// <param name="line">待裁剪的线段</param>
        /// <param name="window">裁剪窗口</param>
        /// <returns>裁剪完后的线段</returns>
        public CG_Line clipLines_LiangBarsky(CG_Line line, CG_Rectangle window)
        {
            /*
             * 设直线为(x0,y0)->(x1,y1)
             * 参数化表示为 
             *      x=x0+u(x1-x0)
             *      y=y0+u(y1-y0)
             * 裁剪条件为
             *      xmin<=x0+u(x1-x0)<=xmax
             *      ymin<=y0+u(y1-y0)<=ymax
             * 可统一表示为u*pk<=qk  (k=0,1,2,3
             * 根据pk和qk可以求得裁剪线段的端点
             * */

            //直线的参数
            int x0,y0,x1,y1;
            x0=line.Param.start.X;
            y0=line.Param.start.Y;
            x1=line.Param.end.X;
            y1=line.Param.end.Y;
            //裁剪窗口的边界
            int xmin, xmax, ymin, ymax;
            xmin = window.Left;
            xmax = window.Right;
            ymin = window.Bottom;
            ymax = window.Top;

            int[] p=new int[4];
            int[] q=new int[4];
            p[0] = -1 * (x1 - x0); q[0] = x0 - xmin;//left
            p[1] = x1 - x0; q[1] = xmax - x0;//right
            p[2] = -1 * (y1 - y0); q[2] = y0 - ymin;//bottom
            p[3] = y1 - y0; q[3] = ymax - y0;//top

            double u1 = 0, u2 = 1;//初始裁剪端点为原线段的端点
            for (int k = 0; k < 4; k++)
            {
                if (p[k] == 0 && q[k] < 0) return null;//线段完全在边界外部 
                double r = Convert.ToDouble(q[k]) / Convert.ToDouble(p[k]);
                if (p[k] < 0)
                {
                    if (r > u1) u1 = r;
                }
                else//pk>0
                {
                    if (r < u2) u2 = r;
                }
            }
            if (u1 > u2) return null;

            Point q0 = new Point(x0 + Convert.ToInt32(u1 * (x1 - x0)), y0 + Convert.ToInt32(u1 * (y1 - y0)));
            Point q1 = new Point(x0 + Convert.ToInt32(u2 * (x1 - x0)), y0 + Convert.ToInt32(u2 * (y1 - y0)));
            CG_Line clippedLine = new CG_Line(q0, q1, line.Param.color);
            return clippedLine;
        }

        /// <summary>
        /// 寻找所有的直线的交点
        /// </summary>
        public string findIntersections()
        {
            if (listLine.Count<2) 
            {
                MessageBox.Show("当前直线图元少于2个！");
                return null;
            }
            LinkedListNode<CG_Line> head, tail;
            head = listLine.First;
            string output = "";
            while (head!=null)
            {
                Point p0, p1, q0, q1;
                tail = head.Next;
                while (tail != null)
                {
                    p0 = head.Value.Param.start;
                    p1 = head.Value.Param.end;
                    q0 = tail.Value.Param.start;
                    q1 = tail.Value.Param.end;
                    if (Tool.isCross(p0, p1, q0, q1) == true)
                    {
                        Point tmp = Tool.crossPoint(p0, p1, q0, q1);
                        output += "(" + tmp.X.ToString() + ", " + tmp.Y.ToString() + "); ";
                        drawCircle_Bresenham(tmp, 2, Color.Red);
                    }
                    tail = tail.Next;
                }
                head = head.Next;
            }
            return output;
        }

        /// <summary>
        /// 判断点是否在当前多边形内
        /// </summary>
        /// <param name="p">坐标</param>
        public void checkInPolygon(Point p)
        {
            if (curPolygon == null) return;
            string str="";
            if (curPolygon.isInPolygon(p) == true) str = "在内部";
            else str = "不在内部";
            MessageBox.Show(str);
        }

        /// <summary>
        /// 用当前的矩形作为裁剪窗口裁剪所有的线段
        /// </summary>
        public void clipLines_usingCurRectangle()
        {
            if (listLine.Count == 0) return;
            LinkedListNode<CG_Line> cur = listLine.First;
            CG_Line newLine;
            while (cur != null)
            {
                newLine = clipLines_LiangBarsky(cur.Value, curRectangle);
                if (newLine != null)
                {
                    cur.Value = newLine;
                    drawLine_Bresenham(newLine.Param.start,newLine.Param.end, Color.Red);
                }
                cur = cur.Next;
            }
        }

        /// <summary>
        /// 双边裁剪
        /// </summary>
        /// <param name="target">目标多边形</param>
        /// <param name="window">裁剪窗口多边形</param>
        /// <returns></returns>
        public CG_Polygon[] clipPolygon_WeilerAtherton(CG_Polygon target, CG_Polygon window)
        {
            Point t_first, t_prev, t_cur;//target
            Point w_first, w_prev, w_cur;//window
            Point tmp;

            //将两多边形裁剪相关的成员链表清空防止之前的裁剪影响本次
            if (target.clipVertex != null) target.clipVertex.Clear();
            if (window.clipVertex != null) window.clipVertex.Clear();
            if (target.intersections != null) target.intersections.Clear();
            if (window.intersections != null) window.intersections.Clear();
            target.intersections = new LinkedList<CG_Polygon.tagIntersections>();
            window.intersections = new LinkedList<CG_Polygon.tagIntersections>();

            //求出两多边形的交点列表
            t_first = target.getVertex();
            t_prev = t_first;
            t_cur = target.getVertex();
            do
            {
                bool isIn=window.isInPolygon(t_prev);//目标多边形边的始点是否在窗口内部
                w_first = window.getVertex();
                w_prev = w_first;
                w_cur = window.getVertex();
                do
                {
                    if (Tool.isCross(t_prev, t_cur, w_prev, w_cur) == true)
                    {
                        tmp = Tool.crossPoint(t_prev, t_cur, w_prev, w_cur);
                        target.addIntersections(tmp, t_prev, t_cur, isIn);
                        window.addIntersections(tmp, w_prev, w_cur, isIn);
                    }
                    w_prev = w_cur;
                    w_cur = window.getVertex();
                } while (w_prev != w_first);
                t_prev = t_cur;
                t_cur = target.getVertex();
            } while (t_prev != t_first);

            //将顶点加入到多边形的顶点列表中
            target.setClipVertex();
            window.setClipVertex();
            LinkedListNode<CG_Polygon.tagClipVertex> tclip_cur=target.clipVertex.First;
            LinkedListNode<CG_Polygon.tagClipVertex> wclip_cur;
            while (tclip_cur != null)
            {
                if (tclip_cur.Value.isIntersection == true)
                {
                    wclip_cur = window.clipVertex.First;
                    while (wclip_cur != null)
                    {
                        if (wclip_cur.Value.isIntersection == true && wclip_cur.Value.v == tclip_cur.Value.v)
                        {
                            CG_Polygon.tagClipVertex tmp3 = tclip_cur.Value;
                            LinkedListNode<CG_Polygon.tagClipVertex> prev = wclip_cur.Previous;
                            window.clipVertex.Remove(wclip_cur);
                            if (prev != null)
                            {
                                window.clipVertex.AddAfter(prev, tmp3);
                            }
                            else
                            {
                                window.clipVertex.AddFirst(tmp3);
                            }
                            break;
                        }
                        wclip_cur = wclip_cur.Next;
                    }
                }
                tclip_cur = tclip_cur.Next;
            }

            LinkedList<CG_Polygon> resultPolygon = new LinkedList<CG_Polygon>();//存放结果多边形的多边形列表
            //开始裁剪
            LinkedListNode<CG_Polygon.tagClipVertex> inter=target.findUnTracked();
            //寻找一个没有被跟踪过的交点
            while (inter != null)
            {
                CG_Polygon newPolygon = new CG_Polygon(Color.Red);
                
                LinkedListNode<CG_Polygon.tagClipVertex> newNode=inter;
                bool polygonFinish = false;//若已搜索到一个封闭多边形则为true
                bool trackingTarget = true;//记录当前沿哪一个窗口边界进行跟踪
                while (polygonFinish == false)
                {
                    
                    target.setTracked(newNode.Value.v);
                    window.setTracked(newNode.Value.v);
                    if (newPolygon.addVertex(newNode.Value.v) == false)
                    {
                        polygonFinish = true;
                        break;
                    }
                    if (newNode.Value.isIn == true)
                    {
                        //交点为入点，则沿目标多边形边界跟踪
                        newNode = target.findByPoint(newNode.Value.v);
                        trackingTarget = true;
                    }
                    else
                    {
                        //交点为出点，沿窗口多边形边界跟踪
                        newNode = window.findByPoint(newNode.Value.v);
                        trackingTarget = false;
                    }
                    newNode = newNode.Next;
                    if (newNode == null)
                    {
                        if (trackingTarget == true) newNode = target.clipVertex.First;
                        else newNode = window.clipVertex.First;
                    }
                    
                    while (newNode!=null && newNode.Value.isIntersection == false)
                    {
                        if (newPolygon.addVertex(newNode.Value.v) == true)
                        {
                            newNode = newNode.Next;
                            if (newNode == null)
                            {
                                if (trackingTarget == true) newNode = target.clipVertex.First;
                                else newNode = window.clipVertex.First;
                            }
                        }
                        else
                        {
                            polygonFinish = true;
                            break;
                        }
                    }
                    
                }
                resultPolygon.AddLast(newPolygon);
                inter = target.findUnTracked();
            }

            //返回结果多边形列表
            if (resultPolygon.Count == 0)
            {
                CG_Polygon[] ret;
                //无交点，结果多边形列表为空
                if (window.isInPolygon(target.getFirst()) == true)
                {
                    //目标多边形完全在裁剪窗口内部
                    ret = new CG_Polygon[1];
                    ret[0] = target;
                    return ret;
                }
                if (target.isInPolygon(window.getFirst()) == true)
                {
                    //裁剪窗口完全在目标多边形内部
                    ret = new CG_Polygon[1];
                    ret[0] = window;
                    return ret;
                }
                return null;
            }
            else
            {
                CG_Polygon[] ret = new CG_Polygon[resultPolygon.Count];
                int n = resultPolygon.Count;
                for (int i = 0; i < n; i++)
                {
                    ret[i] = resultPolygon.First();
                    resultPolygon.RemoveFirst();
                }
                return ret;
            }
        }

        /// <summary>
        /// 用当前多边形作为裁剪窗口裁剪所有的多边形
        /// </summary>
        public void clipPolygons_usingCurPolygon()
        {
            if (listPolygon.Count == 0) return;
            LinkedListNode<CG_Polygon> cur = listPolygon.First;
            while (cur != null)
            {
                if (cur.Value.Name != curPolygon.Name)
                {
                    CG_Polygon[] group = clipPolygon_WeilerAtherton(cur.Value, curPolygon);
                    if (group != null)
                    {
                        foreach (CG_Polygon tmp in group)
                        {
                            tmp.Color = Color.Red;
                            drawPolygon(tmp);
                        }
                    }
                }
                cur = cur.Next;
            }

        }

        /// <summary>
        /// 添加贝塞尔曲线的控制顶点
        /// </summary>
        /// <param name="p">控制顶点坐标</param>
        public void addControlPoints(Point p)
        {
            tmpPoints.AddLast(p);
        }

        /// <summary>
        /// 绘制贝塞尔曲线
        /// </summary>
        /// <param name="color"></param>
        public void drawBezier(Color color)
        {
            Point[] tmp = tmpPoints.ToArray();
            curBezier = new CG_Bezier();
            curBezier.ControlPoints = tmp;
            curBezier.generateBezier();
            Point[] points = curBezier.getPoints();
            Point prev = points[0];
            for (int i = 1; i < points.Count(); i++)
            {
                drawLine_Bresenham(prev, points[i], color);
                prev = points[i];
            }

        }
    }
}
