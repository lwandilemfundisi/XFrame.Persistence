namespace XFrame.Persistence
{
    public interface IPersistenceConfiguration
    {
        void ConfigurePersistence(IServiceProvider serviceProvider);
    }
}
