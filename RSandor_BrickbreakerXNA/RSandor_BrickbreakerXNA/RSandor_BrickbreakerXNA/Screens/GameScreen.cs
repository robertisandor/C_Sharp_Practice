using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using RSandor_BrickbreakerXNA.GameObjects;

namespace RSandor_BrickbreakerXNA.Screens
{
    public class GameScreen : Screen
    {
        Ball bouncingBall;
        Texture2D paddleTexture;
        Paddle paddle;
        List<Brick> listOfBricks;
        int totalAmountOfBricks = 14;
        int startingBrickXPosition = 30;
        int startingBrickYPosition = 30;
        int xDistanceBetweenBricks = 40;
        int yDistanceBetweenBricks = 30;
        Vector2 brickSize;
        Vector2 currentBrickPosition;
        Color[] brickColors;

        public GameScreen(ScreenTypes screenType)
            : base(screenType)
        {

        }

        // I need contentmanager to load the content
        public override void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            // this portion needs to go into gamescreen
            // to figure out the height of the paddle, 
            // I created a variable just for the texture
            paddleTexture = content.Load<Texture2D>("Paddle");
            // this should be moved to the GameScreen once I confirm that this works
            // to load something, we need to access the ContentManager's Content property
            // and use the Load method, which requires a type and a string for the name

            // if I want to use the viewport, I need to use GraphicsDevice (notice the capital G & D)
            // GraphicsDevice is a property
            // once I move everything to screen and add the GraphicsDevice parameter, then I need to change GraphicsDevice to the parameter
            bouncingBall = new Ball(content.Load<Texture2D>("Ball"), new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height / 2), Color.Crimson, -1, 1, graphicsDevice.Viewport);
            paddle = new Paddle(content.Load<Texture2D>("Paddle"), new Vector2(graphicsDevice.Viewport.Width / 2, graphicsDevice.Viewport.Height - paddleTexture.Height), Color.NavajoWhite, 3, 0, graphicsDevice.Viewport);
            listOfBricks = new List<Brick>(totalAmountOfBricks);
            currentBrickPosition = new Vector2(startingBrickXPosition, startingBrickYPosition);
            brickSize = new Vector2(70, 35);
            brickColors = new Color[] { Color.Firebrick, Color.Orange, Color.Yellow, Color.ForestGreen };
            for (int index = 0, currentColorIndex = 0; index < totalAmountOfBricks; index++)
            {
                if (currentBrickPosition.X + brickSize.X + xDistanceBetweenBricks > graphicsDevice.Viewport.Width)
                {
                    currentBrickPosition = new Vector2(startingBrickXPosition, currentBrickPosition.Y + brickSize.Y + yDistanceBetweenBricks);
                    currentColorIndex++;
                }
                listOfBricks.Add(new Brick(graphicsDevice, currentBrickPosition, brickSize, brickColors[currentColorIndex]));
                currentBrickPosition = new Vector2(currentBrickPosition.X + brickSize.X + xDistanceBetweenBricks, currentBrickPosition.Y);
            }
        }

        // I need keyboardstate because I'm using the keys to control the paddle
        public override void Update(GameTime gameTime, KeyboardState keyboard)
        {
            // this portion goes into gamescreen as well
            paddle.Move(keyboard);
            paddle.CheckBoundaries();

            bouncingBall.Move();
            bouncingBall.CheckBoundaries();
            CheckCollision(paddle, bouncingBall, listOfBricks);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int index = 0; index < listOfBricks.Count; index++)
            {
                listOfBricks[index].Draw(spriteBatch);
            }

            bouncingBall.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
        }

        public void CheckCollision(Paddle paddle, Ball ball, List<Brick> brickList)
        {
            if (paddle.HitBox.Intersects(ball.HitBox))
            {
                ball.YSpeed *= -1;
            }

            for (int index = 0; index < brickList.Count; index++)
            {
                if (ball.HitBox.Intersects(brickList[index].HitBox))
                {
                    // go through each of the side in the hitboxes
                    // use a foreach if possible; faster than a for
                    foreach (Brick.Side side in Enum.GetValues(typeof(Brick.Side)))
                    {
                        // why is it bouncing on the bottom in a weird way?
                        // it was because it was hitting multiple hitboxes at the same time
                        brickList[index].HitBoxes[side].Item2(ball, brickList[index], side);
                    }
                    brickList.Remove(brickList[index]);
                }
            }
        }
    }
}
