using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using T7_P2_1.Providers;
using Unity;
using System.Data.Entity;
using T7_P2_1.Infrastructure;
using Unity.Lifetime;
using T7_P2_1.Repositories;
using T7_P2_1.Models;
using Unity.WebApi;
using T7_P2_1.Services;

[assembly: OwinStartup(typeof(T7_P2_1.Startup))]
namespace T7_P2_1
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var container = SetupUnity();
            ConfigureOAuth(app, container);

            HttpConfiguration config = new HttpConfiguration();
            config.DependencyResolver = new UnityDependencyResolver(container);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(config);
            app.UseWebApi(config);

        }

        public void ConfigureOAuth(IAppBuilder app, UnityContainer container)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(container)
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        private UnityContainer SetupUnity()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<DbContext, AuthContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IGenericRepository<ApplicationUser>, GenericRepository<ApplicationUser>>();
            container.RegisterType<IAuthRepository, AuthRepository>();

            container.RegisterType<IUserService, UserService>();
            return container;
        }

    }

}