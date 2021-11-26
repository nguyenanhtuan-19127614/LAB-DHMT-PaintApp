using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _19127614_Lab01
{
    class EquilateralRectangle : Circle
    {
        public double angle_step;
        public int step;
        public EquilateralRectangle(Point start, Point end) : base(start, end)
        {
            this.angle_step = 360 / 4;
            this.step = 5;
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
            for (double i = 1; i < step; i += 1)
            {
                double angle = angle_step * i * System.Math.PI / 180;

                next.X = (int)(this.startPoint.X + radius * System.Math.Cos(angle));
                next.Y = (int)(this.startPoint.Y + radius * System.Math.Sin(angle));

                Line tempLine = new Line(cur, next);
                gl.Begin(OpenGL.GL_POINTS);
                tempLine.Draw(gl);
                gl.End();

                cur = next;
            }
            return true;
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
