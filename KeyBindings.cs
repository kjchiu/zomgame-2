/*
 * A way to keep all the key bindings in one place.
 * 
 * Seperated by states.
 * 
 */

using Microsoft.Xna.Framework.Input;

namespace Zomgame
{
    static class KeyBindings
    {
        // General
        public static Keys L_CTRL = Keys.LeftControl;
        public static Keys R_CTRL = Keys.RightControl;

        public static Keys L_SHIFT = Keys.LeftShift;
        public static Keys R_SHIFT = Keys.RightShift;

        public static Keys L_ALT = Keys.LeftAlt;
        public static Keys R_ALT = Keys.RightAlt;

        // Game state
        public static Keys UP = Keys.Up;
        public static Keys DOWN = Keys.Down;
        public static Keys LEFT = Keys.Left;
        public static Keys RIGHT = Keys.Right;

        public static Keys PICK_UP = Keys.G;
        public static Keys OPEN_INV = Keys.I;

        //Menu states
        public static Keys CLOSE_INV = Keys.I;
        public static Keys DROP_ITEM = Keys.D;

        //Note: multiple-key bindings ALWAYS assume that the first key is the 'modifier' ie ctrl/alt/shift
        //may not use these
        public static Keys[] ATTACK_UP = new Keys[] { Keys.LeftControl, Keys.Up };
        public static Keys[] ATTACK_DOWN = new Keys[] { Keys.LeftControl, Keys.Down };
        public static Keys[] ATTACK_LEFT = new Keys[] { Keys.LeftControl, Keys.Left };
        public static Keys[] ATTACK_RIGHT = new Keys[] { Keys.LeftControl, Keys.Right };
    }
}
