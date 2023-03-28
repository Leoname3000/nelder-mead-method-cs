namespace Src;

public class NelderMeadMethod
{
    public NelderMeadMethod(double alpha, double beta, double gamma, double sigma)
    {
        _alpha = alpha;
        _beta = beta;
        _gamma = gamma;
        _sigma = sigma;
    }
    private readonly double _alpha;
    private readonly double _beta;
    private readonly double _gamma;
    private readonly double _sigma;

    Point Reflect(Point centroid, Point worst) => (1 + _alpha) * centroid - _alpha * worst;
    Point Expand(Point centroid, Point reflected) => (1 - _beta) * centroid + _beta * reflected;
    Point Contract(Point centroid, Point toContract) => (1 - _gamma) * centroid + _gamma * toContract;
    
    public Point Run(IFunction function, int iterations, double simplexRange = 1)
    {
        Simplex simplex = new Simplex(simplexRange);
        String ReportString(String name, Point point) => 
            $"{name}: f{point} = {Math.Round(function.Calc(point), 2, MidpointRounding.AwayFromZero):#0.00} | ";

        for (int i = 0; i < iterations; i++)
        {
            // Step 1. Ordering simplex.
            simplex.Order(function);
            Point best = simplex.Best;
            Point good = simplex.Good;
            Point worst = simplex.Worst;

            String reportString = $"Iteration {i} | ";
            reportString += ReportString("Best", best) + ReportString("Good", good) + ReportString("Worst", worst);
                
            // Step 2. Finding centroid.
            Point centroid = simplex.Centroid();
            
            // Step 3. Reflection.
            Point reflected = Reflect(centroid, worst);
            reportString += ReportString("Reflected", reflected);
            if (function.Calc(best) <= function.Calc(reflected) && function.Calc(reflected) < function.Calc(good))
            {
                simplex.Worst = reflected;
            }

            // Step 4. Expansion.
            else if (function.Calc(reflected) < function.Calc(best))
            {
                Point expanded = Expand(centroid, reflected);
                reportString += ReportString("Expanded", expanded);
                if (function.Calc(expanded) < function.Calc(reflected))
                    simplex.Worst = expanded;
                else
                    simplex.Worst = reflected;
            }
            
            // Step 5. Contraction (Point shrinkage).
            else
            {
                Point toContract = reflected;
                if (function.Calc(worst) < function.Calc(reflected))
                    toContract = worst;
                Point contracted = Contract(centroid, toContract);
                reportString += ReportString("Contracted", contracted);
                if (function.Calc(contracted) < function.Calc(toContract))
                    simplex.Worst = contracted;
                
                // Step 6. Shrinkage (Simplex shrinkage).
                else
                    simplex.Shrink(_sigma);
            }
            Console.WriteLine(reportString);
        }
        Console.WriteLine($"Final result: f{simplex.Best} = {Math.Round(function.Calc(simplex.Best), 2, MidpointRounding.AwayFromZero):#0.00}");
        return simplex.Best;
    }
}