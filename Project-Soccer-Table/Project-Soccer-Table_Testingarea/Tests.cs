using System;
using NUnit.Framework;
using Project_Soccer_Table.classes.services;

namespace Project_Soccer_Table_Testingarea
{
    [TestFixture]
    public class LeagueServiceTests
    {
        [Test]
        public void AddResult_WinLoss()
        {
            var service = new LeagueService();
            service.AddResult("TeamA", 2, "TeamB", 1);
            var table = service.GetTable();
            Assert.AreEqual(1, table.Count);
            Assert.AreEqual("TeamA", table[0].Name);
            Assert.AreEqual(3, table[0].Points);
            Assert.AreEqual(1, table[0].Wins);
            Assert.AreEqual(0, table[0].Losses);
            Assert.AreEqual(0, table[0].Draws);
            Assert.AreEqual(2, table[0].GoalsScored);
            Assert.AreEqual(1, table[0].GoalsConceded);
        }
 
        // Add more tests for different scenarios
    }
}