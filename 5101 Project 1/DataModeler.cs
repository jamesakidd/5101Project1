using System.Collections.Generic;

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
            //parse file
            //foreach line:
            //info[cityName] = new CityInfo(params);
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