using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.UnitTests
{
    [TestFixture]
    public class UserSettingsEngineTests
    {
        private Mock<ILoggerFactory> mockLogger;
        private ActionStatus actionStatus;
        private Mock<IUserSettingsRepository> mockUserSettingsRepository;

        [SetUp]
        public void Setup()
        {
            mockLogger = new Mock<ILoggerFactory>();
            mockUserSettingsRepository = new Mock<IUserSettingsRepository>();
            actionStatus = new ActionStatus();
        }

        [Test]
        public void GetUserSettings_should_call_repository_method()
        {
            var userId = "user1";

            var userSettingsEngine = new UserSettingsEngine(mockLogger.Object, actionStatus, mockUserSettingsRepository.Object);

            userSettingsEngine.GetUserSettings(userId);

            mockUserSettingsRepository.Verify(x => x.GetSettings(It.IsAny<string>()));
            
        }

        [Test]
        public void StoreUserSettings_should_call_repository_method()
        {

            var userSettingsEngine = new UserSettingsEngine(mockLogger.Object, actionStatus, mockUserSettingsRepository.Object);

            userSettingsEngine.StoreUserSettings(new Entities.UserSettings());

            mockUserSettingsRepository.Verify(x => x.StoreSettings(It.IsAny<UserSettings>()));

        }


    }
}
