using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// when teaching XNA, I should focus on class design first
// there is a fair bit of math & calculations involved,
// but if that becomes an issue for the student,
// use hardcoded values and focus on the class design

// students may get the tendency to create too many classes
// if that becomes the case, show them that we can use properties
// to differentiate between, say, the user's paddle & an AI paddle
// rather than creating a whole new class for that

// how common is it for professional programmers to create their own library
// rather than using someone else's library? Why or why not?

namespace RSandor_BrickbreakerXNA
{
    // what was a pass-through constructor?

    // if I wanted to share initialization code between multiple constructors,
    // I could use a private helper method
    // or I could use constructor chaining,
    // which passes one constructor to another using ": this(firstParameter, secondParameter)"
    // assuming there are 2 parameters in that example 

    // if I wanted to create a Button class that had rounded corners
    // I could have the rounded part have transparency
    // then, to detect collisions,
    // I could keep a list (or, even better, a dictionary) of
    // those transparencies
    // I would have a list of transparencies because there would be fewer
    // transparencies than pixels that are being used to display the button class

    // scale bricks to the size of the screen
    public class Sprite
    {
        // make Color & Texture have protected set
        // private set and no set are the same thing
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        // why should I have hitbox based on position?
        // Position is set by the main program when it's initialized
        public Rectangle HitBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int) Position.Y, Width, Height);
            }
        }

        // => in property is an expression-bodied property
        // height & width should be based off of the texture, not necessarily the hitbox
        public int Height => Texture.Height;
        public int Width => Texture.Width;

        // every sprite should need a texture, a position and a color
        // private constructor also used in Factory pattern
        public Sprite(Texture2D texture, Vector2 position, Color color)
        {
            Texture = texture;
            Position = position;
            Color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // eventually I could use the draw with 9 parameters
            // 3 parameters should suffice for now
            spriteBatch.Draw(Texture, HitBox, Color);
        }
    }
}
