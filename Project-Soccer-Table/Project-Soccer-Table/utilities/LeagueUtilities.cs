using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project_Soccer_Table.classes.services;
using Project_Soccer_Table.models;

namespace Project_Soccer_Table.utilities
{
    public static class LeagueUtils
    {
        private static string HighlightSortOption(string sortOption)
        {
            return $"\x1b[93m{sortOption.ToUpper()}\x1b[39m";
        }

        public static void PrintTable(List<Team> teams, string sortOption)
        {
            teams = SortService.SortTable(teams, sortOption);

            Console.Clear();
            Console.WriteLine("\n\x1b[93mSorted by: " + sortOption + "\x1b[39m");

            string PointsHeader = "Points";
            string WinsHeader = "Wins";
            string LossesHeader = "Losses";
            string DrawsHeader = "Draws";
            string GoalsScoredHeader = "GS";
            string GoalsConcededHeader = "GC";
            string GoalDifferenceHeader = "GD";

            switch (sortOption)
            {
                case "Points":
                    PointsHeader = HighlightSortOption("Points");
                    break;
                case "Wins":
                    WinsHeader = HighlightSortOption("Wins");
                    break;
                case "Losses":
                    LossesHeader = HighlightSortOption("Losses");
                    break;
                case "Draws":
                    DrawsHeader = HighlightSortOption("Draws");
                    break;
                case "GoalsScored":
                    GoalsScoredHeader = HighlightSortOption("GS");
                    break;
                case "GoalsConceded":
                    GoalsConcededHeader = HighlightSortOption("GC");
                    break;
                case "GoalDifference":
                    GoalDifferenceHeader = HighlightSortOption("GD");
                    break;
            }

            Console.WriteLine("{0,-5} {1,-26} {2,5} {3,7} {4,9} {5,7} {6,5} {7,7} {8,7}",
                "Rank", "Team", PointsHeader, WinsHeader, LossesHeader, DrawsHeader, GoalsScoredHeader,
                GoalsConcededHeader, GoalDifferenceHeader);

            for (int i = 0; i < teams.Count; i++)
            {
                var team = teams[i];
                string Points = $"{team.Points}";
                string Wins = $"{team.Wins}";
                string Losses = $"{team.Losses}";
                string Draws = $"{team.Draws}";
                string GoalsScored = $"{team.GoalsScored}";
                string GoalsConceded = $"{team.GoalsConceded}";
                string GoalDifference = $"{team.GoalDifference}";

                switch (sortOption)
                {
                    case "Points":
                        Points = HighlightSortOption(Points);
                        break;
                    case "Wins":
                        Wins = HighlightSortOption(Wins);
                        break;
                    case "Losses":
                        Losses = HighlightSortOption(Losses);
                        break;
                    case "Draws":
                        Draws = HighlightSortOption(Draws);
                        break;
                    case "GoalsScored":
                        GoalsScored = HighlightSortOption(GoalsScored);
                        break;
                    case "GoalsConceded":
                        GoalsConceded = HighlightSortOption(GoalsConceded);
                        break;
                    case "GoalDifference":
                        GoalDifference = HighlightSortOption(GoalDifference);
                        break;
                }

                Console.WriteLine("{0,-5} {1,-26} {2,5} {3,9} {4,7} {5,8} {6,8} {7,7} {8,7}",
                    i + 1, team.Name, Points, Wins, Losses, Draws,
                    GoalsScored, GoalsConceded, GoalDifference);
            }
        }

        public static List<(string team1, int score1, string team2, int score2, string league)> ReadResults(string srcPath)
        {
            var folders = Directory.GetDirectories(srcPath);
            var results = new List<(string team1, int score1, string team2, int score2, string league)>();

            foreach (var folder in folders)
            {
                var folderName = GetFolderName(folder);
                var files = Directory.GetFiles(folder);
                foreach (var file in files)
                {
                    var lines = File.ReadAllLines(file);
                    foreach (var line in lines)
                    {
                        var teamtuple = ParseMatchResult(line, folderName);

                        results.Add((teamtuple.Item1, teamtuple.Item2, teamtuple.Item3, teamtuple.Item4, folderName));
                    }
                }
            }

            return results;
        }

        public static Tuple<string, int, string, int> ParseMatchResult(string matchString, string folderName)
        {
            int scoreSeparatorIndex = matchString.IndexOf(':');
            if (scoreSeparatorIndex == -1)
            {
                throw new ArgumentException("Invalid match string format.");
            }

            int scoreStartIndex = scoreSeparatorIndex - 1;
            while (scoreStartIndex > 0 && Char.IsDigit(matchString[scoreStartIndex - 1]))
            {
                scoreStartIndex--;
            }

            string firstTeam = matchString.Substring(0, scoreStartIndex).Trim();

            int scoreEndIndex = scoreSeparatorIndex + 2;
            string[] scores = matchString.Substring(scoreStartIndex, scoreEndIndex - scoreStartIndex + 1).Split(':');

            int firstScore = int.Parse(scores[0].Trim());
            int secondScore = int.Parse(scores[1].Trim());

            string secondTeam = matchString.Substring(scoreEndIndex + 1).Trim();

            return Tuple.Create(firstTeam, firstScore, secondTeam, secondScore);
        }

        private static string GetFolderName(string folderPath)
        {
            return folderPath.Substring(folderPath.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }

        public static void ExportToFile(string srcPath, List<Team> teams)
        {
            DateTime now = DateTime.Now;
            string fileName = $"{now.ToFileTime()}-table.txt";

            // Create the file and write the header
            File.Create(srcPath + $"/{fileName}").Close();

            using (StreamWriter sw = File.AppendText(srcPath + $"/{fileName}"))
            {
                sw.WriteLine("Rank,Team,Points,Wins,Losses,Draws,GS,GC,GD");

                for (int i = 0; i < teams.Count; i++)
                {
                    var team = teams[i];
                    sw.WriteLine("" +
                                 $"{i + 1},{team.Name},{team.Points},{team.Wins},{team.Losses},{team.Draws},{team.GoalsScored},{team.GoalsConceded}," +
                                 $"{team.GoalDifference}");
                }
            }
        }
    }
}