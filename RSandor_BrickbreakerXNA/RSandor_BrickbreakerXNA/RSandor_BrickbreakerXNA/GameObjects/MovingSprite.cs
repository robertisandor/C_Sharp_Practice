using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RSandor_BrickbreakerXNA.GameObjects
{
    public abstract class MovingSprite : Sprite
    {
        // this inherits the update & draw methods
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }
        // figure out whether this needs to be public & private
        // do we need a property for the viewport?
        protected Viewport viewport;

        // the constructor should accept a viewport as well and have a property for the viewport (if we need it)
        public MovingSprite(Texture2D texture, Vector2 position, Color color, int xSpeed, int ySpeed, Viewport viewport) : base(texture, position, color)
        {
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            this.viewport = viewport;
        }

        public abstract void CheckBoundaries();
    }
}
