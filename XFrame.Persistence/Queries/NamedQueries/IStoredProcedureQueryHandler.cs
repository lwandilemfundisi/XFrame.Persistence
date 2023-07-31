using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.NamedQueries
{
    public interface IStoredProcedureQueryHandler<TDomain, TNamedCriteria>
        where TDomain : class
        where TNamedCriteria : NamedCriteria
    {
        Task<TDomain> Find(IStoredProcedureQuery<TNamedCriteria> namedQuery);

        Task<IEnumerable<TDomain>> FindAll(IStoredProcedureQuery<TNamedCriteria> namedQuery);
    }
}
