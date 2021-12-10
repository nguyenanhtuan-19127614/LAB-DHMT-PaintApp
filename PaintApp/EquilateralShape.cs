using System;
using System.Collections.Generic;
using System.Windows;
using System.Text;
using System.Linq;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _19127614_Lab01
{
    class EquilateralRectangle : Polygon
    {
        public double angle_step;
        public int step;
        public EquilateralRectangle(Point start, Point end) : base(start, end)
        {
            this.radius = Convert.ToInt32(Math.Sqrt(Math.Pow((end.X - start.X), 2) + Math.Pow((end.Y - start.Y), 2)));
            this.angle_step = 360 / 4;
            this.step = 5;
            this.points= new List<Point>();
        }
        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }
            Point cur = new Point();
            Point next = new Point();
            cur.X = (int)(this.startPoint.X + radius * System.Math.Cos(0));
            cur.Y = (int)(this.startPoint.Y + radius * System.Math.Sin(0));
            this.points.Add(cur);
            for (double i = 1; i < step; i += 1)
            {
                double angle = angle_step * i * System.Math.PI / 180;

                next.X = (int)(this.startPoint.X + radius * System.Math.Cos(angle));
                next.Y = (int)(this.startPoint.Y + radius * System.Math.Sin(angle));
                this.points.Add(next);
                Line tempLine = new Line(cur, next);               
                tempLine.Draw(gl);          
                for (int j = 0; j < this.width; j++)
                {
                    for (int k = 0; k < this.height; k++)
                    {
                        
                        if(tempLine.wall[j, k]==true)
                        {
                            this.wall[j, k] = true;
                        }    
                        //this.wall[j, k] = this.wall[j, k] ^ tempLine.wall[j, k];
                    }
                }
                cur = next;
            }
            return true;
        }
        public override void FloodFill_Fill(OpenGL gl)
        {
            if (floodfill_filled == true)
            {
                if (coloredPixel.Count > 0)
                {
                    foreach (var p in coloredPixel)
                    {
                        gl.Color(fill_color.R, fill_color.G, fill_color.B);
                        gl.Begin(OpenGL.GL_POINTS);
                        gl.Vertex(p.X, p.Y);
                        gl.End();
                    }
                    return;
                }

                Stack<Point> stack = new Stack<Point>();
                stack.Push(new Point(this.startPoint.X + 1, this.startPoint.Y + 1));
                //gl.Color(fill_color.R, fill_color.G, fill_color.B);
                while (stack.Count > 0)
                {
                    Point p = stack.Pop();
                    coloredPixel.Add(p);
                    //Trace.WriteLine(p);

                    //đi lên
                    if (this.wall[p.X + 1, p.Y] == false)
                    {
                        Point temp = new Point(p.X + 1, p.Y);
                        if (this.Find(coloredPixel, temp) == false)
                        {
                            stack.Push(temp);
                        }
                    }
                    // đi xuống
                    if (this.wall[p.X - 1, p.Y] == false)
                    {
                        Point temp = new Point(p.X - 1, p.Y);
                        if (this.Find(coloredPixel, temp) == false)
                        {
                            stack.Push(temp);
                        }
                    }
                    // qua phải
                    if (this.wall[p.X, p.Y + 1] == false)
                    {
                        Point temp = new Point(p.X, p.Y + 1);
                        if (this.Find(coloredPixel, temp) == false)
                        {
                            stack.Push(temp);
                        }
                    }
                    // qua trái
                    if (this.wall[p.X, p.Y - 1] == false)
                    {
                        Point temp = new Point(p.X, p.Y - 1);
                        if (this.Find(coloredPixel, temp) == false)
                        {
                            stack.Push(temp);
                        }
                    }
                }
            }
            return;
        }
        public override void TurnOnFloodFill()
        {
            floodfill_filled = true;
        }
    }
    class EquilateralPentagon : EquilateralRectangle
    {
        public EquilateralPentagon(Point start, Point end) : base(start, end)
        {
            this.angle_step = 360 / 5;
            this.step = 6;
        }
    }
    class EquilateralHexagon : EquilateralRectangle
    {
        public EquilateralHexagon(Point start, Point end) : base(start, end)
        {
            this.angle_step = 360 / 6;
            this.step = 7;
        }
    }
}
