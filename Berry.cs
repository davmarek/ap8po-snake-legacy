namespace Snake;

public class Berry : Pixel
{
    private const ConsoleColor BerryColor = ConsoleColor.Red;

    private readonly Random _random = new();


    private readonly int _maxX;
    private readonly int _maxY;
    

    public Berry(int widthLimit, int heightLimit)
    {
        Color = BerryColor;
        _maxX = widthLimit - 2;
        _maxY = heightLimit - 2;
        RespawnBerry();
    }

    public void RespawnBerry()
    {
        PosX = _random.Next(1, _maxX);
        PosY = _random.Next(1, _maxY);
    }
}