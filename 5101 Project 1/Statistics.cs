using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace _5101_Project_1
{
    public class Statistics
    {

        // Property: a variable of the Dictionary generic type called CityCatalogue
        // holds cities' information returned from the DataModeler class. The variable
        // key is the city name (or city id) itself, and the value is an object of the CityInfo class.
        public Dictionary<int, CityInfo> CityCatalogue;



        //  Constructor(file name, file type).The user must specify the file name,
        //  “Canadacities”, and then determine the file type or extension to be JSON,
        //  XML, or CSV. You may get the value of the CityCatalogue here in this
        //  constructor by calling the DataModeler.Parse method
        public Statistics(string fileName, string fileType)
        {
            CityCatalogue = new DataModeler().ParseFile(fileName, fileType);
        }


        // ************************************************************************
        // ********************** -- CITY STAT METHODS -- //***********************
        // ************************************************************************

        //return list of CityInfo to account fo duplicate names
        //in client: if(list.count > 1) - display city,province and ask user to choose one
        //in client: if(list is empty) - tell user to not be dumb
        public List<CityInfo> DisplayCityInformation(string cityName)    //*************  UNTESTED  *************
        {
            //change cityName arg to title case
            new CultureInfo("en-US", false).TextInfo.ToTitleCase(cityName);
            return (from city in CityCatalogue where string.Equals(city.Value.CityName, cityName, StringComparison.CurrentCultureIgnoreCase) select city.Value).ToList();
        }

        /// <summary>
        /// It will return the largest population city in a province
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
                .Where(c => string.Equals(c.Value.CityName, province, StringComparison.CurrentCultureIgnoreCase)).OrderBy(c => c.Value.Population).First().Value;
        }


        //CompareCitiesPopluation: This method will take two parameters
        //each represents one city.It will return the city with a larger population
        //and the population number of each city.
        public void CompareCitiesPopluation(string city1, string city2)
        {
            //not sure how to go about returning all that.. maybe just print from this method? don't love that idea. 
            //Maybe just return a list of the requested cities in a specific order, but does that satisfy the rubric?
        }



        /// <summary>
        /// Opens latlong.net in a browser window, centered on the given city,province
        /// </summary>
        /// <param name="city">The city to locate</param>
        /// <param name="province">The province in which the given city is</param>
        public void ShowCityOnMap(string city, string province) //*************  UNTESTED  *************
        {
            CityInfo cityToFind = CityCatalogue.First(cityInfo => string.Equals(cityInfo.Value.CityName, city, StringComparison.CurrentCultureIgnoreCase) &&
                                                                  string.Equals(cityInfo.Value.Province, province, StringComparison.CurrentCultureIgnoreCase)).Value;

            string uri = $"https://www.latlong.net/c/?lat={cityToFind.Latitude}&long={cityToFind.Longitude}";
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { UseShellExecute = true, FileName = uri });
        }//ShowCityOnMap

        public double CalculateDistanceBetweenCities(string cityA, string provinceA, string cityB, string provinceB)
        {
            CityInfo cityOne = CityCatalogue.First(cityInfo => string.Equals(cityInfo.Value.CityName, cityA, StringComparison.CurrentCultureIgnoreCase) &&
                                                               string.Equals(cityInfo.Value.Province, provinceA, StringComparison.CurrentCultureIgnoreCase)).Value;
            CityInfo cityTwo = CityCatalogue.First(cityInfo => string.Equals(cityInfo.Value.CityName, cityB, StringComparison.CurrentCultureIgnoreCase) &&
                                                               string.Equals(cityInfo.Value.Province, provinceB, StringComparison.CurrentCultureIgnoreCase)).Value;

            return GetDistance(cityOne.Latitude, cityOne.Longitude, cityTwo.Latitude, cityTwo.Longitude);
        }
        /// <summary>
        /// Calculates the distance between two lon/lat points using the Haversine formula ( https://en.wikipedia.org/wiki/Haversine_formula )
        /// </summary>
        /// <param name="lat1">Latitude of first location</param>
        /// <param name="lon1">Longitude of first location</param>
        /// <param name="lat2">Latitude of second location</param>
        /// <param name="lon2">Longitude of second location</param>
        /// <returns>The distance between the given points in Kilometers</returns>
        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const int radius = 6371;
            double dLat = DegreeToRads(lat2 - lat1);
            double dLon = DegreeToRads(lon2 - lon1);
            double a =
                    Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreeToRads(lat1)) * Math.Cos(DegreeToRads(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return radius * c;
        }

        /// <summary>
        /// converts a given degrees into radians
        /// </summary>
        /// <param name="deg">the degrees to be converted</param>
        /// <returns>the given number in radians</returns>
        private double DegreeToRads(double deg)
        {
            return deg * (Math.PI / 180);
        }



    }//class
}//ns