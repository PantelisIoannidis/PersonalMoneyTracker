using NUnit.Framework;
using PMT.DataLayer.Repositories;
using PMT.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace PMT.DataLayer.IntegrationTests
{
    [TestFixture]
    public class IdentityRepositoryTests
    {
        private IUnityContainer container;
        private MainDb mainDb;
        const string userName = "testUser1";
        IIdentityRepository identityRepository;

        public IdentityRepositoryTests()
        {
            container = UnityConfig.GetConfiguredContainer();
            mainDb = container.Resolve<MainDb>();
            identityRepository = container.Resolve<IIdentityRepository>();
        }
        [Test]
        public void GetUserId_should_return_id_for_name_input()
        {
            //Arrange
            var userId = "userIdxxxxx";
            ApplicationUser user = new ApplicationUser() {Id= userId,UserName =userName};
            mainDb.Users.Add(user);
            mainDb.SaveChanges();

            //Act
            var retrieved_id = identityRepository.GetUserId(userName);

            //Assert
            Assert.That(userId, Is.EqualTo(retrieved_id));
        }

    }
}
