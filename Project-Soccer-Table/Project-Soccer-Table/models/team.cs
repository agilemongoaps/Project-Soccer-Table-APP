using System.Collections.Generic;

namespace Project_Soccer_Table.models
{
    public class Team
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public string League { get; set; }
        public List<int> PlayDays { get; set; }
 
        public int GoalDifference => GoalsScored - GoalsConceded;
 
        public Team(string name)
        {
            Name = name;
            PlayDays = new List<int>();
        }

        public Team(string name, int points, int wins, int losses, int draws, int goalsScored, int goalsConceded, int playedDay, string league)
        {
            Name = name;
            Points = points;
            Wins = wins;
            Losses = losses;
            Draws = draws;
            GoalsScored = goalsScored;
            GoalsConceded = goalsConceded;
            League = league;
            PlayDays = new List<int> { playedDay };
        }
    }
}