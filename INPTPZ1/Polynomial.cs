using System.Collections.Generic;

namespace INPTPZ1
{
    namespace Mathematics
    {
        public class Polynomial
        {

            public List<ComplexNumber> Coefficients { get; set; }

            public Polynomial() => Coefficients = new List<ComplexNumber>();

            public Polynomial Derive()
            {
                Polynomial poly = new Polynomial();
                for (int i = 1; i < Coefficients.Count; i++)
                {
                    poly.Coefficients.Add(Coefficients[i].Multiply(new ComplexNumber() { Real = i }));
                }

                return poly;
            }

            public ComplexNumber Evaluate(ComplexNumber imputNumber)
            {
                ComplexNumber finalResult = ComplexNumber.Zero;
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    ComplexNumber coefficient = Coefficients[i];
                    ComplexNumber multiplyResult = imputNumber;
                    int power = i;

                    if (i > 0)
                    {
                        for (int j = 0; j < power - 1; j++)
                            multiplyResult = multiplyResult.Multiply(imputNumber);

                        coefficient = coefficient.Multiply(multiplyResult);
                    }

                    finalResult = finalResult.Add(coefficient);
                }

                return finalResult;
            }

            public override string ToString()
            {
                string output = "";
                for (int i = 0; i < Coefficients.Count; i++)
                {
                    output += Coefficients[i];
                    if (i > 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            output += "x";
                        }
                    }
                    output += " + ";
                }
                return output;
            }
        }
    }
}