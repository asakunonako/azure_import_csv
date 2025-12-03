using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace azure_import_csv
{
    public class CsvCheck
    {
        public (Restaurant,bool) check(List<string> lists) 
        {

            Restaurant restaurant = new Restaurant();
            bool existError = false;

            for (int i = 0; i < lists.Count; i++)
            {
                string list = lists[i];
                {


                    switch (i)
                    {
                        // 飲食店ID
                        case 0:
                            if (IsNullOrEmptyCheck(list) && RestaurantIdCheck(list))
                            {
                                restaurant.RestaurantId = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 飲食店名
                        case 1:
                            if (IsNullOrEmptyCheck(list) && RestaurantNameCheck(list))
                            {
                                restaurant.RestaurantName = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // ジャンルID
                        case 2:
                            if (IsNullOrEmptyCheck(list) && GenreIdCheck(list))
                            {
                                restaurant.GenreId = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 駅ID
                        case 3:
                            if (IsNullOrEmptyCheck(list) && StationIdCheck(list))
                            {
                                restaurant.StationId = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 郵便番号
                        case 4:
                            if (IsNullOrEmptyCheck(list) && PostCodeCheck(list))
                            {
                                restaurant.PostCode = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 住所
                        case 5:
                            if (AddressCheck(list))
                            {
                                restaurant.Address = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 電話番号
                        case 6:
                            if (TelCheck(list))
                            {
                                restaurant.Tel = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 営業時間
                        case 7:
                            if (BusinessHoursCheck(list))
                            {
                                restaurant.BusinessHours = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 月
                        case 8:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Monday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 火
                        case 9:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Tuesday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 水
                        case 10:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Wednesday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 木
                        case 11:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Thursday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 金
                        case 12:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Friday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 土
                        case 13:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Saturday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                        // 日
                        case 14:
                            if (DayOfWeekCheck(list))
                            {
                                restaurant.Sunday = list;
                            }
                            else
                            {
                                existError = true;
                            }
                            break;
                    }
                }
            }
            return (restaurant,existError);
        }
            // 空白チェックメソッド

            public static bool IsNullOrEmptyCheck(string val)
            {
                if (!string.IsNullOrEmpty(val))
                {
                    return true;
                }
                return false;
            }

            // 飲食店IDチェックメソッド
            public static bool RestaurantIdCheck(string restaurant_id)
            {
                if (restaurant_id is string && restaurant_id.Length <= 10)
                {
                    return true;
                }
                return false;
            }

            // 飲食店名チェックメソッド
            public static bool RestaurantNameCheck(string restaurant_name)
            {
                if (restaurant_name is string && restaurant_name.Length <= 100)
                {
                    return true;
                }
                return false;
            }

            // ジャンルIDチェックメソッド
            public static bool GenreIdCheck(string genre_id)
            {
                if (genre_id is string && genre_id.Length <= 10)
                {
                    return true;
                }
                return false;
            }

            // 駅IDチェックメソッド
            public static bool StationIdCheck(string station_id)
            {
                if (station_id is string && station_id.Length <= 10)
                {
                    return true;
                }
                return false;
            }

            // 郵便番号チェックメソッド
            public static bool PostCodeCheck(string post_code)
            {
                if (post_code is string && post_code.Length <= 8 && Regex.IsMatch(post_code, @"^\d{3}-\d{4}$"))
                {
                    return true;
                }
                return false;
            }

            // 住所チェックメソッド
            public static bool AddressCheck(string address)
            {
                if (address is string && address.Length <= 100)
                {
                    return true;
                }
                return false;
            }

            // 電話番号チェックメソッド
            public static bool TelCheck(string tel)
            {
                if (tel is string && tel.Length <= 20)
                {
                    return true;
                }
                return false;
            }

            // 営業時間チェックメソッド
            public static bool BusinessHoursCheck(string business_hours)
            {
                if (business_hours is string && business_hours.Length <= 20)
                {
                    return true;
                }
                return false;
            }

            // 曜日チェックメソッド
            public static bool DayOfWeekCheck(string day_of_week)
            {
                bool result;
                if (bool.TryParse(day_of_week, out result))
                {
                    return true;
                }
                return false;
            }
        }
    }
