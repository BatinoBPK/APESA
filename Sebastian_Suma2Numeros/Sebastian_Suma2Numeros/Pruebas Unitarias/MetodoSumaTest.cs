﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sebastian_Suma2Numeros.Controllers;

namespace Sebastian_Suma2Numeros.Pruebas_Unitarias
{
    [TestClass]
    public class MetodoSumaTest
    {
        [TestMethod]
        public void TestSumar_PositiveNumbers()
        {
            var controller = new SebastianApesaController();
            int num1 = 1;
            int num2 = 2;

            int result = controller.Sumar(num1, num2);

            Assert.AreEqual(3, result, "La suma de 1 y 2 debe ser igual a 3.");
        }

        [TestMethod]
        public void TestSumar_NegativeAndPositiveNumbers()
        {
            var controller = new SebastianApesaController();
            int num1 = -1;
            int num2 = 2;

            int result = controller.Sumar(num1, num2);

            Assert.AreEqual(1, result, "La suma de -1 y 2 debe ser igual a 1.");
        }
    }
}
