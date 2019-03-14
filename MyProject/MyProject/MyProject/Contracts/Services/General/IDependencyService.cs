namespace MyProject.Contracts.Services.General
{
    public interface IDependencyService
    {
        T Get<T>() where T : class;
    }
}
