using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

////https://www.youtube.com/watch?v=SGZgvMwjq2U
namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WindowHeight = 16;
            // Console.WindowWidth = 32;
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            Random randomNumber = new Random();
            int score = 5;
            bool isGameOver = false;
            Pixel snakeHead = new Pixel();
            snakeHead.PosX = screenWidth / 2;
            snakeHead.PosY = screenHeight / 2;
            snakeHead.Color = ConsoleColor.Red;
            string movement = "RIGHT";
            List<int> snakeBodiesX = new List<int>();
            List<int> snakeBodiesY = new List<int>();
            int berryX = randomNumber.Next(0, screenWidth);
            int berryY = randomNumber.Next(0, screenHeight);
            DateTime tijd = DateTime.Now;
            DateTime tijd2 = DateTime.Now;
            bool isButtonPressed = false;
            while (true)
            {
                Console.Clear();
                if (snakeHead.PosX == screenWidth - 1 || snakeHead.PosX == 0 || snakeHead.PosY == screenHeight - 1 ||
                    snakeHead.PosY == 0)
                {
                    isGameOver = true;
                }

                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }

                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, screenHeight - 1);
                    Console.Write("■");
                }

                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }

                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(screenWidth - 1, i);
                    Console.Write("■");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                if (berryX == snakeHead.PosX && berryY == snakeHead.PosY)
                {
                    score++;
                    berryX = randomNumber.Next(1, screenWidth - 2);
                    berryY = randomNumber.Next(1, screenHeight - 2);
                }

                for (int i = 0; i < snakeBodiesX.Count(); i++)
                {
                    Console.SetCursorPosition(snakeBodiesX[i], snakeBodiesY[i]);
                    Console.Write("■");
                    if (snakeBodiesX[i] == snakeHead.PosX && snakeBodiesY[i] == snakeHead.PosY)
                    {
                        isGameOver = true;
                    }
                }

                if (isGameOver == true)
                {
                    break;
                }

                Console.SetCursorPosition(snakeHead.PosX, snakeHead.PosY);
                Console.ForegroundColor = snakeHead.Color;
                Console.Write("■");
                Console.SetCursorPosition(berryX, berryY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                tijd = DateTime.Now;
                isButtonPressed = false;
                while (true)
                {
                    tijd2 = DateTime.Now;
                    if (tijd2.Subtract(tijd).TotalMilliseconds > 500)
                    {
                        break;
                    }

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        //Console.WriteLine(toets.Key.ToString());
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && isButtonPressed == false)
                        {
                            movement = "UP";
                            isButtonPressed = true;
                        }

                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && isButtonPressed == false)
                        {
                            movement = "DOWN";
                            isButtonPressed = true;
                        }

                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && isButtonPressed == false)
                        {
                            movement = "LEFT";
                            isButtonPressed = true;
                        }

                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && isButtonPressed == false)
                        {
                            movement = "RIGHT";
                            isButtonPressed = true;
                        }
                    }
                }

                snakeBodiesX.Add(snakeHead.PosX);
                snakeBodiesY.Add(snakeHead.PosY);
                switch (movement)
                {
                    case "UP":
                        snakeHead.PosY--;
                        break;
                    case "DOWN":
                        snakeHead.PosY++;
                        break;
                    case "LEFT":
                        snakeHead.PosX--;
                        break;
                    case "RIGHT":
                        snakeHead.PosX++;
                        break;
                }

                if (snakeBodiesX.Count() > score)
                {
                    snakeBodiesX.RemoveAt(0);
                    snakeBodiesY.RemoveAt(0);
                }
            }

            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }

        class Pixel
        {
            public int PosX { get; set; }
            public int PosY { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}