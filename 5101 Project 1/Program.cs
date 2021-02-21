using System;
using System.Collections.Generic;
using System.Drawing;
using Pastel;

/*
 * Program.cs
 * Date: 02/13/2021
 * The main driving/console/client program that will allow users to query the dataset.
 */

namespace _5101_Project_1
{

    class Program
    {
        static void Main()
        {
            // Instantiate the util class
            Util util = new Util();

            // Print the title and instructions
            Console.Write(util.PrintTitle());
            Console.Write(util.PrintInstructions());

            Statistics stats = default;
            bool isResetCatalogue = true; // flag if we should reset the catalogue
            bool isProgramDone = false;   // flag if the program is done

            while (!isProgramDone)
            { // Main program loop

                int catalogueSelection = 0;
                if (isResetCatalogue)
                {

                    // Get what type of catalogue we want to build
                    Console.WriteLine("Fetching list of available file names to be processed and queried...\n");
                    Console.WriteLine(util.GetFiles());
                    catalogueSelection = util.GetCatalogueSelection();

                    // Check for exit
                    if (catalogueSelection == -1)
                    {
                        isProgramDone = true;
                        break;
                    }
                    // Check for reset
                    if (catalogueSelection == -2)
                    {
                        isResetCatalogue = true;
                        continue;
                    }

                    // Create the catalogue
                    stats = new Statistics(util.files[catalogueSelection].FullName, util.files[catalogueSelection].Extension);
                    Console.WriteLine("\nA city catalogue has now been populated from the " + util.files[catalogueSelection].Name.Pastel(Color.Aquamarine) + " file...");
                    Console.WriteLine("Fetching list of available data querying routines that can be run on the " + util.files[catalogueSelection].Name.Pastel(Color.Aquamarine) + " file...\n");

                    // Reset the flag
                    isResetCatalogue = false;
                }
                int querySelection = util.GetQuerySelection(catalogueSelection);

                // Check for exit
                if (querySelection == -1)
                {
                    isProgramDone = true;
                    break;
                }
                // Check for reset
                if (querySelection == -2)
                {
                    isResetCatalogue = true;
                    continue;
                }

                // ******************** //
                // *** 1. CITY INFO *** //
                // ******************** //
                if (querySelection == 1)
                {

                    bool isDone = false;
                    while (!isDone)
                    {

                        Console.Write("Enter a city name: ");
                        String city = Console.ReadLine().Trim(' ');

                        // Check if empty string
                        if (city.Length == 0)
                        {
                            Console.WriteLine("Invalid city, please try again\n".Pastel(Color.Red));
                            continue;
                        }

                        // Test if we should exit
                        if (city.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }

                        if (city.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        List<CityInfo> info;
                        try
                        {
                            info = stats.DisplayCityInformation(city);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid city, please try again: ".Pastel(Color.Red) + ex.Message);
                            continue;
                        }

                        if (info.Count <= 0)
                        {
                            Console.WriteLine("Invalid city, please try again\n".Pastel(Color.Red));
                            continue;
                        }

                        int citySelection = 0;
                        if (info.Count > 1)
                        {
                            citySelection = util.SelectDuplicateCity(info);
                        }

                        string name = $"Name: {info[citySelection].CityName.Pastel(Color.Green)}";
                        string pop = $"Population: {info[citySelection].Population.ToString("0,0").Pastel(Color.Green)}";
                        string location = $"\t\tLocation: Latitude - {info[citySelection].Latitude.ToString().Pastel(Color.Green)}, Longitude - {info[citySelection].Longitude.ToString().Pastel(Color.Green)}";

                        Console.WriteLine("\n\n");
                        Console.Write(new string(' ', (Console.WindowWidth - name.Length) / 2));
                        Console.WriteLine(name);
                        Console.Write(new string(' ', (Console.WindowWidth - pop.Length) / 2));
                        Console.WriteLine(pop);
                        Console.Write(new string(' ', (Console.WindowWidth - location.Length) / 2));
                        Console.WriteLine(location + "\n\n");

                        isDone = true;
                    }
                }

                // ********************************** //
                // *** 2. Display Province Cities *** //
                // ********************************** //
                if (querySelection == 2)
                {

                    bool isDone = false;
                    while (!isDone)
                    {

                        Console.Write("Enter a province name: ");
                        String province = Console.ReadLine().Trim(' ');

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        List<String> cities = stats.DisplayProvinceCities(province);
                        if (cities.Count == 0)
                        {
                            Console.WriteLine("Invalid province, please try again.\n".Pastel(Color.Red));
                        }
                        else
                        {
                            Console.WriteLine("\n\tThese are the cities in " + province + ": ");
                            foreach (var c in cities)
                            {
                                Console.WriteLine("\t\t - " + c.Pastel(Color.Green));
                            }
                            isDone = true;
                        }
                    }
                }

                // ************************************** //
                // *** 3. Display Province Population *** //
                // ************************************** //
                if (querySelection == 3)
                {

                    bool isDone = false;
                    while (!isDone)
                    {

                        // Get input
                        Console.Write("Enter a province name: ");
                        String province = Console.ReadLine().Trim(' ');

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        // Validate input
                        int population = -1;
                        population = stats.DisplayProvincePopulation(province);
                        if (population <= 0)
                        {
                            Console.WriteLine("Invalid province, please try again.\n".Pastel(Color.Red));
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("\n\nThe population of " + province.Pastel(Color.Aquamarine) + " is: " + population.ToString("0,0").Pastel(Color.Green) + "\n");
                            isDone = true;
                        }
                    }
                }

                // ************************************* //
                // *** 4. Match Cities Population **** //
                // ************************************* //
                if (querySelection == 4)
                {

                    // Get province name
                    bool isDone = false;
                    while (!isDone)
                    {

                        // Get input
                        Console.Write("Enter two city names, separated by a comma, to see which city has the larger population (eg. London, Toronto): ");
                        String input = Console.ReadLine();

                        // Test if we should reset
                        if (input.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (input.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        // Split into array
                        String[] cities = input.Split(",");

                        // Remove all whitespace
                        for (int i = 0; i < cities.Length; ++i)
                        {
                            cities[i] = cities[i].Trim(' ');
                        }

                        // Validate input
                        if (cities.Length != 2)
                        {
                            Console.WriteLine("Invalid input, please try again.\n".Pastel(Color.Red));
                            continue;
                        }

                        try
                        {
                            Tuple<string, int, int> largerCity = stats.CompareCitiesPopluation(cities[0], cities[1]);
                            Console.WriteLine("\n\n" + largerCity.Item1.Pastel(Color.Aquamarine) + " is the bigger city with a population of " + largerCity.Item2.ToString("0,0").Pastel(Color.Green) + " compared to " + largerCity.Item3.ToString("0,0").Pastel(Color.Green) + "\n");
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: Could not find one or more cities, please check spelling and try again - ".Pastel(Color.Red) + ex.Message + "\n");
                        }
                    }
                }

                // ********************************** //
                // *** 5. Distance Between Cities *** //
                // ********************************** //
                if (querySelection == 5)
                {
                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Enter two city-province pairs, separated by a comma, to find the distance between them (eg. London, Ontario, Toronto, Ontario): ");
                        String input = Console.ReadLine();

                        // Test if we should reset
                        if (input.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (input.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        // Split into array
                        String[] cities = input.Split(",");

                        // Remove all whitepsace
                        for (int i = 0; i < cities.Length; ++i)
                        {
                            cities[i] = cities[i].Trim(' ');
                        }

                        // Validate input lenght
                        if (cities.Length != 4)
                        {
                            Console.WriteLine("Invalid input, please try again.\n".Pastel(Color.Red));
                            continue;
                        }

                        // Try to search for it
                        try
                        {
                            double distance = stats.CalculateDistanceBetweenCities(cities[0].Trim(), cities[1].Trim(),
                                cities[2].Trim(), cities[3].Trim());
                            Console.WriteLine(
                                $"\n\nThe Distance between {cities[0].Pastel(Color.Aquamarine)}, {cities[1].Pastel(Color.Aquamarine)} and {cities[2].Pastel(Color.Aquamarine)}, {cities[3].Pastel(Color.Aquamarine)} is: {distance.ToString("0,0").Pastel(Color.Green)} Kilometers\n");
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: Could not find one or more cities, please check spelling and try again - ".Pastel(Color.Red) + ex.Message);
                        }

                    }
                }

                // *************************** //
                // *** 6. Show city on map *** //
                // *************************** //
                if (querySelection == 6)
                {
                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Enter a city name and province, separated by a comma. (eg. London, Ontario): ");
                        string input = Console.ReadLine();

                        // Test if we should reset
                        if (input.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (input.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        // Splint into array
                        string[] cities = input.Split(",");

                        // Remove all whitepsace
                        for (int i = 0; i < cities.Length; ++i)
                        {
                            cities[i] = cities[i].Trim(' ');
                        }

                        // Verify that we got 2 inputs
                        if (cities.Length != 2)
                        {
                            Console.WriteLine("Invalid input, please try again.\n".Pastel(Color.Red));
                            continue;
                        }

                        string city = cities[0];
                        string province = cities[1];

                        try
                        {
                            stats.ShowCityOnMap(city, province);
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: could not find city, check spelling and try again - ".Pastel(Color.Red) + ex.Message);
                        }
                    }
                }

                // *************************************** //
                // *** 7. Rank Provinces By Population *** //
                // *************************************** //
                if (querySelection == 7)
                {
                    bool isDone = false;
                    while (!isDone)
                    {

                        Console.WriteLine("\n\nProvinces sorted by population: ");
                        SortedDictionary<int, string> prov = stats.RankProvincesByPopulation();

                        int count = 1;
                        foreach (var p in prov)
                        {

                            string num = "\t" + count + ". \t";
                            Console.WriteLine("{0,-25} {1,-45} Pop: {2,-10}", num.Pastel(Color.Aquamarine), p.Value.Pastel(Color.Green), p.Key.ToString("N0").Pastel(Color.Green));
                            ++count;
                        }
                        Console.WriteLine();
                        isDone = true;
                    }
                }

                // ******************************* //
                // *** 8. Rank province cities *** //
                // ******************************* //
                if (querySelection == 8)
                {
                    bool isDone = false;
                    while (!isDone)
                    {

                        Console.WriteLine("\n\nProvinces sorted by number of cities: ");
                        SortedDictionary<int, string> prov = stats.RankProvincesByCities();

                        int count = 1;
                        foreach (var p in prov)
                        {

                            string num = "\t" + count + ". \t";
                            Console.WriteLine("{0,-25} {1,-45} {2,-10}", num.Pastel(Color.Aquamarine), p.Value.Pastel(Color.Green), p.Key.ToString("N0").Pastel(Color.Chartreuse));
                            ++count;
                        }
                        Console.WriteLine();
                        isDone = true;
                    }
                }

                // ********************************** //
                // *** 9. Get Capital Of Province *** //
                // ********************************** //
                if (querySelection == 9)
                {
                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Input the name of the province you want to find the capital of: ");
                        String prov = Console.ReadLine();

                        // Test if we should reset
                        if (prov.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (prov.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        // Remove whitespace
                        prov = prov.Trim(' ');

                        // Check if empty string
                        if (prov.Length == 0)
                        {
                            Console.WriteLine("Invalid input, try again...".Pastel(Color.Red));
                            continue;
                        }

                        try
                        {
                            CityInfo city = stats.GetCapital(prov);
                            string result = $"\t\t\tThe capital of {prov.Pastel(Color.Green)} is {city.CityName.Pastel(Color.Chartreuse)}\n";
                            Console.WriteLine("\n\n");
                            Console.Write(new string(' ', (Console.WindowWidth - result.Length) / 2));
                            Console.WriteLine(result);
                            Console.WriteLine("\n");
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error: could not find province, check spelling and try again - ".Pastel(Color.Red) + ex.Message + "\n");
                        }
                    }
                }

                // ************************************************************ //
                // *** 10. Show city with smallest population ***************** //
                // ************************************************************ //
                if (querySelection == 10)
                {

                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Enter a province to get the city with the smallest population: ");
                        String province = Console.ReadLine();

                        // Remove whitespace
                        province = province.Trim(' ');

                        // Check if empty string
                        if (province.Length == 0)
                        {
                            Console.WriteLine("Invalid input, try again...".Pastel(Color.Red));
                            continue;
                        }

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        // Check for empty string
                        if (province.Length == 0)
                        {
                            Console.WriteLine("Invalid input, try again...".Pastel(Color.Red));
                        }

                        try
                        {
                            string result =
                                $"\t\t\tCity with Smallest population in {province.Pastel(Color.Aquamarine)}: {stats.DisplaySmallestPopulationCity(province).CityName.Pastel(Color.Green)}";

                            Console.WriteLine("\n\n");
                            Console.Write(new string(' ', (Console.WindowWidth - result.Length) / 2));
                            Console.WriteLine(result);
                            Console.WriteLine("\n");
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid province, check spelling and try again - ".Pastel(Color.Red) + ex.Message + "\n");

                        }
                    }
                }

                // ********************************************* //
                // *** 11. Show city with largest population *** //
                // ********************************************* //
                if (querySelection == 11)
                {

                    bool isDone = false;
                    while (!isDone)
                    {
                        Console.Write("Enter a province to get the city with the largest population: ");
                        string province = Console.ReadLine();

                        // Remove whitespace
                        province = province.Trim(' ');

                        // Check for empty string
                        if (province.Length == 0)
                        {
                            Console.WriteLine("Invalid input, try again...".Pastel(Color.Red));
                        }

                        // Test if we should reset
                        if (province.ToUpper().Equals("RESET"))
                        {
                            isResetCatalogue = true;
                            break;
                        }
                        // Test if we should exit
                        if (province.ToUpper().Equals("EXIT"))
                        {
                            isProgramDone = true;
                            break;
                        }

                        try
                        {
                            string result = $"\t\t\tCity with Largest population in {province.Pastel(Color.Aquamarine)}: {stats.DisplayLargestPopulationCity(province).CityName.Pastel(Color.Green)}";
                            Console.WriteLine("\n");
                            Console.Write(new string(' ', (Console.WindowWidth - result.Length) / 2));
                            Console.WriteLine(result);
                            Console.WriteLine("\n\n");
                            isDone = true;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Invalid province, please try again: ".Pastel(Color.Red) + ex.Message);
                        }
                    }
                }

            }
            Console.WriteLine("Exiting Program...");
        }

    }
}

