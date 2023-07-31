using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.CriteriaQueries
{
    public interface ICriteriaDomainQuery<TDomainCriteria> : IDomainQuery
        where TDomainCriteria : DomainCriteria
    {
        TDomainCriteria BuildDomainCriteria();
    }
}
