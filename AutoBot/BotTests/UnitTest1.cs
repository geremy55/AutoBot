using System;
using AutoBot.Helpers;
using AutoBot.Interfaces;
using AutoBot.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BotTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSetData_InContainer()
        {
            // Arrange:
            var settings = new ProgrammSettingsModel();
            var testSet = settings;
            ISendSettings sets = new SendSettingsContainer();

            // Act:
            sets.SetData(settings);

            // Assert:
            Assert.AreEqual<ProgrammSettingsModel>(testSet, sets.GetData());
        }
    }
}
