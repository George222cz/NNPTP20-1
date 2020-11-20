using System;
using System.Collections.Generic;
using System.Drawing;
using INPTPZ1.Mathematics;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        private const int MAX_ITERATIONS = 30;
        private const double DEVIATION = 0.0001;
        private const string DEFAULT_OUTPUT = "../../../out.png";

        private static int bitmapWidth, bitmapHeight;
        private static double xmin, xmax, ymin, ymax, xstep, ystep;
        private static string output;
        private static Bitmap bitmap;

        static void Main(string[] args)
        {
            SetVariables(args);

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Polynomial polynomial = new Polynomial();
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(ComplexNumber.Zero);
            polynomial.Coefficients.Add(new ComplexNumber() { Real = 1 });
            Polynomial derivatedPolynomial = polynomial.Derive();

            Console.WriteLine(polynomial);
            Console.WriteLine(derivatedPolynomial);

            for (int x = 0; x < bitmapWidth; x++)
            {
                for (int y = 0; y < bitmapHeight; y++)
                {
                    ComplexNumber location = GetLocationComplexNumber(x,y);
                    int iterationPassed = CalculateRootUsingNewtonMethod(polynomial, derivatedPolynomial, ref location);
                    int rootIndex = FindRootIndex(roots, location);
                    ColorizePixel(x, y, iterationPassed, rootIndex);
                }
            }
            bitmap.Save(output ?? DEFAULT_OUTPUT);
        }

        private static void SetVariables(string[] args)
        {
            bitmapWidth = int.Parse(args[0]);
            bitmapHeight = int.Parse(args[1]);
            xmin = double.Parse(args[2]);
            xmax = double.Parse(args[3]);
            ymin = double.Parse(args[4]);
            ymax = double.Parse(args[5]);
            output = args[6];
            xstep = (xmax - xmin) / bitmapWidth;
            ystep = (ymax - ymin) / bitmapHeight;
            bitmap = new Bitmap(bitmapWidth, bitmapHeight);
        }

        private static ComplexNumber GetLocationComplexNumber(int x, int y)
        {
            double real = xmin + x * xstep;
            double imaginary = ymin + y * ystep;
            if (real == 0)
                real = DEVIATION;
            if (imaginary == 0)
                imaginary = DEVIATION;
            return new ComplexNumber(real, imaginary);
        }

        private static int CalculateRootUsingNewtonMethod(Polynomial poly, Polynomial derivatedPoly, ref ComplexNumber location)
        {
            int iterationsPassed = 0;
            for (int i = 0; i < MAX_ITERATIONS; i++)
            {
                ComplexNumber difference = poly.Evaluate(location).Divide(derivatedPoly.Evaluate(location));
                location = location.Subtract(difference);

                if (Math.Pow(difference.Real, 2) + Math.Pow(difference.Imaginary, 2) >= 0.5)
                {
                    i--;
                }
                iterationsPassed++;
            }
            return iterationsPassed;
        }

        private static int FindRootIndex(List<ComplexNumber> roots, ComplexNumber location)
        {
            bool known = false;
            int index = 0;
            for (int i = 0; i < roots.Count; i++)
            {
                if (Math.Pow(location.Real - roots[i].Real, 2) + Math.Pow(location.Imaginary - roots[i].Imaginary, 2) <= 0.01)
                {
                    known = true;
                    index = i;
                }
            }
            if (!known)
            {
                roots.Add(location);
                index = roots.Count;
            }
            return index;
        }

        private static void ColorizePixel(int x, int y, int iteration, int rootIndex)
        {
            Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            Color color = colors[rootIndex % colors.Length];
            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - (int)iteration * 2), 255), 
                Math.Min(Math.Max(0, color.G - (int)iteration * 2), 255), 
                Math.Min(Math.Max(0, color.B - (int)iteration * 2), 255));
            bitmap.SetPixel(x, y, color);
        }
    }
}
