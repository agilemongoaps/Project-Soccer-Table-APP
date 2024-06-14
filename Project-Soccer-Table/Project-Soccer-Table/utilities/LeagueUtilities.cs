using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project_Soccer_Table.models;

namespace Project_Soccer_Table.utilities
{
    public static class LeagueUtils
    {
        public static void PrintTable(List<Team> teams)
        {
            Console.WriteLine("{0,-3} {1,-20} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5} {8,5} {9,5}", 
                "Rank", "Team", "Points", "Wins", "Losses", "Draws", "GF", "GA", "GD");
 
            for (int i = 0; i < teams.Count; i++)
            {
                var team = teams[i];
                Console.WriteLine("{0,-3} {1,-20} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5} {8,5}", 
                    i + 1, team.Name, team.Points, team.Wins, team.Losses, team.Draws, 
                    team.GoalsScored, team.GoalsConceded, team.GoalDifference);
            }
        }
 
        public static List<(string team1, int score1, string team2, int score2)> ReadResults(string filePath)
        {
            var results = new List<(string, int, string, int)>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(parts[0], parts[1], parts[2], parts[3]);
                if (parts.Length == 4)
                {
                    var team1 = parts[0];
                    var score1 = int.Parse(parts[1]);
                    var team2 = parts[3];
                    var score2 = int.Parse(parts[2]);
                    results.Add((team1, score1, team2, score2));
                }
            }
            return results;
        }
    }
}