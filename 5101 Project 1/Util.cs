using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection.PortableExecutable;

namespace _5101_Project_1
{
    public class Util
    {
        // Path
        private const string dataPath = @"..\..\..\Data";
        

        // File names
        public FileInfo[] files;
        public const string LINE = "--------------------------------------------------------------------------------\n";

        // Prints the title
        public String PrintTitle()
        {
            string str = "";
            // Print Title
            str += LINE;
            str += "Program functionality\n";
            str += LINE;
            return str;
        }

        // Prints the Instructions
        public String PrintInstructions()
        {
            string str = "";
            // Print Title
            str += "\t- To exit program, enter 'exit' at any prompt.\n";
            str += "\t- To start program from the begining enter 'restart' at any prompt.\n";
            str += "\t- You will be presented with a numbered list of options.\n";
            str += "\t- When prompted, Enter a corrispoiding filename, filetype or query\n\n";
            return str;
        }

        // Get available files
        public String GetFiles()
        {
            files = new DirectoryInfo(dataPath).GetFiles();

            string str = "";
            int count = 1;
            foreach (var f in files) {
                str += "\t" + count + ") " + f.Name + "\n";
                ++count;
            }

            return str;
        }


        // Gets input from user and returns an approprate catalogue
        public int GetCatalogueSelection()
        {

            int selection = 0;
            bool isSelectedFileType = false;
            //Statistics stats;
            while (!isSelectedFileType) {

                Console.Write("Select an option from the list above (e.g. 1, 2,): ");
                // Read input
                String input = Console.ReadLine();

                // check if we should reset
                if (input.ToUpper().Equals("EXIT"))
                {
                    return -1;
                }
                if (input.ToUpper().Equals("RESET")) {
                    return -2;
                }

                // Check if we got a number
                try {
                    selection = Int32.Parse(input);
                }
                catch (Exception ex) {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                    continue;
                }

                // Check if number is in range
                if (selection < 1 || selection > files.Length) {
                    Console.WriteLine("Invalid Input: selection is not in range");
                }
                else {
                    isSelectedFileType = true;
                }
            }
            return selection - 1; // minus 1 to account for indexing
        }

        public int GetQuerySelection(int catalogueSelection)
        {
            int querySelection = 0;
            bool isValidQueryInput = false;
            while (!isValidQueryInput) {

                Console.WriteLine(DisplayQueries());

                // Read input
                Console.Write("Select a data query routine from the list above for the " + files[catalogueSelection].Name + " file (e.g. 1, 2,): ");
                string input = Console.ReadLine();
            
                // Check for exit
                if (input.ToUpper().Equals("EXIT"))
                {
                    return -1;
                }
                // Check for reset
                if (input.ToUpper().Equals("RESET")) {
                    return -2;
                }

                // Check if we got a number
                try {
                    querySelection = Int32.Parse(input);
                }
                catch (Exception ex) {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                    continue;
                }

                // Check if number is in range
                if (querySelection < 0 || querySelection > 12) {
                    Console.WriteLine("Invalid Input: selection is not in range");
                }
                else {
                    isValidQueryInput = true;
                }
            }
            return querySelection;
        }


        // Display queries
        public String DisplayQueries()
        {
            String str = "\n";
            str += LINE;
            str += "\tQUERY ROUTINES\n";
            str += LINE;
            str += "\t 1) Display City Information\n";
            str += "\t 2) Display Province Cities\n";
            str += "\t 3) Calculate Province Population\n";
            str += "\t 4) Match Cities Population\n";
            str += "\t 5) Distance Between Cities\n";
            str += "\t 6) Display city on map\n";
            str += "\t 7) Rank Provinces by Population\n";
            str += "\t 8) Rank Provinces by Cities\n";
            str += "\t 9) Get Capital of Province\n";
            str += "\t10) City with Smallest Population\n";
            str += "\t11) City with Largest Population\n";
            str += "\nType \"reset\" to select a different data source\n";
            str += "Type \"exit\" to exit\n";
            return str;
        }


        // Select Duplicate City
        // Accepts a list of cities, these cities should be duplicate names
        // It will prompt the user for 
        public int SelectDuplicateCity(List<CityInfo> cities)
        {
            int citySelection = 0;
            bool isSelected = false;
            while (!isSelected) {

                // Select between two cities
                Console.WriteLine("Please choose which " + cities[0].CityName + " by typeing the associated number: ");
                int count = 1;
                foreach (CityInfo c in cities) {
                    Console.WriteLine(count + ". City: " + c.CityName + " Province: " + c.Province);
                    ++count;
                }

                String num = Console.ReadLine();

                // Check if we got a number
                try {
                    citySelection = Int32.Parse(num);
                    if (citySelection < 0 || citySelection > cities.Count) {
                        Console.WriteLine("Invalid Input: selection out of range");
                    }
                    else {
                        isSelected = true;
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                    continue;
                }
            }
            return citySelection - 1;
        }

        
    }
}
