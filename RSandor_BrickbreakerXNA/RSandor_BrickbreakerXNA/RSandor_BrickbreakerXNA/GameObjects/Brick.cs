using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace RSandor_BrickbreakerXNA.GameObjects
{
    // allocations are basically on the stack
    // tuples are temporary classes
    public class Brick : Sprite
    {
        private GraphicsDevice graphicsDevice;
        // right now, I'm using a 2D array;
        // eventually, I should change the brick so it relies solely on scale
        // and not on a 2D array
        private Color[] pixelArray;
        // would I need the size variable?
        // private Vector2 Size;
        // with Pong, I multiplied the scale by the size
        // add 4 hitboxes, 1 for each side
        private int hitBoxSize = 1;

        public enum Side
        {
            Top,
            Right,
            Bottom,
            Left
        }

        public Dictionary<Side, Tuple<Rectangle, Action<Ball, Brick, Side>>> HitBoxes;
        public Brick(GraphicsDevice graphicsDevice, Vector2 position, Vector2 size, Color color) : base(new Texture2D(graphicsDevice, (int)size.X, (int)size.Y), position, color)
        {
            this.graphicsDevice = graphicsDevice;
            pixelArray = new Color[(int)size.X * (int)size.Y];
            // how do I handle this ArgumentException when the Texture2d's dimensions aren't 1x1?
            // initialize Texture2D with the dimensions of the brick that you want
            // requires 1d array
            for (int index = 0; index < pixelArray.Length; index++)
            {
                pixelArray[index] = color;
            }
            // when it's a dark blue, it's a hint that it's redundant (e.g. an unnecessary cast)
            // SetData infers which type the array passed in is
            Texture.SetData(pixelArray);
            HitBoxes = new Dictionary<Side, Tuple<Rectangle, Action<Ball, Brick, Side>>>
            {
                // the ball should be forced outside of the brick
                // before we change its direction
                // we would do this in the lambda
                // lambdas can be used just like regular functions
                // in that they can have curly braces and multiple statements
                // we would 
                { Side.Top, new Tuple<Rectangle,  Action<Ball, Brick, Side>>(
                    new Rectangle(HitBox.X - hitBoxSize, HitBox.Y, HitBox.Width, hitBoxSize),
                    (ball, brick, side) => {
                        // if the ball bounces on the top and it is going down, bounce it up
                        ball.YSpeed *= ball.HitBox.Intersects(brick.HitBoxes[side].Item1) && ball.YSpeed > 0 ? -1 : 1;
                        // don't change the position; it's very hard to implement
                        // ball.Position = ball.HitBox.Intersects(brick.HitBoxes[side].Item1) ? new Vector2(ball.HitBox.X, brick.HitBox.Y - ball.HitBox.Height) : ball.Position;
                    })
                },
                { Side.Right, new Tuple<Rectangle,  Action<Ball, Brick, Side>>(new Rectangle(HitBox.X + HitBox.Width, HitBox.Y, hitBoxSize, HitBox.Height),
                    (ball, brick, side) => {
                        // if the ball bounces on the right and it is going left, bounce it right
                        ball.XSpeed *= ball.HitBox.Intersects(brick.HitBoxes[side].Item1) && ball.XSpeed < 0 ? -1 : 1;
                        // it's most likely an issue with hitting 2 hitboxes at once; how do I resolve that?
                        // check the speed and make sure it only changes if it isn't going the direction you want to 
                        // ball.Position = ball.HitBox.Intersects(brick.HitBoxes[side].Item1) ? new Vector2(brick.HitBox.X + brick.HitBox.Width, ball.HitBox.Y) : ball.Position;
                    })
                },
                { Side.Bottom, new Tuple<Rectangle,  Action<Ball, Brick, Side>>(new Rectangle(HitBox.X, HitBox.Y + HitBox.Height - hitBoxSize, HitBox.Width, hitBoxSize),
                    (ball, brick, side) => {
                        // if the ball bounces on the bottom and it is going up, bounce it down
                        ball.YSpeed *= ball.HitBox.Intersects(brick.HitBoxes[side].Item1) && ball.YSpeed < 0 ? -1 : 1;
                        // don't change the position; it's very hard to implement
                        // ball.Position = ball.HitBox.Intersects(brick.HitBoxes[side].Item1) ? new Vector2(ball.HitBox.X, brick.HitBox.Y + brick.HitBox.Height) : ball.Position;
                    })
                },
                { Side.Left, new Tuple<Rectangle,  Action<Ball, Brick, Side>>(new Rectangle(HitBox.X, HitBox.Y, hitBoxSize, HitBox.Height),
                    (ball, brick, side) => {
                        // if the ball bounces on the left and it is going right, bounce it left
                        ball.XSpeed *= ball.HitBox.Intersects(brick.HitBoxes[side].Item1) && ball.XSpeed > 0 ? -1 : 1;
                        // don't change the position; it's very hard to implement
                        // ball.Position = ball.HitBox.Intersects(brick.HitBoxes[side].Item1) ? new Vector2(brick.HitBox.X - brick.HitBox.Width, ball.HitBox.Y) : ball.Position;
                    })
                }
            };
        }
    }
}
