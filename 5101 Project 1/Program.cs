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

            // Prmopt the user to choose what type of file they would like to parse
            // And then create the catalogue
            int catalogueSelection = util.GetCatalogueSelection();
            Statistics stats = new Statistics(util.files[catalogueSelection].FullName, util.files[catalogueSelection].Extension);
            Console.WriteLine("A city catalogue has now been populated from the " + util.files[catalogueSelection].Name + " file.\n");
            Console.WriteLine("Fetching list of available data querying routines that can be run on the " + util.files[catalogueSelection].Name + " file.\n");

            // Prompt the user for what type of query they would like to run
            bool programIsDone = false;
            while (!programIsDone) {


                int querySelection = util.GetQuerySelection(catalogueSelection);


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
                        String city = Console.ReadLine().Trim(' ');

                        // Check if empty string
                        if (city.Length == 0)
                        {
                            Console.WriteLine("Invalid city, please try again\n");
                            continue;
                        }

                        List<CityInfo> info;
                        try {
                            info = stats.DisplayCityInformation(city);
                        }
                        catch (Exception ex) {
                            Console.WriteLine("Invalid city, please try again: " + ex.Message);
                            continue;
                        }

                        if (info.Count <= 0 ) {
                            Console.WriteLine("Invalid city, please try again\n");
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
                            continue;
                        }
                    }
                }

                // ***************************************** //
                // ********* Display Province Population**** //
                // ***************************************** //
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
                            Console.WriteLine("Invalid input: " + ex.Message);
                        }

                        isDone = true;
                    }
                }


                // **************************************************************************** //
                // ************************ Rank Provinces By Population ********************************** //
                // **************************************************************************** //
                if (querySelection == 8) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Here are all the provinces ranked by population: ");
                        SortedDictionary<int, string> prov = stats.RankProvincesByPopulation();
                        foreach (var p in prov) {
                            Console.WriteLine("Province: " + p.Value + ": " + p.Key);
                        }
                        Console.WriteLine();
                        isDone = true;
                    }
                }

                // **************************************************************************** //
                // ************************ Show province cities ********************************** //
                // **************************************************************************** //
                if (querySelection == 9) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Here are all the provinces, ranked by number of cities");
                        SortedDictionary<int, string> prov = stats.RankProvincesByCities();
                        foreach (var p in prov) {
                            Console.WriteLine("Province: " + p.Value + ": " + p.Key);
                        }
                        // validate province input

                        Console.WriteLine();
                        isDone = true;
                    }
                }

                // ******************************* //
                // *** Get Capital Of Province *** //
                // ******************************* //
                if (querySelection == 10) {
                    bool isDone = false;
                    while (!isDone) {
                        Console.Write("Input the name of the province you want to find the capital of: ");
                        String prov = Console.ReadLine();
                        
                        try
                        {
                            CityInfo city = stats.GetCapital(prov);
                            Console.WriteLine("The capital of " + prov + " is " + city.CityName);
                            isDone = true;
                        }
                        catch(Exception)
                        {
                            Console.WriteLine("Error: invalid input, try again...");
                        }
                    }
                }

                // **************************************************************************** //
                // ************************ Show city with smallest population ***************** //
                // **************************************************************************** //
                if (querySelection == 11)
                {
                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Enter a province to get the city with the smallest population");
                        string province = Console.ReadLine();

                        try
                        {
                            stats.DisplaySmallestPopulationCity(province);
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid province, please try again: " + ex.Message);
                            continue;
                        }
                    }
                }

                // **************************************************************************** //
                // ************************ Show city with largest population ***************** //
                // **************************************************************************** //
                if (querySelection == 12)
                {
                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Enter a province to get the city with the largest population");
                        string province = Console.ReadLine();

                        try
                        {
                            stats.DisplayLargestPopulationCity(province);
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid province, please try again: " + ex.Message);
                            continue;
                        }
                    }
                }

                // ******************** //
                // *** Exit Program *** //
                // ******************** //
                if (querySelection == 0) {
                    programIsDone = true;
                }
            }
            Console.WriteLine("Exiting Program...");
        }

    }
}

