using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.CriteriaQueries
{
    public interface ICriteriaDomainQueryHandler<TDomain, TDomainCriteria>
        where TDomain : class
        where TDomainCriteria : DomainCriteria
    {
        Task<IEnumerable<TDomain>> ExecuteCriteria(ICriteriaDomainQuery<TDomainCriteria> query);

        IEnumerable<TDomain> OnFindAll(IEnumerable<TDomain> results);

        Task<TDomain> Find(ICriteriaDomainQuery<TDomainCriteria> query);

        Task<IEnumerable<TDomain>> FindAll(ICriteriaDomainQuery<TDomainCriteria> query);
    }
}
