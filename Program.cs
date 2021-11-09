using System;
using Raylib_cs;

namespace doomfire
{
    class Program
    {
        static void Main(string[] args)
        {
            Raylib.InitWindow(800, 800, "Doom Fire");

            DoomFire doomFire = new DoomFire(100,100);

            while (!Raylib.WindowShouldClose())
            {
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_D))
                    doomFire.Debug = !doomFire.Debug;
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))
                    doomFire.Wind--;
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))
                    doomFire.Wind++;
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
                    doomFire.Decay--;
                if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
                    doomFire.Decay++;
                doomFire.Speed += (int)Raylib.GetMouseWheelMove();
                    

                doomFire.Update();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.WHITE);

                doomFire.Render();

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}
