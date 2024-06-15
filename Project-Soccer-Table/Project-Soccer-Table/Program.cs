using System;
using Project_Soccer_Table.classes.services;
using Project_Soccer_Table.utilities;

namespace Project_Soccer_Table
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path to the league folder:");
            var leagueFolder = Console.ReadLine();
 
            Console.WriteLine("Enter the last matchday to consider:");
            var lastMatchDay = int.Parse(Console.ReadLine());
 
            var leagueService = new LeagueService();
 
            for (int matchDay = 1; matchDay <= lastMatchDay; matchDay++)
            {
                var results = LeagueUtils.ReadResults(leagueFolder);
                foreach (var (team1, score1, team2, score2) in results)
                {
                    leagueService.AddResult(team1, score1, team2, score2);
                }
            }
 
            var table = leagueService.GetTable();
            LeagueUtils.PrintTable(table);
        }
    }
}