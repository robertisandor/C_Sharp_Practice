using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSandor_BrickbreakerXNA.Screens
{
    public abstract class Screen
    {
        public enum ScreenTypes
        {
            Title,
            Game,
            GameOver
        }

        public ScreenTypes ScreenType { get; set; }


        public Screen(ScreenTypes screenType)
        {
            ScreenType = screenType;
        }

        public abstract void LoadContent(GraphicsDevice graphicsDevice, ContentManager content);
        // pass in keyboardstate, not keyboard, same goes for the mouse
        public abstract void Update(GameTime gameTime, KeyboardState keyboard);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
