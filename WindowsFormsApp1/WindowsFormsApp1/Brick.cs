using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Brick
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;
        public Color MyColor;
        public int Lives;

        public Brick(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Lives = 1;
            MyColor = Color.BurlyWood;
        }

        public Brick(int x, int y, int width, int height, int lives)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Lives = lives;
            if (Lives == 1)
                MyColor = Color.BurlyWood;
            if (Lives == 2)
                MyColor = Color.Coral;
            if (Lives == 3)
                MyColor = Color.Brown;
            if (Lives <= -1)
                MyColor = Color.BlueViolet;
        }

        public void Break()
        {
            Lives--;
            if (Lives == 1)
                MyColor = Color.BurlyWood;
            if (Lives == 2)
                MyColor = Color.Coral;
            if (Lives == 3)
                MyColor = Color.Brown;
        }

    }
}
