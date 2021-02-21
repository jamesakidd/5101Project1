using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Pastel;
/*
 * Provides helper functions for client
 * Date: Feb. 19 - 2021
 */


namespace _5101_Project_1
{
    public class Util
    {
        // Path
        private const string dataPath = @"..\..\..\Data";


        // File names
        public FileInfo[] files;
        private static readonly string LINE = new string('-', Console.WindowWidth).Pastel(Color.Chartreuse) + "\n";

        /// <summary>
        /// Returns string for the main title of the program
        /// </summary>
        /// <returns>Title as a string</returns>
        public String PrintTitle()
        {
            string title = "Program functionality\n";
            string str = "";
            // Print Title
            str += LINE;
            str += new string(' ', (Console.WindowWidth - title.Length) / 2);
            str += title;
            str += LINE;
            return str;
        }

        /// <summary>
        /// Returns string containing instructions on program usuage
        /// </summary>
        /// <returns>Instructions as a string</returns>
        public String PrintInstructions()
        {
            string str = "";
            // Print Title
            str += $"\t- To exit program, enter {"'exit'".Pastel(Color.Red)} at any prompt.\n";
            str += $"\t- To start program from the beginning enter {"'restart'".Pastel(Color.Chartreuse)} at any prompt.\n";
            str += "\t- You will be presented with a numbered list of options.\n";
            str += "\t- When prompted, Enter a corresponding filename, file type or query\n\n";
            return str;
        }

        /// <summary>
        /// Returns string for displaying all available files in data path
        /// </summary>
        /// <returns>All the available data files as a string</returns>
        public String GetFiles()
        {
            files = new DirectoryInfo(dataPath).GetFiles();

            string str = "";
            int count = 1;
            foreach (var f in files)
            {
                str += "\t" + count.ToString().Pastel(Color.Chartreuse) + ") " + f.Name.Pastel(Color.Aquamarine) + "\n";
                ++count;
            }

            return str;
        }


        /// <summary>
        /// collects the user selection for the file selection
        /// </summary>
        /// <returns>the user's file choice as an int</returns>
        public int GetCatalogueSelection()
        {

            int selection = 0;
            bool isSelectedFileType = false;
            //Statistics stats;
            while (!isSelectedFileType)
            {

                Console.Write("Select an option from the list above (e.g. 1, 2,): ");
                // Read input
                String input = Console.ReadLine();

                // check if we should reset
                if (input.ToUpper().Equals("EXIT"))
                {
                    return -1;
                }
                if (input.ToUpper().Equals("RESET"))
                {
                    return -2;
                }

                // Check if we got a number
                try
                {
                    selection = Int32.Parse(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                    continue;
                }

                // Check if number is in range
                if (selection < 1 || selection > files.Length)
                {
                    Console.WriteLine("Invalid Input: selection is not in range");
                }
                else
                {
                    isSelectedFileType = true;
                }
            }
            return selection - 1; // minus 1 to account for indexing
        }

        /// <summary>
        /// Collects user input for main menu selection
        /// </summary>
        /// <param name="catalogueSelection"></param>
        /// <returns></returns>
        public int GetQuerySelection(int catalogueSelection)
        {
            int querySelection = 0;
            bool isValidQueryInput = false;
            while (!isValidQueryInput)
            {

                Console.WriteLine(DisplayQueries());

                // Read input
                Console.Write("Select a data query routine from the list above for the " + files[catalogueSelection].Name.Pastel(Color.Aquamarine) + " file (e.g. 1, 2,): ");
                string input = Console.ReadLine();

                // Check for exit
                if (input.ToUpper().Equals("EXIT"))
                {
                    return -1;
                }
                // Check for reset
                if (input.ToUpper().Equals("RESET"))
                {
                    return -2;
                }

                // Check if we got a number
                try
                {
                    querySelection = Int32.Parse(input);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input: ".Pastel(Color.Red) + ex.Message);
                    continue;
                }

                // Check if number is in range
                if (querySelection < 0 || querySelection > 12)
                {
                    Console.WriteLine("Invalid Input: selection is not in range".Pastel(Color.Red));
                }
                else
                {
                    isValidQueryInput = true;
                }
            }
            return querySelection;
        }


        /// <summary>
        /// returns a string for displaying main menu
        /// </summary>
        /// <returns>The Main menu as a string</returns>
        public String DisplayQueries()
        {
            string title = "QUERY ROUTINES\n";
            string str = "\n";
            str += LINE;
            str += new string(' ', (Console.WindowWidth - title.Length) / 2);
            str += title;
            str += LINE;
            str += "\t 1".Pastel(Color.Chartreuse) + ")" + " Display City Information\n".Pastel(Color.Aquamarine);
            str += "\t 2".Pastel(Color.Chartreuse) + ")" + " Display Province Cities\n".Pastel(Color.Aquamarine);
            str += "\t 3".Pastel(Color.Chartreuse) + ")" + " Calculate Province Population\n".Pastel(Color.Aquamarine);
            str += "\t 4".Pastel(Color.Chartreuse) + ")" + " Compare Two Cities' Population\n".Pastel(Color.Aquamarine);
            str += "\t 5".Pastel(Color.Chartreuse) + ")" + " Distance Between Cities\n".Pastel(Color.Aquamarine);
            str += "\t 6".Pastel(Color.Chartreuse) + ")" + " Display city on map\n".Pastel(Color.Aquamarine);
            str += "\t 7".Pastel(Color.Chartreuse) + ")" + " Rank Provinces by Population\n".Pastel(Color.Aquamarine);
            str += "\t 8".Pastel(Color.Chartreuse) + ")" + " Rank Provinces by Cities\n".Pastel(Color.Aquamarine);
            str += "\t 9".Pastel(Color.Chartreuse) + ")" + " Get Capital of Province\n".Pastel(Color.Aquamarine);
            str += "\t10".Pastel(Color.Chartreuse) + ")" + " City with Smallest Population\n".Pastel(Color.Aquamarine);
            str += "\t11".Pastel(Color.Chartreuse) + ")" + " City with Largest Population\n".Pastel(Color.Aquamarine);
            str += $"\nType {"'reset'".Pastel(Color.Chartreuse)} to select a different data source\n".Pastel(Color.Aquamarine);
            str += $"Type {"'exit'".Pastel(Color.Red)} to exit\n".Pastel(Color.Aquamarine);
            return str;
        }


        /// <summary>
        /// Prompts user to choose between two or more cities
        /// </summary>
        /// <param name="cities">A List of city objects to choose from</param>
        /// <returns>the index in the list that the user has chosen</returns>
        public int SelectDuplicateCity(List<CityInfo> cities)
        {
            int citySelection = 0;
            bool isSelected = false;
            while (!isSelected)
            {

                // Select between two cities
                Console.WriteLine("Please choose which " + cities[0].CityName.Pastel(Color.Aquamarine) + " by typing the associated number: ");
                int count = 1;
                foreach (CityInfo c in cities)
                {
                    Console.WriteLine(count.ToString().Pastel(Color.Chartreuse) + ". City: " + c.CityName.Pastel(Color.Aquamarine) + " Province: " + c.Province.Pastel(Color.Aquamarine));
                    ++count;
                }

                string num = Console.ReadLine();

                // Check if we got a number
                try
                {
                    citySelection = Int32.Parse(num);
                    if (citySelection < 0 || citySelection > cities.Count)
                    {
                        Console.WriteLine("Invalid Input: selection out of range");
                    }
                    else
                    {
                        isSelected = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                }
            }
            return citySelection - 1;
        }
    }
}
