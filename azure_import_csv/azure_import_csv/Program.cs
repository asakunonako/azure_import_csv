
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using azure_import_csv;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

class Program
{
    /// <summary>
    /// CSVデータ反映処理をメインメソッドで実行します。
    /// </summary>
    /// <param name="args">コマンドライン引数</param>
    /// <returns>非同期処理</returns>
    public static async Task Main(string[] args)
    {
        Execute testMain = new Execute();
        await testMain.exe();
    }
}
