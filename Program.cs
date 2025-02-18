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
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;
            Random randomNumber = new Random();
            int score = 5;
            bool isGameOver = false;
            Pixel snakeHead = new Pixel
            {
                PosX = windowWidth / 2,
                PosY = windowHeight / 2,
                Color = ConsoleColor.Blue
            };
            Direction movement = Direction.Right;
            Pixel berry = new Pixel
            {
                PosX = randomNumber.Next(0, windowWidth),
                PosY = randomNumber.Next(0, windowHeight),
                Color = ConsoleColor.DarkRed
            };

            List<Pixel> bodyParts = new List<Pixel>();
            DateTime dateTimeBeforeWait;
            DateTime dateTimeWhileWaiting;
            bool isButtonPressed;
            while (true)
            {
                Console.Clear();
                if (snakeHead.PosX == windowWidth - 1 || snakeHead.PosX == 0 || snakeHead.PosY == windowHeight - 1 ||
                    snakeHead.PosY == 0)
                {
                    isGameOver = true;
                }

                RenderBorder(windowWidth, windowHeight);

                Console.ForegroundColor = ConsoleColor.Green;
                if (berry.PosX == snakeHead.PosX && berry.PosY == snakeHead.PosY)
                {
                    score++;
                    berry.PosX = randomNumber.Next(1, windowWidth - 2);
                    berry.PosY = randomNumber.Next(1, windowHeight - 2);
                }

                for (int i = 0; i < bodyParts.Count; i++)
                {
                    RenderCell(bodyParts[i].PosX, bodyParts[i].PosY, ConsoleColor.Green);
                    if (bodyParts[i].PosX == snakeHead.PosX && bodyParts[i].PosY == snakeHead.PosY)
                    {
                        isGameOver = true;
                    }
                }

                if (isGameOver)
                {
                    break;
                }

                RenderCell(snakeHead.PosX, snakeHead.PosY, snakeHead.Color);
                RenderCell(berry.PosX, berry.PosY, berry.Color);

                Console.ForegroundColor = ConsoleColor.White;
                dateTimeBeforeWait = DateTime.Now;
                isButtonPressed = false;
                while (true)
                {
                    dateTimeWhileWaiting = DateTime.Now;
                    if (dateTimeWhileWaiting.Subtract(dateTimeBeforeWait).TotalMilliseconds > 200)
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

                bodyParts.Add(new Pixel
                (
                    snakeHead.PosX,
                    snakeHead.PosY
                ));
                // snakeBodiesY.Add(snakeHead.PosY);
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

                if (bodyParts.Count > score)
                {
                    bodyParts.RemoveAt(0);
                }
            }

            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
        }

        static void RenderBorder(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                RenderCell(i, 0);
                RenderCell(i, height - 1);
            }

            for (int i = 0; i < height; i++)
            {
                RenderCell(0, i);
                RenderCell(width - 1, i);
            }
        }

        static void RenderCell(int posX, int posY, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = color;
            Console.Write("■");
        }
    }
}