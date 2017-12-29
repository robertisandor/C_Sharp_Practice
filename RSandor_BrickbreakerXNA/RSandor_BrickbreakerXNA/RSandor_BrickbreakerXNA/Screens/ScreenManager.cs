using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace RSandor_BrickbreakerXNA.Screens
{
    // this needs to deal with the screens
    public class ScreenManager
    {
        // If I need to specify an enum of a class, I need to specify which class it's coming from
        public Dictionary<Screen.ScreenTypes, Screen> ScreenList;

        private Screen activeScreen;
        private GraphicsDevice graphicsDevice;
        private ContentManager content;

        // have students perhaps create the game screen
        // then the screenmanager class
        public ScreenManager(GraphicsDevice graphicsDevice, ContentManager content, Screen currentScreen)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            currentScreen.LoadContent(graphicsDevice, content);
            setActiveScreen(currentScreen);
        }

        private void setActiveScreen(Screen screenToMakeActive)
        {
            activeScreen = screenToMakeActive;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            activeScreen.Draw(spriteBatch);
        }

        // the update and draw functions will most likely mirror the update and draw functions of the screen's class
        public void Update(GameTime gameTime, KeyboardState keyboard)
        {
            activeScreen.Update(gameTime, keyboard);
        }
    }
}
