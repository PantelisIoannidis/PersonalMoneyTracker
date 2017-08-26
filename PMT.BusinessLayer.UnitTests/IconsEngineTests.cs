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
    public class IconsEngineTests
    {
        private Mock<IIconRepository> mockRepository;
        private Mock<ILoggerFactory> mockLogger;
        IActionStatus actionStatus;
        private List<Icon> returnListFromRepository;

        [SetUp]
        public void SetUp()
        {
            mockRepository = new Mock<IIconRepository>();
            mockLogger = new Mock<ILoggerFactory>();
            actionStatus = new ActionStatus();
            returnListFromRepository = new List<Icon>()
            {
                new Icon(){IconId="1",Name="name1"},
                new Icon(){IconId="2",Name="name2"}
            };
        }

        [Test]
        public void GetAll_Calls_GetAll_Repository_Method()
        {

            mockRepository.Setup(x => x.GetAll()).Returns(returnListFromRepository);

            var iconEngine = new IconsEngine(mockLogger.Object, actionStatus, mockRepository.Object);

            var resultsFromFunctionCall = iconEngine.GetAll();

            mockRepository.VerifyAll();

            Assert.AreEqual(resultsFromFunctionCall, returnListFromRepository);

        }

        [Test]
        public void GetAll_Return_Same_List_As_Repository()
        {

            mockRepository.Setup(x => x.GetAll()).Returns(returnListFromRepository);

            var iconEngine = new IconsEngine(mockLogger.Object, actionStatus, mockRepository.Object);

            var resultsFromFunctionCall = iconEngine.GetAll();

            Assert.AreEqual(resultsFromFunctionCall, returnListFromRepository);

        }


    }
}
