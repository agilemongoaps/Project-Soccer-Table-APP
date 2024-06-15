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
            // Die Header-Zeile hat 9 Platzhalter (von {0} bis {8}), daher muss der Formatstring korrigiert werden.
            Console.WriteLine("{0,-3} {1,-20} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5} {8,5}", 
                "Rank", "Team", "Points", "Wins", "Losses", "Draws", "GF", "GA", "GD");

            for (int i = 0; i < teams.Count; i++)
            {
                var team = teams[i];
                // Die Team-Zeile hat ebenfalls 9 Platzhalter (von {0} bis {8}).
                Console.WriteLine("{0,-3} {1,-20} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5} {8,5}", 
                    i + 1, team.Name, team.Points, team.Wins, team.Losses, team.Draws, 
                    team.GoalsScored, team.GoalsConceded, team.GoalDifference);
            }
        }
 
        public static List<(string team1, int score1, string team2, int score2)> ReadResults(string srcPath)
        {
            // Read the folder and get all subfolders
            var folders = Directory.GetDirectories(srcPath);
            var results = new List<(string team1, int score1, string team2, int score2)>();
            
            foreach (var folder in folders)
            {
                var files = Directory.GetFiles(folder);
                Console.WriteLine(files);
                foreach (var file in files)
                {
                    var lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                    {
                        var teamtuple = ParseMatchResult(line);
                        
                        results.Add((teamtuple.Item1, teamtuple.Item2, teamtuple.Item3, teamtuple.Item4));
                    }
                }
            }
            
            Console.WriteLine("Results read from {0} files", results.ToString());
            return results;
        }
        
        public static Tuple<string, int, string, int> ParseMatchResult(string matchString)
        {
            // Find the index of the score separator ":"
            int scoreSeparatorIndex = matchString.IndexOf(':');
            if (scoreSeparatorIndex == -1)
            {
                throw new ArgumentException("Invalid match string format.");
            }

            // Find the start of the score part (we assume it starts with the first digit before the ':')
            int scoreStartIndex = scoreSeparatorIndex - 1;
            while (scoreStartIndex > 0 && Char.IsDigit(matchString[scoreStartIndex - 1]))
            {
                scoreStartIndex--;
            }

            // Extract the first team name
            string firstTeam = matchString.Substring(0, scoreStartIndex).Trim();

            // Extract the scores
            int scoreEndIndex = scoreSeparatorIndex + 2;
            string[] scores = matchString.Substring(scoreStartIndex, scoreEndIndex - scoreStartIndex + 1).Split(':');

            int firstScore = int.Parse(scores[0].Trim());
            int secondScore = int.Parse(scores[1].Trim());

            // Extract the second team name
            string secondTeam = matchString.Substring(scoreEndIndex + 1).Trim();

            return Tuple.Create(firstTeam, firstScore, secondTeam, secondScore);
        }
    }
}