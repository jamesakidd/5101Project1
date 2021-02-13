using System;
using System.Collections.Generic;
using System.ComponentModel.Design;


namespace _5101_Project_1
{

    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate the util class
            Util util = new Util();

            // Print the title and instructions
            Console.Write(util.PrintTitle());
            Console.Write(util.PrintInstructions());

            // Get a list of all the files
            Console.WriteLine("Fetching list of available file names to be processed and queried...\n");
            Console.WriteLine(util.GetFiles());

            // Validate which type of file we want to parse
            Console.Write("Select an option from the list above (e.g. 1, 2,): ");
            bool isValid = false;
            int selection = 0;
            while (!isValid) {

                // Read input
                string input = Console.ReadLine();

                // Check if we got a number
                try {
                    selection = Int32.Parse(input);
                }
                catch (Exception ex) {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                    continue;
                }

                // Check if number is in range
                if (selection < 1 || selection > util.files.Length) {
                    Console.WriteLine("Invalid Input: selection is not in range");
                }
                else {
                    isValid = true;
                }
            }

            // Build the catalogue **** I think I'm doing this right?
            Statistics stats = new Statistics(util.files[selection - 1].FullName, util.files[selection - 1].Extension);
            Console.WriteLine("A city catalogue has now been populated from the " + util.files[selection - 1].Name + " file.\n");

            // Ask what type of query we want to run on the catalogue
            Console.WriteLine("Fetching list of available data querying routines that can be run on the " + util.files[selection - 1].Name + " file.\n");
            Console.WriteLine(util.DisplayQueries());
            Console.Write("Select a data query routine from the list above for the " + util.files[selection - 1].Name + " file (e.g. 1, 2,): ");

            // Validate query input
            isValid = false;
            int querySelection = 0;
            while (!isValid) {

                // Read input
                string input = Console.ReadLine();

                // Check if we got a number
                try {
                    querySelection = Int32.Parse(input);
                }
                catch (Exception ex) {
                    Console.WriteLine("Invalid Input: " + ex.Message);
                    continue;
                }

                // Check if number is in range
                if (querySelection < 1 || selection > 6) {
                    Console.WriteLine("Invalid Input: selection is not in range");
                }
                else {
                    isValid = true;
                }
            }

            // Run the query THIS IS STILL IN PROGRESS
            // ******* STILL IN PROGRESS ****** //
            switch (querySelection) {
                case 1:
                    Console.WriteLine("SELECTED 1");
                    // This query should print city information,
                    // Probably need to add a prompt here to ask what city you  information is needed
                    foreach (KeyValuePair<int, CityInfo> c in stats.CityCatalogue) {
                        Console.WriteLine(c.Value);
                    }
                    break;
                case 2:
                    // Another type of query would go here
                    Console.WriteLine("Case 2");

                    break;
                case 3:
                    Console.WriteLine("Case 3");
                    break;
                case 4:
                    Console.WriteLine("Case 4");
                    break;
                case 5:
                    Console.WriteLine("Case 5");
                    break;
                case 6:
                    Console.WriteLine("Case 6");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
        }

    }

}
