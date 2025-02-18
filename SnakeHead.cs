namespace Snake;

public class SnakeHead(int posX, int posY) : Pixel(posX, posY, HeadColor)
{
    private const ConsoleColor HeadColor = ConsoleColor.Blue;
    
    
    public void MoveInDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                PosY--;
                break;
            case Direction.Down:
                PosY++;
                break;
            case Direction.Left:
                PosX--;
                break;
            case Direction.Right:
                PosX++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}