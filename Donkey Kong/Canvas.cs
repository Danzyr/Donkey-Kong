using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donkey_Kong
{
    public partial class Canvas : Form
    {
        public Game game;
        public Canvas()
        {
            this.DoubleBuffered = true;
            InitializeComponent();
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Shown += Canvas_Shown;
            Image image = Bitmap.FromFile("C:\\Users\\Dan\\source\\repos\\Donkey Kong\\Donkey Kong\\Sprites\\girder.bmp");
        }

        private void Canvas_Shown(object sender, EventArgs e) 
        {
            game = new Game(this); 
        }
    }
}
