namespace XFrame.Persistence
{
    public interface IPersistenceFactory
    {
        IPersistence GetPersistence<TDomain>() where TDomain : class;
        IPersistence GetPersistence(Type type);
    }
}
