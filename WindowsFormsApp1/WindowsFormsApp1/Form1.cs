using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        
        private List<Ball> balls = new List<Ball>();
        private int k = 0;
        private List<Brick> bricks = new List<Brick>();
        private int brickWidth = 80;
        private int brickHeight = 25;
        private int initialVDistance = 10;
        private int brickDistance = 5;
        private Brick player = new Brick(425, 800, 150, 20, -1);



        public Form1()
        {
            InitializeComponent();
            this.Paint += new PaintEventHandler(PaintBall);
            this.Paint += new PaintEventHandler(PaintBricks);
            this.MouseWheel += new MouseEventHandler(Form1_MouseWheel);
            this.DoubleBuffered = true;
            this.SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer,
                true);
            CreateBricks1();
            balls.Add(new Ball(488, 775, 25, 25));


        }
        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (timer1.Interval + (e.Delta / 40) > 0)
                timer1.Interval += e.Delta / 40;
        }

        private void PaintBall(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            foreach (Ball ball in balls)
            {
                SolidBrush BallBrush = new SolidBrush(ball.color);
                graphics.FillEllipse(BallBrush, ball.X, ball.Y, ball.Width, ball.Height);
                BallBrush.Dispose();
            }

        }

        private void PaintBricks(object sender, PaintEventArgs e)
        {
            graphics = e.Graphics;
            foreach (Brick brick in bricks)
            {
                SolidBrush BrickBrush = new SolidBrush(brick.MyColor);
                graphics.FillRectangle(BrickBrush, brick.X, brick.Y, brick.Width, brick.Height);
                BrickBrush.Dispose();
            }

            SolidBrush plBrush = new SolidBrush(player.MyColor);
            graphics.FillRectangle(plBrush, player.X, player.Y, player.Width, player.Height);
            plBrush.Dispose();
        }

        private void BrickBreake()
        {
            int i = 0;
            int j = 0;
            while (i < balls.Count)
            {
                j = 0;
                while (j < bricks.Count)
                {
                    if (Collapse(balls[i], bricks[j]))
                    {
                        balls[i].Dy = -balls[i].Dy;
                        bricks[j].Break();
                    }

                    if (bricks[j].Lives == 0)
                    {
                        bricks.Remove(bricks[j]);
                        j--;
                    }
                    j++;
                }
                if (Collapse(balls[i], player))
                {
                    balls[i].Dy = -balls[i].Dy;
                }
                i++;
            }

        }

        private bool Collapse(Ball ball, Brick brick)
        {
            for (int i = 0; i < 360; i++)
            {
                double x = ball.X + (ball.Width / 2) * Math.Cos(i);
                double y = ball.Y + (ball.Height / 2) * Math.Sin(i);
                if (x >= brick.X && x <= brick.X + brickWidth && y >= brick.Y && y <= brick.Y + brick.Height)
                    return true;
            }
            return false;
        }

        private void CreateBricks1()
        {
            bricks.Add(new Brick(ClientSize.Width / 2 - (brickWidth / 2), initialVDistance, brickWidth, brickHeight));
            bricks.Add(new Brick(ClientSize.Width / 2 - ((brickWidth / 2) * 3) - brickDistance, initialVDistance + brickHeight + 5,
                brickWidth, brickHeight));
            bricks.Add(new Brick(ClientSize.Width / 2 - (brickWidth / 2), initialVDistance + brickHeight + 5, brickWidth, brickHeight));
            bricks.Add(new Brick(ClientSize.Width / 2 + (brickWidth / 2) + brickDistance, initialVDistance + brickHeight + 5, brickWidth, brickHeight));
            bricks.Add(new Brick(460, 100, brickWidth, brickHeight, 2));
        }


        private void MoveBall()
        {
            foreach (Ball ball in balls)
            {
                if (ball.Move == true)
                {

                    if (ball.X < -5 || ball.X > this.ClientSize.Width - ball.Width)
                    {
                        ball.Dx = -ball.Dx;
                        ball.ChangeColor();
                    }

                    if (ball.Y < 0)
                    {
                        ball.Dy = -ball.Dy;
                        ball.ChangeColor();
                    }

                    if (ball.Y > this.ClientSize.Height - ball.Height)
                    {
                        ball.X = player.X + 75;
                        ball.Y = player.Y - 25;
                        ball.Dy = -ball.Dy;
                        ball.Move = false;
                    }

                    ball.X += ball.Dx;
                    ball.Y += ball.Dy;
                }
            }


            Invalidate();

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveBall();
            BrickBreake();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
//            Random rand = new Random();
//            int ran = rand.Next(50, 150);
//            balls.Add(new Ball(e.Location.X, e.Location.Y, ran, ran));
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                foreach (Ball ball in balls)
                {
                    ball.Move = true;
                }
            }

            if (e.KeyCode == Keys.Left)
            {
                if (player.X >= 3)
                {
                    player.X -= 10;
                    foreach (Ball ball in balls)
                    {
                        if (ball.Move == false)
                            ball.X -= 10;
                    }
                }
            }

            if (e.KeyCode == Keys.Right)
            {
                if (player.X <= ClientSize.Width)
                {
                    player.X += 15;
                    foreach (Ball ball in balls)
                    {
                        if (ball.Move == false)
                            ball.X += 15;
                    }
                }
            }
        }
    }
}
