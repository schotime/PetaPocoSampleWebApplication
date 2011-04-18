using StructureMap;

namespace PetaPocoWebApplication.Infrastructure
{
    public interface IObjectResolver
    {
        T Resolve<T>();
    }

    public class ObjectResolver : IObjectResolver
    {
        public T Resolve<T>()
        {
            return ObjectFactory.GetInstance<T>();
        }
    }
}