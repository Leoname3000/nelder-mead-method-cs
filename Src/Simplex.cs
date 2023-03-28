namespace Src;

public class Simplex
{
    public Simplex(double range)
    {
        _points = Generate(range);
    }
    private List<Point> _points;

    public override String ToString()
    {
        String result = "[ ";
        foreach (Point point in _points)
            result += $"{point} ";
        result += "]";
        return result;
    }

    List<Point> Generate(double range)
    {
        List<Point> pts = new List<Point>();
        Random random = new Random();
        for (int i = 0; i < 3; i++)
        {
            double randomX = random.NextDouble() * range - range / 2;
            double randomY = random.NextDouble() * range - range / 2;
            pts.Add(new Point(randomX, randomY));
        }
        return pts;
    }

    public void Order(IFunction function)
    {
        _points = MergeSort(_points, function.Calc);
    }

    public List<Point> MergeSort(List<Point> src, Func<Point, double> compareBy)
    {
        if (src.Count <= 1) 
            return src;
        int mid = src.Count / 2;
        List<Point> left = src.GetRange(0, mid);
        List<Point> right = src.GetRange(mid, src.Count - mid);
        return Merge(MergeSort(left, compareBy), MergeSort(right, compareBy), compareBy);
    }

    List<Point> Merge(List<Point> left, List<Point> right, Func<Point, double> compareBy)
    {
        List<Point> result = new List<Point>();
        int leftIdx = 0;
        int rightIdx = 0;
        while (leftIdx < left.Count && rightIdx < right.Count)
        {
            if (compareBy(left[leftIdx]) < compareBy(right[rightIdx]))
            {
                result.Add(left[leftIdx]);
                leftIdx++;
            }
            else
            {
                result.Add(right[rightIdx]);
                rightIdx++;
            }
        }
        while (result.Count < left.Count + right.Count)
        {
            if (leftIdx < left.Count)
            {
                result.Add(left[leftIdx]);
                leftIdx++;
            }
            else
            {
                result.Add(right[rightIdx]);
                rightIdx++;
            }
        }
        return result;
    }

    public Point Centroid() => (double) 1 / 2 * (_points[0] + _points[1]);
    public Point Best => _points[0];
    public Point Good => _points[1];
    public Point Worst
    {
        get => _points[2];
        set => _points[2] = value;
    }

    public void Shrink(double shrinkBy)
    {
        List<Point> shrunkPoints = new List<Point>();
        shrunkPoints.Add(_points[0]);
        for (int i = 1; i < _points.Count; i++)
        {
            shrunkPoints.Add((1 - shrinkBy) * _points[0] + shrinkBy * _points[i]);
        }
        _points = shrunkPoints;
    }
}