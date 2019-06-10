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
        public static Dictionary<String, Image> images;
        public static Timer gameTimer;
        public static Tile[][] tileMat;
        public static int n = 4, m = 4;
        public static List<Tile> toMove;
        public static int ticksToMove = 0;
        public static Queue<Point> mousePositions;


        Random r = new Random();
        bool mouseDown;
        int mouseX, mouseY;
        public Form2048()
        {
            InitializeComponent();
            Form2048.images = LoadImages(@"..\..\..\Slike");
            Form2048.toMove = new List<Tile>();
            Form2048.mousePositions = new Queue<Point>();



            Tile.game = this;
            this.Size = new Size(530, 740);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            gameTimer = new Timer();
            gameTimer.Interval = 30;
            gameTimer.Tick += GameLoop ;
            gameTimer.Start();

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
            this.BackgroundImage = images["background"];
            for (int i = 0; i < n; i++)
            {
                tileMat = new Tile[n][];
                for (int j = 0; j < m; j++)
                {
                    tileMat[j] = new Tile[m];
                }
            }
            PictureBox restart = new PictureBox();
            restart.Size = new Size(110, 50);
            restart.Location = new Point(50, 180);
            restart.Click += Restart;
            restart.BackColor = Color.Transparent;
            Controls.Add(restart);
            restart.Image = images["restart"];

            CreateRandom();
            CreateRandom();
        }
        private void Restart(object sender, EventArgs e)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (tileMat[i][j] != null)
                    {
                        tileMat[i][j].Dispose();
                        tileMat[i][j] = null;
                    }

            CreateRandom();
            CreateRandom();
        }
        private void Form2048_MouseMove(object sender, MouseEventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            mouseX = relativePoint.X;
            mouseY = relativePoint.Y;
            //label1.Text = relativePoint.ToString();
        }
        private void CreateRandom()
        {
            List<Point> empty = new List<Point>();
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    if (tileMat[i][j] == null)
                        empty.Add(new Point(i, j));

            Point point = empty[r.Next(empty.Count-1)];
            int i1 = point.X;
            int j1 = point.Y;
            tileMat[i1][j1] = new Tile(i1, j1, 2);
            Controls.Add(tileMat[i1][j1].pb);
        }
        //da bi menjao pozicije jedino sto ti treba je Tile.ChangePosition(i, j)... sve ostale je automatski u tu funkciju
        private void PlayUp()
        {
            label1.Text = "UP";
            PlayAll();
        }
        private void PlayDown()
        {
            label1.Text = "DOWN";
            PlayAll();
        }
        private void PlayLeft()
        {   
            bool move = false;
            label1.Text = "LEFT";
            for (int i = 0; i < n; i++) {
                for (int j = 1; j < m; j++) {
                    if (tileMat[i][j] != null)
                    {
                        int t = j;
                        do
                        {
                            move = tileMat[i][j].Left(i, --t);
                        } while (move == true); 
                    }
                }
            }
            PlayAll();
        }
        private void PlayRight()
        {
            bool move = false;
            label1.Text = "RIGHT";
            for (int i = 0; i < n; i++ )
            {
                for (int j = tileMat[i].Length - 1; j >= 0; j-- )
                {
                    if (tileMat[i][j] != null) {
                        int t = j;
                        do
                        {
                            move = tileMat[i][j].Right(i, ++t);
                        } while (move == true);
                    }
                }
            }
            PlayAll();
        }
        private void PlayAll()
        {
            CreateRandom();
            ticksToMove = 20;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            if (toMove.Count > 0 && ticksToMove>0)
            {
                foreach (Tile t in toMove)
                    t.MoveTick();
                ticksToMove--;
            }
            if(ticksToMove==0)
                toMove.Clear();

        }
        private void PlayMouse()
        {
            if (mousePositions.Count > 1)
            {
                Point first = mousePositions.Peek();
                int sumX = mouseX - first.X;
                int sumY = mouseY - first.Y;

                if (Math.Abs(sumX) > Math.Abs(sumY))
                {
                    if (sumX > 250)
                    {
                        PlayRight();
                        mousePositions.Clear();
                    }
                    else if (sumX < -250)
                    {

                        PlayLeft();
                        mousePositions.Clear();
                    }
                }
                else
                {
                    if (sumY > 250)
                    {
                        PlayDown();
                        mousePositions.Clear();
                    }
                    else if (sumY < -250)
                    {
                        PlayUp();
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
                    PlayLeft();
                    break;
                case Keys.Right:
                case Keys.L:
                case Keys.D:
                    PlayRight();
                    break;
            }
        }

    }
}
