﻿using System;
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
        Random r = new Random();
        public Form2048()
        {
            InitializeComponent();
            Form2048.images = LoadImages(@"..\..\..\Slike");
            Form2048.toMove = new List<Tile>();
            Tile.game = this;
            this.Size = new Size(530, 740);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            gameTimer = new Timer();
            gameTimer.Interval = 30;
            gameTimer.Tick += GameLoop ;

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

            CreateRandom();
            CreateRandom();
        }

        private void Form2048_MouseMove(object sender, MouseEventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            label1.Text = relativePoint.ToString();
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
            PlayAll();
        }
        private void PlayDown()
        {
            PlayAll();
        }
        private void PlayLeft()
        {
            PlayAll();
        }
        private void PlayRight()
        {
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
    }
}
