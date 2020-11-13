using Microsoft.VisualStudio.TestTools.UnitTesting;
using AssemblyScanner;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void LoadAssemblyTestMethod()
        {
            Scanner scaner = new Scanner();
            scaner.AssemblyLoad("");
        }
        [TestMethod]
        public void AssemblyScanTestMethod()
        {
            Scanner scaner = new Scanner();
            scaner.AssemblyLoad("D:\\Bsuir\\SPP\\CCP_Lab_1_Tracer\\CCP_Lab_1_Tracer\\bin\\Debug\\netcoreapp3.1\\CCP_Lab_1_Tracer.dll");
            var inf = scaner.AssemblyScan();
            var infoCel = new InfoCell();
            Assert.AreEqual(infoCel.GetType(), inf.GetType());
        }
        [TestMethod]
        public void AssemblyScanTestMethod2()
        {
            Scanner scaner = new Scanner();
            scaner.AssemblyLoad("");
            scaner.AssemblyScan();
        }
    }
}
