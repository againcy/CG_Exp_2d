using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CG_Tools
{
    /// <summary>
    /// 包含了图形学中的一些常用函数
    /// </summary>
    public static class Tool
    {
        /// <summary>
        /// 计算两点p0和p1间的距离
        /// </summary>
        /// <param name="p0">p0的坐标</param>
        /// <param name="p1">p1的坐标</param>
        /// <returns>距离</returns>
        public static double distanceBetween(Point p0, Point p1)
        {
            double a = (p1.X - p0.X) * (p1.X - p0.X);
            double b = (p1.Y - p0.Y) * (p1.Y - p0.Y);
            return Math.Sqrt(a + b);
        }

        /// <summary>
        /// 计算两点p0和p1间的距离
        /// </summary>
        /// <param name="x0">p0的x坐标</param>
        /// <param name="y0">p0的y坐标</param>
        /// <param name="x1">p1的x坐标</param>
        /// <param name="y1">p1的y坐标</param>
        /// <returns>距离</returns>
        public static double distanceBetween(int x0, int y0, int x1, int y1)
        {
            double a = (x1 - x0) * (x1 - x0);
            double b = (y1 - y0) * (y1 - y0);
            return Math.Sqrt(a + b);
        }

        /// <summary>
        /// 求向量p0->p1和q0->q1的叉积, 只返回-1或1表示结果的符号
        /// </summary>
        /// <returns>叉积</returns>
        public static int vectorProduct(Point p0, Point p1, Point q0, Point q1)
        {
            int a = (p1.X - p0.X) * (q1.Y - q0.Y);
            int b = (p1.Y - p0.Y) * (q1.X - q0.X);
            if (a - b > 0) return 1;
            else return -1;
        }

        /// <summary>
        /// 判断pk是否在(pi,pj)上，需保证是pk在直线(pi,pj)上
        /// </summary>
        /// <param name="pi">线段一端点pi坐标</param>
        /// <param name="pj">线段另一端点pj坐标</param>
        /// <param name="pk">需要判断的点pk坐标</param>
        /// <returns>true:在线段上; false:不在线段上</returns>
        private static bool onSegment(Point pi, Point pj, Point pk)
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
            else return false;
        }

        /// <summary>
        /// 判断pk是否在(pi,pj)上
        /// </summary>
        /// <param name="pi">线段一端点pi坐标</param>
        /// <param name="pj">线段另一端点pj坐标</param>
        /// <param name="pk">需要判断的点pk坐标</param>
        /// <returns>true:在线段上; false:不在线段上</returns>
        public static bool isOnSegment(Point pi, Point pj, Point pk)
        {
            if (vectorProduct(pi, pk, pi, pj) == 0)
            {
                if (onSegment(pi, pj, pk) == true) return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// 判断两条线段(p1,p2)和(q1,q2)是否相交
        /// </summary>
        /// <param name="p1">第一条线段端点</param>
        /// <param name="p2">第一条线段端点</param>
        /// <param name="q1">第二条线段端点</param>
        /// <param name="q2">第二条线段端点</param>
        /// <returns>true:有交点; false:无交点</returns>
        public static bool isCross(Point p1, Point p2, Point q1, Point q2)
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
        /// 求两线段交点，交点坐标经过取整操作。若无交点返回(0, 0)
        /// </summary>
        /// <param name="p1">第一条线段端点p1</param>
        /// <param name="p2">第一条线段端点p2</param>
        /// <param name="q1">第二条线段端点q1</param>
        /// <param name="q2">第二条线段端点q2</param>
        /// <returns>交点坐标，若无交点则为空</returns>
        public static Point crossPoint(Point p1, Point p2, Point q1, Point q2)
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
                    else ret = p1;
                }
                else
                {
                    int x1, x2, x3, x4, y1, y2, y3, y4;//p1(x1,y1) p2(x2,y2) q1(x3,y3) q2(x4,y4)
                    x1 = p1.X; y1 = p1.Y;
                    x2 = p2.X; y2 = p2.Y;
                    x3 = q1.X; y3 = q1.Y;
                    x4 = q2.X; y4 = q2.Y;
                    int b1 = (y2 - y1) * x1 + (x1 - x2) * y1;
                    int b2 = (y4 - y3) * x3 + (x3 - x4) * y3;
                    int D, D1, D2;//行列式
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
        /// 将线段PQ按u:1-u分割并返回分割点
        /// </summary>
        /// <param name="p0">P.X</param>
        /// <param name="p1">P.Y</param>
        /// <param name="q0">Q.X</param>
        /// <param name="q1">Q.Y</param>
        /// <param name="u">分割比</param>
        /// <returns>存储分割点坐标X和Y的长度为2的数组</returns>
        public static double[] divideSegmentByU(double p0, double p1, double q0, double q1, double u)
        {
            double[] ret = new double[2];
            ret[0] = q0 * u + (1 - u) * p0;
            ret[1] = q1 * u + (1 - u) * p1;
            return ret;
        }
    }
}
