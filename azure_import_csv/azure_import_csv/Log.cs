using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azure_import_csv
{
    public class Log
    {
        /// <summary>
        /// ログの設定と追記を行います。
        /// </summary>
        /// <param name="fileName">CSVファイル名を格納した文字列</param>
        /// <param name="message">ログに出力する内容を格納した文字列</param>
        /// <returns>非同期処理</returns>
        // 処理開始ログ
        public async Task AppendLog(string fileName, string message)
        {
            BlobConnection blobconnection = new BlobConnection();
            blobconnection.Connection();


            BlobContainerClient logStartContainer = blobconnection.logContainer;
            BlobClient logStartBlob = logStartContainer.GetBlobClient(fileName);


            // Shift-JISでエンコードする部分
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var encodingStart = System.Text.Encoding.GetEncoding("Shift_JIS");

            // 既存ログを取得（あれば）
            string existingContent = "";
            if (await logStartBlob.ExistsAsync())
            {
                var downloadResponse = await logStartBlob.DownloadContentAsync();
                existingContent = encodingStart.GetString(downloadResponse.Value.Content.ToArray());
            }

            // 新しいメッセージを追記

            string newContent = string.IsNullOrEmpty(existingContent)
                ? message
                : existingContent + Environment.NewLine + message;

            byte[] logStartBytes = encodingStart.GetBytes(newContent);

            using (var stream = new MemoryStream(logStartBytes))
            {
                await logStartBlob.UploadAsync(stream, overwrite: true);
            }

        }
    }
}
