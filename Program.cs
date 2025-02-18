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
            Pixel snakeHead = new Pixel
            {
                PosX = screenWidth / 2,
                PosY = screenHeight / 2,
                Color = ConsoleColor.Red
            };
            Direction movement = Direction.Right;
            List<int> snakeBodiesX = new List<int>();
            List<int> snakeBodiesY = new List<int>();
            int berryX = randomNumber.Next(0, screenWidth);
            int berryY = randomNumber.Next(0, screenHeight);
            DateTime dateTimeBeforeWait;
            DateTime dateTimeWhileWaiting;
            bool isButtonPressed;
            while (true)
            {
                Console.Clear();
                if (snakeHead.PosX == screenWidth - 1 || snakeHead.PosX == 0 || snakeHead.PosY == screenHeight - 1 ||
                    snakeHead.PosY == 0)
                {
                    isGameOver = true;
                }

                RenderBorder(screenWidth, screenHeight);

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

                if (isGameOver)
                {
                    break;
                }

                Console.SetCursorPosition(snakeHead.PosX, snakeHead.PosY);
                Console.ForegroundColor = snakeHead.Color;
                Console.Write("■");
                Console.SetCursorPosition(berryX, berryY);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                dateTimeBeforeWait = DateTime.Now;
                isButtonPressed = false;
                while (true)
                {
                    dateTimeWhileWaiting = DateTime.Now;
                    if (dateTimeWhileWaiting.Subtract(dateTimeBeforeWait).TotalMilliseconds > 500)
                    {
                        break;
                    }

                    if (Console.KeyAvailable && !isButtonPressed)
                    {
                        ConsoleKey key = Console.ReadKey(true).Key;
                        //Console.WriteLine(toets.Key.ToString());

                        if (key.Equals(ConsoleKey.UpArrow) && movement != Direction.Down)
                        {
                            movement = Direction.Up;
                            isButtonPressed = true;
                        }

                        if (key.Equals(ConsoleKey.DownArrow) && movement != Direction.Up)
                        {
                            movement = Direction.Down;
                            isButtonPressed = true;
                        }

                        if (key.Equals(ConsoleKey.LeftArrow) && movement != Direction.Right)
                        {
                            movement = Direction.Left;
                            isButtonPressed = true;
                        }

                        if (key.Equals(ConsoleKey.RightArrow) && movement != Direction.Left)
                        {
                            movement = Direction.Right;
                            isButtonPressed = true;
                        }
                    }
                }

                snakeBodiesX.Add(snakeHead.PosX);
                snakeBodiesY.Add(snakeHead.PosY);
                switch (movement)
                {
                    case Direction.Up:
                        snakeHead.PosY--;
                        break;
                    case Direction.Down:
                        snakeHead.PosY++;
                        break;
                    case Direction.Left:
                        snakeHead.PosX--;
                        break;
                    case Direction.Right:
                        snakeHead.PosX++;
                        break;
                }

                if (snakeBodiesX.Count > score)
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

        static void RenderBorder(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");

                Console.SetCursorPosition(i, height - 1);
                Console.Write("■");
            }

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");

                Console.SetCursorPosition(width - 1, i);
                Console.Write("■");
            }
        }
    }
}