using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.NamedQueries
{
    public interface IStoredProcedureQuery<TNamedCriteria> : IDomainQuery
        where TNamedCriteria : NamedCriteria
    {
        string Name { get; }

        TNamedCriteria BuildNamedCriteria();
    }
}
