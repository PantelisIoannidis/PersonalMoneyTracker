using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PMT.DataLayer.Repositories;
using PMT.DataLayer;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PMT.Web.Controllers;
using PMT.Web.Helpers;
using PMT.Common;
using PMT.BusinessLayer;
using PMT.DataLayer.Seed;
using Microsoft.Extensions.Logging;
using PMT.Models;
using PMT.Common.Helpers;

namespace PMT.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            //container.RegisterInstance<IUnityContainer>(container);

            container.RegisterInstance<IUnityContainer>(container);

            container.RegisterType<ILoggerFactory, LoggerFactory>( new ContainerControlledLifetimeManager ());

            //Business Login
            container.RegisterType<IIdentityEngine, IdentityEngine>();
            container.RegisterType<IMoneyAccountEngine, MoneyAccountEngine>();
            container.RegisterType<ICategoriesEngine, CategoriesEngine>();
            container.RegisterType<ITransactionsEngine, TransactionsEngine>();
            container.RegisterType<IChartsEngine, ChartsEngine>();
            container.RegisterType<IUserSettingsEngine, UserSettingsEngine>();
            container.RegisterType<IIconsEngine, IconsEngine>();




            //Helpers
            container.RegisterType<IActionStatus, ActionStatus>();
            container.RegisterType<ISeedingLists, SeedingLists>();
            container.RegisterType<IPersonalizedSeeding, PersonalizedSeeding>();
            container.RegisterType<ICommonHelper, CommonHelper>();
            container.RegisterType<IUnityFactory, UnityFactory>();
            container.RegisterType<IMapping, Mapping>();
            container.RegisterType<IPeriod, Period>();
            container.RegisterType<ICurrentDateTime, CurrentDateTime>();

            //Repositories
            container.RegisterType<MainDb>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<ISubCategoryRepository, SubCategoryRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<IMoneyAccountRepository, MoneyAccountRepository>();
            container.RegisterType<IIdentityRepository, IdentityRepository>();
            container.RegisterType<IIconRepository, IconRepository>();
            container.RegisterType<IUserSettingsRepository, UserSettingsRepository>();

            //Account Login
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            //container.RegisterType<DbContext, MainDb>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor(new ResolvedParameter<IIdentityEngine>(), new ResolvedParameter<ICommonHelper>()));
            container.RegisterType<ManageController>(new InjectionConstructor(new ResolvedParameter<ICommonHelper>()));

        }
    }
}
