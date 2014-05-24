using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace CG_Exp_2D
{
    /// <summary>
    /// （顶点表示的）多边形类
    /// </summary>
    class CG_Polygon
    {
        public struct tagClipVertex
        {
            public Point v;
            public bool isIntersection;//是否是交点
            public bool isTracked;//是否被跟踪过
            public bool isIn;//是否是入点
        }
        public struct tagIntersections
        {
            public Point p0, p1;//交点所在的边的，始点和终点
            public Point v;//端点
            public bool isIn;//始点是否在窗口内部
        }
        
        /// <summary>
        /// 多边形的顶点链表
        /// </summary>
        private LinkedList<Point> vertex;

        /// <summary>
        /// 包含判断是否是交点信息的顶点列表，用于裁剪
        /// </summary>
        public LinkedList<tagClipVertex> clipVertex;

        /// <summary>
        /// 交点列表
        /// </summary>
        public LinkedList<tagIntersections> intersections;
        
        private LinkedListNode<Point> curV;//用于返回vertex链表的函数

        /// <summary>
        /// 多边形的名字
        /// </summary>
        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }
        private string name;

        /// <summary>
        /// 颜色
        /// </summary>
        public Color Color
        {
            set
            {
                color = value;
            }
            get
            {
                return color;
            }
        }
        private Color color;

        public CG_Polygon(Color c)
        {
            color = c;
            vertex = new LinkedList<Point>();
            intersections = new LinkedList<tagIntersections>();
        }

        public CG_Polygon(Point first, Color c)
        {
            vertex = new LinkedList<Point>();
            intersections = new LinkedList<tagIntersections>();
            vertex.AddFirst(first);
            
            color = c;
        }

        /// <summary>
        /// 计算两点间的距离
        /// </summary>
        /// <param name="p0">一个点</param>
        /// <param name="p1">另一个点</param>
        /// <returns>距离</returns>
        private double distance(Point p0, Point p1)
        {
            double a = Math.Pow(p1.X - p0.X, 2);
            double b = Math.Pow(p1.Y - p0.Y, 2);
            return Math.Sqrt(a + b);
        }

        /// <summary>
        /// 求叉积向量p0->p1和q0->q1, 只返回-1或1表示结果的符号
        /// </summary>
        /// <returns>叉积</returns>
        private int vectorProduct(Point p0, Point p1, Point q0, Point q1)  
        {
            int a=(p1.X-p0.X)*(q1.Y-q0.Y);
            int b=(p1.Y-p0.Y)*(q1.X-q0.X);
            if (a - b > 0) return 1;
            else return -1;
            
        }

        /// <summary>
        /// 判断pk是否在(pi,pj)上
        /// </summary>
        /// <param name="pi"></param>
        /// <param name="pj"></param>
        /// <param name="pk"></param>
        /// <returns>true:在线段上; false:不在线段上</returns>
        private bool onSegment(Point pi, Point pj, Point pk)
        {      
            int minx, miny, maxx, maxy;      
            if (pi.X > pj.X)
            {          
                minx = pj.X;         
                maxx = pi.X;      
            }   
            else
            {         
                minx = pi.X;       
                maxx = pj.X;     
            }       
            if (pi.Y > pj.Y)
            {
                miny = pj.Y;          
                maxy = pi.Y;   
            }  
            else
            {           
                miny = pi.Y;        
                maxy = pj.Y;      
            }      
            if (minx <= pk.X && pk.X <= maxx && miny <= pk.Y && pk.Y <= maxy)    
                return true;     
            else  return false; 
            
        }
        /// <summary>
        /// 判断两条线段是否相交
        /// </summary>
        /// <param name="p1">第一条线段端点</param>
        /// <param name="p2">第一条线段端点</param>
        /// <param name="q1">第二条线段端点</param>
        /// <param name="q2">第二条线段端点</param>
        /// <returns>true:有交点; false:无交点</returns>
        public bool isCross(Point p1, Point p2, Point q1, Point q2)
        {
            int vp1, vp2, vp3, vp4;
            vp1 = vectorProduct(p1, q1, p1, p2);
            vp2 = vectorProduct(p1, q2, p1, p2);
            vp3 = vectorProduct(q1, p1, q1, q2);
            vp4 = vectorProduct(q1, p2, q1, q2);
            if (vp1 * vp2 < 0 && vp3 * vp4 < 0)
            {
                return true;
            }
            else
            {
                if (vp1 == 0 && onSegment(p1, p2, q1)) return true;
                else if (vp2 == 0 && onSegment(p1, p2, q2)) return true;
                else if (vp3 == 0 && onSegment(q1, q2, p1)) return true;
                else if (vp4 == 0 && onSegment(q1, q2, p2)) return true;
            }
            return false;
        }

        /// <summary>
        /// 求两线段交点（需先判断两线段是否有交点
        /// </summary>
        /// <param name="p1">第一条线段端点</param>
        /// <param name="p2">第一条线段端点</param>
        /// <param name="q1">第二条线段端点</param>
        /// <param name="q2">第二条线段端点</param>
        /// <returns>交点坐标，若无交点则为空</returns>
        public Point crossPoint(Point p1, Point p2, Point q1, Point q2)
        {
            //参考http://www.cnblogs.com/devymex/archive/2010/08/19/1803885.html
            Point ret = new Point();
            if (isCross(p1, p2, q1, q2) == false) return ret;
            else
            {
                
                if (p1 == q1 || p1 == q2 || p2 == q1 || p2 == q2)
                {
                    //有端点重合
                    if (p2 == q1 || p2 == q2) ret = p2;
                    else ret=p1;
                }
                else
                {
                    int x1,x2,x3,x4,y1,y2,y3,y4;//p1(x1,y1) p2(x2,y2) q1(x3,y3) q2(x4,y4)
                    x1=p1.X;y1=p1.Y;
                    x2=p2.X;y2=p2.Y;
                    x3=q1.X;y3=q1.Y;
                    x4=q2.X;y4=q2.Y;
                    int b1 = (y2 - y1) * x1 + (x1 - x2) * y1;
                    int b2 = (y4 - y3) * x3 + (x3 - x4) * y3;
                    int D,D1,D2;//行列式
                    D = (x2 - x1) * (y4 - y3) - (x4 - x3) * (y2 - y1);
                    D1 = b2 * (x2 - x1) - b1 * (x4 - x3);
                    D2 = b2 * (y2 - y1) - b1 * (y4 - y3);
                    double x0, y0;//交点坐标
                    x0 = Convert.ToDouble(D1) / Convert.ToDouble(D);
                    y0 = Convert.ToDouble(D2) / Convert.ToDouble(D);
                    ret.X = Convert.ToInt32(x0);
                    ret.Y = Convert.ToInt32(y0);
                }
                return ret;
            }
        }

        /// <summary>
        /// 判断由点v1和v2相连的线段是否和多边形的边相交(未完成)
        /// </summary>
        /// <param name="v1">线段端点</param>
        /// <param name="v2">线段端点</param>
        /// <returns>true:线段和多边形相交; false:线段和多边形不相交</returns>
        public bool isCrossWithPolygon(Point v1, Point v2)
        {
            //todo
            
            return false;
        }

        /// <summary>
        /// 判断点v是否在多边形内部
        /// </summary>
        /// <param name="v">点的坐标</param>
        /// <returns>true:点在多边形内; false:点不在多边形内</returns>
        public bool isInPolygon(Point v)
        {
            //参考http://blog.csdn.net/hjh2005/article/details/9246967
            if (vertex.Count < 3) return true;
            LinkedListNode<Point> prep = vertex.First;
            LinkedListNode<Point> cur = prep.Next;
            Point p1, p2;
            //记录扫描线上，v左边和右边与多边形边的交点个数
            int left = 0;
            int right = 0;
            //通过扫描线与多边形的交点判断点是否在内部
            while (cur != null)
            {
                p1 = prep.Value;
                p2 = cur.Value;
                if ((p1.Y < v.Y && p2.Y >= v.Y) || (p2.Y < v.Y && p1.Y >= v.Y))
                {
                    //判断通过v的扫描线与多边形的交点(当前程序中画布的宽是-300->300)
                    Point tmp=crossPoint(p1,p2,new Point(-300,v.Y),new Point(300,v.Y));
                    if (tmp.X < v.X) left++;
                    else right++;
                }
                prep = cur;
                cur = cur.Next;
            }
            cur = vertex.First;
            p1 = prep.Value;
            p2 = cur.Value;
            if ((p1.Y < v.Y && p2.Y >= v.Y) || (p2.Y < v.Y && p1.Y >= v.Y))
            {
                Point tmp = crossPoint(p1, p2, new Point(-300, v.Y), new Point(300, v.Y));
                if (tmp.X < v.X) left++;
                else right++;
            }
            if (left % 2 == 1 && right % 2 == 1) return true;
            else return false;
        }

        /// <summary>
        /// 判断现有的顶点构成的多边形是否自相交
        /// </summary>
        /// <returns>true:是自相交; false:不是自相交</returns>
        public bool isSelfIntersect()
        {
            if (vertex.Count <= 2) return false;//两个顶点无法构成多边形，但同时也不是自相交的
            
            
            LinkedListNode<Point> v00 = vertex.First;
            LinkedListNode<Point> v01 = v00.Next;
            LinkedListNode<Point> v10,v11;//遍历(v00,v01)之后的所有边
            while (v01 != null)
            {
                v10 = v01;
                v11 = v10.Next;
                while (v11 != null)
                {
                    if (v10.Value == v01.Value)
                    {
                        //共顶点的两条边
                        if (vectorProduct(v11.Value, v10.Value, v11.Value, v00.Value) == 0)
                        {
                            //两条边共线，对于除公共顶点外的两个顶点，判断其是否在另一条线段上
                            if (onSegment(v00.Value, v01.Value, v11.Value) == true) return true;
                            if (onSegment(v10.Value, v11.Value, v00.Value) == true) return true;
                        }
                    }
                    else
                    {
                        //两线段无公共顶点，若有交点，则多边形自相交
                        if (isCross(v00.Value, v01.Value, v10.Value, v11.Value) == true) return true;
                    }
                    v10 = v10.Next;
                    v11 = v11.Next;
                }
                //判断最后一个顶点和第一个顶点的连边
                v10 = vertex.Last;
                v11 = vertex.First;
                if (v11.Value == v00.Value || v10.Value == v01.Value)
                {
                    if (v10.Value == v01.Value)
                    {
                        //共顶点的两条边
                        if (vectorProduct(v11.Value, v10.Value, v11.Value, v00.Value) == 0)
                        {
                            //两条边共线，对于除公共顶点外的两个顶点，判断其是否在另一条线段上
                            if (onSegment(v00.Value, v01.Value, v11.Value) == true) return true;
                            if (onSegment(v10.Value, v11.Value, v00.Value) == true) return true;
                        }
                    }
                    if (v11.Value == v00.Value)
                    {
                        //共顶点的两条边
                        if (vectorProduct(v10.Value, v11.Value, v10.Value, v01.Value) == 0)
                        {
                            //两条边共线，对于除公共顶点外的两个顶点，判断其是否在另一条线段上
                            if (onSegment(v00.Value, v01.Value, v10.Value) == true) return true;
                            if (onSegment(v10.Value, v11.Value, v01.Value) == true) return true;
                        }
                    }
                }
                else
                {
                    if (isCross(v00.Value, v01.Value, v10.Value, v11.Value) == true) return true;
                }
                //(v00,v01)遍历下一条边
                v00 = v00.Next;
                v01 = v00.Next;
            }
            return false;
        }

        private void debug(Point v1, Point v2, Point v3, Point v4)
        {
            MessageBox.Show(v1.X.ToString() + " " + v1.Y.ToString() + "\n" +
                            v2.X.ToString() + " " + v2.Y.ToString() + "\n" +
                            v3.X.ToString() + " " + v3.Y.ToString() + "\n" +
                            v4.X.ToString() + " " + v4.Y.ToString() + "\n");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vNew"></param>
        /// <returns></returns>
        public bool isSelfIntersect(Point vNew)
        {
            if (vertex.Count <= 1) return false;//两个顶点无法构成多边形，但同时也不是自相交的
            LinkedListNode<Point> vLast = vertex.Last;
            LinkedListNode<Point> v10, v11;//从头遍历所有的边
            v10 = vertex.First;
            v11 = v10.Next;
            while (v11 != null)
            {
                if (v11 == vLast)
                {
                    //共顶点的两条边
                    if (vectorProduct(v10.Value, vNew, v10.Value, v11.Value) == 0)
                    {
                        //两条边共线，对于除公共顶点外的两个顶点，判断其是否在另一条线段上
                        if (onSegment(v10.Value, v11.Value, vNew) == true) { /*debug(v10.Value,v11.Value,vNew,vLast.Value);*/ return true; }
                        if (onSegment(vNew, vLast.Value, v10.Value) == true) { /*debug(v10.Value, v11.Value, vNew, vLast.Value);*/ return true; }
                    }
                }
                else
                {
                    //判断新的顶点和最后一个顶点的连线是否和原有的边有交点
                    if (isCross(vNew, vLast.Value, v10.Value, v11.Value) == true) { /*debug(v10.Value, v11.Value, vNew, vLast.Value);*/ return true; }
                }
                v10 = v10.Next;
                v11 = v11.Next;
            }
            return false;
        }

        /// <summary>
        /// 在顶点表的末尾加入顶点
        /// </summary>
        /// <param name="v">待加入的顶点坐标</param>
        /// <returns>true:加入成功; false:加入失败</returns>
        public bool addVertex(Point v)
        {
            if (vertex.Count==0)
            {
                vertex.AddFirst(v);
                return true;
            }
            return addVertexAfter(vertex.Last(), v);
        }

        /// <summary>
        /// 在指定的顶点existV后面加入新的顶点newV
        /// </summary>
        /// <param name="existV">已存在的顶点</param>
        /// <param name="newV">待加入的顶点</param>
        /// <returns>true:加入成功; false:加入失败</returns>
        public bool addVertexAfter(Point existV, Point newV)
        {
            if (vertex.Count==0) return false;
            LinkedListNode<Point> node=vertex.Find(existV);
            LinkedListNode<Point> newNode = vertex.Find(newV);
            if (node == null || newNode != null)
            {
                //不存在existV或待加入的顶点已经存在，则返回 失败
                return false;
            }
            else
            {

                if (isSelfIntersect(newV) == false)
                {
                    vertex.AddAfter(node, newV);
                    //若新加入的顶点构成的多边形是自相交的，则返回 失败
                    //vertex.Remove(newNode);
                    //return false;
                }
                else return false;
            }
            return true;
        }

        /// <summary>
        /// 将交点加入交点列表中
        /// </summary>
        /// <param name="inter">交点</param>
        /// <param name="v0">交点所在边的始点</param>
        /// <param name="v1">交点所在边的终点</param>
        /// <param name="isIn">边的始点是否在窗口内部</param>
        public void addIntersections(Point inter, Point v0, Point v1, bool isIn)
        {
            tagIntersections  tmp;
            tmp.v=inter;
            tmp.p0=v0;
            tmp.p1=v1;
            tmp.isIn = isIn;
            intersections.AddLast(tmp);
        }

        /// <summary>
        /// 创建含交点的顶点列表，交点信息已存储在intersections中
        /// </summary>
        public void setClipVertex() 
        {
            LinkedListNode<Point> prep = vertex.First;
            LinkedListNode<Point> cur = prep.Next;
            tagClipVertex newVertex;
            LinkedList<tagIntersections> inter = new LinkedList<tagIntersections>();
            clipVertex = new LinkedList<tagClipVertex>();
            do
            {
                //边的端点加入列表
                newVertex.v=prep.Value;
                newVertex.isIntersection=false;
                newVertex.isTracked = false;
                newVertex.isIn = false;
                clipVertex.AddLast(newVertex);
                //存储边在(prep,cur)上的交点的列表
                inter.Clear();
                //找到所有在边(prep, cur)上的交点
                LinkedListNode<tagIntersections> curI=intersections.First;
                while (curI != null)
                {
                    if ((curI.Value.p0 == prep.Value && curI.Value.p1 == cur.Value) || (curI.Value.p0 == cur.Value && curI.Value.p1 == prep.Value))
                    {
                        inter.AddLast(curI.Value);
                    }
                    curI = curI.Next;
                }

                bool isIn=false;//记录交点所在边的始点是否在窗口内部
                if (inter.Count > 0) isIn = inter.First.Value.isIn;
                while (inter.Count > 0)
                {
                    newVertex.isIntersection = true;
                    LinkedListNode<tagIntersections> iter = inter.First;
                    LinkedListNode<tagIntersections> mini=null;
                    double min=distance(prep.Value,cur.Value);
                    while (iter != null)
                    {
                        double tmp=distance(prep.Value,iter.Value.v);
                        if (tmp < min)
                        {
                            min = tmp;
                            newVertex.v = iter.Value.v;
                            //newVertex.isIn = iter.Value.isIn;
                            mini = iter;
                        }
                        iter = iter.Next;
                    }
                    
                    newVertex.isIn = !isIn;//交点所在边的始点在窗口外部，则交点为入点；否则为出点
                    isIn = !isIn;//所有的交点按顺序一定是一入一出

                    clipVertex.AddLast(newVertex);
                    inter.Remove(mini);
                }
                /*
                //边的端点加入列表
                newVertex.v = cur.Value;
                newVertex.isIntersection = false;
                clipVertex.AddLast(newVertex);*/
                //遍历
                prep=cur;
                cur=cur.Next;
                if (cur==null) cur=vertex.First;
            }while(prep!=vertex.First);
            
        }

        /// <summary>
        /// 通过坐标找到列表中的节点
        /// </summary>
        /// <param name="p">坐标</param>
        /// <returns>链表节点</returns>
        public LinkedListNode<tagClipVertex> findByPoint(Point p)
        {
            LinkedListNode<tagClipVertex> cur = clipVertex.First;
            while (cur != null)
            {
                if (cur.Value.v == p) return cur;
                cur = cur.Next;
            }
            return null;
        }

        /// <summary>
        /// 寻找下一个没有被跟踪过的交点
        /// </summary>
        /// <returns>该交点在clipVertex中的节点信息</returns>
        public LinkedListNode<tagClipVertex> findUnTracked()
        {
            LinkedListNode<tagClipVertex> cur = clipVertex.First;
            while (cur != null)
            {
                if (cur.Value.isIntersection == true && cur.Value.isTracked == false) break;
                cur = cur.Next;
            }
            return cur;
        }

        /// <summary>
        /// 将p设置为已跟踪过
        /// </summary>
        /// <param name="p">要设置的点的坐标</param>
        public void setTracked(Point p)
        {
            LinkedListNode<tagClipVertex> cur = clipVertex.First;
            while (cur != null)
            {
                if (cur.Value.v == p) 
                {
                    tagClipVertex newNode=cur.Value;
                    newNode.isTracked = true;
                    LinkedListNode<tagClipVertex> prev = cur.Previous;
                    clipVertex.Remove(cur);
                    if (prev == null) clipVertex.AddFirst(newNode);
                    else clipVertex.AddAfter(prev, newNode);
                    break;
                }
                cur = cur.Next;
            }
        }

        /// <summary>
        /// 将顶点顺序改为顺时针存储
        /// </summary>
        public void changeVertexOrder()
        {
            if (vertex.Count < 3) return;
            Point v0 = vertex.First.Value;
            Point v1 = vertex.First.Next.Value ;
            Point v2 = vertex.Last.Value;
            Point[] vct = new Point[2];
            vct[0] = new Point(v1.X - v0.X, v1.Y - v0.Y);
            vct[1] = new Point(v2.X - v0.X, v2.Y - v0.Y);
            double[] angle=new double[2];
            //求出 v0->v1和v0->v2关于y轴正半轴的夹角
            for (int i = 0; i < 2; i++)
            {
                if (vct[i].X != 0)
                {
                    angle[i] = Math.Atan(Math.Abs(Convert.ToDouble(vct[i].Y)) / Math.Abs(Convert.ToDouble(vct[i].X)));
                    if (vct[i].X > 0 && vct[i].Y >= 0) angle[i] = 90 - angle[i];
                    else if (vct[i].X > 0 && vct[i].Y < 0) angle[i] = 90 + angle[i];
                    else if (vct[i].X < 0 && vct[i].Y < 0) angle[i] = 270 - angle[i];
                    else if (vct[i].X < 0 && vct[i].Y >= 0) angle[i] = 270 + angle[i];
                }
                else
                {
                    if (vct[i].Y > 0) angle[i] = 0;
                    else angle[i] = 180;
                }
            }
            if (angle[0] < angle[1]) return;
            else
            {
                //将顶点列表反向
                LinkedList<Point> newList = new LinkedList<Point>();
                newList.AddFirst(vertex.First.Value);
                LinkedListNode<Point> cur=vertex.Last;
                while (cur != vertex.First)
                {
                    newList.AddLast(cur.Value);
                    cur = cur.Previous;
                }
                vertex.Clear();
                vertex = newList;
            }
        }

        /// <summary>
        /// 多边形的顶点个数
        /// </summary>
        public int nVertex
        {
            set
            {
                nVertex = vertex.Count;
            }
            get
            {
                return vertex.Count;
            }
        }

        /// <summary>
        /// 按顺序获取多边形顶点，所有顶点都获取完毕时将返回第一个顶点
        /// </summary>
        /// <returns>按顺序获取的顶点坐标</returns>
        public Point getVertex()
        {
            if (curV == null)
            {
                curV = vertex.First;
            }
            Point ret = curV.Value;
            curV = curV.Next;
            return ret;
        }

        /// <summary>
        /// 返回顶点列表的最后一个顶点，必须保证顶点列表中有点，否则报错
        /// </summary>
        /// <returns>顶点列表的最后一个顶点</returns>
        public Point getLast()
        {
            try
            {
                return vertex.Last();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 返回顶点列表的第一个顶点，必须保证顶点列表中有点，否则报错
        /// </summary>
        /// <returns>顶点列表的第一个顶点</returns>
        public Point getFirst()
        {
            try
            {
                return vertex.First();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }

    /// <summary>
    /// 矩形类
    /// </summary>
    class CG_Rectangle
    {
        /// <summary>
        /// 矩形的名称
        /// </summary>
        public string Name
        {
            set
            {
                name = value;
            }
            get
            {
                return name;
            }
        }
        private string name;

        /// <summary>
        /// 矩形左上角顶点的坐标
        /// </summary>
        public Point Location
        {
            set
            {
                location = value;
                top = location.Y;
                bottom = top - height;
                left = location.X;
                right = left + width;
            }
            get
            {
                return location;
            }
        }
        private Point location;

        /// <summary>
        /// 矩形顶边的y坐标
        /// </summary>
        public int Top
        {
            set
            {
                top = value;
                bottom = top - height;
                location.Y = top;
            }
            get
            {
                return top;
            }
        }
        private int top;

        /// <summary>
        /// 矩形底边的y坐标
        /// </summary>
        public int Bottom
        {
            set
            {
                bottom = value;
                top = bottom + height;
                location.Y = top;
            }
            get
            {
                return bottom;
            }
        }
        private int bottom;

        /// <summary>
        /// 矩形左侧边的x坐标
        /// </summary>
        public int Left
        {
            set
            {
                left = value;
                location.X = left;
                right = left + width;
            }
            get
            {
                return left;
            }
        }
        private int left;

        /// <summary>
        /// 矩形右侧边的x坐标
        /// </summary>
        public int Right
        {
            set
            {
                right = value;
                left = right - width;
                location.X = left;
            }
            get
            {
                return right;
            }
        }
        private int right;

        /// <summary>
        /// 矩形的高
        /// </summary>
        public int Height
        {
            set
            {
                height = value;
                bottom = location.Y - height;
            }
            get
            {
                return height;
            }
        }
        private int height;

        /// <summary>
        /// 矩形的宽
        /// </summary>
        public int Width
        {
            set
            {
                width = value;
                right = left + width;
            }
            get
            {
                return width;
            }
        }
        private int width;

        /// <summary>
        /// 构造矩形
        /// </summary>
        /// <param name="loc">左上角坐标</param>
        /// <param name="h">矩形的高</param>
        /// <param name="w">矩形的宽</param>
        public CG_Rectangle(Point loc, int h, int w)
        {
            location = loc;
            height = h;
            width = w;
            top = loc.Y;
            bottom = loc.Y - h;
            left = loc.X;
            right = loc.X + w;
        }

        /// <summary>
        /// 构造矩形
        /// </summary>
        /// <param name="loc">左上角坐标</param>
        /// <param name="diagonal">右下角坐标</param>
        public CG_Rectangle(Point loc, Point diagonal)
        {
            location = loc;
            height = Math.Abs(loc.Y - diagonal.Y);
            width = Math.Abs(loc.X - diagonal.X);
            top = loc.Y;
            bottom = loc.Y - height;
            left = loc.X;
            right = loc.X + width;
        }
    }
}
