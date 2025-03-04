namespace Snake;

public class Snake(int posX, int posY)
{
    public SnakeHead Head { get; } = new(posX, posY);
    public List<Pixel> Body { get; } = [];

    public void AddBodyPart(Pixel pixel)
    {
        Body.Add(new Pixel
        (
            pixel.PosX,
            pixel.PosY
        ));
    }

    public void RemoveLastBodyPart(int maxCount)
    {
        if (Body.Count > maxCount)
        {
            Body.RemoveAt(0);
        }
    }
}