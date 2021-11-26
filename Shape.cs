using System;
using SharpGL;
using System.Diagnostics;
using System.Drawing;

namespace _19127614_Lab01
{
    //Class cha
    class Shape
    {
        public Point startPoint, endPoint;
        public int radius = 0;
        public int pixel_Size = 0;
        public Color shape_Color;
        public Shape(Point start, Point end)
        {
            this.startPoint = start;
            this.endPoint = end;
        }
        ~Shape() { }
        public virtual void Mode(int size, Color userColor)
        {
            this.pixel_Size = size;
            this.shape_Color = userColor;
        }
        public virtual bool Draw(OpenGL gl)
        {
            return true;
        }
    }
}
