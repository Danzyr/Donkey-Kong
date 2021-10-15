using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Donkey_Kong
{
    public class LevelObject : Entity
    {
        bool hasClicked = false;
        bool isClicked = false;
        int xoffset = 0;
        int yoffset = 0;
        public event EventHandler Clicked;
        public event EventHandler Released;
        public LevelObject(string ImgPath): base(ImgPath)
        {

        } 
        public override void Update()
        {
            if (Game.keys.Contains("mouse1"))
            {
                if (!hasClicked)
                {
                    hasClicked = true;
                    int clickx = Game.lastClickLocation.X;
                    int clicky = Game.lastClickLocation.Y;
                    if (clickx <= (x + width) && clickx >= x && clicky >= y && clicky <= (y + height))
                    {
                        isClicked = true;
                        xoffset = x - clickx;
                        yoffset = y - clicky;
                    }
                }
            }
            else
            {
                if(isClicked)
                {
                    Released?.Invoke(this, new EventArgs()); // ? specifies that it only gets called if it has event handlers.
                }
                hasClicked = false;
            }
            if (isClicked)
            {
                x = Game.mouseLocation.X + xoffset;
                y = Game.mouseLocation.Y + yoffset;
            }
        }
        public void Render(Graphics graphics)
        {
            graphics.DrawImage(sprite, x, y);
        }
    }
}
