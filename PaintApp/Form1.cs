using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.WinForms;

namespace _19127614_Lab01
{
    
    public partial class Form1 : Form
    {
       
        Polygon drawPolygon=null;
        Point pStart, pEnd, mousePos;
        Color userColor = Color.Black;
        int Size, userShape = 0;
        int selectedIndex = -1;
        bool drawing = false;
        bool rightClick = false, leftClick = false;
        
        List<Shape> objDrawing = new List<Shape>();
        public Form1()
        {
            InitializeComponent();
            this.Text = "19127614-HCMUS-Paint";
        }
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            // Get the OpenGL object.
            OpenGL gl = openglControl1.OpenGL;
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
            // Create a perspective transformation.
            gl.Viewport(0, 0, openglControl1.Width, openglControl1.Height);
            // Dùng chỉnh để lật hệ quy chiếu Oxy lại
            //gl.Ortho2D(0, openglControl1.Width,  0,openglControl1.Height);
            gl.Ortho2D(0, openglControl1.Width, openglControl1.Height, 0);

        }
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            
            // Get the OpenGL object.
            OpenGL gl = openglControl1.OpenGL;
            // Set the clear color.
            gl.ClearColor(1, 1, 1, 1);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            // Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            // Load the identity.
            gl.LoadIdentity();
          
        }
        
        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {

            OpenGL gl = openglControl1.OpenGL;
           

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);       

            foreach (var shape in objDrawing)
            {
                
                gl.Color(shape.shape_Color);
                gl.PointSize(shape.pixel_Size);
                shape.Draw(gl);                    
                shape.FloodFill_Fill(gl);
            }
            if (drawing == true)
            {

                if (userShape == 1)
                {
                    Line drawline = new Line(pStart, mousePos);
                    drawline.Mode(Size, userColor);
                    drawline.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawline;
                }
                else if (userShape == 2)
                {
                    Circle drawCircle = new Circle(pStart, mousePos);
                    drawCircle.Mode(Size, userColor);
                    drawCircle.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawCircle;

                }
                else if (userShape == 3)
                {
                    Ellipse drawEllipse = new Ellipse(pStart, mousePos);
                    drawEllipse.Mode(Size, userColor);
                    drawEllipse.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawEllipse;

                }
                else if (userShape == 4)
                {
                    Rectangle drawRectangle = new Rectangle(pStart, mousePos);
                    drawRectangle.Mode(Size, userColor);
                    drawRectangle.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawRectangle;

                }
                else if (userShape == 5)
                {
                    EquilateralRectangle drawSquare = new EquilateralRectangle(pStart, mousePos);
                    drawSquare.Mode(Size, userColor);
                    drawSquare.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawSquare;

                }
                else if (userShape == 6)
                {
                    EquilateralPentagon drawPentagon = new EquilateralPentagon(pStart, mousePos);
                    drawPentagon.Mode(Size, userColor);
                    drawPentagon.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawPentagon;

                }
                else if (userShape == 7)
                {
                    EquilateralHexagon drawHexagon = new EquilateralHexagon(pStart, mousePos);
                    drawHexagon.Mode(Size, userColor);
                    drawHexagon.Draw(gl);
                    objDrawing[objDrawing.Count - 1] = drawHexagon;

                }
                
                else if (userShape == 9)
                {                 
                    for (int i=0;i<objDrawing.Count;i++)
                    {
                        if (objDrawing[i].isInside(pEnd) == true)
                        {
                            selectedIndex = i;
                            Trace.WriteLine(selectedIndex);
                        }
                    }
                }    
            }
          
            gl.Flush();
        }

        private void openglControl1_Load(object sender, EventArgs e)
        { }
        //Event nhấn thả , di chuyển chuột trên opengl
        //Nhấn
        private void openglControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pStart = e.Location;
                pEnd = pStart;
                drawing = true;
                if (userShape != 8 && userShape != 9)
                {                  
                    objDrawing.Add(new Shape(pStart, pStart));
                }
                else if (userShape==8)
                {                    
                    if (drawPolygon.Count()==0)
                    {             
                        objDrawing.Add(new Shape(pStart, pStart));
                    }    
                }    
            }
        }
        //thả
        private void openglControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pEnd = e.Location;
                drawing = false;
                leftClick = true;
                //Add thêm đỉnh vào polygon
                if(userShape==8&&drawPolygon!=null)
                {
                    drawPolygon.addControlPoint(pEnd);
                    objDrawing[objDrawing.Count - 1] = drawPolygon;
                }    
            }
           
            else if (e.Button == MouseButtons.Right)
            {
                rightClick = true;
                //dừng polygon
                if (userShape == 8 && drawPolygon != null)
                {
                    drawPolygon = null;
                    userShape = -1;
                }                
                
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if(userShape==8 && drawPolygon!=null)
                {
                    drawPolygon.makeClosed();
                    objDrawing[objDrawing.Count - 1] = drawPolygon;
                   
                }    
            }
        }
        // di chuyển, Dùng để vẽ như chức năng brush hay pencil của paint+
        private void openglControl1_MouseMove(object sender, MouseEventArgs e)
        {
            OpenGL gl = openglControl1.OpenGL;
            mousePos = e.Location;
            if (drawing == true && userShape == 0)
            {

                Line drawline = new Line(pStart, mousePos);
                drawline.Mode(Size, userColor);
                objDrawing.Add(drawline);
                //drawline.Draw(gl);
                pStart = mousePos;
            }
        }
        //Clear màn hình
        private void button_clear_click(object sender, EventArgs e)
        {
            objDrawing.Clear();
            userShape = -1;
        }
        //Chọn kích cỡ vẽ
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Size = Convert.ToInt32(numericUpDown1.Value);
        }
        //Nút chọn màu, color diaglog
        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                userColor = colorDialog1.Color;
            }
        }

        //nút brushes, bút chì vẽ tự do
        private void button7_Click(object sender, EventArgs e)
        {
            userShape = 0;
        }
        //nút vẽ Line
        private void button3_Click(object sender, EventArgs e)
        {
            userShape = 1;
        }


        //Nút vẽ hình tròn
        private void button4_Click(object sender, EventArgs e)
        {
            userShape = 2;
        }
        //Nút vẽ hình ellipse
        private void button5_Click(object sender, EventArgs e)
        {
            userShape = 3;
        }

        //Nút vẽ hình chứ nhật
        private void button6_Click(object sender, EventArgs e)
        {
            userShape = 4;
        }
        //Nút vẽ hình chữ nhật đều ( hình vuông)
        private void button8_Click(object sender, EventArgs e)
        {
            userShape = 5;
        }

        //Nút vẽ hình ngũ giác đều
        private void button9_Click(object sender, EventArgs e)
        {
            userShape = 6;
        }
        //Nút vẽ hình lục giác đều
        private void button10_Click(object sender, EventArgs e)
        {
            userShape = 7;
        }

        //Vẽ polygon bằng các đỉnh nối nhau
        private void button11_Click(object sender, EventArgs e)
        {
            drawPolygon = new Polygon();
            userShape = 8;
        }
        // chọn object
        private void button12_Click(object sender, EventArgs e)
        {
            userShape = 9;
           
        }
        //Dropdown
        private void button13_Click(object sender, EventArgs e)
        {
            if(panel1.Height==25)
            {
                panel1.Height = 80;
            }    
            else
            {
                panel1.Height = 25;
            }    
        }
        //scanline
        private void button14_Click_1(object sender, EventArgs e)
        {
            if (selectedIndex != -1)
            {
                return;
            }    
        }
        //floodfill
        private void button15_Click(object sender, EventArgs e)
        {
            if(selectedIndex!=-1)
            {
                objDrawing[selectedIndex].TurnOnFloodFill();
                Trace.WriteLine(objDrawing[selectedIndex].floodfill_filled);
                objDrawing[selectedIndex].SetFillColor(userColor);
            }    
            
        }


    }

}
