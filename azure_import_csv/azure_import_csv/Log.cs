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
