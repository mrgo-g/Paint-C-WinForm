using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalPaint
{
    public partial class Form1 : Form
    {
        Graphics g;
        bool isDrawing;
        Pen pen;
        int beginX, beginY, height,weight;
        Color colorToDraw;
        private enum MyShape
        {
            Pen, Shape
        }
        private MyShape currentShape;

        public Form1()
        {
            colorToDraw = Color.Black;
            InitializeComponent();
            g = panel1.CreateGraphics();
            pen = new Pen(colorToDraw, 4);
            pen.SetLineCap(System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.LineCap.Round, System.Drawing.Drawing2D.DashCap.Flat);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            beginX = e.X;
            beginY = e.Y;
            
           pen.Width = Convert.ToInt32(textBox1.Text);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (currentShape.Equals(MyShape.Pen))
            {
                MyPen(e);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            height = e.X - beginX;
            weight = e.Y - beginY;
            Rectangle shape = new Rectangle(beginX, beginY, height, weight);
            if (rect_rbtn.Checked)
            {
                g.DrawRectangle(pen, shape);
            }
            if(elipse_rbtn.Checked)
            {
                g.DrawEllipse(pen, shape);
            }
            if (line_rbtn.Checked)
            {
                Point point1 = new Point(beginX, beginY);
                Point point2 = new Point(e.X, e.Y);
                g.DrawLine(pen, point1, point2);
            }
            
        }
      
        private void MyPen(MouseEventArgs e)
        {
            Point point1 = new Point(beginX, beginY);
            Point point2 = new Point(e.X, e.Y);
            if (isDrawing == true)
            {
                g.DrawLine(pen, point1, point2);
                beginX = e.X;
                beginY = e.Y;
            }
            
           
       
        }
     
        private PictureBox PickedColor;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            pen.Color = p.BackColor;
            colorToDraw = p.BackColor;
            if (PickedColor != null)
                PickedColor.BorderStyle = BorderStyle.FixedSingle;
            PickedColor = p;
            p.BorderStyle = BorderStyle.Fixed3D;
        }
        private void Own_color(object sender, EventArgs e)
        {
            DialogResult ColorPicker = colorDialog1.ShowDialog();
            if (ColorPicker == System.Windows.Forms.DialogResult.OK)
            {
                pen.Color = colorDialog1.Color;
               
            }
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            panel1.Invalidate();
        }

        private void Close_mi_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Save_mi(object sender, EventArgs e)
        {
            int width = panel1.Size.Width;
            int height = panel1.Size.Height;

            using (Bitmap bm = new Bitmap(width, height))
            {
                Graphics graphics = Graphics.FromImage(bm);
                Rectangle rect = panel1.RectangleToScreen(panel1.ClientRectangle);
                graphics.CopyFromScreen(rect.Location, Point.Empty, panel1.Size);

                SaveFileDialog sf = new SaveFileDialog();
                sf.Filter = "Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif|JPEG Image (.jpeg)|*.jpeg|Png Image (.png)|*.png|Tiff Image (.tiff)|*.tiff|Wmf Image (.wmf)|*.wmf";
                if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    var path = sf.FileName;
                    bm.Save(path);
                }
            }
        }

       
        private void statusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = !panel2.Visible;
            if (statusBarToolStripMenuItem.Checked) {
               panel2.Visible=true;
                 }

        }

        private void faqToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(" https://visualstudio.microsoft.com/ru/vs/support/");

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 a = new AboutBox1();
            a.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonPen_Click(object sender, EventArgs e)
        {
            currentShape = MyShape.Pen;
        }

        private void Shape_Click(object sender, EventArgs e)
        {
            currentShape = MyShape.Shape;
        }
       

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
