namespace PetaPocoWebApplication.Infrastructure
{
    public interface IQueryHandler<TViewModel> : IQueryHandler
    {
        TViewModel Handle();
    }
    public interface IQueryHandler<TInputModel, TViewModel> : IQueryHandler
    {
        TViewModel Handle(TInputModel inputmodel);
    }
    public interface IQueryHandler
    {
        object Handle();
        object Handle(object inputmodel);
    }
}