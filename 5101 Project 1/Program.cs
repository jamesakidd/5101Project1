/**
 * Program.cs
 * Date: 02/13/2021
 * The main driving/console/client program that will allow users to query
 * the dataset.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

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

            Statistics stats = default;
            bool isResetCatalogue = true; // flag if we should reset the catalogue
            bool isProgramDone = false;   // flag if the program is done

            while (!isProgramDone) { // Main program loop

                int catalogueSelection = 0;
                if (isResetCatalogue) {

                    // Get what type of catalogue we want to build
                    Console.WriteLine("Fetching list of available file names to be processed and queried...\n");
                    Console.WriteLine(util.GetFiles());
                    catalogueSelection = util.GetCatalogueSelection();

                    // Check for exit
                    if (catalogueSelection == -1) {
                        isProgramDone = true;
                        break;
                    }
                    // Check for reset
                    if (catalogueSelection == -2) {
                        isResetCatalogue = true;
                        continue;
                    }

                    // Create the catalogue
                    stats = new Statistics(util.files[catalogueSelection].FullName, util.files[catalogueSelection].Extension);
                    Console.WriteLine("\nA city catalogue has now been populated from the " + util.files[catalogueSelection].Name + " file...");
                    Console.WriteLine("Fetching list of available data querying routines that can be run on the " + util.files[catalogueSelection].Name + " file...\n");

                    // Reset the flag
                    isResetCatalogue = false;
                }
                int querySelection = util.GetQuerySelection(catalogueSelection);

                // Check for exit
                if (querySelection == -1) {
                    isProgramDone = true;
                    break;
                }
                // Check for reset
                if (querySelection == -2) {
                    isResetCatalogue = true;
                    continue;
                }

                // ******************** //
                // *** 1. CITY INFO *** //
                // ******************** //
                if (querySelection == 1) {

                    bool isDone = false;
                    while (!isDone) {

                        Console.Write("Enter a city name: ");
                        String city = Console.ReadLine().Trim(' ');

                        // Check if empty string
                        if (city.Length == 0) {
                            Console.WriteLine("Invalid city, please try again\n");
                            continue;
                        }

                        // Test if we should exit
                        if (city.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }

                        if (city.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        List<CityInfo> info;
                        try {
                            info = stats.DisplayCityInformation(city);
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Invalid city, please try again: " + ex.Message);
                            continue;
                        }

                        if (info.Count <= 0) {
                            Console.WriteLine("Invalid city, please try again\n");
                            continue;
                        }

                        int citySelection = 0;
                        if (info.Count > 1) {
                            citySelection = util.SelectDuplicateCity(info);
                        }
                        Console.WriteLine("\tName: " + info[citySelection].CityName);
                        Console.WriteLine("\tPopulation: " + info[citySelection].Population);
                        Console.WriteLine("\tLocation: " + info[citySelection].GetLocation() + "\n");
                        isDone = true;
                    }
                }

                // ********************************** //
                // *** 2. Display Province Cities *** //
                // ********************************** //
                if (querySelection == 2) {

                    bool isDone = false;
                    while (!isDone) {

                        Console.Write("Enter a province name: ");
                        String province = Console.ReadLine().Trim(' ');

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        List<String> cities = stats.DisplayProvinceCities(province);
                        if (cities.Count == 0) {
                            Console.WriteLine("Invalid province, please try again.\n");
                        }
                        else {
                            Console.WriteLine("\nThese are the cities in " + province + ": ");
                            foreach (var c in cities) {
                                Console.WriteLine("\t - " + c);
                            }
                            isDone = true;
                            ;
                        }
                    }
                }

                // ************************************** //
                // *** 3. Display Province Population *** //
                // ************************************** //
                if (querySelection == 3) {

                    bool isDone = false;
                    while (!isDone) {

                        // Get input
                        Console.Write("Enter a province name: ");
                        String province = Console.ReadLine().Trim(' ');

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Validate input
                        int population = -1;
                        population = stats.DisplayProvincePopulation(province);
                        if (population <= 0) {
                            Console.WriteLine("Invalid province, please try again.\n");
                            continue;
                        }
                        else {
                            Console.WriteLine("The province of " + province + " has a population of " + population + "\n");
                            isDone = true;
                        }
                    }
                }

                // ************************************* //
                // *** 4. Match Cities Population **** //
                // ************************************* //
                if (querySelection == 4) {

                    // Get province name
                    bool isDone = false;
                    while (!isDone) {

                        // Get input
                        Console.Write("Enter two city names, seperated by a comma, to see which city has the larger population (eg. London, Toronto): ");
                        String input = Console.ReadLine();

                        // Test if we should reset
                        if (input.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (input.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Split into array
                        String[] cities = input.Split(",");

                        // Remove all whitespace
                        for (int i = 0; i < cities.Length; ++i) {
                            cities[i] = cities[i].Trim(' ');
                        }

                        // Validate input
                        if (cities.Length != 2) {
                            Console.WriteLine("Invalid input, please try again.\n");
                            continue;
                        }

                        // This needs to be wrapped in a try catch incase one of the cities doesn't exist
                        try {
                            Tuple<string, int, int> largerCity = stats.CompareCitiesPopluation(cities[0], cities[1]);
                            Console.WriteLine(largerCity.Item1 + " is the bigger city with a population of " + largerCity.Item2 + " compared to " + largerCity.Item3 + "\n");
                            isDone = true;
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Error: Could not find one or more cities, please check spelling and try again - " + ex.Message + "\n");
                        }
                    }
                }

                // ********************************** //
                // *** 5. Distance Between Cities *** //
                // ********************************** //
                if (querySelection == 5) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Enter two city, province pairs, seperated by a comma, to find the distance between them (eg. London, Ontario, Toronto, Ontario): ");
                        String input = Console.ReadLine();

                        // Test if we should reset
                        if (input.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (input.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Split into array
                        String[] cities = input.Split(",");

                        // Remove all whitepsace
                        for (int i = 0; i < cities.Length; ++i) {
                            cities[i] = cities[i].Trim(' ');
                        }

                        // Validate input lenght
                        if (cities.Length != 4) {
                            Console.WriteLine("Invalid input, please try again.\n");
                            continue;
                        }

                        // Try to search for it
                        try {
                            double distance = stats.CalculateDistanceBetweenCities(cities[0].Trim(), cities[1].Trim(),
                                cities[2].Trim(), cities[3].Trim());
                            Console.WriteLine(
                                $"\nDistance between {cities[0]}, {cities[1]} and {cities[2]}, {cities[3]} is {distance} Kilometers\n");
                            isDone = true;
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Error: Could not find one or more cities, please check spelling and try again - " + ex.Message);
                        }

                    }
                }

                // *************************** //
                // *** 6. Show city on map *** //
                // *************************** //
                if (querySelection == 6) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Enter a city name and province, separated by a comma. (eg. London, Ontario): ");
                        string input = Console.ReadLine();

                        // Test if we should reset
                        if (input.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (input.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Splint into array
                        string[] cities = input.Split(",");

                        // Remove all whitepsace
                        for (int i = 0; i < cities.Length; ++i) {
                            cities[i] = cities[i].Trim(' ');
                        }

                        // Verify that we got 2 inputs
                        if (cities.Length != 2) {
                            Console.WriteLine("Invalid input, please try again.\n");
                            continue;
                        }

                        string city = cities[0];
                        string province = cities[1];

                        // Wrap in try catch incase input still somehow fails ie, spelt wrong
                        try {
                            stats.ShowCityOnMap(city, province);
                            isDone = true;
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Error: could not find city, check spelling and try again - " + ex.Message);
                        }
                    }
                }

                // *************************************** //
                // *** 7. Rank Provinces By Population *** //
                // *************************************** //
                if (querySelection == 7) {
                    bool isDone = false;
                    while (!isDone) {

                        Console.WriteLine("Provinces sorted by population (high-low): ");
                        SortedDictionary<int, string> prov = stats.RankProvincesByPopulation();

                        int count = 0;
                        foreach (var p in prov.Reverse()) {

                            string num = "\t" + count.ToString() + ". ";
                            Console.WriteLine("{0,-5} {1,-25} pop: {2,10}", num, p.Value, p.Key.ToString("N0"));
                            ++count;
                        }
                        Console.WriteLine();
                        isDone = true;
                    }
                }

                // ******************************* //
                // *** 8. Show province cities *** //
                // ******************************* //
                if (querySelection == 8) {
                    bool isDone = false;
                    while (!isDone) {
                        
                        Console.WriteLine("Provinces sorted by number of cities (high-low): ");
                        SortedDictionary<int, string> prov = stats.RankProvincesByCities();

                        int count = 1;
                        foreach (var p in prov.Reverse()) {

                            string num = "\t" + count.ToString() + ". ";
                            Console.WriteLine(string.Format("{0,-5} {1,-25} {2,10}", num, p.Value, p.Key.ToString("N0")));
                            ++count;
                        }
                        Console.WriteLine();
                        isDone = true;
                    }
                }

                // ********************************** //
                // *** 9. Get Capital Of Province *** //
                // ********************************** //
                if (querySelection == 9) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Input the name of the province you want to find the capital of: ");
                        String prov = Console.ReadLine();

                        // Test if we should reset
                        if (prov.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (prov.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Remove whitespace
                        prov = prov.Trim(' ');

                        // Check if empty string
                        if (prov.Length == 0) {
                            Console.WriteLine("Invalid input, try again...");
                            continue;
                        }

                        // Wrap in try catch incase spelling errors or bad data
                        try {
                            CityInfo city = stats.GetCapital(prov);
                            Console.WriteLine("The capital of " + prov + " is " + city.CityName + "\n");
                            isDone = true;
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Error: could not find province, check spelling and try again - " + ex.Message + "\n");
                        }
                    }
                }

                // ************************************************************ //
                // *** 10. Show city with smallest population ***************** //
                // ************************************************************ //
                if (querySelection == 10) {

                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Enter a province to get the city with the smallest population: ");
                        String province = Console.ReadLine();

                        // Remove whitespace
                        province = province.Trim(' ');

                        // Check if empty string
                        if (province.Length == 0) {
                            Console.WriteLine("Invalid input, try again...");
                            continue;
                        }

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Check for empty string
                        if (province.Length == 0) {
                            Console.WriteLine("Invalid input, try again...");
                        }

                        // Wrap in try catch incase spelling errors or bad data
                        try {
                            Console.WriteLine($"City with Smallest population in {province}: {stats.DisplaySmallestPopulationCity(province).CityName}");
                            isDone = true;
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Invalid province, check spelling and try again - " + ex.Message + "\n");

                        }
                    }
                }

                // ********************************************* //
                // *** 11. Show city with largest population *** //
                // ********************************************* //
                if (querySelection == 11) {

                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Enter a province to get the city with the largest population: ");
                        string province = Console.ReadLine();

                        // Remove whitespace
                        province = province.Trim(' ');

                        // Check for empty string
                        if (province.Length == 0) {
                            Console.WriteLine("Invalid input, try again...");
                        }

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET")) {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT")) {
                            isProgramDone = true;
                            break;
                        }

                        // Wrap in try catch incase spelling errors or bad data
                        try {
                            Console.WriteLine($"City with Largest population in {province}: {stats.DisplayLargestPopulationCity(province).CityName}");
                            isDone = true;
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Invalid province, please try again: " + ex.Message);
                        }
                    }
                }

            }
            Console.WriteLine("Exiting Program...");
        }

    }
}

