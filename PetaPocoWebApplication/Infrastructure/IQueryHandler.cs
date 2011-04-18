namespace PetaPocoWebApplication.Infrastructure
{
    public interface IQueryHandler<TViewModel>
    {
        void Handle(TViewModel viewmodel);
    }
}