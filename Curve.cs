﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using CG_Tools;

namespace CG_Exp_2D
{
    
    abstract class Curve
    {
        /*
        /// <summary>
        /// 绘制曲线时，当前点的坐标
        /// </summary>
        private Point cur;
        private T decision_Pk;//当前的决策参数Pk
        public struct tagParam { };//决定曲线形状的参数列表
        private tagParam param;
         * */
        /*
        public  abstract Point[] pointBySymmetry();//根据对称性求点
        public  abstract void getNextDecision();//获取下一个决策参数
        public  abstract bool getNextPoint();//获取cur点的下一个点的坐标*/
        public abstract Point[] getPoints();//获取曲线上所有点的坐标
         
        /// <summary>
        /// 交换两个点的坐标
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void swap<U>(ref U a,ref U b)
        {
            U tmp;
            tmp = a;
            a = b;
            b = tmp;
            
        }
    }
   
    /// <summary>
    /// 直线类
    /// </summary>
    class CG_Line:Curve
    {
        /*    k=-1   k=1
         *      \  |  /   
         *       \3|2/
         *      4 \|/ 1
         *   --------------
         *         |
         *         |
         * */
        /// <summary>
        /// 当前点的坐标
        /// </summary>
        private Point cur;

        /// <summary>
        /// const_1=2delta y;  const_2=2delta y-2delta x
        /// </summary>
        private int const_1, const_2;//计算决策变量时的几个参数，详见构造函数中的说明

        /// <summary>
        /// 决策参数
        /// </summary>
        private int decision_Pk;
        public struct tagParam
        {
            public Point start, end;//起始点
            public Color color;//颜色
        }
        /// <summary>
        /// 直线的名字
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
        /// 返回直线的起始点参数
        /// </summary>
        public tagParam Param
        {
            get
            {
                return originalParam;
            }
        }
        /// <summary>
        /// 起点和终点的坐标
        /// </summary>
        private tagParam param;
        private tagParam originalParam;

        /// <summary>
        /// 存储直线上所有点的点集
        /// </summary>
        private Point[] points;
        /// <summary>
        /// 表示若以原直线的起始点为原点，直线位于的区域，将xy坐标系平分成8个区域，用于对称性
        /// </summary>
        private int area;

        /// <summary>
        /// 构造函数，传入的参数为起始点横纵坐标、终点横纵坐标，并求出曲线对应在坐标系区域1的对称点
        /// </summary>
        /// <param name="pStart">起点坐标</param>
        /// <param name="pEnd">终点坐标</param>
        public CG_Line(Point pStart,Point pEnd, Color c)
        {
            points = null;
            param = new tagParam();
            param.start = pStart;
            param.end = pEnd;
            param.color = c;
            //方便起见，默认起点在终点的下方
            if (param.start.X > param.end.X)
            {
                swap(ref param.start, ref param.end);
            }
            if (param.start.Y > param.end.Y)
            {
                swap(ref param.start, ref param.end);
            }
            originalParam = param;
            this.initialize();
        }

        /// <summary>
        /// 根据Bresenham画线算法生成直线上的所有点并返回包含这些点的一个 点集
        /// </summary>
        /// <returns>点集</returns>
        public override Point[] getPoints()
        {
            if (points != null) return points;
            else
            {
                LinkedList<Point> listPoints = new LinkedList<Point>();
                //根据bresenham画线算法将点生成出来并保存在listPoints中
                foreach (Point cur in this.curPoint())
                {
                    listPoints.AddLast(cur);
                }
                while (this.getNextPoint() == true)
                {
                    foreach (Point cur in this.curPoint())
                    {
                        listPoints.AddLast(cur);
                    }
                }
                //将listPoints转移到points中
                points = new Point[listPoints.Count];
                LinkedListNode<Point> tmp=listPoints.First;
                int n=0;
                while (tmp!=null)
                {
                    points[n]=tmp.Value;
                    n++;
                    tmp=tmp.Next;
                }
                listPoints.Clear();
                return points;
            }
        }

