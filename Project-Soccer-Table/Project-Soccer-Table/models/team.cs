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
 
        public int GoalDifference => GoalsScored - GoalsConceded;
 
        public Team(string name)
        {
            Name = name;
        }
    }
}