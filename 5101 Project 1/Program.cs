/**
 * Program.cs
 * Date: 02/13/2021
 * The main driving/console/client program that will allow users to query
 * the dataset.
 */
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

            bool isReset = false;

            while (!isReset) {

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

                // Create the catalogue 
                Statistics stats = new Statistics(util.files[selection - 1].FullName, util.files[selection - 1].Extension);
                Console.WriteLine("A city catalogue has now been populated from the " + util.files[selection - 1].Name + " file.\n");

                // Ask what type of query we want to run on the catalogue
                Console.WriteLine("Fetching list of available data querying routines that can be run on the " + util.files[selection - 1].Name + " file.\n");
                Console.WriteLine(util.DisplayQueries());


                // Validate query input
                isValid = false;
                int querySelection = 0;
                while (!isValid) {

                    // Read input
                    Console.Write("Select a data query routine from the list above for the " + util.files[selection - 1].Name + " file (e.g. 1, 2,): ");
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
                    if (querySelection < 1 || querySelection > 7) {
                        Console.WriteLine("Invalid Input: selection is not in range");
                    }
                    else {
                        isValid = true;
                    }
                }

                // ******************************************************* //
                // ********** Query Selection **************************** //
                // ******************************************************* //

                // ************************************* //
                // ********** CITY INFO **************** //
                // ************************************* //
                if (querySelection == 1) {

                    bool isDone = false;
                    // Get city name
                    while (!isDone) {

                        Console.Write("Enter a city name: ");
                        String city = Console.ReadLine();

                        List<CityInfo> info;
                        try {
                            info = stats.DisplayCityInformation(city);
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Invalid city, please try again: " + ex.Message);
                            continue;
                        }

                        int citySelection = 0;
                        if (info.Count > 1) {
                            citySelection = util.SelectDuplicateCity(info);
                        }
                        Console.WriteLine("\tName" + info[citySelection].CityName);
                        Console.WriteLine("\tPopulation: " + info[citySelection].Population);
                        Console.WriteLine("\tLocation: " + info[citySelection].GetLocation() + "\n");
                        isDone = true;
                        isReset = true;
                    }
                }

                // ************************************* //
                // ********* Display Province Cities**** //
                // ************************************* //
                if (querySelection == 2) {

                    // Get province name
                    bool isDone = false;
                    while (!isDone) {

                        Console.Write("Enter a province name: ");
                        String province = Console.ReadLine();

                        List<String> cities = stats.DisplayProvinceCities(province);
                        if (cities.Count == 0) {
                            Console.WriteLine("Invalid province, please try again.\n");
                        }
                        else {
                            foreach (var c in cities) {
                                Console.WriteLine("\t" + c);
                            }
                            isDone = true;
                            isReset = true;
                        }
                    }
                }
                // Calculate Province Population
                if (querySelection == 3) {

                    bool isDone = false;
                    while (!isDone) {

                        // Get input
                        Console.Write("Enter a province name: ");
                        String province = Console.ReadLine();

                        // Validate input
                        int population = -1;
                        population = stats.DisplayProvincePopulation(province);
                        if (population < 0) {
                            Console.WriteLine("Invalid province, please try again.\n");
                            continue;
                        }
                        else {
                            Console.WriteLine("The province of " + province + " has a population of " + population + "\n");
                            isDone = true;
                            isReset = true;
                        }
                    }
                }

                // ************************************* //
                // ******** Match Cities Population **** //
                // ************************************* //
                if (querySelection == 4) {

                    // Get province name
                    bool isDone = false;
                    while (!isDone) {

                        // Get input
                        Console.Write("Enter two city names, seperated by a comma, to see which city has the larger population (eg. London, Toronto): ");
                        String input = Console.ReadLine();
                        String[] cities = input.Split(",");

                        // Validate input
                        if (cities.Length != 2) {
                            Console.WriteLine("Invalid input, please try again.\n");
                            continue;
                        }

                        /*** UNCOMMENT WHEN FUNCIONTALITY IS INTEGRATED ***/
                        //CityInfo largerCity = stats.CompareCitiesPopluation(cities[0], cities[1]);
                        Console.WriteLine("Largest city: + largerCity.name (PLACEHOLDER)");
                        Console.WriteLine("Population: + largerCity.population (PLACEHOLDER)\n");
                        isDone = true;
                        isReset = true;
                    }
                }

                // ************************************* //
                // ******** Distance Between Cities **** //
                // ************************************* //
                if (querySelection == 5) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Enter two city, province pairs, seperated by a comma, to find the distance between them (eg. London, Ontario, Toronto, Ontario): ");
                        String input = Console.ReadLine();
                        String[] cities = input.Split(",");

                        if (cities.Length != 4) {
                            Console.WriteLine("Invalid input, please try again.\n");
                            continue;
                        }

                        double distance = stats.CalculateDistanceBetweenCities(cities[0].Trim(), cities[1].Trim(), cities[2].Trim(), cities[3].Trim());
                        Console.WriteLine($"\nDistance between {cities[0]}, {cities[1]} and {cities[2]}, {cities[3]} is {distance} Kilometers\n");
                        isDone = true;
                        isReset = true;
                    }
                }

                // **************************************************************************** //
                // ******** Restart Program And Choose Another File or File Type To Querys **** //
                // **************************************************************************** //
                if (querySelection == 7) {
                    Console.WriteLine("Resetting...\n");
                }

                // **************************************************************************** //
                // ************************ Show city on map ********************************** //
                // **************************************************************************** //
                if (querySelection == 6) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Enter a city name and province, separated by a comma. (eg. London, Ontario)");
                        string input = Console.ReadLine();
                        string[] cities = input.Split(",");
                        string city = cities[0];
                        string province = cities[1];
                        if (cities.Length < 2) {
                            Console.WriteLine("Invalid input, please try again.\n");
                            continue;
                        }
                        try {
                            stats.ShowCityOnMap(city, province);
                        }
                        catch (Exception ex) {
                            Console.WriteLine(ex.Message);
                        }

                        isDone = true;
                        isReset = true;
                    }

                }
            }
            Console.WriteLine("Exiting Program...");
        }
    }
}

