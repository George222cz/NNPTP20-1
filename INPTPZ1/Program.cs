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
        static void Main(string[] args)
        {
            int bitmapWidth = int.Parse(args[0]);
            int bitmapHeight = int.Parse(args[1]);
            double xmin = double.Parse(args[2]);
            double xmax = double.Parse(args[3]);
            double ymin = double.Parse(args[4]);
            double ymax = double.Parse(args[5]);
            string output = args[6];

            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            double xstep = (xmax - xmin) / bitmapWidth;
            double ystep = (ymax - ymin) / bitmapHeight;

            List<ComplexNumber> roots = new List<ComplexNumber>();
            Poly poly = new Poly();
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(ComplexNumber.Zero);
            poly.Coefficients.Add(new ComplexNumber() { Real = 1 });
            Poly derivatedPoly = poly.Derive();

            Console.WriteLine(poly);
            Console.WriteLine(derivatedPoly);

            Color[] colors = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            // for every pixel in image...
            for (int x = 0; x < bitmapWidth; x++)
            {
                for (int y = 0; y < bitmapHeight; y++)
                {
                    ComplexNumber location = new ComplexNumber().Initialize(xmin,xmin,xstep,ystep,x,y);                    
                    int iterationPassed = CalculateRootUsingNewtonMethod(poly, derivatedPoly, ref location);
                    int rootIndex = FindRootIndex(roots, location);
                    ColorizePixel(bitmap,colors,x,y,iterationPassed,rootIndex);
                }
            }
            bitmap.Save(output ?? "../../../out.png");
        }

        // find solution of equation using newton's iteration
        private static int CalculateRootUsingNewtonMethod(Poly poly, Poly derivatedPoly, ref ComplexNumber location)
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

        // find solution root number
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

        // colorize pixel according to root number
        private static void ColorizePixel(Bitmap bitmap, Color[] colors, int x, int y, int iteration, int rootIndex)
        {
            Color color = colors[rootIndex % colors.Length];
            color = Color.FromArgb(
                Math.Min(Math.Max(0, color.R - (int)iteration * 2), 255), 
                Math.Min(Math.Max(0, color.G - (int)iteration * 2), 255), 
                Math.Min(Math.Max(0, color.B - (int)iteration * 2), 255));
            bitmap.SetPixel(x, y, color);
        }
    }
}
