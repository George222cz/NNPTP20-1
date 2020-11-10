using System;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class ComplexNumber
        {
            private const double DEVIATION = 0.0001;
            public double Real { get; set; }
            public float Imaginary { get; set; }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber Initialize(double xmin, double ymin, double xstep, double ystep, int x, int y)
            {
                Real = xmin + x * xstep;
                Imaginary = (float)(ymin + y * ystep);

                if (Real == 0)
                    Real = DEVIATION;
                if (Imaginary == 0)
                    Imaginary = (float)DEVIATION;
                return this;
            }

            public ComplexNumber Multiply(ComplexNumber number)
            {
                return new ComplexNumber()
                {
                    Real = (Real * number.Real) - (Imaginary * number.Imaginary),
                    Imaginary = (float)((Real * number.Imaginary) + (Imaginary * number.Real))
                };
            }

            public ComplexNumber Add(ComplexNumber number)
            {
                return new ComplexNumber()
                {
                    Real = Real + number.Real,
                    Imaginary = Imaginary + number.Imaginary
                };
            }

            public ComplexNumber Subtract(ComplexNumber number)
            {
                return new ComplexNumber()
                {
                    Real = Real - number.Real,
                    Imaginary = Imaginary - number.Imaginary
                };
            }

            internal ComplexNumber Divide(ComplexNumber number)
            {
                ComplexNumber numerator = Multiply(new ComplexNumber() { Real = number.Real, Imaginary = -number.Imaginary });
                double divisor = number.Real * number.Real + number.Imaginary * number.Imaginary;

                return new ComplexNumber()
                {
                    Real = numerator.Real / divisor,
                    Imaginary = (float)(numerator.Imaginary / divisor)
                };
            }

            public double GetAbS()
            {
                return Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }
            public double GetAngleInDegrees()
            {
                return Math.Atan(Imaginary / Real);
            }

            public override string ToString()
            {
                return $"({Real} + {Imaginary}i)";
            }

            public override bool Equals(object input)
            {
                if (input is ComplexNumber)
                {
                    ComplexNumber inputComplexNumber = input as ComplexNumber;
                    return inputComplexNumber.Real == Real && inputComplexNumber.Imaginary == Imaginary;
                }
                return base.Equals(input);
            }

            public override int GetHashCode()
            {
                int hashCode = -837395861;
                hashCode = hashCode * -1521134295 + Real.GetHashCode();
                hashCode = hashCode * -1521134295 + Imaginary.GetHashCode();
                return hashCode;
            }
        }
    }
}