using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Donkey_Kong
{
    abstract public class Entity
    {
        public Image sprite;       
        public int x;
        public int y;
        public event EventHandler deleted;
        public static float scale = 1;
        public int height
        {
            get
            {
                return sprite.Height;
            }
        }
        public int width
        {
            get
            {
                return sprite.Width;
            }
        }
        public Entity(string ImgPath)
        {
            sprite = Image.FromFile(ImgPath);
            Game.EntityList.Add(this); 
        }
        public int[,] GetBoundingPoints() // Gets the hitbox for game objects
        {
            int[,] points =
            { {x, y},                       // Top left       0
            {x + width, y },                // Top right      1
            {x, y + height },               // Bottom left    2
            {x + width, y + height}};       // Bottom right   3
                                            // manually inserting values into a 2d array    

            return points;
        }
        public bool IsColliding(Entity entity)
        {
            int[,] points1 = GetBoundingPoints();
            int[,] points2 = entity.GetBoundingPoints();
            int XOverlap = Math.Max(0, Math.Min(points1[3, 0], points2[3, 0]) - Math.Max(points1[0, 0], points2[0, 0])); // The point coordinates' x values refers to the BoundingPoints defined above, the y values refer to the specific part of their function, so for example point1[3, 0] refers to the bottom right and x + width.
            int YOverlap = Math.Max(0, Math.Min(points1[3, 1], points2[3, 1]) - Math.Max(points1[0, 1], points2[0, 1])); //
            if (XOverlap > 0 && YOverlap > 0) // If there is an overlap IsColliding will return true
            { return true; }
            else
            { return false; }
        }
        public abstract void Update();

        public virtual void Delete() // This handles the killing of enemies and removal of their sprites as well as the death explosion animation
        {
            Game.EntityList.Remove(this);   // Deletes enemies that have been shot
            EventHandler handler = deleted;
            handler?.Invoke(this, new EventArgs());
        }

        public void Render(Graphics graphics)
        {
            graphics.DrawImage(sprite, x * scale, y * scale, width * scale, height * scale);
        }
    }
}
