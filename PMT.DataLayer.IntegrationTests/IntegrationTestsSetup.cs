using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PMT.BusinessLayer;
using PMT.Common;
using PMT.DataLayer.Repositories;
using PMT.DataLayer.Seed;
using PMT.Entities;
using PMT.Web;
using PMT.Web.Controllers;
using PMT.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using Microsoft.Practices.Unity;
using PMT.Web.App_Start;

namespace PMT.DataLayer.IntegrationTests
{
    [SetUpFixture]
    public class IntegrationTestsSetup
    {
        IUnityContainer container;
        const string userName = "testUser1";
        MainDb mainDb;
        IIdentityRepository identityRepository;

        public IntegrationTestsSetup()
        {
            SetDataDirectory();
            DeleteDatabaseIfExists();
        }

        private void DeleteDatabaseIfExists()
        {
            if (Database.Exists(Literals.ConnectionStrings.MainConnectionStringName))
                Database.Delete(Literals.ConnectionStrings.MainConnectionStringName);
        }

        private void SetDataDirectory()
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", NUnit.Framework.TestContext.CurrentContext.TestDirectory);
        }
    }
}
