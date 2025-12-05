using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azure_import_csv.Infra.Database
{
    public class DbConn
    {
        private static DbConn? instance = null;

        public string DbConnectionString { get; private set; } =  "";

        public SqlConnection? Connection { get; private set; } = null;

        public static DbConn Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbConn();
                    instance.DbConnectionString = Config.AppConfig.Instance.DbConnectionString;
                    // DBへの接続開始
                    // コネクションをオープンします。
                    instance.Connection = new SqlConnection(instance.DbConnectionString);
                    instance.Connection.Open();
                }
                return instance;
            } 
        }

             

        private DbConn()
        {
        }


    }
}
