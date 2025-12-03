using Azure.Storage.Blobs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Configuration;

namespace azure_import_csv
{
    public class Execute
    {
        List<Restaurant> restaurantList_1 = new List<Restaurant>();
        Log log = new Log();

        /// <summary>
        /// CSVデータ反映処理を実行します。
        /// </summary>
        /// <returns>非同期処理</returns>
        public async Task exe()
        {
            // Azure Storageへ接続
            BlobConnection blobconnection = new BlobConnection();
            blobconnection.Connection();

            // 以下の内容で処理ログを記載する
            string logStartFileName = $"startlog_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";
            string logStartContent = System.Configuration.ConfigurationManager.AppSettings["StartLog"];
            await log.AppendLog(logStartFileName, logStartContent);
            Console.WriteLine(logStartContent);

            // uploadコンテナより、CSVファイルを取得する
            List<string> restaurantFiles = new List<string>();

            var pattern = @"^RESTAURANT_\d{14}\.csv$";
            await foreach (var blobItem in blobconnection.uploadContainer.GetBlobsAsync())
            {
                if (Regex.IsMatch(blobItem.Name, pattern))
                {
                    restaurantFiles.Add(blobItem.Name);
                }
            }


            // 飲食店CSVファイルが存在する限り、繰り返し行う
            // 飲食店データCSVファイルのデータを1行ずつ読み込み、以下の項目チェックを行う
            foreach (var file in restaurantFiles)
            {
                bool existError = false;
                BlobClient blobClient = blobconnection.uploadContainer.GetBlobClient(file);

                var downloadInfo = await blobClient.DownloadAsync();
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                using (var reader = new StreamReader(downloadInfo.Value.Content, Encoding.GetEncoding("Shift-JIS")))
                {
                    // エラーの行数を格納するためのリスト
                    List<int> errorLines = new List<int>();
                    int csvLineNumber = 0;

                    while (!reader.EndOfStream)
                    {
                        Restaurant restaurant = new Restaurant();
                        csvLineNumber++;
                        string? line = reader.ReadLine();
                        string[] values = line.Split(',');
                        List<string> lists = new List<string>(values);

                        var check = new CsvCheck();
                        (restaurant, existError) = check.check(lists);

                        if (existError == false)
                        {
                            restaurantList_1.Add(restaurant);
                        }
                        else
                        {
                            // エラー行番号を記録
                            errorLines.Add(csvLineNumber);
                        }
                    }

                    // NGの場合
                    if (errorLines.Count > 0)
                    {
                        // エラーが発生した場合はエラーコンテナへ移動し、処理終了
                        BlobClient sourceBlob = blobconnection.uploadContainer.GetBlobClient(file);
                        BlobClient errorBlob = blobconnection.errorContainer.GetBlobClient(file);

                        if (await sourceBlob.ExistsAsync())
                        {
                            await errorBlob.StartCopyFromUriAsync(sourceBlob.Uri);
                        }

                        await sourceBlob.DeleteIfExistsAsync();

                        string errorFileName = $"{Path.GetFileNameWithoutExtension(file)}.txt";
                        string logErrorFileName = $"errorlog_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";

                        // 1行ずつログ出力
                        foreach (var lineNo in errorLines)
                        {
                            // ログ出力
                            string errorConfig = System.Configuration.ConfigurationManager.AppSettings["ErrorLog"];
                            string logErrorContent = string.Format(errorConfig, errorFileName, lineNo);
                            await log.AppendLog(logErrorFileName, logErrorContent);
                            Console.WriteLine(logErrorContent);
                        }
                        string logErrorEndContent = System.Configuration.ConfigurationManager.AppSettings["EndLog"];
                        Console.WriteLine(logErrorEndContent);
                        continue; // 次のファイルの処理に移る   
                    }

                    // 正常処理
                    DbRegist dbRegist = new DbRegist();
                    await dbRegist.regist(restaurantList_1);

                    BlobClient uploadBlob = blobconnection.uploadContainer.GetBlobClient(file);
                    BlobClient backupBlob = blobconnection.backupContainer.GetBlobClient(file);

                    if (await uploadBlob.ExistsAsync())
                    {
                        await backupBlob.StartCopyFromUriAsync(uploadBlob.Uri);
                    }

                    await uploadBlob.DeleteIfExistsAsync();
                    // restaurantFilesリストの中身を削除
                    restaurantList_1.Clear();
                }
            }
                // 以下の内容で処理ログを記載する
                string logEndFileName = $"endlog_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";
            string logEndContent = ConfigurationManager.AppSettings["EndLog"];
            await log.AppendLog(logEndFileName, logEndContent);
            Console.WriteLine(logEndContent);
        }
    }
}