using System;
using Raylib_cs;

namespace doomfire
{
    public class DoomFire
    {
        private static Color[] pallete = { new Color(7,7,7, 255),new Color(31,7,7, 255),new Color(47,15,7, 255),new Color(71,15,7, 255),new Color(87,23,7, 255),new Color(103,31,7, 255),new Color(119,31,7, 255),new Color(143,39,7, 255),new Color(159,47,7, 255),new Color(175,63,7, 255),new Color(191,71,7, 255),new Color(199,71,7, 255),new Color(223,79,7, 255),new Color(223,87,7, 255),new Color(223,87,7, 255),new Color(215,95,7, 255),new Color(215,95,7, 255),new Color(215,103,15, 255),new Color(207,111,15, 255),new Color(207,119,15, 255),new Color(207,127,15, 255),new Color(207,135,23, 255),new Color(199,135,23, 255),new Color(199,143,23, 255),new Color(199,151,31, 255),new Color(191,159,31, 255),new Color(191,159,31, 255),new Color(191,167,39, 255),new Color(191,167,39, 255),new Color(191,175,47, 255),new Color(183,175,47, 255),new Color(183,183,47, 255),new Color(183,183,55, 255),new Color(207,207,111, 255),new Color(223,223,159, 255),new Color(239,239,199, 255),new Color(255,255,255, 255) };
        private static Random random = new Random();
        
        public int Width { get; set; }
        public int Height { get; set; }

        private int speed = 20;
        public int Speed
        {
            get { return speed; }
            set { speed = Math.Max(value, 1); }
        }
        
        private int wind = 3;
        public int Wind
        {
            get { return wind; }
            set { wind = value; }
        }
        
        private int decay = 3;
        public int Decay
        {
            get { return decay; }
            set { decay = Math.Max(value, 2); }
        }
        

        public bool Debug { get; set; }
        
        private int[] grid;
        private float delay;
        private float elapsed;

        public DoomFire(int width, int height)
        {
            Width = width;
            Height = height;

            InitGrid();
            CreateFireSource();
        }

        private void InitGrid() 
        {
            grid = new int[Width * Height];
        }

        private void CreateFireSource()
        {
            int startIndex = (Height - 1) * Width; 
            for (int i = 0; i < Width; i++)
            {
                grid[startIndex + i] = 36;
            }
        }

        public void Update()
        {
            delay = 1f / Speed;
            elapsed += Raylib.GetFrameTime();
            if (elapsed < delay)
                return;
            
            elapsed -= delay;

            for (int y = 0; y < Height - 1; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x;
                    int decay = random.Next(0, Decay);
                    int wind = Wind > 0 ? random.Next(0, Wind + 1) : random.Next(Wind, 0);
                    int updateIndex = Math.Clamp(index + wind, 0, Width * Height - 1);
                    grid[updateIndex] = Math.Max(grid[index + Width] - decay, 0);
                }
            }
        }

        public void Render()
        {
            int screenWidth = Raylib.GetScreenWidth();
            int screenHeight = Raylib.GetScreenHeight();
            
            int cellWidth = screenWidth / Width;
            int cellHeight = screenHeight / Height;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int index = y * Width + x;
                    int xPos = cellWidth * x;
                    int yPos = cellHeight * y;
                    Color color = pallete[grid[index]];

                    Raylib.DrawRectangle(xPos, yPos, cellWidth, cellHeight, color);
                    
                    if (Debug)
                        Raylib.DrawText(grid[index].ToString(), xPos, yPos, 10, Color.BLUE);
                }
            }

            Raylib.DrawText($"Speed: {Speed}\nWind: {Wind}\nDecay: {Decay}", 10, 10, 16, Color.WHITE);
        }
    }
}
