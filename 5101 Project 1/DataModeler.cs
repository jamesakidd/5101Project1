using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace _5101_Project_1
{
    public class DataModeler
    {
        public delegate void ParseHandler(string file);
        private Dictionary<int, CityInfo> Cities = new Dictionary<int, CityInfo>(); //~~  was mentioned in class that we can use the cityId instead of cityName to avoid dupes. Would make it <int, CityInfo> ~~
        public void ParseXML(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StreamReader(fileName));
            XmlNodeList nodeList = doc.GetElementsByTagName("CanadaCity");
            bool isTitleLine = true;
            foreach(XmlNode node in nodeList)
            {
                if (isTitleLine)
                {
                    isTitleLine = false;
                    continue;
                }
                //getting the attribute data from the CanadaCity node
                XmlNodeList nodeData = node.ChildNodes;

                //CityInfo object to store city properties
                CityInfo city = new CityInfo();

                //adding data to CityInfo Properties
                city.CityName = nodeData[0].InnerText;
                city.CityAscii = nodeData[1].InnerText;
                city.Latitude = decimal.Parse(nodeData[2].InnerText);
                city.Longitude = decimal.Parse(nodeData[3].InnerText);
                //city.Country = cityData[4]; country is not tracked
                city.Province = nodeData[5].InnerText; ;

                //if the capital is empty string in the file it does not denote a capital
                if (nodeData[6].InnerText == "admin")
                    city.IsCapital = true;
                city.Population = int.Parse(nodeData[7].InnerText);
                city.CityID = int.Parse(nodeData[8].InnerText);

                //adding the cityInfo object to the dictionary with the key being its id
                Cities[city.CityID] = city;
            }
        }

        public void ParseJSON(string fileName)
        {
            string json = File.ReadAllText(fileName);
            List<CityInfo> cities = JsonConvert.DeserializeObject<List<CityInfo>>(json);
            foreach(CityInfo city in cities)
            {
                Cities[city.CityID] = city;
            }
        }

        public void ParseCSV(string fileName)
        {
            bool isTitleLine = true;

            //get all lines from the file
            List<string> lines = new List<string>(File.ReadAllLines(fileName));
            foreach (string line in lines)
            {
                if (isTitleLine)
                {
                    isTitleLine = false;
                    continue;
                }
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
                case ".csv":
                    ParseCSV(filename);
                    break;
                case ".json":
                    ParseJSON(filename);
                    break;
                case ".xml":
                    ParseXML(filename);
                    break;
                default:
                    break; //Throw exception here on invalid filetype? or validate it in client before calling?
            }
            return Cities;
        }
    }
}