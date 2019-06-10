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
    public static class Game
    {
        public static Dictionary<String, Image> images;
        public static Tile[][] tileMat;
        public static int n = 4, m = 4;
        public static List<Tile> toMove;
        public static List<Tile> toDraw;
        public static List<Tile> toDelete;
        public static int ticksToMove = 0;
        public static int tickRate;
        public static bool animationPlaying;
        public static Timer animationTimer;
        public static Form2048 form;
        public static Random r;
        
        public static void Init(Form2048 _form)
        {
            r = new Random();
            form = _form;
            animationPlaying = false;
            tickRate = 10;
            animationTimer = new Timer();
            animationTimer.Interval = 20;
            animationTimer.Tick += GameLoop;
            animationTimer.Start();

            ticksToMove = 0;
            toDraw = new List<Tile>();
            toMove = new List<Tile>();
            toDelete = new List<Tile>();
            n = 4; m = 4;

            for (int i = 0; i < n; i++)
            {
                tileMat = new Tile[n][];
                for (int j = 0; j < m; j++)
                {
                    tileMat[j] = new Tile[m];
                }
            }
        }

        public static void Restart()
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
        private static void GameLoop(object sender, EventArgs e)
        {

            if (toMove.Count > 0 && animationPlaying)
            {
                foreach (Tile t in toMove)
                    t.MoveTick();
                ticksToMove--;
            }
            if (ticksToMove == 0)
            {
                if (animationPlaying)
                {
                    foreach (Tile t in toDraw)
                        t.UpdateImage();
                    toDraw.Clear();

                    foreach (Tile t in toDelete)
                        t.Dispose();
                    toDelete.Clear();


                }
                if (toMove.Count > 0)
                    CreateRandom();
                toMove.Clear();

                animationPlaying = false;

            }

        }
        public static void PlayUp()
        {
            if (animationPlaying)
                return;
            for (int j = 0; j < 4; j++)
                for (int i = 0; i < 4; i++)
                    if (tileMat[i][j] != null)
                        tileMat[i][j].Up();
            PlayAll();
        }
        public static void PlayDown()
        {
            if (animationPlaying)
                return;
                for (int j = 0; j < 4; j++)
                    for (int i = 3; i >= 0; i--)
                        if (tileMat[i][j] != null)
                            tileMat[i][j].Down();
            PlayAll();
        }
        public static void PlayLeft()
        {
            if (animationPlaying)
                return;
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (tileMat[i][j] != null)
                        tileMat[i][j].Left();
            PlayAll();
        }
        public static void PlayRight()
        {
            if (animationPlaying)
                return;
            for (int i = 0; i < 4; i++)
                for (int j = 3; j >= 0; j--)
                    if (tileMat[i][j] != null)
                        tileMat[i][j].Right();
            PlayAll();
        }
        public static void PlayAll()
        {
            if(toMove.Count>0)
                ticksToMove = tickRate;
            animationPlaying = true;

        }
        public static void CreateRandom()
        {
            List<Point> empty = new List<Point>();
            for (int i = 0; i < Game.n; i++)
                for (int j = 0; j < Game.m; j++)
                    if (Game.tileMat[i][j] == null)
                        empty.Add(new Point(i, j));

            Point point = empty[r.Next(empty.Count - 1)];
            int i1 = point.X;
            int j1 = point.Y;
            Game.tileMat[i1][j1] = new Tile(i1, j1, 2);
            form.DrawTile(tileMat[i1][j1]);
        }
    }
}
