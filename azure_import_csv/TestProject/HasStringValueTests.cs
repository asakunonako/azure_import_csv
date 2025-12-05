using azure_import_csv;

namespace TestProject
{
    [TestClass]
    public sealed class HasStringValueTests
    {
        [TestMethod]
        public void IsNullOrEmptyCheckTest1()
        {

            bool result = azure_import_csv.Models.HasStringValue.IsNullOrEmptyCheck("テスト");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNullOrEmptyCheckTest2()
        {
            bool result = azure_import_csv.Models.HasStringValue.IsNullOrEmptyCheck(null);
            Assert.IsFalse(result);
        }
    }
}
