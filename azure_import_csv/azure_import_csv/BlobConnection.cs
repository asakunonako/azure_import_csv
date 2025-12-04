using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace azure_import_csv
{
    public class BlobConnection
    {
        public BlobContainerClient uploadContainer { get; private set; }
        public BlobContainerClient logContainer { get; private set; }
        public BlobContainerClient errorContainer { get; private set; }
        public BlobContainerClient backupContainer { get; private set; }

        /// <summary>
        /// 設定ファイルの読み込みとAzureの接続を行います。
        /// </summary>
        /// <returns>接続が成功したかを示す真偽値</returns>
        public bool GetConfigSettingAndCanOpenConnect()
        {
            bool ret = false;

            try
            {
                var appconfig = new AppConfig();
                string uploadContainerName = System.Configuration.ConfigurationManager.AppSettings["UploadContainer"];
                string logContainerName = System.Configuration.ConfigurationManager.AppSettings["LogContainer"];
                string errorContainerName = System.Configuration.ConfigurationManager.AppSettings["ErrorContainer"];
                string backupContainerName = System.Configuration.ConfigurationManager.AppSettings["BackupContainerZZZ"];

                BlobServiceClient blobServiceClient = new BlobServiceClient(appconfig.BlobConnString);
                uploadContainer = blobServiceClient.GetBlobContainerClient(uploadContainerName);
                logContainer = blobServiceClient.GetBlobContainerClient(logContainerName);
                errorContainer = blobServiceClient.GetBlobContainerClient(errorContainerName);
                backupContainer = blobServiceClient.GetBlobContainerClient(backupContainerName);

                ret = true;
            }

            catch (Exception ex) when (
                ex is NullReferenceException ||
                ex is Azure.RequestFailedException ||
                ex is ConfigurationErrorsException ||
                ex is ArgumentNullException ||
                ex is FormatException)
            {
                Log log = new Log();
                // 以下の内容で処理ログを記載する
                string logEndFileName = $"endlog_{DateTime.UtcNow:yyyyMMdd_HHmmss}.txt";
                string logEndContent = "処理異常終了";
                log.AppendLog(logEndFileName, logEndContent);



                // ログメッセージを構築
                var sb = new StringBuilder();
                sb.AppendLine("=== Exception Occurred ===");
                sb.AppendLine($"Type       : {ex.GetType().FullName}");
                sb.AppendLine($"Message    : {ex.Message}");
                sb.AppendLine($"Source     : {ex.Source}");
                sb.AppendLine($"TargetSite : {ex.TargetSite}");
                sb.AppendLine($"StackTrace : {ex.StackTrace}");

                // Azure特有の情報
                if (ex is Azure.RequestFailedException rfe)
                {
                    sb.AppendLine("--- Azure Request Details ---");
                    sb.AppendLine($"Status     : {rfe.Status}");
                    sb.AppendLine($"ErrorCode  : {rfe.ErrorCode}");
                }

                // 内部例外がある場合
                if (ex.InnerException != null)
                {
                    sb.AppendLine("--- Inner Exception ---");
                    sb.AppendLine($"Type    : {ex.InnerException.GetType().FullName}");
                    sb.AppendLine($"Message : {ex.InnerException.Message}");
                }

                // 非同期メソッドを同期的に呼び出す
                log.AppendLog("ErrorLog.txt", sb.ToString()).GetAwaiter().GetResult();



            }
            finally
            {
            }




            return ret;
        }

        /// <summary>
        /// 設定ファイルの読み込みとAzureの接続を行います。
        /// </summary>
        public void Connection() 
        {

            // 鶴田のやり方
            //  NULLは明示的に対処するべき、と考えているから。なので、まずは非許容型で対処、できないときは都度検討
            string connString = "";
            connString = System.Configuration.ConfigurationManager.AppSettings["ConnectionAzure"] ?? "";

            string uploadContainerName = System.Configuration.ConfigurationManager.AppSettings["UploadContainer"];
            string logContainerName = System.Configuration.ConfigurationManager.AppSettings["LogContainer"];
            string errorContainerName = System.Configuration.ConfigurationManager.AppSettings["ErrorContainer"];
            string backupContainerName = System.Configuration.ConfigurationManager.AppSettings["BackupContainer"];

            BlobServiceClient blobServiceClient = new BlobServiceClient(connString);
            uploadContainer = blobServiceClient.GetBlobContainerClient(uploadContainerName);
            logContainer = blobServiceClient.GetBlobContainerClient(logContainerName);
            errorContainer = blobServiceClient.GetBlobContainerClient(errorContainerName);
            backupContainer = blobServiceClient.GetBlobContainerClient(backupContainerName);


        }
    }


    public class AppConfig
    {

        public string? BlobConnString { get; private set; }
        public string? BlobUploadContainerName { get; private set; }
        public string? BlobLogContainerName { get; private set; }
        public string? BlobErrorContainerName { get; private set; }
        public string? BlobBackupContainerName { get; private set; }

        public AppConfig()
        {
            // App.Config より入力する
            this.BlobConnString = System.Configuration.ConfigurationManager.AppSettings["ConnectionAzure"];
            this.BlobUploadContainerName = System.Configuration.ConfigurationManager.AppSettings["UploadContainer"];
            this.BlobLogContainerName = System.Configuration.ConfigurationManager.AppSettings["LogContainer"];
            this.BlobErrorContainerName = System.Configuration.ConfigurationManager.AppSettings["ErrorContainer"];
            this.BlobBackupContainerName = System.Configuration.ConfigurationManager.AppSettings["BackupContainer"];
        }



    }
}
