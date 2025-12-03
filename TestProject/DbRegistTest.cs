using azure_import_csv;
using System.Net;

namespace TestProject;

[TestClass]
public class DbRegistTest
{
    [TestMethod]
    public void registTest1()
    {
        DbRegist item = new DbRegist();
       
        List<Restaurant> objList = new List <Restaurant>();

        Restaurant obj = new Restaurant();

        bool result;

        obj.RestaurantId = "0000000001";
        obj.RestaurantName = "スシロー";
        obj.GenreId = "0000000001";
        obj.StationId = "0000000001";
        obj.PostCode = "530-0013";
        obj.Address = "大阪市北区茶屋町2-16 イースクエア茶屋町 6階";
        obj.Tel = "09000000000";
        obj.BusinessHours = "11:00～23:00";
        obj.Monday = "True";
        obj.Tuesday = "True";
        obj.Wednesday = "True";
        obj.Thursday = "True";
        obj.Friday = "True";
        obj.Saturday = "True";
        obj.Sunday = "True";

        objList.Add(obj);
        item.regist(objList);
        result = true;

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void registTest2()
    {
        DbRegist item = new DbRegist();

        List<Restaurant> objList = new List<Restaurant>();

        Restaurant obj = new Restaurant();

        bool result;

        obj.RestaurantId = "abcdefghijk";
        obj.RestaurantName = "aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxyyyzzz";
        obj.GenreId = "abcdefghijk";
        obj.StationId = "abcdefghijk";
        obj.PostCode = "a";
        obj.Address ="aaaabbbbccccddddeeeeffffgggghhhhiiiijjjjkkkkllllmmmmnnnnooooppppqqqqrrrrssssttttuuuuvvvvwwwwxxxyyyzzz";
        obj.Tel ="abcdefghijklmnopqrstu";
        obj.BusinessHours = "abcdefghijklmnopqrstu";
        obj.Monday ="a";
        obj.Tuesday = "a";
        obj.Wednesday = "a";
        obj.Thursday =  "a";
        obj.Friday = "a";
        obj.Saturday = "a";
        obj.Friday = "a";

        objList.Add(obj);
        item.regist(objList);
        result = false;

        Assert.IsFalse(result);
    }
}
