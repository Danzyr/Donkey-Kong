using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Donkey_Kong
{
    public class Game
    {
        public static HashSet<string> keys;
        public static Point lastClickLocation;
        public static Point mouseLocation;
        public Canvas window;
        public Girder girder;
        public static List<Entity> EntityList = new List<Entity>();
        private Thread gameloopthread;
        public int rightScore = 0;
        public int leftScore = 0;
        // Adding too many static methods is considered bad coding. Static methods should be used for global aspects of a class
        private ManualResetEvent pause = new ManualResetEvent(true);
        public bool ShowGameOver = false;
        public bool CanvasClosed = false;
        public Game(Canvas canvas)
        {
            //Paint is the event of the window being painted white when the window is created. Canvas_Paint is an event listener.
            this.window = canvas;
            gameloopthread = new Thread(Gameloop);
            gameloopthread.IsBackground = true;
            gameloopthread.Start();
            canvas.KeyDown += Canvas_KeyPress;
            canvas.KeyUp += Canvas_KeyRelease;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.Paint += Canvas_Renderer;
            keys = new HashSet<string>();
            girder = new Girder();
        }

        public void PauseGame()
        {
            if (!pause.WaitOne(0))
            {
                pause.Set();
            }
            else
            {
                pause.Reset();
            }
        }
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation = e.Location;
        }
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
               keys.Remove("mouse1");
            }            
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                keys.Add("mouse1");
                lastClickLocation = e.Location;
            }
        }

        public void Canvas_KeyRelease(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)  // if the player releases the A key, the velocity is set to 0, this is repeated for the D and W keys below
            { keys.Remove("left"); }
            if (e.KeyCode == Keys.Right)
            { keys.Remove("right"); }
            if (e.KeyCode == Keys.Up)
            { keys.Remove("up"); }
        }
        //Key release listener and logic.
        public void Canvas_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            { keys.Add("left"); }
            if (e.KeyCode == Keys.Right)
            { keys.Add("right"); }
            if (e.KeyCode == Keys.Up)
            { keys.Add("up"); }
        }
        //e is referential to the event the function is listening for.
        private void Canvas_Renderer(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.Black);
            girder.Update();
            girder.Render(graphics);
        }
        private void Gameloop()
        {
            while (gameloopthread.IsAlive && !CanvasClosed)
            {
                window.BeginInvoke((MethodInvoker)delegate { window.Refresh(); });
                pause.WaitOne();
                Thread.Sleep(1);
            }
        }
    }
}
