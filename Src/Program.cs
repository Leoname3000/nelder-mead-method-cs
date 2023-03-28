﻿namespace Src;

internal class Program
{
    public static void Main(string[] args)
    {
        IFunction function = new HimmelblauFunction();
        NelderMeadMethod nelderMeadMethod = new NelderMeadMethod(1, 2, 0.5, 0.5);
        nelderMeadMethod.Run(function, 50, 50);
    }
}