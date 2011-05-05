using System;
using System.Web.Mvc;
using System.Web.Routing;
using PetaPocoWebApplication.Controllers;
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

        protected override Type GetControllerType(RequestContext requestContext, string controllerName)
        {
            return typeof (FrontController);
        }
    }
}