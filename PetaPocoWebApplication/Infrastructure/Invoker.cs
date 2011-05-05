using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetaPocoWebApplication.Infrastructure
{
    public interface IInvoker
    {
        void InvokeCommand<TInputModel>(TInputModel inputModel);
        TResultModel InvokeCommand<TInputModel, TResultModel>(TInputModel inputModel);
        TViewModel InvokeQuery<TViewModel>() where TViewModel : new();
        TViewModel InvokeQuery<TInputModel, TViewModel>(TInputModel inputModel) where TViewModel : new();
    }

    public class Invoker : IInvoker
    {
        private readonly IObjectResolver _objectResolver;

        public Invoker(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public void InvokeCommand<TInputModel>(TInputModel inputModel)
        {
            var handler = _objectResolver.Resolve<ICommandHandler<TInputModel>>();
            handler.Handle(inputModel);
        }

        public TResultModel InvokeCommand<TInputModel, TResultModel>(TInputModel inputModel)
        {
            var handler = _objectResolver.Resolve<ICommandHandler<TInputModel, TResultModel>>();
            return handler.Handle(inputModel);
        }

        public TViewModel InvokeQuery<TViewModel>() where TViewModel : new()
        {
            var viewmodel = new TViewModel();
            var handler = _objectResolver.Resolve<IQueryHandler<TViewModel>>();
            handler.Handle(viewmodel);
            return viewmodel;
        }

        public TViewModel InvokeQuery<TInputModel, TViewModel>(TInputModel inputModel) where TViewModel : new()
        {
            var handler = _objectResolver.Resolve<IQueryHandler<TViewModel, TInputModel>>();
            return (TViewModel)handler.Handle(inputModel);
        }
    }

}