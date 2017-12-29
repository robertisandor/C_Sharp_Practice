using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RSandor_Hundreds
{

    public class HundredsGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random numberGenerator;
        List<Ball> ballList;
        Texture2D ballImage;
        int xLimit;
        int yLimit;
        // I need to do something with scale 
        Vector2 scale = Vector2.One;
        Vector2 totalScorePosition;
        TimeSpan elapsedGameTime = TimeSpan.Zero;
        TimeSpan target = TimeSpan.FromMilliseconds(75);
        TimeSpan elapsedLevelUpTime = TimeSpan.Zero;
        TimeSpan delayTime = TimeSpan.FromMilliseconds(2000);
        int totalScore = 0;
        int bottomSpeed = -3;
        int topSpeed = 0;
        bool lostGame = false;
        SpriteFont defaultFont;

        // I need to check if the mouse is hovering over the circle
        // if it is, then increase the scale?
        // what do I do to make the balls scale?

        // I need to use the Time to make sure that it doesn't scale too fast
        public HundredsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            numberGenerator = new Random();

            Ball.Font = Content.Load<SpriteFont>("GameFont");
            ballImage = Content.Load<Texture2D>("SmallIceBall");
            // I need to load the texture ahead of time to figure out its dimensions
            // do I need textures at all?
            // all of the balls will start out at the same size
            // as they grow bigger, the image will scale
            // the scaling CAN be done
            xLimit = GraphicsDevice.Viewport.Width - ballImage.Width;
            yLimit = GraphicsDevice.Viewport.Height - ballImage.Height;
            topSpeed = Math.Abs(bottomSpeed);
            defaultFont = Content.Load<SpriteFont>("GameFont");
            totalScorePosition = new Vector2(xLimit / 2, 50) - defaultFont.MeasureString($"Score: {totalScore.ToString()}");
            ballList = new List<Ball>(10);
            for (int index = 0; index < ballList.Capacity; index++)
            {
                // I need to make sure that the newly-generated balls don't collide with each other
                // if they do, generate a new position that doesn't collide with any previously-made balls
                // don't divide the vector; everything gets crammed into the middle of the screen and more issues result because of that
                ballList.Add(new Ball(ballImage, Color.Azure, new Vector2(numberGenerator.Next(xLimit), numberGenerator.Next(yLimit)), numberGenerator.Next(bottomSpeed, topSpeed), numberGenerator.Next(bottomSpeed, topSpeed)) { ID = index, Scale = scale });
                RespawnBallPosition(ballList, index, numberGenerator, xLimit, yLimit);
                RespawnBallSpeed(ballList, numberGenerator, bottomSpeed, topSpeed, index);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var mouseState = Mouse.GetState();

            // should I check for the collisions before I check for the mouse?
            DetectCollision(GraphicsDevice, gameTime, ballList, ref lostGame);
            for (int index = 0; index < ballList.Count; index++)
            {
                // this check needs to be better
                // this should be probably be a Ball class function
                // that accepts a MouseState parameter, a GameTime parameter, and a TimeSpan parameter
                // (for mouseState, gameTime and elapsedGameTime, respectively)
                if ((mouseState.X < (ballList[index].X + ballList[index].Size) && mouseState.X > ballList[index].X) &&
                mouseState.Y > ballList[index].Y && mouseState.Y < ballList[index].Y + ballList[index].Size)
                {
                    // only increase the TimeSpan if the mouse is hovering over it
                    ballList[index].IsGrowing = true;
                    elapsedGameTime += gameTime.ElapsedGameTime;
                    if (elapsedGameTime > target)
                    {
                        elapsedGameTime = TimeSpan.Zero;
                        ballList[index].Value++;
                        totalScore++;
                    }
                    ballList[index].Scale = new Vector2(ballList[index].Scale.X, ballList[index].Scale.Y) * 1.005f;
                }
                else
                {
                    ballList[index].IsGrowing = false;
                }
                ballList[index].Update(GraphicsDevice, gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            for (int index = 0; index < ballList.Count; index++)
            {
                ballList[index].Draw(spriteBatch);
            }

            spriteBatch.DrawString(defaultFont, $"Score: {totalScore.ToString()}", totalScorePosition, Color.Red);

            if (lostGame)
            {
                spriteBatch.DrawString(defaultFont, "You Lost", new Vector2(xLimit / 2, yLimit / 2) - defaultFont.MeasureString("You Lost") / 2, Color.Green);
                
                elapsedLevelUpTime += gameTime.ElapsedGameTime;
                if (elapsedLevelUpTime > delayTime)
                {
                    elapsedLevelUpTime = TimeSpan.Zero;
                    for (int index = 0; index < ballList.Count; index++)
                    {
                        ballList[index].Position = new Vector2(numberGenerator.Next(xLimit), numberGenerator.Next(yLimit));
                    }

                    RestartLevel(ballList, numberGenerator, xLimit, yLimit, ref totalScore);
                    lostGame = false;
                }
            }

            if (totalScore >= 100)
            {
                spriteBatch.DrawString(defaultFont, "You Won This Level!", new Vector2(xLimit / 2, yLimit / 2) - defaultFont.MeasureString("You Won This Level!") / 2, Color.Green);

                elapsedLevelUpTime += gameTime.ElapsedGameTime;
                if (elapsedLevelUpTime > delayTime)
                {
                    elapsedLevelUpTime = TimeSpan.Zero;
                    ballList.Add(new Ball(ballImage, Color.Azure, new Vector2(numberGenerator.Next(xLimit), numberGenerator.Next(yLimit)), numberGenerator.Next(bottomSpeed, topSpeed), numberGenerator.Next(bottomSpeed, topSpeed)) { ID = ballList.Count + 1, Scale = scale });

                    for (int index = 0; index < ballList.Count; index++)
                    {
                        ballList[index].Position = new Vector2(numberGenerator.Next(xLimit), numberGenerator.Next(yLimit));
                        RespawnBallSpeed(ballList, numberGenerator, bottomSpeed, topSpeed, index);
                    }

                    RestartLevel(ballList, numberGenerator, xLimit, yLimit, ref totalScore);
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void RespawnBallPosition(List<Ball> ballList, int outerIndex, Random numberGenerator, int xLimit, int yLimit)
        {
            for (int innerIndex = 0; innerIndex < outerIndex; innerIndex++)
            {
                // if the newly-created ball collides with one of the previously-generated balls, set it to a new position
                // then check again
                if (ballList[outerIndex].HitBox.Intersects(ballList[innerIndex].HitBox))
                {
                    ballList[outerIndex].Position = new Vector2(numberGenerator.Next(xLimit), numberGenerator.Next(yLimit));
                    innerIndex = 0;
                }
            }
        }

        public void RespawnBallSpeed(List<Ball> ballList, Random numberGenerator, int bottomSpeed, int topSpeed, int index)
        {
            // if the ball speed is zero, change it
            while (ballList[index].XSpeed == 0 && ballList[index].YSpeed == 0)
            {
                ballList[index].XSpeed = numberGenerator.Next(bottomSpeed, topSpeed);
                ballList[index].YSpeed = numberGenerator.Next(bottomSpeed, topSpeed);
            }
        }

        public void RestartLevel(List<Ball> ballList, Random numberGenerator, int xLimit, int yLimit, ref int totalScore)
        {
            totalScore = 0;

            for (int index = 0; index < ballList.Count; index++)
            {
                ballList[index].Scale = Vector2.One;
                ballList[index].Value = 0;
                RespawnBallPosition(ballList, index, numberGenerator, xLimit, yLimit);
            }
        }

        // this represents the difference between the x & y values if we swapped the speeds
        public float CalculateTentativeDistance(List<Ball> ballList, int indexOfFirst, int indexOfSecond)
        {
            float tentativeDifferenceBetweenXValues = (ballList[indexOfSecond].Center.X + ballList[indexOfFirst].XSpeed) - (ballList[indexOfFirst].Center.X + ballList[indexOfSecond].XSpeed);
            float tentativeDifferenceBetweenYValues = (ballList[indexOfSecond].Center.Y + ballList[indexOfFirst].YSpeed) - (ballList[indexOfFirst].Center.Y + ballList[indexOfSecond].YSpeed);
            float tentativeDistance = (float)Math.Sqrt((tentativeDifferenceBetweenXValues * tentativeDifferenceBetweenXValues) + (tentativeDifferenceBetweenYValues * tentativeDifferenceBetweenYValues));
            return tentativeDistance;
        }

        // this function should detect collisions as they're moving
        // I need to use both ways of calculating since the more sophisticated way has more costly calculations
        // similar concept to checking the first big hitbox, then the 4 smaller hitboxes

        // at one point, some of the balls were "paired" with each other and could be seen vibrating
        // sometimes, they're not even touching; is the calculation off? should I use floats?

        // no, the calculation isn't off; I don't think it was an issue with it being ints or floats either
        // calculate the distance of the balls if the speeds were swapped and they moved one time
        // if that calculated distance is larger (they're moving away from each other)
        // then we should swap speeds
        // otherwise, if we swapped, they would be closer, which would cause them to stay "paired"
        public void DetectCollision(GraphicsDevice graphicsDevice, GameTime gameTime, List<Ball> listOfBalls, ref bool lostGame)
        {
            // do I need to have both loops inside the function?
            // should I just accept two more Ball parameters (that represent the two balls that I'm checking)
            // and just put the loops 

            for (int indexOfFirst = 0; indexOfFirst < listOfBalls.Count; indexOfFirst++)
            {
                for (int indexOfSecond = indexOfFirst + 1; indexOfSecond < listOfBalls.Count; indexOfSecond++)
                {
                    if (listOfBalls[indexOfFirst].HitBox.Intersects(listOfBalls[indexOfSecond].HitBox))
                    {
                        // use the distance formula to calculate the collisions between the balls
                        // if (x2-x1)^2 + (y2 - y1)^2 <= (r1 + r2)^2

                        // this looks verbose already
                        // I should probably use a lambda to calculate the distance
                        float differenceBetweenXValues = listOfBalls[indexOfSecond].Center.X - listOfBalls[indexOfFirst].Center.X;
                        float differenceBetweenYValues = listOfBalls[indexOfSecond].Center.Y - listOfBalls[indexOfFirst].Center.Y;
                        float sumOfRadii = ((listOfBalls[indexOfSecond].Size / 2) + (listOfBalls[indexOfFirst].Size / 2));

                        float distance = (float)Math.Sqrt((differenceBetweenXValues * differenceBetweenXValues) + (differenceBetweenYValues * differenceBetweenYValues));

                        // if the distance between the 2 points is less than the radius squared, that means the two circles have collided

                        // if the distance after the swap will be greater (they move apart), then we swap the speeds
                        // otherwise, we let them go on their merry way (because they would be moving closer if we swapped them)

                        if ((differenceBetweenXValues * differenceBetweenXValues) + (differenceBetweenYValues * differenceBetweenYValues) < (sumOfRadii * sumOfRadii))
                        {
                            if (listOfBalls[indexOfFirst].IsGrowing || listOfBalls[indexOfSecond].IsGrowing)
                            {
                                lostGame = true;
                            }
                            else if (CalculateTentativeDistance(listOfBalls, indexOfFirst, indexOfSecond) > distance)
                            {
                                // just switch the speeds
                                float tempXSpeedHolder = listOfBalls[indexOfFirst].XSpeed;
                                float tempYSpeedHolder = listOfBalls[indexOfFirst].YSpeed;
                                listOfBalls[indexOfFirst].XSpeed = listOfBalls[indexOfSecond].XSpeed;
                                listOfBalls[indexOfFirst].YSpeed = listOfBalls[indexOfSecond].YSpeed;
                                listOfBalls[indexOfSecond].XSpeed = (int)tempXSpeedHolder;
                                listOfBalls[indexOfSecond].YSpeed = (int)tempYSpeedHolder;
                            }
                        }
                    }
                }
            }
        }
    }
}
