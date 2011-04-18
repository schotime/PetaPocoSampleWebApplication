using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PetaPoco;
using PetaPocoWebApplication.Infrastructure;
using PetaPocoWebApplication.Models;
using Spark;
using Spark.Web.Mvc;
using StructureMap;

namespace PetaPocoWebApplication
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ControllerBuilder.Current.SetControllerFactory(new PetaPocoControllerFactory());

            ObjectFactory.Initialize(x => x.AddRegistry<PetaPocoRegistry>());

            Database.Mapper = new MyMapper();

            var settings = new SparkSettings();
            settings.SetAutomaticEncoding(true);

            // Note: you can change the list of namespace and assembly
            // references in Views\Shared\_global.spark
            SparkEngineStarter.RegisterViewEngine(settings);

            InsertInitialData();
        }

        private void InsertInitialData()
        {
            var database = PetaPocoRegistry.GetDatabase();
            using (var scope = database.Transaction)
            {
                var periods = database.Fetch<BudgetPeriod>();
                if (!periods.Any())
                {
                    var period = new BudgetPeriod
                                     {
                                         Description = "April",
                                         FromDate = new DateTime(2011, 4, 1),
                                         ToDate = new DateTime(2011, 4, 30)
                                     };

                    database.Insert(period);

                    var expense = new Expense
                                      {
                                          BudgetPeriodId = period.BudgetPeriodId,
                                          Description = "Test Expense",
                                          BudgetAmount = 20.0m
                                      };

                    var expense2 = new Expense
                                       {
                                           BudgetPeriodId = period.BudgetPeriodId,
                                           Description = "Extra meals",
                                           BudgetAmount = 100.0m
                                       };

                    database.Insert(expense);
                    database.Insert(expense2);
                }

                scope.Complete();
            }
        }
    }
}