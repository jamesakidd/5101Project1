namespace _5101_Project_1
{
    public class CityInfo
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CityAscii { get; set; }
        public int Population { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string GetProvince()
        {
            return "Province of city";
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