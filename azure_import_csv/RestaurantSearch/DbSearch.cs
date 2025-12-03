using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RestaurantSearch
{
    public class DbSearch
    {
        // メソッドを作成
        public DataTable SearchTable(string parameter1, string parameter2)
        {
            var table = new DataTable();

            // メソッド内で
            // DBへの接続
            // 接続文字列を貼り付ける
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=testdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var sql = @"SELECT A.restaurant_name, B.genre_name, C.station_name, A.business_hours, A.monday, A.tuesday, A.wednesday, A.thursday, A.friday, A.saturday, A.sunday FROM restaurant as A INNER JOIN m_genre as B ON B.genre_id = A.genre_id INNER JOIN m_station as C ON C.station_id = A.station_id WHERE B.genre_name LIKE @genre_name AND C.station_name LIKE @station_name";

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                // データベースの接続開始
                connection.Open();

                // SQLの設定
                // データベースを検索
                command.CommandText = sql;

                command.Parameters.Add(new SqlParameter("@genre_name", parameter1));
                command.Parameters.Add(new SqlParameter("@station_name", parameter2));

                // SQLの実行
                var adapter = new SqlDataAdapter(command);

                // SQLを実行して取得した結果を格納する
                adapter.Fill(table);

                // 検索した結果はデバックかconsoleで出す
                Debug.WriteLine(table);

                // データベースの接続終了
                connection.Close();

                // 値を返す
                return table;

            }
        }
    }
}
