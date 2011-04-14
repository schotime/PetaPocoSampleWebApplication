using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaPocoControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
                return ObjectFactory.GetInstance(controllerType) as IController;
                        
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}