using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


// https://codereview.stackexchange.com/questions/127515/first-c-program-snake-game
// https://www.youtube.com/watch?v=SGZgvMwjq2U

namespace Snake;

class Program
{
    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }


    static void Main(string[] args)
    {
        //Console.WindowHeight = 16;
        //Console.WindowWidth = 32;
        var windowWidth = Console.WindowWidth;
        var windowHeight = Console.WindowHeight;
        var randomNumber = new Random();
        var score = 5;
        var gameOver = false;

        Pixel headPixel = new Pixel();
        headPixel.XPos = windowWidth / 2;
        headPixel.YPos = windowHeight / 2;
        headPixel.Color = ConsoleColor.Red;

        var movement = Direction.RIGHT;

        List<int> bodyPositionsX = new List<int>();
        List<int> bodyPositionsY = new List<int>();

        int berryX = randomNumber.Next(0, windowWidth);
        int berryY = randomNumber.Next(0, windowHeight);

        DateTime dateTime1 = DateTime.Now;
        DateTime dateTime2 = DateTime.Now;

        var buttonPressed = false;


        while (true)
        {
            Console.Clear();
            gameOver = headPixel.XPos == windowWidth - 1 || headPixel.XPos == 0 || headPixel.YPos == windowHeight - 1 ||
                       headPixel.YPos == 0;

            DrawBorders(windowWidth, windowWidth);

            Console.ForegroundColor = ConsoleColor.Green;
            if (berryX == headPixel.XPos && berryY == headPixel.YPos)
            {
                score++;
                berryX = randomNumber.Next(1, windowWidth - 2);
                berryY = randomNumber.Next(1, windowHeight - 2);
            }

            for (var i = 0; i < bodyPositionsX.Count; i++)
            {
                Console.SetCursorPosition(bodyPositionsX[i], bodyPositionsY[i]);
                Console.Write("■");
                if (bodyPositionsX[i] == headPixel.XPos && bodyPositionsY[i] == headPixel.YPos)
                {
                    gameOver = true;
                }
            }

            if (gameOver)
            {
                break;
            }

            DrawPixel(headPixel.XPos, headPixel.YPos, headPixel.Color);
            DrawPixel(berryX, berryY, ConsoleColor.Cyan);

            dateTime1 = DateTime.Now;
            buttonPressed = false;

            while (true)
            {
                dateTime2 = DateTime.Now;
                if (dateTime2.Subtract(dateTime1).TotalMilliseconds > 500)
                {
                    break;
                }

                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    //Console.WriteLine(toets.Key.ToString());
                    if (key.Equals(ConsoleKey.UpArrow) && movement != Direction.DOWN && !buttonPressed)
                    {
                        movement = Direction.UP;
                        buttonPressed = true;
                    }

                    if (key.Equals(ConsoleKey.DownArrow) && movement != Direction.UP && !buttonPressed)
                    {
                        movement = Direction.DOWN;
                        buttonPressed = true;
                    }

                    if (key.Equals(ConsoleKey.LeftArrow) && movement != Direction.RIGHT &&
                        !buttonPressed)
                    {
                        movement = Direction.LEFT;
                        buttonPressed = true;
                    }

                    if (key.Equals(ConsoleKey.RightArrow) && movement != Direction.LEFT &&
                        !buttonPressed)
                    {
                        movement = Direction.RIGHT;
                        buttonPressed = true;
                    }
                }
            }

            bodyPositionsX.Add(headPixel.XPos);
            bodyPositionsY.Add(headPixel.YPos);
            switch (movement)
            {
                case Direction.UP:
                    headPixel.YPos--;
                    break;
                case Direction.DOWN:
                    headPixel.YPos++;
                    break;
                case Direction.LEFT:
                    headPixel.XPos--;
                    break;
                case Direction.RIGHT:
                    headPixel.XPos++;
                    break;
            }

            if (bodyPositionsX.Count > score)
            {
                bodyPositionsX.RemoveAt(0);
                bodyPositionsY.RemoveAt(0);
            }
        }

        Console.SetCursorPosition(windowWidth / 5, windowHeight / 2);
        Console.WriteLine("Game over, Score: " + score);
        Console.SetCursorPosition(windowWidth / 5, windowHeight / 2 + 1);
    }

    static void DrawPixel(int x, int y, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write("■");
    }

    static void DrawBorders(int windowWidth, int windowHeight)
    {
        for (int i = 0; i < windowWidth; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("■");

            Console.SetCursorPosition(i, windowHeight - 1);
            Console.Write("■");
        }


        for (int i = 0; i < windowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");

            Console.SetCursorPosition(windowWidth - 1, i);
            Console.Write("■");
        }
    }
}

class Pixel
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public ConsoleColor Color { get; set; }
}