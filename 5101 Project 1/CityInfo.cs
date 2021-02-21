using Newtonsoft.Json;

namespace _5101_Project_1
{
    public class CityInfo
    {
        [JsonProperty("id")]
        public int CityID { get; set; }
        [JsonProperty("city")]
        public string CityName { get; set; }
        [JsonProperty("city_ascii")]
        public string CityAscii { get; set; }
        [JsonProperty("population")]
        public int Population { get; set; }
        [JsonProperty("admin_name")]
        public string Province { get; set; }
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }
        [JsonProperty("lng")]
        public decimal Longitude { get; set; }
        [JsonProperty("capital")]
        public string Capital { get; set; }

        /// <summary>
        /// Returns the province the city is in
        /// </summary>
        /// <returns>the province as a string</returns>
        public string GetProvince()
        {
            return Province;
        }

        /// <summary>
        /// Returns the current population
        /// </summary>
        /// <returns>The population as an int</returns>
        public int GetPopulation()
        {
            return Population;
        }

        /// <summary>
        /// returns the location of the current city
        /// </summary>
        /// <returns>A string listing the current city's name, lat and long</returns>
        public string GetLocation()
        {
            return $"City: {CityName}, Latitude: {Latitude}, Longitude: {Longitude}";
        }
    }
}