using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace _2048
{

    public partial class Form2048 : Form
    {




        Random r = new Random();
        bool mouseDown;
        int mouseX, mouseY;
        Queue<Point> mousePositions;
        public Form2048()
        {
            InitializeComponent();

            Game.Init(this);
            mousePositions = new Queue<Point>();
            Game.images = LoadImages(@"..\..\..\Slike");

            Tile.game = this;
            this.Size = new Size(526, 740);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            

            Timer t = new Timer();
            t.Interval = 40;
            t.Tick += MouseMotion;
            t.Start();
            this.Icon = Icon.ExtractAssociatedIcon(@"..\..\..\Slike\icon.ico");
            this.Text = "Felix 2048";
        }
        public Dictionary<String, Image> LoadImages(string path)
        {
            Dictionary<String, Image> images = new Dictionary<String, Image>();
            string[] buffer = Directory.GetFiles(path, "*.png");
            foreach (string imagePath in buffer)
            {
                string[] parts = imagePath.Split('\\');
                string name = parts[parts.Length - 1].Split('.')[0];
                Image img = Image.FromFile(imagePath);
                images.Add(name, img);
            }
            return images;

        }

        private void Form2048_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Game.images["background"];
            
            PictureBox restart = new PictureBox();
            restart.Size = new Size(110, 50);
            restart.Location = new Point(50, 180);
            restart.Click += Restart;
            restart.BackColor = Color.Transparent;
            Controls.Add(restart);
            restart.Image = Game.images["restart"];

            Game.Restart();
        }
        private void Restart(object sender, EventArgs e)
        {
            Game.Restart();
        }
        private void Form2048_MouseMove(object sender, MouseEventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            mouseX = relativePoint.X;
            mouseY = relativePoint.Y;
            //label1.Text = relativePoint.ToString();
        }
        public void DrawTile(Tile t)
        {
            Controls.Add(t.pb);
        }
        //da bi menjao pozicije jedino sto ti treba je Tile.ChangePosition(i, j)... sve ostale je automatski u tu funkciju
        

        
        private void PlayMouse()
        {
            if (mousePositions.Count > 1)
            {
                Point first = mousePositions.Peek();
                int sumX = mouseX - first.X;
                int sumY = mouseY - first.Y;

                if (Math.Abs(sumX) > Math.Abs(sumY))
                {
                    if (sumX > 200)
                    {
                        Game.PlayRight();
                        mousePositions.Clear();
                    }
                    else if (sumX < -200)
                    {

                        Game.PlayLeft();
                        mousePositions.Clear();
                    }
                }
                else
                {
                    if (sumY > 200)
                    {
                        Game.PlayDown();
                        mousePositions.Clear();
                    }
                    else if (sumY < -200)
                    {
                        Game.PlayUp();
                        mousePositions.Clear();
                    }
                }
            }
        }
        private void MouseMotion(object sender, EventArgs e)
        {
            if (mouseDown)
            {
                mousePositions.Enqueue(new Point(mouseX, mouseY));
                PlayMouse();
            }
            else
            {
                PlayMouse();
                mousePositions.Clear();
            }

            if (mousePositions.Count > 4)
                mousePositions.Dequeue();
        }





        private void Form2048_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
        }

        private void Form2048_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void Form2048_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) { 
                case Keys.A:
                case Keys.Left:
                case Keys.H:
                    Game.PlayLeft();
                    break;
                case Keys.Right:
                case Keys.L:
                case Keys.D:
                    Game.PlayRight();
                    break;
                case Keys.W:
                case Keys.K:
                case Keys.Up:
                    Game.PlayUp();
                    break;
                case Keys.S:
                case Keys.J:
                case Keys.Down:
                    Game.PlayDown();
                    break;


            }
        }

    }
}
