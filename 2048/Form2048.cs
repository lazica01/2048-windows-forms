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
        public Form2048()
        {
            InitializeComponent();
            images = LoadImages(@"..\..\..\Slike");
            this.Size = new Size(530, 740);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

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
            Tile t = new Tile(0, 0, 2);
            Controls.Add(t.pb);

            t = new Tile(1, 1, 2);
            Controls.Add(t.pb);

        }

        private void Form2048_MouseMove(object sender, MouseEventArgs e)
        {
            var relativePoint = this.PointToClient(Cursor.Position);
            label1.Text = relativePoint.ToString();
        }
    }
}
