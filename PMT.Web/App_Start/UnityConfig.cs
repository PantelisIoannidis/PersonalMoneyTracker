using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using PMT.DataLayer.Repositories;
using PMT.Contracts.Repositories;
using PMT.DataLayer;
using PMT.DataLayer.Context;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PMT.Web.Controllers;
using PMT.Web.Helpers;
using PMT.Common;
using PMT.BusinessLayer;
using PMT.DataLayer.Seeding;

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


            container.RegisterType<IIdentityEngine, IdentityEngine>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<DbContext, IdentityDb>();
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<AccountController>(new InjectionConstructor(new ResolvedParameter<IIdentityEngine>()));
            container.RegisterType<ManageController>(new InjectionConstructor());

            container.RegisterType<ISeeding, Seeding>();
            
            
            container.RegisterType<ISecurityHelper, SecurityHelper>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IUserAccountRepository, UserAccountRepository>();
            container.RegisterType<IIdentityRepository, IdentityRepository>();
            container.RegisterType<IUnityFactory, UnityFactory>();

        }
    }
}
