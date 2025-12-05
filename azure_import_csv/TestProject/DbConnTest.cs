using azure_import_csv.Infra.Database;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Data;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject;

[TestClass]
public class DbConnTest
{
    [TestMethod]
    public void DbConnTest1()
    {
        var target = DbConn.Instance;
        Assert.AreEqual(target.DbConnectionString, "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    [TestMethod]
    public void DbConnTest2()
    {
        var target = DbConn.Instance;
        var state = target.Connection!.State;
        Assert.AreEqual(ConnectionState.Open, state, "OpenÇ≈ÇÕÇ»Ç¢ÅB");
    }
}

