using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


/*
 * Statistics.cs
 * 2/16/2021
 * Contains methods for return statistics from the CityCatalogue dataset.
 */

namespace _5101_Project_1
{
    public class Statistics
    {
        public Dictionary<int, CityInfo> CityCatalogue;

        public Statistics(string fileName, string fileType)
        {
            CityCatalogue = new DataModeler().ParseFile(fileName, fileType);
        }

        // ************************************************************************
        // ********************** -- CITY STAT METHODS -- //***********************
        // ************************************************************************


        /// <summary>
        /// Returns a List of city objects matching the given city name.
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns>A list of the matching city names</returns>
        public List<CityInfo> DisplayCityInformation(string cityName)
        {
            return (from city in CityCatalogue where string.Equals(city.Value.CityName, cityName, StringComparison.CurrentCultureIgnoreCase) select city.Value).ToList();
        }

        /// <summary>
        /// Returns the largest population city in a province
        /// </summary>
        /// <param name="province"></param>
        /// <returns>CityInfo Object of the city with the biggest pop in the given province</returns>
        public CityInfo DisplayLargestPopulationCity(string province)  //*************  UNTESTED  *************
        {
            return CityCatalogue
                .Where(c => string.Equals(c.Value.CityName, province, StringComparison.CurrentCultureIgnoreCase))
                .OrderByDescending(c => c.Value.Population).First().Value;
        }

        /// <summary>
        /// It will return the smallest population city in a province
        /// </summary>
        /// <param name="province"></param>
        /// <returns>CityInfo Object of the city with the biggest pop in the given province</returns>
        public CityInfo DisplaySmallestPopulationCity(string province) //*************  UNTESTED  *************
        {
            return CityCatalogue
                .Where(c => string.Equals(c.Value.CityName, province, StringComparison.CurrentCultureIgnoreCase))
                .OrderBy(c => c.Value.Population).First().Value;
        }

        /// <summary>
        /// Returns tuple containing the name of the larger city and the two populations
        /// </summary>
        /// <param name="cityA">First city to compare</param>
        /// <param name="cityB">Second city to compare</param>
        /// <returns>Tuple containing: name of city, larger population, smaller population</returns>
        public Tuple<string, int, int> CompareCitiesPopluation(string cityA, string cityB)
        {
            List<CityInfo> city1 = DisplayCityInformation(cityA);
            List<CityInfo> city2 = DisplayCityInformation(cityB);
            Util util = new Util();

            var compareCity1 = city1.Count > 1 ? city1[util.SelectDuplicateCity(city1)] : city1[0];
            var compareCity2 = city2.Count > 1 ? city2[util.SelectDuplicateCity(city2)] : city2[0];

            return compareCity1.Population > compareCity2.Population
                ? new Tuple<string, int, int>(compareCity1.CityName, compareCity1.Population,
                      compareCity2.Population)
                : new Tuple<string, int, int>(compareCity2.CityName, compareCity2.Population,
                      compareCity1.Population);
        }

        /// <summary>
        /// Opens latlong.net in a browser window, centered on the given city,province
        /// </summary>
        /// <param name="city">The city to locate</param>
        /// <param name="province">The province in which the given city is</param>
        public void ShowCityOnMap(string city, string province)
        {
            int id = CityCatalogue.First(cityInfo => string.Equals(cityInfo.Value.CityName, city, StringComparison.CurrentCultureIgnoreCase) &&
                                                                  string.Equals(cityInfo.Value.Province, province, StringComparison.CurrentCultureIgnoreCase)).Key;

            string uri = $"https://www.latlong.net/c/?lat={CityCatalogue[id].Latitude}&long={CityCatalogue[id].Longitude}";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { UseShellExecute = true, FileName = uri });
        }

        /// <summary>
        /// Returns the distance in Kilometers between the two given city,provinces
        /// </summary>
        /// <param name="cityA">The origin city</param>
        /// <param name="provinceA">The origin city's province</param>
        /// <param name="cityB">The destination city</param>
        /// <param name="provinceB">the destination city's province</param>
        /// <returns>The distance in Kilometers</returns>
        public double CalculateDistanceBetweenCities(string cityA, string provinceA, string cityB, string provinceB)
        {

            int cityOne = CityCatalogue.First(cityInfo => string.Equals(cityInfo.Value.CityName, cityA, StringComparison.CurrentCultureIgnoreCase) &&
                                                       string.Equals(cityInfo.Value.Province, provinceA, StringComparison.CurrentCultureIgnoreCase)).Key;

            int cityTwo = CityCatalogue.First(cityInfo => string.Equals(cityInfo.Value.CityName, cityB, StringComparison.CurrentCultureIgnoreCase) &&
                                                               string.Equals(cityInfo.Value.Province, provinceB, StringComparison.CurrentCultureIgnoreCase)).Key;

            double distance = GetDistance(CityCatalogue[cityOne].Latitude, CityCatalogue[cityOne].Longitude, CityCatalogue[cityTwo].Latitude, CityCatalogue[cityTwo].Longitude);
            return Math.Round(distance, 0, MidpointRounding.AwayFromZero);
        }

