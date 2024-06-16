using System;
using System.Collections.Generic;
using NUnit.Framework;
using Project_Soccer_Table.classes.services;
using Project_Soccer_Table.models;

namespace Project_Soccer_Table_Testingarea
{
    [TestFixture]
    public class LeagueServiceTests
    {
        [Test]
        public void AddResult_AddsResultToTeams()
        {
            // Arrange
            var leagueService = new LeagueService();
            leagueService.AddResult("Team1", 2, "Team2", 1, "Day01");

            // Act
            var table = leagueService.GetTable();

            // Assert
            Assert.AreEqual(2, table.Count);
        }

        [Test]
        public void AddResult_AddsResultToTeams1()
        {
            // Arrange
            var leagueService = new LeagueService();
            leagueService.AddResult("Team1", 1, "Team2", 2, "Day01");

            // Act
            var table = leagueService.GetTable();

            // Assert
            Assert.AreEqual(2, table.Count);
            Assert.AreEqual("Team2", table[0].Name);
            Assert.AreEqual(3, table[0].Points);
            Assert.AreEqual(1, table[0].Wins);
            Assert.AreEqual(0, table[0].Losses);
            Assert.AreEqual(0, table[0].Draws);
            Assert.AreEqual(2, table[0].GoalsScored);
            Assert.AreEqual(1, table[0].GoalsConceded);
            Assert.AreEqual(1, table[0].GoalDifference);
        }

        [Test]
        public void AddResult_AddsResultToTeams3()
        {
            // Arrange
            var leagueService = new LeagueService();
            leagueService.AddResult("Team1", 1, "Team2", 1, "Day01");

            // Act
            var table = leagueService.GetTable();

            // Assert
            Assert.AreEqual(2, table.Count);
            Assert.AreEqual("Team1", table[0].Name);
            Assert.AreEqual(1, table[0].Points);
            Assert.AreEqual(0, table[0].Wins);
            Assert.AreEqual(0, table[0].Losses);
        }
        
        private List<Team> GetSampleTeams()
        {
            return new List<Team>
            {
                new Team("Team1", 10, 3, 1, 1, 12, 7, 1, "League1"),
                new Team("Team2", 8, 3, 1, 1, 10, 7, 1, "League1"),
                new Team("Team3", 10, 2, 2, 1, 10, 7, 1, "League1"),
                new Team("Team4", 15, 5, 0, 0, 15, 9, 1, "League1")
            };
        }

        [Test]
        public void SortTable_SortsByPointsCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "Points");

            // Assert
            Assert.AreEqual(15, sortedTeams[0].Points);
            Assert.AreEqual(10, sortedTeams[1].Points);
            Assert.AreEqual(10, sortedTeams[2].Points);
            Assert.AreEqual(8, sortedTeams[3].Points);
        }

        [Test]
        public void SortTable_SortsByGoalDifferenceCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "GoalDifference");

            // Assert
            Assert.AreEqual(6, sortedTeams[0].GoalDifference);
            Assert.AreEqual(5, sortedTeams[1].GoalDifference);
            Assert.AreEqual(3, sortedTeams[2].GoalDifference);
            Assert.AreEqual(3, sortedTeams[3].GoalDifference);
        }

        [Test]
        public void SortTable_SortsByWinsCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "Wins");

            // Assert
            Assert.AreEqual(5, sortedTeams[0].Wins);
            Assert.AreEqual(3, sortedTeams[1].Wins);
            Assert.AreEqual(3, sortedTeams[2].Wins);
            Assert.AreEqual(2, sortedTeams[3].Wins);
        }

        [Test]
        public void SortTable_SortsByLossesCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "Losses");

            // Assert
            Assert.AreEqual(2, sortedTeams[0].Losses);
            Assert.AreEqual(1, sortedTeams[1].Losses);
            Assert.AreEqual(1, sortedTeams[2].Losses);
            Assert.AreEqual(0, sortedTeams[3].Losses);
        }

        [Test]
        public void SortTable_SortsByDrawsCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "Draws");

            // Assert
            Assert.AreEqual(1, sortedTeams[0].Draws);
            Assert.AreEqual(1, sortedTeams[1].Draws);
            Assert.AreEqual(1, sortedTeams[2].Draws);
            Assert.AreEqual(0, sortedTeams[3].Draws);
        }

        [Test]
        public void SortTable_SortsByGoalsScoredCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "GoalsScored");

            // Assert
            Assert.AreEqual(15, sortedTeams[0].GoalsScored);
            Assert.AreEqual(12, sortedTeams[1].GoalsScored);
            Assert.AreEqual(10, sortedTeams[2].GoalsScored);
            Assert.AreEqual(10, sortedTeams[3].GoalsScored);
        }

        [Test]
        public void SortTable_SortsByGoalsConcededCorrectly()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act
            var sortedTeams = SortService.SortTable(teams, "GoalsConceded");

            // Assert
            Assert.AreEqual(9, sortedTeams[0].GoalsConceded);
            Assert.AreEqual(7, sortedTeams[1].GoalsConceded);
            Assert.AreEqual(7, sortedTeams[2].GoalsConceded);
            Assert.AreEqual(7, sortedTeams[3].GoalsConceded);
        }

        [Test]
        public void SortTable_InvalidSortType_ThrowsArgumentException()
        {
            // Arrange
            var teams = GetSampleTeams();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => SortService.SortTable(teams, "InvalidSortType"));
        }
    }
}