namespace _5101_Project_1
{
    public class CityInfo
    {
        public int CityID { get; set; } //" id "
        public string CityName { get; set; } // " city "
        public string CityAscii { get; set; } // " city_ascii "
        public int Population { get; set; } // " population "
        public string Province { get; set; } // ~~ field called " admin_name " in CSV file. ~~
        public decimal Latitude { get; set; } // " lat "
        public decimal Longitude { get; set; } // " lng "
        public bool IsCapital { get; set; } = false; // **************** NOT LISTED IN THE PROJECT DOC - NEED TO FIND OUT IF THIS IS ALLOWED ****************

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