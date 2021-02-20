using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace _5101_Project_1
{
    public class DataModeler
    {
        public delegate void ParseHandler(string file);
        private Dictionary<int, CityInfo> Cities = new Dictionary<int, CityInfo>();

        /// <summary>
        /// Parses a XML file to be passed to the main city catalouge
        /// </summary>
        /// <param name="fileName">The XML file to be parsed</param>
        public void ParseXML(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new StreamReader(fileName));
            XmlNodeList nodeList = doc.GetElementsByTagName("CanadaCity");
            foreach(XmlNode node in nodeList)
            {
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

                city.Capital = nodeData[6].InnerText;
                city.Population = int.Parse(nodeData[7].InnerText);
                city.CityID = int.Parse(nodeData[8].InnerText);

                //adding the cityInfo object to the dictionary with the key being its id
                Cities[city.CityID] = city;
            }
        }

        /// <summary>
        /// Parses a JSON file to be passed to the main city catalouge
        /// </summary>
        /// <param name="fileName">The name of the file to be parsed</param>
        public void ParseJSON(string fileName)
        {
            string json = File.ReadAllText(fileName);
            List<CityInfo> cities = JsonConvert.DeserializeObject<List<CityInfo>>(json);
            foreach(CityInfo city in cities)
            {
                if (city.CityName == "")
                    continue;
                Cities[city.CityID] = city;
            }
        }

        /// <summary>
        /// Parses a CSV file to be passed to the main city catalouge
        /// </summary>
        /// <param name="fileName">The name of the file to be parsed</param>
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
                city.Capital = cityData[6];
                city.Population = int.Parse(cityData[7]);
                city.CityID = int.Parse(cityData[8]);

                //adding the cityInfo object to the dictionary with the key being its id
                Cities[city.CityID] = city;
            }
        }

        /// <summary>
        /// Adds the appropriate parsing method to the delegate
        /// </summary>
        /// <param name="filename">The name of the file to be parsed</param>
        /// <param name="type">The type of file to be parsed</param>
        /// <returns>A Dictionary containing the main city catalog</returns>
        public Dictionary<int, CityInfo> ParseFile(string filename, string type) 
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
            }
            return Cities;
        }
    }
}