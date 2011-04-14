using System;
using System.Web.Mvc;
using StructureMap;

namespace PetaPocoWebApplication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class FilterUsingAttribute : FilterAttribute, IAuthorizationFilter, IActionFilter, IResultFilter, IExceptionFilter
    {
        private readonly Type filterType;
        private object instantiatedFilter;

        public FilterUsingAttribute(Type filterType)
        {
            this.filterType = filterType;
        }

        public Type FilterType
        {
            get { return filterType; }
        }

        private void ExecuteFilter<TFilter>(Action<TFilter> execution) where TFilter : class
        {
            if (instantiatedFilter == null)
            {
                instantiatedFilter = ObjectFactory.GetInstance(filterType);
            }

            if (instantiatedFilter is TFilter)
            {
                execution((TFilter)instantiatedFilter);
            }
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            ExecuteFilter<IAuthorizationFilter>(filter => filter.OnAuthorization(filterContext));
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ExecuteFilter<IActionFilter>(filter => filter.OnActionExecuting(filterContext));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ExecuteFilter<IActionFilter>(filter => filter.OnActionExecuted(filterContext));
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ExecuteFilter<IResultFilter>(filter => filter.OnResultExecuting(filterContext));
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ExecuteFilter<IResultFilter>(filter => filter.OnResultExecuted(filterContext));
        }

        public void OnException(ExceptionContext filterContext)
        {
            ExecuteFilter<IExceptionFilter>(filter => filter.OnException(filterContext));
        }
    }
}