using PetaPoco;
using StructureMap.Configuration.DSL;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaPocoRegistry : Registry
    {
        public PetaPocoRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });

            For<IDatabaseQuery>().HttpContextScoped().Use(GetDatabase);
            For<IDatabase>().HttpContextScoped().Use(GetDatabase);

        }

        private static IDatabase GetDatabase()
        {
            return new MyDb("Peta");
        }
    }
}
