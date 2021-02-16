using Newtonsoft.Json;

namespace _5101_Project_1
{
    public class CityInfo
    {
        [JsonProperty("id")]
        public int CityID { get; set; } //" id "
        [JsonProperty("city")]
        public string CityName { get; set; } // " city "
        [JsonProperty("city_ascii")]
        public string CityAscii { get; set; } // " city_ascii "
        [JsonProperty("population")]
        public int Population { get; set; } // " population "
        [JsonProperty("admin_name")]
        public string Province { get; set; } // ~~ field called " admin_name " in CSV file. ~~
        [JsonProperty("lat")]
        public decimal Latitude { get; set; } // " lat "
        [JsonProperty("lng")]
        public decimal Longitude { get; set; } // " lng "
        [JsonProperty("capital")]
        public string Capital { get; set; } // **************** NOT LISTED IN THE PROJECT DOC - NEED TO FIND OUT IF THIS IS ALLOWED ****************

        //cities that are capitals of their province are marked with the string " admin " in the " capital " column. 

        public string GetProvince()
        {
            return Province;
        }

        public int GetPopulation()
        {
            return Population;
        }

        public string GetLocation()
        {
            return $"City: {CityName}, Latitude: {Latitude}, Longitude: {Longitude}";
        }
    }
}