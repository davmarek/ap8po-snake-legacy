namespace Snake;

public class Game
{
    private readonly int _windowWidth = Console.WindowWidth;
    private readonly int _windowHeight = Console.WindowHeight;

    private int _score = 5;
    private bool _isGameOver;
    private Direction _movement = Direction.Right;

    private readonly SnakeHead _snakeHead;
    private readonly Berry _berry;
    private readonly List<Pixel> _bodyParts = [];

    public Game()
    {
        _snakeHead = new SnakeHead(
            _windowWidth / 2,
            _windowHeight / 2
        );

        _berry = new Berry(_windowWidth, _windowHeight);
    }


    public void Start()
    {
        // What happens every frame
        while (true)
        {
            // Check for collisions with borders
            _isGameOver = IsPixelInBorder(_snakeHead);


            // Check for collision with berry
            if (_snakeHead.EqualsPosition(_berry))
            {
                _score++;
                _berry.RespawnBerry();
            }

            // Re-render screen
            Console.Clear();

            foreach (var bodyPart in _bodyParts)
            {
                RenderCell(bodyPart.PosX, bodyPart.PosY, ConsoleColor.Green);
                if (bodyPart.EqualsPosition(_snakeHead))
                {
                    _isGameOver = true;
                }
            }

            RenderPixel(_snakeHead);
            RenderPixel(_berry);
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
                buttonWasPressed = HandleInput();
            }

            // End-of-frame logic
            // Add a new body part
            _bodyParts.Add(new Pixel
            (
                _snakeHead.PosX,
                _snakeHead.PosY
            ));

            // Move the snake head
            _snakeHead.MoveInDirection(_movement);

            // Remove the last part of the body (depends on the current score)
            if (_bodyParts.Count > _score)
            {
                _bodyParts.RemoveAt(0);
            }
        }

        RenderGameOver();
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