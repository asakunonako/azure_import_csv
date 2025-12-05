using azure_import_csv;
using Microsoft.Identity.Client;

namespace TestProject.Infra.Config;

[TestClass]
public class AppConfigTests
{
    [TestMethod]
    public void ConnectionTest()
    {
        //var target = new azure_import_csv.Infra.Config.AppConfig();
        var target = azure_import_csv.Infra.Config.AppConfig.Instance;
        Assert.AreEqual(target.BlobConnectionString, "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1");
    }
}
