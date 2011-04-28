using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetaPocoWebApplication.Infrastructure
{
    public interface ICommandInvoker
    {
        CommandResult Invoke<TInputModel>(TInputModel inputModel);
    }

    public class CommandInvoker : ICommandInvoker
    {
        private readonly IObjectResolver _objectResolver;

        public CommandInvoker(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public CommandResult Invoke<TInputModel>(TInputModel inputModel)
        {
            var handler = _objectResolver.Resolve<ICommandHandler<TInputModel>>();
            return handler.Handle(inputModel);
        }
    }

    public class CommandResult
    {
        public bool Success { get; set; }

    }
}