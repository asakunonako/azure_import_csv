using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace azure_import_csv
{
    public class Restaurant
    {
        public string? RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string GenreId { get; set; }
        public string StationId { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Tel { get; set; }
        public string BusinessHours { get; set; }
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }
    }
}
