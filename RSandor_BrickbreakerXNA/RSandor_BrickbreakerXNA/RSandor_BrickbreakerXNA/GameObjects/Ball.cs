using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RSandor_BrickbreakerXNA.GameObjects
{
    // ARM heap allocation is slow
    // much slower than Intel
    // lazy-loading - deferring initialization of an object until it's needed
    public class Ball : MovingSprite
    {
        public Ball(Texture2D texture, Vector2 position, Color color, int xSpeed, int ySpeed, Viewport viewport) : base(texture, position, color, xSpeed, ySpeed, viewport)
        {

        }

        public void Move()
        {
            Position = new Vector2(Position.X + XSpeed, Position.Y + YSpeed);
        }

        // create a method; if you use the same method
        // in multiple classes, then you can decide whether to include
        // it in a parent class and make it abstract
        public override void CheckBoundaries()
        {
            if (HitBox.X + HitBox.Width > viewport.Width || HitBox.X < 0)
            {
                XSpeed *= -1;
            }
            if (HitBox.Y < 0)
            {
                YSpeed *= -1;
            }

            // what happens when it goes off the screen?
            // announce game over & that they lost?
            // it should go to the game over screen
            // I need to change the viewport; I shouldn't make it public
            if (HitBox.Y + HitBox.Height > viewport.Height)
            {

            }
        }
    }
}
