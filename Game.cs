namespace Snake;

public class Game
{
    private readonly int _windowWidth = Console.WindowWidth;
    private readonly int _windowHeight = Console.WindowHeight;

    private int _score = 5;
    private bool _isGameOver;
    private Direction _movement = Direction.Right;

    private readonly Random _randomNumber = new();


    public void Start()
    {
        var snakeHead = new Pixel
        {
            PosX = _windowWidth / 2,
            PosY = _windowHeight / 2,
            Color = ConsoleColor.Blue
        };
        
        var berry = new Pixel
        {
            PosX = _randomNumber.Next(0, _windowWidth),
            PosY = _randomNumber.Next(0, _windowHeight),
            Color = ConsoleColor.DarkRed
        };

        var bodyParts = new List<Pixel>();

        // What happens every frame
        while (true)
        {
            // Check for collisions with borders
            if (snakeHead.PosX == _windowWidth - 1 || snakeHead.PosX == 0 || snakeHead.PosY == _windowHeight - 1 ||
                snakeHead.PosY == 0)
            {
                _isGameOver = true;
            }
            
            // Check for collision with berry
            if (berry.PosX == snakeHead.PosX && berry.PosY == snakeHead.PosY)
            {
                _score++;
                berry.PosX = _randomNumber.Next(1, _windowWidth - 2);
                berry.PosY = _randomNumber.Next(1, _windowHeight - 2);
            }
            
            // Re-render screen
            Console.Clear();
            
            foreach (var part in bodyParts)
            {
                RenderCell(part.PosX, part.PosY, ConsoleColor.Green);
                if (part.PosX == snakeHead.PosX && part.PosY == snakeHead.PosY)
                {
                    _isGameOver = true;
                }
            }
            
            RenderPixel(snakeHead);
            RenderPixel(berry);
            RenderBorder();

            // End the game if there was a collision with border or body 
            if (_isGameOver)
            {
                break;
            }
            
            var dateTimeBeforeWait = DateTime.Now;
            var buttonWasPressed = false;
            // Waiting for the next frame (read the inputs for the next move)
            while (true)
            {
                // Check the timer
                var dateTimeWhileWaiting = DateTime.Now;
                if (dateTimeWhileWaiting.Subtract(dateTimeBeforeWait).TotalMilliseconds > 200)
                {
                    break;
                }

                // Read the key only if available or no key was already pressed 
                if (!Console.KeyAvailable || buttonWasPressed) continue;
                
                
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow when _movement != Direction.Down:
                        _movement = Direction.Up;
                        buttonWasPressed = true;
                        break;
                    case ConsoleKey.DownArrow when _movement != Direction.Up:
                        _movement = Direction.Down;
                        buttonWasPressed = true;
                        break;
                    case ConsoleKey.LeftArrow when _movement != Direction.Right:
                        _movement = Direction.Left;
                        buttonWasPressed = true;
                        break;
                    case ConsoleKey.RightArrow when _movement != Direction.Left:
                        _movement = Direction.Right;
                        buttonWasPressed = true;
                        break;
                }
            }
            
            // End-of-frame logic

            // Add a new body part
            bodyParts.Add(new Pixel
            (
                snakeHead.PosX,
                snakeHead.PosY
            ));
            
            // Move the snake head
            switch (_movement)
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
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // Remove the last part of the body (depends on the current score)
            if (bodyParts.Count > _score)
            {
                bodyParts.RemoveAt(0);
            }
        }

        // Game over text
        Console.SetCursorPosition(_windowWidth / 5, _windowHeight / 2);
        Console.WriteLine("Game over, Score: " + _score);
        Console.SetCursorPosition(_windowWidth / 5, _windowHeight / 2 + 1);
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

    private static void RenderCell(int posX, int posY, ConsoleColor color = ConsoleColor.White)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = color;
        Console.Write("■");
        Console.SetCursorPosition(0, 0);
    }

    private static void RenderPixel(Pixel pixel)
    {
        RenderCell(pixel.PosX, pixel.PosY, pixel.Color ?? ConsoleColor.White);
    }
}