        /// <summary>
        /// 初始化各种参数
        /// </summary>
        private void initialize()
        {
            area = 1;
            //根据对称性将直线翻折到区域1
            if (param.end.X == param.start.X)//直线与y轴平行，则按在区域2处理
            {
                area = 2;
            }
            else
            {
                double k = Convert.ToDouble(param.end.Y - param.start.Y) / Convert.ToDouble(param.end.X - param.start.X);
                if (k > 1)//在区域2
                {
                    area = 2;
                }
                if (k < -1)//区域3
                {
                    area = 3;
                }
                if (k >= -1 && k < 0)//区域4
                {
                    area = 4;
                }
            }
            //区域3和区域4先做关于y轴的对称
            if (area == 3 || area == 4)
            {
                param.end.X = -1 * param.end.X;
                param.start.X = -1 * param.start.X;
            }
            //区域2和区域4需要再做一次关于y=x的对称
            if (area == 2 || area == 3)
            {
                int tmp;
                tmp = param.start.X;
                param.start.X = param.start.Y;
                param.start.Y = tmp;
                tmp = param.end.X;
                param.end.X = param.end.Y;
                param.end.Y = tmp;
            }
            //求计算过程中要用的参数，此时只要根据|k|<1做参数就可以了
            const_1 = 2 * Math.Abs(param.end.Y - param.start.Y);// 2delta y
            const_2 = const_1 - 2 * Math.Abs(param.end.X - param.start.X);//2delta y-2delta x
            decision_Pk = const_1 - Math.Abs(param.end.X - param.start.X);//p0=2delta y-delta x
            cur = param.start;
        }

        /// <summary>
        /// 获取下一个决策参数
        /// </summary>
        private void getNextDecision()
        {
            if (decision_Pk < 0)
            {
                decision_Pk = decision_Pk + const_1;
            }
            else
            {
                decision_Pk = decision_Pk + const_2;
            }
        }

        /// <summary>
        /// 获得关于当前点的对称点，对于直线来说即是原直线所在区域的对称点
        /// </summary>
        /// <returns></returns>
        private  Point[] pointBySymmetry()
        {
            Point[] ret = new Point[1] ;
            ret[0] = cur;
            int tmp;
            if (area == 1) return ret;
            if (area == 2 || area == 3)
            {
                tmp = ret[0].X;
                ret[0].X = ret[0].Y;
                ret[0].Y = tmp;
            }
            if (area == 3 || area == 4)
            {
                ret[0].X = -1 * ret[0].X;
            }
            return ret;
        }
        
        /// <summary>
        /// 获取下一个点的位置
        /// </summary>
        /// <returns>true:成功获取;false:所有点已获取完毕</returns>
        private  bool getNextPoint()
        {
            if (cur.X >= param.end.X) return false;
            Point[] retArr = new Point[1];
            if (decision_Pk < 0)
            {
                cur.X++;
            }
            else
            {
                cur.X++;
                cur.Y++;
            }
            getNextDecision();
            
            return true;
        }

        /// <summary>
        /// 获取当前点的坐标，因为是直线，一次返回一个点
        /// </summary>
        /// <returns>当前点</returns>
        private  Point[] curPoint()
        {
            Point[] arr = new Point[1];
            arr = pointBySymmetry();
            return arr;
        }
    }

    /// <summary>
    /// 圆类
    /// </summary>
    class CG_Circle : Curve
    {
        /// <summary>
        /// 当前点的坐标
        /// </summary>
        private Point cur;

        /// <summary>
        /// 存储圆上的所有点
        /// </summary>
        private Point[] points;

        /// <summary>
        /// 圆的名字
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
        /// 决策参数
        /// </summary>
        private double decision_Pk;
        public struct tagParam
        {
            public Point center;
            public int radius;
            public Color color;
        }

        public tagParam Param
        {
            get
            {
                return param;
            }
        }
        /// <summary>
        /// 参数列表，包含圆心坐标和半径
        /// </summary>
        private tagParam param;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pCenter">圆心坐标</param>
        /// <param name="R">半径</param>
        public CG_Circle(Point pCenter, int R, Color c)
        {
            points = null;
            param = new tagParam();
            param.center = pCenter;
            param.radius = R;
            param.color = c;
            cur.X = 0;
            cur.Y = R;
            decision_Pk = 5.0 / 4.0 - R;
        }

        /// <summary>
        /// 获取圆上的所有点
        /// </summary>
        /// <returns></returns>
        public override Point[] getPoints()
        {
            if (points != null) return points;
            else
            {
                LinkedList<Point> listPoints = new LinkedList<Point>();
                //根据bresenham画线算法将点生成出来并保存在listPoints中
                foreach (Point cur in this.curPoint())
                {
                    listPoints.AddLast(cur);
                }
                while (this.getNextPoint() == true)
                {
                    foreach (Point cur in this.curPoint())
                    {
                        listPoints.AddLast(cur);
                    }
                }
                //将listPoints转移到points中
                points = new Point[listPoints.Count];
                LinkedListNode<Point> tmp = listPoints.First;
                int n = 0;
                while (tmp != null)
                {
                    points[n] = tmp.Value;
                    n++;
                    tmp = tmp.Next;
                }
                listPoints.Clear();
                return points;
            }
        }
        
