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
        private int value;
        private int moveDistanceH;
        private int moveDistanceV;


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
            pb.Image = Form2048.images[value.ToString()];
        }
        public void MoveTick()
        {
            int x = pb.Location.X, y = pb.Location.Y;
            pb.Location = new Point(x+moveDistanceH, y+moveDistanceV);
            game.Update();
        }
        public void ChangePosition(int i, int j)
        {
            //pb.Location = new Point(offsetX + j * 100, offsetY + i * 100);
            if (Form2048.tileMat[i][j] != null)
                Form2048.tileMat[i][j].Dispose();
            Form2048.tileMat[i][j] = this;


            moveDistanceH = -5 * (this.j - j);
            moveDistanceV = -5 * (this.i - i);
            Form2048.toMove.Add(this);


            //ovo ce da bude mnogo komplikovanija funkcija sa animacijama
            //     2|------------------>
            //             1|---------->
            // naprimer 1 i 2 ce u isto vreme da stignu na cilj treba malo matematikica ovde xD
            // ovo na kraju implementiramo.

        }
        public void Dispose()
        {
            this.pb.Dispose();
        }
        /*
        public void Up()
        {
            if(...)
               UpdateImage(value)
               
          
           ChangePosition(end_i, end_j);
        }
        public void Down()
        {
         * if(...)
               UpdateImage(value)
               
          
           ChangePosition(end_i, end_j);
        }
        public void Left()
        {
         * if(...)
               UpdateImage(value)
               
          
           ChangePosition(end_i, end_j);
        }
        public void Right()
        {
         * if(...)
               UpdateImage(value)
               
          
           ChangePosition(start_i, start_j, end_i, end_j);
        }
        */

    }
}
