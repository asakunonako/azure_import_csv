using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azure_import_csv.Infra.Config
{
    public class AppConfig
    {

        private static AppConfig? instance = null;

        // 接続文字列
        public  string BlobConnectionString { get; private set; } = "";
        public  string DbConnectionString { get; private set; } = "";

        // コンテナ名
        public  string UploadContainer { get; private set; } = "";
        public  string LogContainer { get; private set; } = "";
        public  string ErrorContainer { get; private set; } = "";
        public  string BackupContainer { get; private set; } = "";

        // ログメッセージのひな形
        public  string LogMessageStart { get; private set; } = "";
        public  string LogMessageError { get; private set; } = "";
        public  string LogMessageException { get; private set; } = "";
        public  string LogMessageEnd { get; private set; } = "";

        public static AppConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppConfig();
                }
                return instance;
            }

        }

        private AppConfig()
        {
            // App.Config より入力する
            BlobConnectionString = System.Configuration.ConfigurationManager.AppSettings["BlobConnectionString"] ?? "";
            
            DbConnectionString = System.Configuration.ConfigurationManager.AppSettings["DbConnectionString"] ?? "";

            UploadContainer = System.Configuration.ConfigurationManager.AppSettings["UploadContainer"] ?? "";
            LogContainer = System.Configuration.ConfigurationManager.AppSettings["LogContainer"] ?? "";
            ErrorContainer = System.Configuration.ConfigurationManager.AppSettings["ErrorContainer"] ?? "";
            BackupContainer = System.Configuration.ConfigurationManager.AppSettings["BackupContainer"] ?? "";

            LogMessageStart = System.Configuration.ConfigurationManager.AppSettings["LogMessageStart"] ?? "";
            LogMessageError = System.Configuration.ConfigurationManager.AppSettings["LogMessageError"] ?? "";
            LogMessageException = System.Configuration.ConfigurationManager.AppSettings["LogMessageException"] ?? "";
            LogMessageEnd = System.Configuration.ConfigurationManager.AppSettings["LogMessageEnd"] ?? "";
        }
    }
}
