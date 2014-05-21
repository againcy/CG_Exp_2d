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
    class Polygon
    {
        /// <summary>
        /// 多边形的顶点链表
        /// </summary>
        private LinkedList<Point> vertex;
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
        

        public Polygon(Point first)
        {
            vertex = new LinkedList<Point>();
            vertex.AddFirst(first);
            curV = vertex.First;
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
        private bool isCross(Point p1, Point p2, Point q1, Point q2)
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
                /*
                double delta = vectorProduct(p2.X - p1.X, q1.X - q2.X, p2.Y - p1.Y, q1.Y - q2.Y);
                if (delta ==0 )  // delta==0，表示两线段重合或平行  
                {
                    return false;
                }
                double namenda = vectorProduct(q1.X - p1.X, q1.X - q2.X, q1.Y - p1.Y, q1.Y - q2.Y) / delta;
                if (namenda > 1 || namenda < 0)
                {
                    return false;
                }
                double miu = vectorProduct(p2.X - p1.X, q1.X - p1.X, p2.Y - p1.Y, q1.Y - p1.Y) / delta;
                if (miu > 1 || miu < 0)
                {
                    return false;
                }
                return true;
                */
                /*
                d1 ====>   (P2 - P1) x (Q1 - P1) (叉积)

　　　　        d2 ====>   (P2 - P1) x (Q2 - P1) (叉积)

　　　　        d3 ====>   (Q2 - Q1) x (P1 - Q1) (叉积)

　　　　        d4 ====>   (Q2 - Q1) x (P2 - P1) (叉积)*/
                /*
                int d1, d2, d3, d4;
                d1 = determinant(p2.X - p1.X, p2.Y - p1.Y, q1.X - p1.X, q1.Y - p1.Y);
                d2 = determinant(p2.X - p1.X, p2.Y - p1.Y, q2.X - p1.X, q2.Y - p1.Y);
                d3 = determinant(q2.X - q1.X, q2.Y - q1.Y, p1.X - q1.X, p1.Y - q1.Y);
                d4 = determinant(q2.X - q1.X, q2.Y - q1.Y, p2.X - p1.X, p2.Y - p1.Y);
                if (d1 * d2 < 0 && d3 * d4 < 0) return true;
                else return false;
                          */
                
            
        }

        /// <summary>
        /// 判断由点v1和v2相连的线段是否和多边形的边相交
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
            //todo
            return true;
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
            if (curV == null) return vertex.First();
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
}
