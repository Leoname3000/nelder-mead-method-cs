namespace Src;

public class Point
{
    public Point(double x, double y)
    {
        _x = x;
        _y = y;
    }
    private readonly double _x;
    private readonly double _y;

    public double X => _x;
    public double Y => _y;

    public override String ToString()
    {
        double roundedX = Math.Round(_x, 2, MidpointRounding.AwayFromZero);
        double roundedY = Math.Round(_y, 2, MidpointRounding.AwayFromZero);
        return $"({roundedX:0.00};{roundedY:0.00})";
    }
    
    public static Point operator +(Point p1, Point p2) => new (p1.X + p2.X, p1.Y + p2.Y);
    public static Point operator -(Point p1, Point p2) => new (p1.X - p2.X, p1.Y - p2.Y);
    public static Point operator *(double a, Point p) => new (a * p.X, a * p.Y);
}