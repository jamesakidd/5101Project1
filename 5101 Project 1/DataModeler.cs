using System.Collections.Generic;
using System.IO;

namespace _5101_Project_1
{
    public class DataModeler
    {
        public delegate void ParseHandler(string file);
        private Dictionary<int, CityInfo> Cities = new Dictionary<int, CityInfo>(); //~~  was mentioned in class that we can use the cityId instead of cityName to avoid dupes. Would make it <int, CityInfo> ~~
        public void ParseXML(string fileName)
        {
            //parse file
            //foreach line:
            //info[cityName] = new CityInfo(params);
        }

        public void ParseJSON(string fileName)
        {
            //parse file
            //foreach line:
            //info[cityName] = new CityInfo(params);
        }

        public void ParseCSV(string fileName)
        {
            //get all lines from the file
            List<string> lines = new List<string>(File.ReadAllLines(fileName));
            foreach (string line in lines)
            {
                //get the data on each line delimited by comma ','
                List<string> cityData = new List<string>(line.Split(","));

                //CityInfo object to hold the data
                CityInfo city = new CityInfo();

                //adding data to CityInfo Properties
                city.CityName = cityData[0];
                city.CityAscii = cityData[1];
                city.Latitude = decimal.Parse(cityData[2]);
                city.Longitude = decimal.Parse(cityData[3]);
                //city.Country = cityData[4]; country is not tracked
                city.Province = cityData[5];

                //if the capital is empty string in the file it does not denote a capital
                if (cityData[6] == "admin")
                    city.IsCapital = true;
                
                city.Population = int.Parse(cityData[7]);
                city.CityID = int.Parse(cityData[8]);

                //adding the cityInfo object to the dictionary with the key being its id
                Cities[city.CityID] = city;
            }
        }

        //this method calls one of the Parse functions based on the file type
        public Dictionary<int, CityInfo> ParseFile(string filename, string type) // I think this needs to just return a CityInfo object. and is called from statisitcs? Unsure.
        {
            switch (type)
            {
                case "csv":
                    ParseCSV(filename);
                    break;
                case "json":
                    ParseJSON(filename);
                    break;
                case "xml":
                    ParseXML(filename);
                    break;
                default:
                    break; //Throw exception here on invalid filetype? or validate it in client before calling?
            }
            return Cities;
        }
    }
}