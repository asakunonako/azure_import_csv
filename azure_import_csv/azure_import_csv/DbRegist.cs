using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;

namespace azure_import_csv
{
    public class DbRegist
    {
        Log log = new Log();

        public async Task regist(List<Restaurant> restaurantList_2)
        {
            // OKの場合、DBへの接続開始
            // 接続文字列を貼り付ける
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionDb"];
            // 実行するSQL
            var sql = "INSERT INTO restaurant VALUES(@restaurant_id,@restaurant_name, @genre_id, @station_id, @post_code, @address, @tel, @business_hours, @monday, @tuesday, @wednesday, @thursday, @friday, @saturday, @sunday)";

            // Azure Storageへ接続
            BlobConnection blobconnection = new BlobConnection();
            blobconnection.Connection();


            // ファイル名を取得（uploadコンテナから最初の1件）
            string fileName = "";
            await foreach (BlobItem blobItem in blobconnection.uploadContainer.GetBlobsAsync())
            {
                fileName = blobItem.Name;
                break; // 1件だけ処理する場合
            }

            using (var connection = new SqlConnection(connectionString))
            {
                // DBへの接続開始
                // コネクションをオープンします。
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;
                    command.CommandText = sql; // INSERT文を設定

                    try
                    {

                        foreach (var restaurant_info in restaurantList_2)
                        {
                            // データの登録
                            // チェックしたデータの各項目をT_飲食店テーブルに登録する
                            // データ登録のSQLを実行します。
                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@restaurant_id", restaurant_info.RestaurantId));
                            command.Parameters.Add(new SqlParameter("@restaurant_name", restaurant_info.RestaurantName));
                            command.Parameters.Add(new SqlParameter("@genre_id", restaurant_info.GenreId));
                            command.Parameters.Add(new SqlParameter("@station_id", restaurant_info.StationId));
                            command.Parameters.Add(new SqlParameter("@post_code", restaurant_info.PostCode));
                            command.Parameters.Add(new SqlParameter("@address", restaurant_info.Address));
                            command.Parameters.Add(new SqlParameter("@tel", restaurant_info.Tel));
                            command.Parameters.Add(new SqlParameter("@business_hours", restaurant_info.BusinessHours));
                            command.Parameters.Add(new SqlParameter("@monday", restaurant_info.Monday));
                            command.Parameters.Add(new SqlParameter("@tuesday", restaurant_info.Tuesday));
                            command.Parameters.Add(new SqlParameter("@wednesday", restaurant_info.Wednesday));
                            command.Parameters.Add(new SqlParameter("@thursday", restaurant_info.Thursday));
                            command.Parameters.Add(new SqlParameter("@friday", restaurant_info.Friday));
                            command.Parameters.Add(new SqlParameter("@saturday", restaurant_info.Saturday));
                            command.Parameters.Add(new SqlParameter("@sunday", restaurant_info.Sunday));
                            command.ExecuteNonQuery();

                        }
                        transaction.Commit(); // 成功したらコミット
                    }
                    catch (Exception ex)
                    {
                        // 以降、何らかのエラーが発生した際はロールバックし、DBへの接続を解除する
                        transaction.Rollback();


                        // エラー時にファイルを error コンテナへ移動
                        BlobClient sourceBlob = blobconnection.uploadContainer.GetBlobClient(fileName);
                        BlobClient errorBlob = blobconnection.errorContainer.GetBlobClient(fileName);

                        if (await sourceBlob.ExistsAsync())
                        {
                            await errorBlob.StartCopyFromUriAsync(sourceBlob.Uri);
                        }

                        await sourceBlob.DeleteIfExistsAsync();

                        // また以下の内容で処理ログを記載する
                        string logExceptionFileName = $"exceptionlog_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";
                        string ExceptionConfig = System.Configuration.ConfigurationManager.AppSettings["ExceptionLog"];
                        string logExceptionContent = string.Format(ExceptionConfig, ex.StackTrace);
                        await log.AppendLog(logExceptionFileName, logExceptionContent);
                        Console.WriteLine(logExceptionContent);

                    }
                }
            }
        }
    }
}
