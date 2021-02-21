using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace _5101_Project_1
{
    /*
     * DataModeler.cs
     * Contains methods for return parsing csv, json and xml files
     * and modeling it into a catalogue of cities to use in the program
     */
    public class DataModeler
    {
        //the delegate used to call the different parse methods
        public delegate void ParseHandler(string file);

        //This dictionary will store the catalogue of cities
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
                city.Province = nodeData[5].InnerText;

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
            //creating a ParseHandler delegate to call the different parse methods
            ParseHandler parseHandler;

            //adding the method to the parse handler based on file type
            //after the catalogue is created the method is removed from the parse handler
            //this is in case the user resets the file type to a different type
            switch (type)
            {
                case ".csv":
                    //setting the parse csv method to the delegate
                    parseHandler = ParseCSV;
                    //passing the filename to the delegate
                    parseHandler(filename);
                    //removing the parse method from the delegate
                    parseHandler -= ParseCSV;
                    break;
                case ".json":
                    //setting the parse json method to the delegate
                    parseHandler = ParseJSON;
                    //passing the filename to the delegate
                    parseHandler(filename);
                    //removing the parse method from the delegate
                    parseHandler -= ParseJSON;
                    break;
                case ".xml":
                    //setting the parse xml method to the delegate
                    parseHandler = ParseXML;
                    //passing the filename to the delegate
                    parseHandler(filename);
                    //removing the parse method from the delegate
                    parseHandler -= ParseXML;
                    break;
            }
            //return the catalogue of cities
            return Cities;
        }
    }
}