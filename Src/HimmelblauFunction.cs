namespace Src;

public class HimmelblauFunction : IFunction
{
    public double Calc(Point p)
    {
        return Math.Pow(Math.Pow(p.X, 2) + p.Y - 11, 2) + Math.Pow(p.X + Math.Pow(p.Y, 2) - 7, 2);
    }
}