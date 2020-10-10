using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MonoGameTemplate
{
    class Controller
    {
        public static KeyboardState keyboardState;
        public static MouseState mouseState;

        public static void Update()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(key) & !keyboardState.IsKeyDown(key))
                return true;
            return false;
        }

        public static bool IsKeyUp(Keys key)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyUp(key) & !keyboardState.IsKeyUp(key))
                return true;
            return false;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return Keyboard.GetState().IsKeyDown(key);
        }

        public static bool IsKeyReleased(Keys key)
        {
            return Keyboard.GetState().IsKeyUp(key);
        }

        public static bool IsRightButtonPressed()
        {
            MouseState state = Mouse.GetState();
            if (state.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsRightButtonDown()
        {
            MouseState state = Mouse.GetState();
            if (state.RightButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsRightButtonUp()
        {
            MouseState state = Mouse.GetState();
            if (state.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsRightButtonReleased()
        {
            MouseState state = Mouse.GetState();
            if (state.RightButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsLeftButtonDown()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsLeftButtonUp()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsLeftButtonPressed()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsLeftButtonReleased()
        {
            MouseState state = Mouse.GetState();
            if (state.LeftButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsMiddleButtonDown()
        {
            MouseState state = Mouse.GetState();
            if (state.MiddleButton == ButtonState.Pressed && mouseState.MiddleButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsMiddleButtonUp()
        {
            MouseState state = Mouse.GetState();
            if (state.MiddleButton == ButtonState.Released && mouseState.MiddleButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsMiddleButtonPressed()
        {
            MouseState state = Mouse.GetState();
            if (state.MiddleButton == ButtonState.Pressed)
                return true;
            return false;
        }

        public static bool IsMiddleButtonReleased()
        {
            MouseState state = Mouse.GetState();
            if (state.MiddleButton == ButtonState.Released)
                return true;
            return false;
        }

        public static bool IsScrollingUp()
        {
            MouseState state = Mouse.GetState();
            if (state.ScrollWheelValue > mouseState.ScrollWheelValue)
                return true;
            return false;
        }
        
        public static bool IsScrollingDown()
        {
            MouseState state = Mouse.GetState();
            if (state.ScrollWheelValue < mouseState.ScrollWheelValue)
                return true;
            return false;
        }

        public static Vector2 GetMousePosition()
        {
            MouseState state = Mouse.GetState();
            return new Vector2(state.Position.X, state.Position.Y);
        }

        public static Vector2 GetMousePosition(Scene scene, Camera camera)
        {
            MouseState state = Mouse.GetState();

            float cw = camera.bounds.Width;
            float sw = Game.width;
            float ch = camera.bounds.Height;
            float sh = Game.height;

            float x = cw / sw;
            float y = ch / sh;

            return new Vector2(state.Position.X * x + camera.bounds.X, state.Position.Y * y + camera.bounds.Y);
        }
    }
}
