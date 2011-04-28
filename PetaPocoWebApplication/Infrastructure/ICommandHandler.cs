namespace PetaPocoWebApplication.Infrastructure
{
    public interface ICommandHandler<TInputModel>
    {
        void Handle(TInputModel inputModel);
    }
    public interface ICommandHandler<TInputModel, TResultModel>
    {
        TResultModel Handle(TInputModel inputModel);
    }
}