namespace Snake;

public class Pixel
{
    public Pixel()
    {
    }

    public Pixel(int posX, int posY, ConsoleColor color = ConsoleColor.White)
    {
        PosX = posX;
        PosY = posY;
        Color = color;
    }

    public int PosX { get; set; }
    public int PosY { get; set; }
    public ConsoleColor ?Color { get; init; }

    public bool EqualsPosition(Pixel pixel)
    {
        return PosX == pixel.PosX && PosY == pixel.PosY;
    }
    
    
}