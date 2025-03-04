using System.Diagnostics;

namespace Snake;

public class Game
{
    // Constants
    private const int TimeBetweenFrames = 200;

    // Game parameters
    private readonly int _windowWidth = Console.WindowWidth;
    private readonly int _windowHeight = Console.WindowHeight;

    // State
    private int _score = 5;
    private bool _isGameOver;
    private Direction _movement = Direction.Right;

    // Game objects
    private readonly Snake _snake;
    private readonly Berry _berry;

    public Game()
    {
        _snake = new Snake(
            _windowWidth / 2,
            _windowHeight / 2
        );

        _berry = new Berry(_windowWidth, _windowHeight);
    }


    public void Start()
    {
        // Main loop
        while (true)
        {
            // Check for collisions with borders
            _isGameOver = IsPixelInBorder(_snake.Head);

            // Check for collision with berry
            if (_snake.Head.EqualsPosition(_berry))
            {
                _score++;
                _berry.RespawnBerry();
            }

            RenderGame();

            // End the game if there was a collision with border or body 
            if (_isGameOver)
            {
                break;
            }

            // Waiting for the next frame (read the inputs for the next move)
            var stopwatch = Stopwatch.StartNew();
            var buttonWasPressed = false;
            while (stopwatch.ElapsedMilliseconds <= TimeBetweenFrames)
            {
                // Read the key only if available or no key was already pressed 
                if (!Console.KeyAvailable || buttonWasPressed) continue;
                buttonWasPressed = HandleInput();
            }

            // End-of-frame logic
            _snake.AddBodyPart(_snake.Head);
            _snake.Head.MoveInDirection(_movement);
            _snake.RemoveLastBodyPart(_score);
        }

        RenderGameOver();
    }

    // Re-render the entire game - snake, berry and borders
    private void RenderGame()
    {
        Console.Clear();
        _isGameOver = RenderBody(_snake) ?? _isGameOver;
        RenderPixel(_snake.Head);
        RenderPixel(_berry);
        RenderBorder();
    }

    private void RenderBorder()
    {
        for (var i = 0; i < _windowWidth; i++)
        {
            RenderCell(i, 0);
            RenderCell(i, _windowHeight - 1);
        }

        for (var i = 0; i < _windowHeight; i++)
        {
            RenderCell(0, i);
            RenderCell(_windowWidth - 1, i);
        }
    }


    private static void RenderPixel(Pixel pixel)
    {
        RenderCell(pixel.PosX, pixel.PosY, pixel.Color ?? ConsoleColor.White);
    }

    private static void RenderCell(int posX, int posY, ConsoleColor color = ConsoleColor.White)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = color;
        Console.Write("■");
        Console.SetCursorPosition(0, 0);
    }


    private static bool? RenderBody(Snake snake)
    {
        foreach (var bodyPart in snake.Body)
        {
            RenderCell(bodyPart.PosX, bodyPart.PosY, ConsoleColor.Green);
            if (bodyPart.EqualsPosition(snake.Head))
            {
                return true;
            }
        }

        return null;
    }

    private bool IsPixelInBorder(Pixel pixel)
    {
        return pixel.PosX == 0 || pixel.PosY == 0 || pixel.PosX == _windowWidth - 1 || pixel.PosY == _windowHeight - 1;
    }

    private void RenderGameOver()
    {
        Console.SetCursorPosition(_windowWidth / 5, _windowHeight / 2);
        Console.WriteLine("Game over, Score: " + _score);
        Console.SetCursorPosition(_windowWidth / 5, _windowHeight / 2 + 1);
    }


    // Returns whether a valid input has been registered
    private bool HandleInput()
    {
        var key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.UpArrow when _movement != Direction.Down:
                _movement = Direction.Up;
                return true;

            case ConsoleKey.DownArrow when _movement != Direction.Up:
                _movement = Direction.Down;
                return true;

            case ConsoleKey.LeftArrow when _movement != Direction.Right:
                _movement = Direction.Left;
                return true;

            case ConsoleKey.RightArrow when _movement != Direction.Left:
                _movement = Direction.Right;
                return true;

            default:
                return false;
        }
    }
}