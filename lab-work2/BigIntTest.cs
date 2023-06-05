using lab_work2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LabWork1
{
    [TestClass]
    public class BigIntTest
    {
        [TestMethod]
        public void FromString()
        {
            var bigInt = new BigInt("102");

            Assert.AreEqual("+102", bigInt.ToString());
        }

        [TestMethod]
        public void DeleteNuls()
        {
            var bigInt = new BigInt("00102");

            Assert.AreEqual("+102", bigInt.ToString());
        }

        [TestMethod]
        [TestCategory("addition")]
        public void BigIntSumPositive()
        {
            var bigInt1 = new BigInt("00102");
            var bigInt2 = new BigInt("00109");

            var sum = bigInt1 + bigInt2;
            Assert.AreEqual("+211", sum.ToString());
        }
        
        [TestMethod]
        [TestCategory("addition")]
        public void BigIntSumPositiveWithNegative()
        {
            var bigInt1 = new BigInt("00102");
            var bigInt2 = new BigInt("-00109");

            var sum = bigInt1 + bigInt2;
            Assert.AreEqual("-7", sum.ToString());
        }
        
        [TestMethod]
        [TestCategory("addition")]
        public void BigIntSumNegatives()
        {
            var bigInt1 = new BigInt("-00102");
            var bigInt2 = new BigInt("-00109");

            var sum = bigInt1 + bigInt2;
            Assert.AreEqual("-211", sum.ToString());
        }
        
        
        [TestMethod]
        [TestCategory("Reduce")]
        public void BigIntReducePositives()
        {
            var bigInt1 = new BigInt("00102");
            var bigInt2 = new BigInt("00109");
            
            var bigInt3 = new BigInt("37812930162371623781924617294");
            var bigInt4 = new BigInt("33753333473491848219047819274");


            var sum = bigInt2 - bigInt1;
            var sum2 = bigInt3 - bigInt4;
            Assert.AreEqual("+7", sum.ToString());
            Assert.AreEqual("+4059596688879775562876798020", sum2.ToString());
        }
        
        [TestMethod]
        [TestCategory("Reduce")]
        public void BigIntReducePositiveWithNegative()
        {
            var bigInt1 = new BigInt("-00102");
            var bigInt2 = new BigInt("00109");
            var bigInt3 = new BigInt("10");

            var res = bigInt2 - bigInt1;
            var res2 = bigInt3 - bigInt2;
            Assert.AreEqual("+211", res.ToString());
            Assert.AreEqual("-99", res2.ToString());
        }
        
        [TestMethod]
        [TestCategory("Reduce")]
        public void BigIntReduceNegatives()
        {
            var bigInt1 = new BigInt("-00102");
            var bigInt2 = new BigInt("-00109");

            var sum = bigInt2 - bigInt1;
            Assert.AreEqual("-7", sum.ToString());
        }
        
        [TestMethod]
        public void BigReduceAndAdiotionTest()
        {
            var d = new BigInt("+12345678");
            var e = new BigInt("+8765432");
            var f = new BigInt("-12345");

            var t1 = d + e;//21111110
            var t2 = d - e;//3580246
            var t3 = d + f;//12333333
            var t4 = d - f;//12358023
            var t5 = e - d;//-3580246
            var t6 = e - f;//8777777
            var t7 = f - e;//-8777777
            Assert.AreEqual("+21111110",t1.ToString());
            Assert.AreEqual("+3580246", t2.ToString());
            Assert.AreEqual("+12333333",t3.ToString());
            Assert.AreEqual("+12358023",t4.ToString());
            Assert.AreEqual("-3580246", t5.ToString());
            Assert.AreEqual("+8777777", t6.ToString());
            Assert.AreEqual("-8777777", t7.ToString());
        }
        
        [TestMethod]
        [TestCategory("Multiple")]
        public void BigIntMultiplePositives()
        {
            var bigInt1 = new BigInt("002");
            var bigInt2 = new BigInt("000");

            var res = bigInt2 * bigInt1;
            Assert.AreEqual("+0", res.ToString());
        }

        [TestMethod]
        
        
        [TestCategory("Multiple")]
        public void BigIntMultiplePositiveWithNegative()
        {
            var bigInt1 = new BigInt("002");
            var bigInt2 = new BigInt("-009");

            var res = bigInt2 * bigInt1;
            Assert.AreEqual("-18", res.ToString());
        }
        
        [TestMethod]
        [TestCategory("Multiple")]
        public void BigIntMultipleNegatives()
        {
            var bigInt1 = new BigInt("-002");
            var bigInt2 = new BigInt("-009");

            var bigInt3 = new BigInt("9223372036854775807");

            var res = bigInt2 * bigInt1;
            Assert.AreEqual("+18", res.ToString());
            Assert.AreEqual("+18446744073709551614", (bigInt3 * new BigInt("2")).ToString());
        }

        [TestMethod]
        [TestCategory("Div")]
        public void BigIntDiv()
        {
            var BigInt1 = new BigInt("10");
            var BigInt2 = new BigInt("2");
            var BigInt3 = new BigInt("3");

            var BigInt4 = new BigInt("37812930162371623781924617294");
            var BigInt5 = new BigInt("48219047819274");

            Assert.AreEqual("+5", (BigInt1 / BigInt2).ToString());
            Assert.AreEqual("+3", (BigInt1 / BigInt3).ToString());
            Assert.AreEqual("+784190726952868", (BigInt4 / BigInt5).ToString());
        }
        
        [TestMethod]
        [TestCategory("Div")]
        public void BigIntMod()
        {
            var BigInt1 = new BigInt("10");
            var BigInt2 = new BigInt("6");
            var BigInt3 = new BigInt("3");
            
            var BigInt4 = new BigInt("37812930162371623781924617294");
            var BigInt5 = new BigInt("48219047819274");
            
            Assert.AreEqual("+4", (BigInt1 % BigInt2).ToString());
            Assert.AreEqual("+1", (BigInt1 % BigInt3).ToString());
            Assert.AreEqual("+41271544639462", (BigInt4 % BigInt5).ToString());
        }   
        

        [TestMethod]
        [TestCategory("Pow")]
        public void BigIntPowPositive()
        {
            var bigInt1 = new BigInt("002");
            var bigInt2 = new BigInt("009");


            var res = bigInt1.Pow(bigInt2);
            Assert.AreEqual("+512", res.ToString());
        }
        
        [TestMethod]
        [TestCategory("Pow")]
        public void BigIntPowNegative()
        {
            var bigInt1 = new BigInt("-002");
            var bigInt2 = new BigInt("009");
            var bigInt3 = new BigInt("008");


            var res = bigInt1.Pow(bigInt2);
            var res2 = bigInt1.Pow(bigInt3);
            Assert.AreEqual("-512", res.ToString());
            Assert.AreEqual("+256", res2.ToString());
        }
        
        [TestMethod]
        [TestCategory("Compare")]
        public void BigIntCompare()
        {
            var bigInt1 = new BigInt("-0012");
            var bigInt2 = new BigInt("008");
            var bigInt3 = new BigInt("009");

            Assert.IsFalse(bigInt3 < bigInt2);
            Assert.IsFalse(bigInt3 <= bigInt2);
            Assert.IsFalse(bigInt3 < bigInt3);
            Assert.IsFalse(bigInt3 < bigInt1);
            Assert.IsFalse(bigInt3 <= bigInt1);

            Assert.IsTrue(bigInt1 < bigInt3);
            Assert.IsTrue(bigInt2 < bigInt3);
            Assert.IsTrue(bigInt1 <= bigInt1);


            Assert.IsFalse(bigInt3 > bigInt3);
            Assert.IsFalse(bigInt1 > bigInt2);
            Assert.IsFalse(bigInt2 > bigInt3);
            Assert.IsFalse(bigInt2 >= bigInt3);

            Assert.IsTrue(bigInt2 >= bigInt2);
            Assert.IsTrue(bigInt2 > bigInt1);
            Assert.IsTrue(bigInt3 > bigInt2);

            Assert.IsFalse(bigInt2 == bigInt3);
            Assert.IsTrue(bigInt2 == bigInt2);
            
            Assert.IsFalse(bigInt2 != bigInt2);
            Assert.IsTrue(bigInt2 != bigInt3);
        }   
        
        [TestMethod]
        [TestCategory("isSimple")]
        public void BigIntIsSimple()
        {
            Assert.IsFalse(new BigInt("008").IsSimple());
            Assert.IsFalse(new BigInt("009").IsSimple());
            Assert.IsFalse(new BigInt("25").IsSimple());
            Assert.IsFalse(new BigInt("125").IsSimple());
            Assert.IsFalse(new BigInt("63271836192352").IsSimple());
            
            Assert.IsTrue(new BigInt("2").IsSimple());
            Assert.IsTrue(new BigInt("3").IsSimple());
            Assert.IsTrue(new BigInt("5").IsSimple());
            Assert.IsTrue(new BigInt("7").IsSimple());
            Assert.IsTrue(new BigInt("11").IsSimple());
            Assert.IsTrue(new BigInt("13").IsSimple());
            Assert.IsTrue(new BigInt("17").IsSimple());
            Assert.IsTrue(new BigInt("19").IsSimple());
        }
    }
}
