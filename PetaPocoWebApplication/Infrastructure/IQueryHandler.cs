namespace PetaPocoWebApplication.Infrastructure
{
    public interface IQueryHandler<TViewModel>
    {
        void Handle(TViewModel viewmodel);
    }
    public interface IQueryHandler<TInputModel, TViewModel>
    {
        void Handle(TViewModel viewmodel, TInputModel inputmodel);
    }
}