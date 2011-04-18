using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetaPocoWebApplication.Infrastructure
{
    public interface IQueryInvoker
    {
        TViewModel Invoke<TViewModel>() where TViewModel : new();
    }

    public class QueryInvoker : IQueryInvoker
    {
        private readonly IObjectResolver _objectResolver;

        public QueryInvoker(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public TViewModel Invoke<TViewModel>() where TViewModel : new()
        {
            var viewmodel = new TViewModel();
            var handler = _objectResolver.Resolve<IQueryHandler<TViewModel>>();
            handler.Handle(viewmodel);
            return viewmodel;
        }
    }
}