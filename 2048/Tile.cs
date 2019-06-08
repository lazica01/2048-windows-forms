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
    class Tile
    {
        public static int offsetX = 60;
        public static int offsetY = 250;

        public PictureBox pb;
        private int i, j;
        private int value;

        public Tile(int i, int j, int value)
        {
            pb = new PictureBox();
            pb.Size = new Size(90, 90);
            this.i = i;
            this.j = j;
            ChangePosition(i, j);
            ChangePosition(i+2, j+2);

            UpdateImage(value);
        }
     
        public void UpdateImage(int value)
        {
            this.value = value;
            pb.Image = Form2048.images[value.ToString()];
        }
        private void ChangePosition(int i, int j)
        {
            pb.Location = new Point(offsetX + j * 100, offsetY + i * 100);
            //ovo ce da bude mnogo komplikovanija funkcija sa animacijama
            //     2|------------------>
            //             1|---------->
            // naprimer 1 i 2 ce u isto vreme da stignu na cilj treba malo matematikica ovde xD
            // ovo na kraju implementiramo.
            pb.Update();

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
