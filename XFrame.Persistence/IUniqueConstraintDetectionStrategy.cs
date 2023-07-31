namespace XFrame.Persistence
{
    public interface IUniqueConstraintDetectionStrategy
    {
        bool IsUniqueConstraintViolation(Exception exception);
    }
}
