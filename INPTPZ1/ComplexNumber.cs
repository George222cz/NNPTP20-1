using System;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class ComplexNumber
        {
            public double Real { get; set; }
            public double Imaginary { get; set; }

            public readonly static ComplexNumber Zero = new ComplexNumber()
            {
                Real = 0,
                Imaginary = 0
            };

            public ComplexNumber()
            {
                Real = 0;
                Imaginary = 0;
            }

            public ComplexNumber(double real, double imaginary)
            {
                Real = real;
                Imaginary = imaginary;
            }

            public ComplexNumber Multiply(ComplexNumber number)
            {
                return new ComplexNumber()
                {
                    Real = (Real * number.Real) - (Imaginary * number.Imaginary),
                    Imaginary = (Real * number.Imaginary) + (Imaginary * number.Real)
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
                    Imaginary = numerator.Imaginary / divisor
                };
            }

            public double GetAbs()
            {
                return Math.Sqrt(Real * Real + Imaginary * Imaginary);
            }
            public double GetAngleInDegrees()
            {
                double radians = (Real < 0 ? Math.PI : (Imaginary < 0 ? (2 * Math.PI) : 0)) + Math.Atan(Imaginary / Real);
                return (180 / Math.PI) * radians;
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