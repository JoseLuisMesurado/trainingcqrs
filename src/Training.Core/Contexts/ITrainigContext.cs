namespace Training.Core.Contexts
{
    public interface ITrainigContext 
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
