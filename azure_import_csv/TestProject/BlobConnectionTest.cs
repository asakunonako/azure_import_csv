using azure_import_csv;
using Microsoft.Identity.Client;

namespace TestProject;

[TestClass]
public class BlobConnectionTest
{
    [TestMethod]
    public void ConnectionTest()
    {
        BlobConnection item = new BlobConnection();
        bool ret;
        item.Connection();
        ret = true;
        Assert.IsTrue(ret);
    }


    [TestMethod, Priority(1000)]
    public void BlobÇ÷ÇÃê⁄ë±OPEN_Test()
    {
        var blobConnection = new BlobConnection();
        Assert.AreEqual(blobConnection.GetConfigSettingAndCanOpenConnect(), true);
    }




    //[TestMethod, Priority(2000)]
    //public void AppConfigTest()
    //{
    //    azure_import_csv.AppConfig appconfig = new ();
    //    Assert.IsNotNull(appconfig.BlobConnString);
    //}
}
