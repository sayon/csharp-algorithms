using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithmis.Numbers;
namespace tester
{
    [TestClass]
    public class LongNumberTest
    {
        [TestMethod]
        public void TestCreation()
        {
            BigInt num = new BigInt(33);
            Assert.AreEqual("100001", num.ToString());
        }

        [TestMethod]
        public void TestAddition()
        {
            BigInt num = new BigInt(33);
            BigInt num1 = new BigInt(33);
            var num2 = num + num1;
            Assert.AreEqual("1000010", num2.ToString());
        }
    }
}
