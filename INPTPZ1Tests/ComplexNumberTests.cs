using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace INPTPZ1.Mathematics.Tests
{
    [TestClass()]
    public class ComplexNumberTests
    {

        [TestMethod()]
        public void AddTest()
        {
            ComplexNumber numberOne = new ComplexNumber()
            {
                Real = 10,
                Imaginary = 20
            };
            ComplexNumber numberTwo = new ComplexNumber()
            {
                Real = 1,
                Imaginary = 2
            };

            ComplexNumber actual = numberOne.Add(numberTwo);
            ComplexNumber shouldBe = new ComplexNumber()
            {
                Real = 11,
                Imaginary = 22
            };

            Assert.AreEqual(shouldBe, actual);
        }
    }
}


