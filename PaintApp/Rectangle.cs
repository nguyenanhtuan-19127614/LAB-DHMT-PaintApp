using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;
namespace _19127614_Lab01
{
    class Rectangle : Line
    {
        public Point leftDown, rightUp;
        public Rectangle(Point start, Point end) : base(start, end)
        {
            //đỉnh trái dưới
            this.leftDown.X = start.X;
            this.leftDown.Y = end.Y;
            //đỉnh phải trên
            this.rightUp.X = end.X;
            this.rightUp.Y = start.Y;
        }
        public override bool Draw(OpenGL gl)
        {
            if (this.startPoint == this.endPoint)
            {
                return false;
            }
            Line lineUp = new Line(this.startPoint, this.rightUp);
            Line lineRight = new Line(this.startPoint, this.leftDown);
            Line lineDown = new Line(this.leftDown, this.endPoint);
            Line lineLeft = new Line(this.rightUp, this.endPoint);
            lineUp.Draw(gl);
            lineRight.Draw(gl);
            lineDown.Draw(gl);
            lineLeft.Draw(gl);
            return true;
        }
    }
}
