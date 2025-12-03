using azure_import_csv;

namespace TestProject
{
    [TestClass]
    public sealed class CsvCheckTest
    {
        [TestMethod]
        public void checkTest1()
        {
            CsvCheck item = new CsvCheck();
            Restaurant obj = new Restaurant();
            bool result;

            List<string> objList = new List<string>();
            objList.Add("0000000001");
            objList.Add("スシロー");
            objList.Add("0000000001");
            objList.Add("0000000001");
            objList.Add("530-0013");
            objList.Add("大阪市北区茶屋町2-16 イースクエア茶屋町 6階");
            objList.Add("09000000000");
            objList.Add("11:00～23:00");
            objList.Add("True");
            objList.Add("True");
            objList.Add("True");
            objList.Add("True");
            objList.Add("True");
            objList.Add("True");
            objList.Add("True");
            (obj, result) = item.check(objList);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void checkTest2()
        {
            CsvCheck item = new CsvCheck();
            Restaurant obj = new Restaurant();
            bool result;

            List<string> objList = new List<string>();
            objList.Add("abcdefghijk");
            objList.Add("aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxyyyzzz");
            objList.Add("abcdefghijk");
            objList.Add("abcdefghijk");
            objList.Add("a");
            objList.Add("aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxyyyzzz");
            objList.Add("abcdefghijklmnopqrstu");
            objList.Add("abcdefghijklmnopqrstu");
            objList.Add("a");
            objList.Add("a");
            objList.Add("a");
            objList.Add("a");
            objList.Add("a");
            objList.Add("a");
            objList.Add("a");
            (obj, result) = item.check(objList);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNullOrEmptyCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.IsNullOrEmptyCheck("テスト");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsNullOrEmptyCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.IsNullOrEmptyCheck(null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RestaurantIdCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.RestaurantIdCheck("テスト");
            Assert.IsTrue(result);
        }

        public void RestaurantIdCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.RestaurantIdCheck("abcdefghijk");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RestaurantNameCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.RestaurantNameCheck("テスト");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RestaurantNameCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.RestaurantNameCheck("aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxyyyzzz");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GenreIdCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.GenreIdCheck("テスト");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GenreIdCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.GenreIdCheck("abcdefghijk");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void StationIdCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.StationIdCheck("テスト");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StationIdCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.StationIdCheck("abcdefghijk");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PostCodeCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.PostCodeCheck("111-1111");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PostCodeCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.PostCodeCheck("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AddressCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.AddressCheck("テスト");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AddressCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.AddressCheck("aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxyyyzzz");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void TelCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.TelCheck("090-0000-0000");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TelCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.TelCheck("abcdefghijklmnopqrstu");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void BusinessHoursCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.BusinessHoursCheck("テスト");
            Assert.IsTrue(result);
        }

        public void BusinessHoursCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.BusinessHoursCheck("abcdefghijklmnopqrstu");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DayOfWeekCheckTest1()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.DayOfWeekCheck("True");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DayOfWeekCheckTest2()
        {
            CsvCheck csvcheck = new CsvCheck();
            bool result = CsvCheck.DayOfWeekCheck("a");
            Assert.IsFalse(result);
        }

    }
}