        /// <summary>
        /// 根据圆的对称性，返回包含8个点的点集
        /// </summary>
        /// <returns>8个点</returns>
        private  Point[] pointBySymmetry()//根据对称性求点
        {
            /*0-8为8个八分圆上对应的点的坐标
             *      \ 4|0 /
             *       \ | /
             *     5  \|/  1
             * -----------------
             *     6  /|\  2
             *       / | \
             *      / 7|3 \
             * */
            
            int tmp;
            Point[] pArr = new Point[8];
            {
                pArr[0] = cur;
            }
            {
                pArr[1] = cur;
                tmp = pArr[1].X;
                pArr[1].X = pArr[1].Y;
                pArr[1].Y = tmp;
            }
            {
                pArr[2] = pArr[1];
                pArr[2].Y = -1 * pArr[2].Y;
            }
            {
                pArr[3] = pArr[0];
                pArr[3].Y = -1 * pArr[3].Y;
            }
            for (int i = 4; i <= 7; i++)
            {
                pArr[i] = pArr[i - 4];
                pArr[i].X = -1 * pArr[i].X;
            }
            return pArr;
        }
        private  void getNextDecision()//获取下一个决策参数
        {
            if (decision_Pk < 0)
            {
                decision_Pk = decision_Pk + 2 * cur.X + 1;
            }
            else
            {
                decision_Pk = decision_Pk + 2 * cur.X + 1 - 2 * cur.Y;
            }
        }
        /// <summary>
        /// 获取下一个点
        /// </summary>
        /// <returns>true:成功获取;false:圆已生成完毕</returns>
        private bool getNextPoint()
        {
            if (cur.X >= cur.Y)
            {
                return false;
            }
            else
            {
                if (decision_Pk < 0)
                {
                    cur.X++;
                }
                else
                {
                    cur.X++;
                    cur.Y--;
                }
                getNextDecision();
                return true;
            }
        }

        /// <summary>
        /// 获取包含当前的点以及其对称点的点集，并平移至圆心为center的圆的位置
        /// </summary>
        /// <returns>圆心在center的圆上的点</returns>
        private Point[] curPoint()
        {
            Point[] arr = new Point[8];
            arr = pointBySymmetry();
            for (int i = 0; i < 8; i++)
            {
                arr[i].X += param.center.X;
                arr[i].Y += param.center.Y;
            }
            return arr;
        }
    }

    /// <summary>
    /// 贝塞尔曲线类
    /// </summary>
    class CG_Bezier : Curve
    {
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
        /// 曲线的控制顶点
        /// </summary>
        public Point[] ControlPoints
        {
            set
            {
                controlPoints = value;
                cntControlPoints = controlPoints.Length;
            }
            get
            {
                return controlPoints;
            }
        }
        private Point[] controlPoints;
        private int cntControlPoints;

        public struct tagAccPoint
        {
            public double X, Y;
        }
        /// <summary>
        /// 曲线上的点
        /// </summary>
        private LinkedList<Point> points;

        /// <summary>
        /// u从0到1均匀取数的个数
        /// </summary>
        private const int rate = 100;

        public CG_Bezier()
        {
            points = new LinkedList<Point>();
            controlPoints = null;
        }

        /// <summary>
        /// 生成贝塞尔曲线上的点
        /// </summary>
        /// <returns>true:成功生成; false:未设置控制顶点生成失败</returns>
        public bool generateBezier()
        {
            if (controlPoints == null) return false;
            points.AddFirst(controlPoints[0]);
            double u;
            tagAccPoint[] accPoints=new tagAccPoint[cntControlPoints];
            
            for (int i = 1; i < rate; i++)
            {
                //将控制顶点的坐标放入数组，作为第一次递推的初始参数
                for (int j = 0; j < cntControlPoints; j++)
                {
                    accPoints[j].X = controlPoints[j].X;
                    accPoints[j].Y = controlPoints[j].Y;
                }
                //计算参数u
                u = Convert.ToDouble(i) / Convert.ToDouble(rate);
                //生成曲线上的点
                for (int depth = cntControlPoints; depth > 1; depth--)
                {
                    for (int k=0;k<depth-1;k++)
                    {
                        double[] tmp = CG_Tools.Tool.divideSegmentByU(accPoints[k].X, accPoints[k].Y, accPoints[k + 1].X, accPoints[k + 1].Y, u);
                        accPoints[k].X = tmp[0];
                        accPoints[k].Y = tmp[1];
                    }
                }
                //将点加入曲线的点集
                points.AddLast(new Point(Convert.ToInt32(accPoints[0].X), Convert.ToInt32(accPoints[0].Y)));
            }
            points.AddLast(controlPoints[cntControlPoints-1]);
            return true;
        }

        /// <summary>
        /// 获取曲线上的点
        /// </summary>
        /// <returns>返回曲线上的点</returns>
        public override Point[] getPoints()
        {
            Point[] ret = new Point[points.Count];
            ret = points.ToArray();
            return ret;
        }
    }
}
