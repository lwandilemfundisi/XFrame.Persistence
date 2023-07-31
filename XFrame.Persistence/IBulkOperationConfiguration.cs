namespace XFrame.Persistence
{
    public interface IBulkOperationConfiguration
    {
        int DeletionBatchSize { get; }
    }
}
