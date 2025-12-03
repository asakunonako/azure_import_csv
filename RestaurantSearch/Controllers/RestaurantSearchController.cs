using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace RestaurantSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantSearchController : ControllerBase
    {
        // JSON形式で返ってくるときに必要
        [HttpGet]
        public Result execute([FromQuery] string genre_name, [FromQuery] string station_name)
        {

            DbSearch search = new DbSearch();
            DataTable data = search.SearchTable(genre_name, station_name);
            Result result = new Result();
            // resultの中のrestaurant_infoをインスタンス化
            result.restaurant_info = new List<RestaurantInfo>();

            // Dataテーブル型の検索結果が0件の場合
            if (data.Rows.Count == 0)
            {
                // resultのmessageに「該当する飲食店データが見つかりません」を格納
                result.message = "該当する飲食店データが見つかりません";
                // returnする
                return result;
            }

            // Dataテーブル型の検索結果が存在する場合
            foreach (DataRow list in data.Rows)
            {
                // RestaurantInfo クラスをインスタンス化
                RestaurantInfo restaurant = new RestaurantInfo();
                {
                    // 列名でアクセスし、RestaurantInfoの各プロパティに検索結果を入れる
                    restaurant.restaurant_name = list["restaurant_name"].ToString();
                    restaurant.genre_name = list["genre_name"].ToString();
                    restaurant.station_name = list["station_name"].ToString();
                }

                // resultの中のrestaurant_infoにRestaurantInfoの値をAddで入れる
                result.restaurant_info.Add(restaurant);
            }
            // return result
            return result;

        }
    }
}
