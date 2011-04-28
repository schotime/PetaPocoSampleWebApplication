namespace PetaPocoWebApplication.Infrastructure
{
    public interface ICommandHandler<TInputModel>
    {
        CommandResult Handle(TInputModel inputModel);
    }
}