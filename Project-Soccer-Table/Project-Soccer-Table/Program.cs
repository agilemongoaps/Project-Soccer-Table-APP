using System;
using System.IO;
using System.Runtime.CompilerServices;
using Project_Soccer_Table.classes.services;
using Project_Soccer_Table.models;
using Project_Soccer_Table.utilities;

namespace Project_Soccer_Table
{
    class Program
    {
        static string _fullPath;
        static LeagueService _leagueService = new LeagueService();
        
        static void Main(string[] args)
        {
            ReadFiles();
            PrintMenu();
        }

        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Print Table");
            Console.WriteLine("2. Sort Table");
            Console.WriteLine("3. Export Table");
            Console.WriteLine("4. Exit");
            
            Console.Write("Enter the menu option: ");
            string menuOption = Console.ReadLine();
            
            switch (menuOption)
            {
                case "1":
                    PrintTable();
                    break;
                case "2":
                    PrintSortMenu();
                    break;
                case "3":
                    Console.Write("\nExportpart: ");
                    string exportOption = Console.ReadLine();
                    LeagueUtils.ExportToFile(exportOption, _leagueService.GetTable());
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
        
        static void PrintSortMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Sort by Points");
            Console.WriteLine("2. Sort by Goal Difference");
            Console.WriteLine("3. Sort by Wins");
            Console.WriteLine("4. Sort by Losses");
            Console.WriteLine("5. Sort by Draws");
            Console.WriteLine("6. Sort by Goals Scored");
            Console.WriteLine("7. Sort by Goals Conceded");
            Console.WriteLine("8. Return to Main Menu");
            
            Console.Write("Enter the sort type: ");
            string sortType = Console.ReadLine();

            switch (sortType)
            {
                case "1":
                    PrintTable();
                    break;
                
                case "2":
                    PrintTable("GoalDifference");
                    break;
                
                case "3":
                    PrintTable("Wins");
                    break;
                
                case "4":
                    PrintTable("Losses");
                    break;
                
                case "5":
                    PrintTable("Draws");
                    break;
                
                case "6":
                    PrintTable("GoalsScored");
                    break;
                
                case "7":
                    PrintTable("GoalsConceded");
                    break;
                
                default:
                    PrintMenu();
                    break;
            }
        }

        static void PrintTable(string sortOption = "Points")
        {
            var table = _leagueService.GetTable();
            LeagueUtils.PrintTable(table, sortOption);
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
            PrintMenu();
        }

        static void ReadFiles()
        {
            _fullPath = Path.GetFullPath("../../src");
            
            var leagueFolder = _fullPath;

            var results = LeagueUtils.ReadResults(leagueFolder);
            foreach (var (team1, score1, team2, score2) in results)
            {
                _leagueService.AddResult(team1, score1, team2, score2);
            }
        }
    }
}