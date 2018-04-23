using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Ball
    {
        public int X;
        public int Y;
        public int Height;
        public int Width;
        public int Dx;
        public int Dy;
        public Color color;
        public int Power;
        public bool Move;

        public Ball(int x, int y, int width, int height)
        {
            Random rand = new Random();
            X = x;
            Y = y;
            Height = height;
            Width = width;
            Dx = rand.Next(-10, 10);
            Dy = rand.Next(-10, 10);
            Move = false;
            ChangeColor();
        }


        public  void ChangeColor()
        {
            Random rand = new Random();
            color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }
    }
}
