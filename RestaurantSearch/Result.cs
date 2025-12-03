namespace RestaurantSearch
{
    public class Result
    {
        public string message { get; set; }
        public List<RestaurantInfo> restaurant_info { get; set; }
    }

    public class RestaurantInfo 
    {
        public string restaurant_name { get; set; }
        public string genre_name { get; set; }
        public string station_name { get; set; }

    }
}
