using azure_import_csv;

namespace TestProject;

[TestClass]
public class LogTest
{
    [TestMethod]
    public void LogSettingTest()
    {
        Log item = new Log();
        bool result;
        item.AppendLog("test.txt","ƒeƒXƒg‚Å‚·");
        result = true;
        Assert.IsTrue(result);

    }
}
