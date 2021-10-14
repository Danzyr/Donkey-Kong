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
    public partial class Form1 : Form
    {
        Editor editor;
        NewLevel levelDialog;   // Defining a property for the new level window
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            levelDialog = new NewLevel(); //Instantiating the property
            levelDialog.Show();
            editor = new Editor("Level Editor");
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
