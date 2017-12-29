using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RSandor_BrickbreakerXNA.GameObjects
{
    public class Paddle : MovingSprite
    {
        public Paddle(Texture2D texture, Vector2 position, Color color, int xSpeed, int ySpeed, Viewport viewport) : base(texture, position, color, xSpeed, ySpeed, viewport)
        {
            
        }

        public void Move(KeyboardState keyboard)
        {
            if(keyboard.IsKeyDown(Keys.Right))
            {
                Position = new Vector2(Position.X + XSpeed, Position.Y);
            }
            else if (keyboard.IsKeyDown(Keys.Left))
            {
                Position = new Vector2(Position.X - XSpeed, Position.Y);
            }
        }

        // rather than add a viewport parameter in the CheckBoundaries function, 
        // add a private field that you'll populate in the constructor
        // that way, I won't need to constantly ask for the viewport (especially since it isn't changing)
        public override void CheckBoundaries()
        {
            if (HitBox.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }
            else if (HitBox.X + HitBox.Width > viewport.Width)
            {
                Position = new Vector2(viewport.Width - HitBox.Width, Position.Y);
            }
        }
    }
}
