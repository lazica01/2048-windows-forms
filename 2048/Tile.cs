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
    public class Tile
    {
        public static int offsetX = 60;
        public static int offsetY = 250;
        public static Form2048 game;

        public PictureBox pb;
        private int i, j;
        public int value;
        private int moveDistanceH;
        private int moveDistanceV;

        public Tile() { }

        public Tile(int i, int j, int value)
        {
            pb = new PictureBox();
            pb.Size = new Size(90, 90);
            this.i = i;
            this.j = j;
            pb.Location = new Point(offsetX + j * 100, offsetY + i * 100);
            //ChangePosition(i, j);

            UpdateImage(value);
        }
     
        public void UpdateImage(int value)
        {
            this.value = value;
            pb.Image = Game.images[value.ToString()];
        }
        public void MoveTick()
        {
            int x = pb.Location.X, y = pb.Location.Y;
            pb.Location = new Point(x+moveDistanceH, y+moveDistanceV);
        }
        public void ChangePosition(int i, int j)
        {
            //pb.Location = new Point(offsetX + j * 100, offsetY + i * 100);
            if (i == this.i && j == this.j)
                return;


            if (Game.tileMat[i][j] != null)
                Game.toDelete.Add(Game.tileMat[i][j]);
            Game.tileMat[i][j] = this;


            moveDistanceH = -5 * (this.j - j);
            moveDistanceV = -5 * (this.i - i);
            Game.toMove.Add(this);


            //ovo ce da bude mnogo komplikovanija funkcija sa animacijama
            //     2|------------------>
            //             1|---------->
            // naprimer 1 i 2 ce u isto vreme da stignu na cilj treba malo matematikica ovde xD
            // ovo na kraju implementiramo.
            Game.tileMat[this.i][this.j]=null;

            this.i = i;
            this.j = j;


        }
        public void Dispose()
        {
            this.pb.Dispose();
        }
        
        public void Up()
        {
            int setI=0;
            for(int i = this.i; i!=0; i--)
            {
                if (Game.tileMat[i - 1][j] != null)
                    if (Game.tileMat[i - 1][j].value == value)
                    {
                        Game.toDraw.Add(this);
                        setI = i - 1;
                        break;
                    }
                    else
                    {
                        setI = i;
                        break;
                    }
            }
            ChangePosition(setI, this.j);
        }
        public void Down()
        {
            int setI = 3;
            for (int i = this.i; i != 3; i++)
            {
                if (Game.tileMat[i + 1][j] != null)
                    if (Game.tileMat[i + 1][j].value == value)
                    {
                        Game.toDraw.Add(this);
                        setI = i + 1;
                        break;
                    }
                    else
                    {
                        setI = i;
                        break;
                    }
            }
            ChangePosition(setI, this.j);
        }
        public void Left()
        {
            int setJ = 0;
            for (int j = this.j; j != 0; j--)
            {
                if (Game.tileMat[i][j - 1] != null)
                    if (Game.tileMat[i][j - 1].value == value)
                    {
                        Game.toDraw.Add(this);
                        setJ = j - 1;
                        break;
                    }
                    else
                    {
                        setJ = j;
                        break;
                    }
            }
            ChangePosition(this.i, setJ);
        }
        public void Right()
        {
            int setJ = 3;
            for (int j = this.j; j != 3; j++)
            {
                if (Game.tileMat[i][j + 1] != null)
                    if (Game.tileMat[i][j + 1].value == value)
                    {
                        Game.toDraw.Add(this);
                        setJ = j + 1;
                        break;
                    }
                    else
                    {
                        setJ = j;
                        break;
                    }
            }
            ChangePosition(this.i, setJ);
        }
         
        

    }
}
