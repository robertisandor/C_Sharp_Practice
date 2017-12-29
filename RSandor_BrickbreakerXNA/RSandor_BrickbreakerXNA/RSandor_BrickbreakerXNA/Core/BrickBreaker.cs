using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using RSandor_BrickbreakerXNA.Screens;

// dirty reads
// if a transaction is opened, no other user can access the data in that table (SQL)
// most servers aren't configured for dirty read

// can't define macros in C#
// no static inheritance

// in UML, association means you have a relationship with something else
// associations can be bi-directional, unidirectional, aggregration and reflexive
// dependency - an element or set of elements requires other elements for their specification or implementation
// two or more elements in this relationship are called tuples
// aggregation - more specific than association; part-whole or part-of relationship, must be binary
// e.g. library has students and books; student can exist without library; relationship between student and library is aggregation
// a particular engine may be used in many different models of cars 
// realization - relationship between classes and interfaces
namespace RSandor_BrickbreakerXNA
{
    // to create a new game in XNA, create a new Windows Game (4.0)
    public class BrickBreaker : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;

        // how do I add the textures so that it goes wherever the project goes?
        // the path shown on the asset is a recognition of where it came from,
        // not where it's currently located; it's a relative path

        public BrickBreaker()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        // maybe have some sort of manager
        // does this belong in the Game Screen as well? Probably
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // when creating the screen manager, instantiate the screen as part of the parameters
            screenManager = new ScreenManager(GraphicsDevice, Content, new GameScreen(Screen.ScreenTypes.Game));
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            // use Keyboard.GetState(), not new KeyboardState()
            // this stays in Brickbreaker; we're going to pass the KeyboardState
            var keyboard = Keyboard.GetState();

            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //  this.Exit();

            screenManager.Update(gameTime, keyboard);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // I remember that spriteBatch had a begin and an end, but what did those do exactly?
            // this tells it not to do calculations while it's updating; this tells it to prepare the calculations for the drawing
            // only during the Draw function
            spriteBatch.Begin();

            // make this into a foreach
            screenManager.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
