using System.Collections.Generic;
using System.Linq;
using Project_Soccer_Table.models;

namespace Project_Soccer_Table.classes.services
{
    public class LeagueService
    {
        private List<Team> Teams;
 
        public LeagueService()
        {
            Teams = new List<Team>();
        }
 
        public void AddResult(string team1, int score1, string team2, int score2)
        {
            var team1Record = GetOrCreateTeam(team1);
            var team2Record = GetOrCreateTeam(team2);
 
            if (score1 > score2)
            {
                team1Record.Wins++;
                team1Record.Points += 3;
                team2Record.Losses++;
            }
            else if (score1 < score2)
            {
                team2Record.Wins++;
                team2Record.Points += 3;
                team1Record.Losses++;
            }
            else
            {
                team1Record.Draws++;
                team2Record.Draws++;
                team1Record.Points++;
                team2Record.Points++;
            }
 
            team1Record.GoalsScored += score1;
            team1Record.GoalsConceded += score2;
            team2Record.GoalsScored += score2;
            team2Record.GoalsConceded += score1;
        }
 
        public List<Team> GetTable()
        {
            return Teams
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.Wins)
                .ThenBy(t => t.Name)
                .ToList();
        }
 
        private Team GetOrCreateTeam(string name)
        {
            var team = Teams.FirstOrDefault(t => t.Name == name);
            if (team == null)
            {
                team = new Team(name);
                Teams.Add(team);
            }
            return team;
        }
    }
}