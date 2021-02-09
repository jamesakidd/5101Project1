using System.Collections.Generic;

namespace _5101_Project_1
{
    public class DataModeler
    {
        public delegate void ParseHandler(string file);
        private Dictionary<string, CityInfo> info = new Dictionary<string, CityInfo>();
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
        public Dictionary<string, CityInfo> ParseFile(string filename, string type)
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
                    break;
            }
            return info;
        }
    }
}