        // ************************************************************************
        // ******************** -- PROVINCE STAT METHODS -- //*********************
        // ************************************************************************

        /// <summary>
        /// Returns total population for the given province
        /// </summary>
        /// <param name="province">The province to total the population of</param>
        /// <returns>The total population</returns>
        public int DisplayProvincePopulation(string province)
        {
            return CityCatalogue.Where(cityInfo =>
                string.Equals(cityInfo.Value.Province, province, StringComparison.CurrentCultureIgnoreCase))
                .Sum(cityInfo => cityInfo.Value.Population);
        }

        /// <summary>
        /// returns a list of all the cities in the given province
        /// </summary>
        /// <param name="province">The province to get a list of all cities from</param>
        /// <returns>A list of strings consisting of all the cities in the given province</returns>
        public List<String> DisplayProvinceCities(string province)
        {
            return (from cityInfo in CityCatalogue
                    where string.Equals(cityInfo.Value.Province, province, StringComparison.CurrentCultureIgnoreCase)
                    select cityInfo.Value.CityName).ToList();
        }

        /// <summary>
        /// Returns a sorted dictionary of all province's populations
        /// </summary>
        /// <returns>a sorted dictionary. Key: Total population Value: The Province</returns>
        public SortedDictionary<int, string> RankProvincesByPopulation() //*************  UNTESTED  *************
        {
            SortedDictionary<int, string> retSortedDictionary = new SortedDictionary<int, string>();
            var provList = GetProvinceList();

            foreach (string prov in provList)
            {
                int totalPop = DisplayProvincePopulation(prov);

                //IF - pop already exists: Adds one to pop
                if (!retSortedDictionary.ContainsKey(totalPop))
                {
                    retSortedDictionary.Add(totalPop, prov);
                }
                else
                {
                    retSortedDictionary.Add(totalPop + 1, prov);
                }
            }
            return retSortedDictionary;
        }

        /// <summary>
        /// Returns a sorted dictionary ranking all provinces by how many cities they have
        /// </summary>
        /// <returns>a sorted dictionary. Key: Total cities Value: The Province</returns>
        public SortedDictionary<int, string> RankProvincesByCities() //*************  UNTESTED  *************
        {
            SortedDictionary<int, string> retSortedDictionary = new SortedDictionary<int, string>();
            var provList = GetProvinceList();

            foreach (string prov in provList)
            {
                retSortedDictionary.Add(CityCatalogue.Count(c => c.Value.Province.Equals(prov)), prov);
            }
            return retSortedDictionary;
        }


        /// <summary>
        /// Returns the capital city for a given province
        /// </summary>
        /// <param name="province">The province</param>
        /// <returns>Cityinfo for the province's capital</returns>
        public CityInfo GetCapital(string province) //*************  UNTESTED  *************
        {
            return CityCatalogue.First(cityInfo =>
                string.Equals(cityInfo.Value.Province, province, StringComparison.CurrentCultureIgnoreCase) &&
                cityInfo.Value.IsCapital).Value;
        }




        // ************************************************************************
        // *********************** -- HELPER METHODS -- //*************************
        // ************************************************************************

        /// <summary>
        /// Calculates the distance between two lon/lat points using the Haversine formula ( https://en.wikipedia.org/wiki/Haversine_formula )
        /// </summary>
        /// <param name="lat1">Latitude of first location</param>
        /// <param name="lon1">Longitude of first location</param>
        /// <param name="lat2">Latitude of second location</param>
        /// <param name="lon2">Longitude of second location</param>
        /// <returns>The distance between the given points in Kilometers</returns>
        private double GetDistance(decimal lat1, decimal lon1, decimal lat2, decimal lon2)
        {
            const int radius = 6371;
            decimal dLat = DegreeToRads(lat2 - lat1);
            decimal dLon = DegreeToRads(lon2 - lon1);
            double a =
                    Math.Sin((double)(dLat / 2)) * Math.Sin((double)(dLat / 2)) +
                    Math.Cos((double)DegreeToRads(lat1)) * Math.Cos((double)DegreeToRads(lat2)) *
                    Math.Sin((double)(dLon / 2)) * Math.Sin((double)(dLon / 2));
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return radius * c;
        }

        /// <summary>
        /// converts a given degrees into radians
        /// </summary>
        /// <param name="deg">the degrees to be converted</param>
        /// <returns>the given number in radians</returns>
        private decimal DegreeToRads(decimal deg)
        {
            return deg * (decimal)(Math.PI / 180);
        }

        /// <summary>
        /// returns IEnumerable of all distinct provinces in CityCatalogue
        /// </summary>
        /// <returns></returns>
        private IEnumerable<String> GetProvinceList()
        {
            return CityCatalogue.Select(c => c.Value.Province).Distinct();
        }
    }//class
}//ns