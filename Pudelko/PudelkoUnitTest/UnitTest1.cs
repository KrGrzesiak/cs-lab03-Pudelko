using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System;
using PudelkoLib;


namespace PudelkoUnitTests
{

    [TestClass]
    public static class InitializeCulture
    {
        [AssemblyInitialize]
        public static void SetEnglishCultureOnAllUnitTest(TestContext context)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
        }
    }

    // ========================================

    [TestClass]
    public class UnitTestsPudelkoConstructors
    {
        private static double defaultSize = 0.1; // w metrach
        private static double accuracy = 0.001; //dok³adnoœæ 3 miejsca po przecinku

        private void AssertPudelko(Pudelko p, double expectedA, double expectedB, double expectedC)
        {
            Assert.AreEqual(expectedA, p.A, delta: accuracy);
            Assert.AreEqual(expectedB, p.B, delta: accuracy);
            Assert.AreEqual(expectedC, p.C, delta: accuracy);
        }

        #region Constructor tests ================================

        [TestMethod, TestCategory("Constructors")]
        public void Constructor_Default()
        {
            Pudelko p = new Pudelko();

            Assert.AreEqual(defaultSize, p.A, delta: accuracy);
            Assert.AreEqual(defaultSize, p.B, delta: accuracy);
            Assert.AreEqual(defaultSize, p.C, delta: accuracy);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów licz¹ siê 3 miejsca po przecinku
        public void Constructor_3params_DefaultMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.543, 3.1,
                 1.0, 2.543, 3.1)]
        [DataRow(1.0001, 2.54387, 3.1005,
                 1.0, 2.543, 3.1)] // dla metrów licz¹ siê 3 miejsca po przecinku
        public void Constructor_3params_InMeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100.0, 25.5, 3.1,
                 1.0, 0.255, 0.031)]
        [DataRow(100.0, 25.58, 3.13,
                 1.0, 0.255, 0.031)] // dla centymertów liczy siê tylko 1 miejsce po przecinku
        public void Constructor_3params_InCentimeters(double a, double b, double c,
                                                      double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(a: a, b: b, c: c, unit: UnitOfMeasure.centimeter);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(100, 255, 3,
                 0.1, 0.255, 0.003)]
        [DataRow(100.0, 25.58, 3.13,
                 0.1, 0.025, 0.003)] // dla milimetrów nie licz¹ siê miejsca po przecinku
        public void Constructor_3params_InMilimeters(double a, double b, double c,
                                                     double expectedA, double expectedB, double expectedC)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b, c: c);

            AssertPudelko(p, expectedA, expectedB, expectedC);
        }


        // ----

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_DefaultMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a, b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(1.0, 2.5, 1.0, 2.5)]
        [DataRow(1.001, 2.599, 1.001, 2.599)]
        [DataRow(1.0019, 2.5999, 1.001, 2.599)]
        public void Constructor_2params_InMeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(a: a, b: b, unit: UnitOfMeasure.meter);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 2.5, 0.11, 0.025)]
        [DataRow(100.1, 2.599, 1.001, 0.025)]
        [DataRow(2.0019, 0.25999, 0.02, 0.002)]
        public void Constructor_2params_InCentimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 2.0, 0.011, 0.002)]
        [DataRow(100.1, 2599, 0.1, 2.599)]
        [DataRow(200.19, 2.5999, 0.2, 0.002)]
        public void Constructor_2params_InMilimeters(double a, double b, double expectedA, double expectedB)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a, b: b);

            AssertPudelko(p, expectedA, expectedB, expectedC: 0.1);
        }

        // -------

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_DefaultMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(2.5)]
        public void Constructor_1param_InMeters(double a)
        {
            Pudelko p = new Pudelko(a);

            Assert.AreEqual(a, p.A);
            Assert.AreEqual(0.1, p.B);
            Assert.AreEqual(0.1, p.C);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11.0, 0.11)]
        [DataRow(100.1, 1.001)]
        [DataRow(2.0019, 0.02)]
        public void Constructor_1param_InCentimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.centimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(11, 0.011)]
        [DataRow(100.1, 0.1)]
        [DataRow(200.19, 0.2)]
        public void Constructor_1param_InMilimeters(double a, double expectedA)
        {
            Pudelko p = new Pudelko(unit: UnitOfMeasure.milimeter, a: a);

            AssertPudelko(p, expectedA, expectedB: 0.1, expectedC: 0.1);
        }

        // ---

        public static IEnumerable<object[]> DataSet1Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5, 3.1},
            new object[] {1.0, -2.5, 3.1},
            new object[] {1.0, 2.5, -3.1},
            new object[] {-1.0, -2.5, 3.1},
            new object[] {-1.0, 2.5, -3.1},
            new object[] {1.0, -2.5, -3.1},
            new object[] {-1.0, -2.5, -3.1},
            new object[] {0, 2.5, 3.1},
            new object[] {1.0, 0, 3.1},
            new object[] {1.0, 2.5, 0},
            new object[] {1.0, 0, 0},
            new object[] {0, 2.5, 0},
            new object[] {0, 0, 3.1},
            new object[] {0, 0, 0},
            new object[] {10.1, 2.5, 3.1},
            new object[] {10, 10.1, 3.1},
            new object[] {10, 10, 10.1},
            new object[] {10.1, 10.1, 3.1},
            new object[] {10.1, 10, 10.1},
            new object[] {10, 10.1, 10.1},
            new object[] {10.1, 10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_DefaultMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet1Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.01, 0.1, 1)]
        [DataRow(0.1, 0.01, 1)]
        [DataRow(0.1, 0.1, 0.01)]
        [DataRow(1001, 1, 1)]
        [DataRow(1, 1001, 1)]
        [DataRow(1, 1, 1001)]
        [DataRow(1001, 1, 1001)]
        [DataRow(1, 1001, 1001)]
        [DataRow(1001, 1001, 1)]
        [DataRow(1001, 1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InCentimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.centimeter);
        }


        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1, 1)]
        [DataRow(1, -1, 1)]
        [DataRow(1, 1, -1)]
        [DataRow(-1, -1, 1)]
        [DataRow(-1, 1, -1)]
        [DataRow(1, -1, -1)]
        [DataRow(-1, -1, -1)]
        [DataRow(0, 1, 1)]
        [DataRow(1, 0, 1)]
        [DataRow(1, 1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(0, 1, 0)]
        [DataRow(1, 0, 0)]
        [DataRow(0, 0, 0)]
        [DataRow(0.1, 1, 1)]
        [DataRow(1, 0.1, 1)]
        [DataRow(1, 1, 0.1)]
        [DataRow(10001, 1, 1)]
        [DataRow(1, 10001, 1)]
        [DataRow(1, 1, 10001)]
        [DataRow(10001, 10001, 1)]
        [DataRow(10001, 1, 10001)]
        [DataRow(1, 10001, 10001)]
        [DataRow(10001, 10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_InMiliimeters_ArgumentOutOfRangeException(double a, double b, double c)
        {
            Pudelko p = new Pudelko(a, b, c, unit: UnitOfMeasure.milimeter);
        }


        public static IEnumerable<object[]> DataSet2Meters_ArgumentOutOfRangeEx => new List<object[]>
        {
            new object[] {-1.0, 2.5},
            new object[] {1.0, -2.5},
            new object[] {-1.0, -2.5},
            new object[] {0, 2.5},
            new object[] {1.0, 0},
            new object[] {0, 0},
            new object[] {10.1, 10},
            new object[] {10, 10.1},
            new object[] {10.1, 10.1}
        };

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_DefaultMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DynamicData(nameof(DataSet2Meters_ArgumentOutOfRangeEx))]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.01, 1)]
        [DataRow(1, 0.01)]
        [DataRow(0.01, 0.01)]
        [DataRow(1001, 1)]
        [DataRow(1, 1001)]
        [DataRow(1001, 1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InCentimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(-1, -1)]
        [DataRow(0, 1)]
        [DataRow(1, 0)]
        [DataRow(0, 0)]
        [DataRow(0.1, 1)]
        [DataRow(1, 0.1)]
        [DataRow(0.1, 0.1)]
        [DataRow(10001, 1)]
        [DataRow(1, 10001)]
        [DataRow(10001, 10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_2params_InMilimeters_ArgumentOutOfRangeException(double a, double b)
        {
            Pudelko p = new Pudelko(a, b, unit: UnitOfMeasure.milimeter);
        }




        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_DefaultMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(10.1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.meter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1.0)]
        [DataRow(0)]
        [DataRow(0.01)]
        [DataRow(1001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InCentimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.centimeter);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow(-1)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(10001)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_1param_InMilimeters_ArgumentOutOfRangeException(double a)
        {
            Pudelko p = new Pudelko(a, unit: UnitOfMeasure.milimeter);
        }

        #endregion



        #region ToString tests ===================================

        [TestMethod, TestCategory("String representation")]
        public void ToString_Default_Culture_EN()
        {
            var p = new Pudelko(2.5, 9.321);
            string expectedStringEN = "2.500 m × 9.321 m × 0.100 m";

            Assert.AreEqual(expectedStringEN, p.ToString());
        }

        [DataTestMethod, TestCategory("String representation")]
        [DataRow(null, 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("m", 2.5, 9.321, 0.1, "2.500 m × 9.321 m × 0.100 m")]
        [DataRow("cm", 2.5, 9.321, 0.1, "250.0 cm × 932.1 cm × 10.0 cm")]
        [DataRow("mm", 2.5, 9.321, 0.1, "2500 mm × 9321 mm × 100 mm")]
        public void ToString_Formattable_Culture_EN(string format, double a, double b, double c, string expectedStringRepresentation)
        {
            var p = new Pudelko(a, b, c, unit: UnitOfMeasure.meter);
            Assert.AreEqual(expectedStringRepresentation, p.ToString(format));
        }

        [TestMethod, TestCategory("String representation")]
        [ExpectedException(typeof(FormatException))]
        public void ToString_Formattable_WrongFormat_FormatException()
        {
            var p = new Pudelko(1);
            var stringformatedrepreentation = p.ToString("wrong code");
        }

        #endregion



        #region Pole, Objêtoœæ ===================================
        [TestMethod, TestCategory("Property \"Pole\"")]
        public void Property_Pole()
        {
            var p = new Pudelko(10, 7.5, 2.59, UnitOfMeasure.meter);
            double exceptedValue = 240.65;

            Assert.AreEqual(exceptedValue, p.Pole);
        }

        [TestMethod, TestCategory("Property \"Pole\"")]
        public void Property_Pole_NULL()
        {
            var p = new Pudelko();
            double exceptedValue = 0.06;

            Assert.AreEqual(exceptedValue, p.Pole);
        }

        [TestMethod, TestCategory("Property \"Pole\"")]
        [DataRow(0.5, 1.35, 0.40, UnitOfMeasure.meter, 2.83)]
        [DataRow(10.8, 15.1, 20, UnitOfMeasure.centimeter, 0.136216)]
        [DataRow(1450, 123, 5678, UnitOfMeasure.milimeter, 18.219688)]
        [DataRow(12, 56, 123, UnitOfMeasure.milimeter, 0.018072)]
        public void Property_Pole_for_different_units_of_mesure(double a, double b, double c, UnitOfMeasure unit, double expectedValue)
        {
            var p = new Pudelko(a, b, c, unit);

            Assert.AreEqual(expectedValue, p.Pole);
        }



        [TestMethod, TestCategory("Property \"Objetosc\"")]
        public void Property_Objetosc()
        {
            var p = new Pudelko(10, 7.5, 2.59, UnitOfMeasure.meter);
            double exceptedValue = 194.25;

            Assert.AreEqual(exceptedValue, p.Objetosc);
        }

        [TestMethod, TestCategory("Property \"Objetosc\"")]
        public void Property_Objetosc_NULL()
        {
            var p = new Pudelko();
            double exceptedValue = 0.001;

            Assert.AreEqual(exceptedValue, p.Objetosc);
        }

        [TestMethod, TestCategory("Property \"Objetosc\"")]
        [DataRow(0.5, 1.35, 0.40, UnitOfMeasure.meter, 0.27)]
        [DataRow(10.8, 15.1, 20.6, UnitOfMeasure.centimeter, 0.003359448)]
        [DataRow(145, 123, 56, UnitOfMeasure.milimeter, 0.00099876)]
        [DataRow(121, 562, 123, UnitOfMeasure.milimeter, 0.008364246)]
        public void Property_objetosc_for_different_units_of_mesure(double a, double b, double c, UnitOfMeasure unit, double expectedValue)
        {
            var p = new Pudelko(a, b, c, unit);

            Assert.AreEqual(expectedValue, p.Objetosc);
        }

        #endregion



        #region Equals ===========================================
        [TestMethod, TestCategory("Equal")]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, false, null, null, null, null)]
        [DataRow(null, null, null, null, true, null, null, null, null)]
        public void p1_is_Equal_to_NULL(double? a1, double? b1, double? c1, UnitOfMeasure unit1, bool exceptedValue, double? a2, double? b2, double? c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b2, c2, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            Assert.AreEqual(exceptedValue, p1.Equals(p2));
        }

        [TestMethod, TestCategory("Equal")]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, true, 1.3, 5.6, 2.2, UnitOfMeasure.meter)]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, false, 3.1, 5.6, 2.2, UnitOfMeasure.meter)]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, true, 2.2, 1.3, 5.6, UnitOfMeasure.meter)]
        [DataRow(2.2, 1.3, 5.6, UnitOfMeasure.meter, true, 2200, 1300, 5600, UnitOfMeasure.milimeter)]
        [DataRow(2.2, 1.3, 5.6, UnitOfMeasure.meter, true, 220.0, 130.0, 560.0, UnitOfMeasure.centimeter)]
        [DataRow(35.0, 12.0, 15.5, UnitOfMeasure.centimeter, true, 0.35, 0.12, 0.155, UnitOfMeasure.meter)]
        [DataRow(35.0, 12.0, 15.5, UnitOfMeasure.centimeter, true, 0.155, 0.12, 0.35, UnitOfMeasure.meter)]
        [DataRow(2033, 2033, 5050, UnitOfMeasure.milimeter, true, 2.033, 2.033, 5.05, UnitOfMeasure.meter)]
        [DataRow(2033, 5050, 2033, UnitOfMeasure.milimeter, true, 2.033, 2.033, 5.05, UnitOfMeasure.meter)]
        public void p1_is_Equal_to_p2(double a1, double b1, double c1, UnitOfMeasure unit1, bool exceptedValue, double a2, double b2, double c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            Assert.AreEqual(exceptedValue, p1.Equals(p2));
        }

        #endregion

        

        #region Operators overloading ===========================
        [TestMethod, TestCategory("Operators")]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, true, 1.3, 5.6, 2.2, UnitOfMeasure.meter)]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, false, 2.2, 9.2, 1.0, UnitOfMeasure.meter)]
        [DataRow(0.5, 1.01, 0.55, UnitOfMeasure.meter, true, 101, 50, 55, UnitOfMeasure.centimeter)]
        [DataRow(1.7, 0.77, 0.78, UnitOfMeasure.meter, true, 770, 780, 1700, UnitOfMeasure.milimeter)]
        public void Operatpr_Equal(double a1, double b1, double c1, UnitOfMeasure unit1, bool exceptedValue, double a2, double b2, double c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            Assert.AreEqual(exceptedValue, p1 == p2);
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow(null, null, null, null, true, null, null, null, null)]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, false, null, null, null, null)]
        public static void Operator_Equal_Null(double? a1, double? b1, double? c1, UnitOfMeasure unit1, bool exceptedValue, double? a2, double? b2, double? c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            Assert.AreEqual(exceptedValue, p1 == p2);
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, false, 1.3, 5.6, 2.2, UnitOfMeasure.meter)]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, true, 2.2, 9.2, 1.0, UnitOfMeasure.meter)]
        [DataRow(0.5, 1.01, 0.55, UnitOfMeasure.meter, false, 101, 50, 55, UnitOfMeasure.centimeter)]
        [DataRow(1.7, 0.77, 0.78, UnitOfMeasure.meter, false, 770, 780, 1700, UnitOfMeasure.milimeter)]
        public void Operatpr_Not_Equal(double a1, double b1, double c1, UnitOfMeasure unit1, bool exceptedValue, double a2, double b2, double c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            Assert.AreEqual(exceptedValue, p1 != p2);
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow(null, null, null, null, false, null, null, null, null)]
        [DataRow(1.3, 5.6, 2.2, UnitOfMeasure.meter, true, null, null, null, null)]
        public static void Operator_Not_Equal_Null(double? a1, double? b1, double? c1, UnitOfMeasure unit1, bool exceptedValue, double? a2, double? b2, double? c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            Assert.AreEqual(exceptedValue, p1 != p2);
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow(null, null, null, null, 0.008, null, null, null, null)]
        [DataRow(null, null, null, null, 0.616, 0.7, 0.6, 1, UnitOfMeasure.meter)]     
        public static void Operator_Plus_Null(double? a1, double? b1, double? c1, UnitOfMeasure unit1, double exceptedValue, double? a2, double? b2, double? c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            var pud = p1 + p2;

            Assert.AreEqual(exceptedValue, pud.Objetosc);
        }

        [TestMethod, TestCategory("Operators")]
        [DataRow(0.3, 0.3, 0.5, UnitOfMeasure.meter, 0.36, 0.5, 0.3, 0.3, UnitOfMeasure.meter)]
        [DataRow(0.3, 0.3, 0.5, UnitOfMeasure.meter, 0.36, 50, 30, 30, UnitOfMeasure.centimeter)]
        [DataRow(0.3, 0.3, 0.5, UnitOfMeasure.meter, 0.36, 500, 300, 300, UnitOfMeasure.milimeter)]
        public static void Operator_Plus(double a1, double b1, double c1, UnitOfMeasure unit1, double exceptedValue, double a2, double b2, double c2, UnitOfMeasure unit2)
        {
            var p1 = new Pudelko(a1, b1, c1, unit1);
            var p2 = new Pudelko(a2, b2, c2, unit2);

            var pud = p1 + p2;

            Assert.AreEqual(exceptedValue, pud.Objetosc);
        }

        #endregion

        #region Conversions =====================================
        [TestMethod]
        public void ExplicitConversion_ToDoubleArray_AsMeters()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            double[] tab = (double[])p;
            Assert.AreEqual(3, tab.Length);
            Assert.AreEqual(p.A, tab[0]);
            Assert.AreEqual(p.B, tab[1]);
            Assert.AreEqual(p.C, tab[2]);
        }

        [TestMethod]
        public void ImplicitConversion_FromAalueTuple_As_Pudelko_InMilimeters()
        {
            var (a, b, c) = (2500, 9321, 100); // in milimeters, ValueTuple
            Pudelko p = (a, b, c);
            Assert.AreEqual((int)(p.A * 1000), a);
            Assert.AreEqual((int)(p.B * 1000), b);
            Assert.AreEqual((int)(p.C * 1000), c);
        }

        #endregion

        #region Indexer, enumeration ============================
        [TestMethod]
        public void Indexer_ReadFrom()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            Assert.AreEqual(p.A, p[0]);
            Assert.AreEqual(p.B, p[1]);
            Assert.AreEqual(p.C, p[2]);
        }

        [TestMethod]
        public void ForEach_Test()
        {
            var p = new Pudelko(1, 2.1, 3.231);
            var tab = new[] { p.A, p.B, p.C };
            int i = 0;
            foreach (double x in p)
            {
                Assert.AreEqual(x, tab[i]);
                i++;
            }
        }

        #endregion

        #region Parsing =========================================

        #endregion

    }
}