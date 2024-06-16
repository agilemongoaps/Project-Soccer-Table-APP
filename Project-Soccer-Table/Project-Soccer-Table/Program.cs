using System;
using System.Collections.Generic;
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
            Console.WriteLine("3. Print league table");
            Console.WriteLine("4. Export Table");
            Console.WriteLine("5. Exit");
            
            Console.Write("Enter the menu option: ");
            string menuOption = Console.ReadLine();
            
            switch (menuOption)
            {
                case "1":
                    PrintTable("Points", _leagueService.GetTable());
                    break;
                case "2":
                    PrintSortMenu(_leagueService.GetTable());
                    break;
                case "3":
                    PrintLeagueTable();
                    break;
                case "4":
                    Console.Write("\nExport path: ");
                    string exportOption = Console.ReadLine();
                    LeagueUtils.ExportToFile(exportOption, _leagueService.GetTable());
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

        static void PrintLeagueTable()
        {
            Console.Clear();
            List<string> leagues = _leagueService.GetLeagues();
            
            int count = 0;
            for (int i = 0; i < leagues.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {leagues[i]}");
                count++;
            }
            
            Console.Write("Enter the leagueId: ");
            string leagueId = Console.ReadLine();
            
            List<Team> table = _leagueService.GetTable();
            
            if(int.TryParse(leagueId, out int leagueIndex) && leagueIndex > 0 && leagueIndex <= count)
            {
                table = SortService.FilterLeague(table, leagues[leagueIndex - 1]);
                PrintTable("Points", table);
            }
            else
            {
                Console.WriteLine("Invalid leagueId. Press any key to return to the main menu.");
                Console.ReadKey();
                PrintMenu();
            }
        }
        
        static void PrintSortMenu(List<Team> table = null)
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
            
            if (table == null)
            {
                table = _leagueService.GetTable();
            }

            switch (sortType)
            {
                case "1":
                    PrintTable("Points", table);
                    break;
                
                case "2":
                    PrintTable("GoalDifference", table);
                    break;
                
                case "3":
                    PrintTable("Wins", table);
                    break;
                
                case "4":
                    PrintTable("Losses", table);
                    break;
                
                case "5":
                    PrintTable("Draws", table);
                    break;
                
                case "6":
                    PrintTable("GoalsScored", table);
                    break;
                
                case "7":
                    PrintTable("GoalsConceded", table);
                    break;
                
                default:
                    PrintMenu();
                    break;
            }
        }

        static void PrintTable(string sortOption, List<Team> table)
        {
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
            foreach (var (team1, score1, team2, score2, league) in results)
            {
                _leagueService.AddResult(team1, score1, team2, score2, league);
            }
        }
    }
}