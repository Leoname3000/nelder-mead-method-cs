namespace Src;

public class RosenbrockFunction : IFunction
{
    public double Calc(Point p) => Math.Pow(1 - p.X, 2) + 100 * Math.Pow(p.Y - Math.Pow(p.X, 2), 2);
}