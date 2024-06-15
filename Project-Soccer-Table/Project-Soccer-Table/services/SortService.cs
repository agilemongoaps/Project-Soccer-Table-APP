using System;
using System.Collections.Generic;
using System.Linq;
using Project_Soccer_Table.models;

namespace Project_Soccer_Table.classes.services
{
    public static class SortService
    {
        static string _sortType;
        
        public static List<Team> SortTable(List<Team> teams, string sortType)
        {
            _sortType = sortType;
            
            switch (_sortType)
            {
                case "Points":
                    teams = teams.OrderByDescending(t => t.Points).ToList();
                    break;
                case "GoalDifference":
                    teams = teams.OrderByDescending(t => t.GoalDifference).ToList();
                    break;
                case "Wins":
                    teams = teams.OrderByDescending(t => t.Wins).ToList();
                    break;
                case "Losses":
                    teams = teams.OrderByDescending(t => t.Losses).ToList();
                    break;
                case "Draws":
                    teams = teams.OrderByDescending(t => t.Draws).ToList();
                    break;
                case "GoalsScored":
                    teams = teams.OrderByDescending(t => t.GoalsScored).ToList();
                    break;
                case "GoalsConceded":
                    teams = teams.OrderByDescending(t => t.GoalsConceded).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid sort type."); 
            }
            
            return teams;
        }
    }
}