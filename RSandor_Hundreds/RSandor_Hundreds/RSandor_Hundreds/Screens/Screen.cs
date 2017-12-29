using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSandor_Hundreds
{
    public abstract class Screen
    {
        public enum ScreenType
        {
            TitleScreen,
            GameScreen,
            GameOverScreen
        }

        ScreenType screenType;

        public Screen(ScreenType screenType)
        {
            this.screenType = screenType;
        }
    }
}
