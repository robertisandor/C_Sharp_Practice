using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSandor_Hundreds
{
    // I don't think I need a Sprite class since there are only balls on the screen

    public class Ball
    {
        public static SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }
        // I chose to make a member called Size because the width and height should be the same
        public float Size => (Texture.Width * Scale.X);
        // I need a Func, Action or Delegate; I'm not sure which

        public int ID { get; set; }
        public int Value { get; set; } = 0;

        public Vector2 Scale { get; set; } = Vector2.One;

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, (int)Size, (int)Size);
            }
        }

        public int X => HitBox.X;
        public int Y => HitBox.Y;

        public float Radius => Size / 2;

        public Vector2 Center
        {
            get
            {
                return new Vector2(X + (Size / 2), Y + (Size / 2));
            }
        }

        public bool IsGrowing = false;

        public Ball(Texture2D texture, Color color, Vector2 position, int xSpeed, int ySpeed)
        {
            Texture = texture;
            Color = color;
            Position = position;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // invalid operation exception? occurs when you forget to add spriteBatch.Begin() and spriteBatch.End()
            spriteBatch.Draw(Texture, HitBox, Color);

            Vector2 textPos = Position + (new Vector2(Size) - Font.MeasureString(Value.ToString())) / 2f;
            spriteBatch.DrawString(Font, Value.ToString(), textPos, Color.Red);
        }

        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            Position = new Vector2(Position.X + XSpeed, Position.Y + YSpeed);
            // some balls are next to the edge and when they're bumped into, vibrate along the edge
            // as they're bumped into more, they can go further off or on the screen

            // check for the speed as well; only reverse the speed if it's going off the edge
            if ((Position.X + Size >= graphicsDevice.Viewport.Width && XSpeed > 0) || 
                (Position.X <= 0 && XSpeed < 0))
            {
                XSpeed *= -1;
            }

            if ((Position.Y + Size >= graphicsDevice.Viewport.Height && YSpeed > 0) ||
                (Position.Y <= 0 && YSpeed < 0))
            {
                YSpeed *= -1;
            }
        }
    }
}
