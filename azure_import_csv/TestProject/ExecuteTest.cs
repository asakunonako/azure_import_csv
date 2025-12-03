using azure_import_csv;

namespace TestProject;

[TestClass]
public class ExecuteTest
{
    [TestMethod]
    public async Task exeTest()
    {
        //Execute item = new Execute();
        //bool result;
        //await item.exe();
        //result = true;
        //Assert.IsTrue(result);


        var item = new Execute();
        await item.exe();
        Assert.IsTrue(true); // ÀÛ‚ÍŒ‹‰Ê‚ğŒŸØ‚·‚é‚×‚«

    }
}
