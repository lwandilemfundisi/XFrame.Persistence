using XFrame.Common;
using XFrame.Common.Extensions;
using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.CriteriaQueries
{
    public abstract class CriteriaDomainQueryHandler<TDomain, TDomainCriteria> : ICriteriaDomainQueryHandler<TDomain, TDomainCriteria>
        where TDomain : class
        where TDomainCriteria : DomainCriteria
    {
        private readonly IPersistenceFactory _persistenceFactory;

        public CriteriaDomainQueryHandler(IPersistenceFactory persistenceFactory)
        {
            _persistenceFactory = persistenceFactory;
        }

        public IPersistence Repository
        {
            get
            {
                return _persistenceFactory.GetPersistence<TDomain>();
            }
        }

        public async Task<IEnumerable<TDomain>> ExecuteCriteria(ICriteriaDomainQuery<TDomainCriteria> query)
        {
            var results = await ExecuteQueryResults(query.BuildDomainCriteria());

            if (query.SingleResult)
            {
                VerifyOneResult(results);
            }

            return results;
        }

        public virtual IEnumerable<TDomain> OnFindAll(IEnumerable<TDomain> results)
        {
            return results;
        }

        public async Task<TDomain> Find(ICriteriaDomainQuery<TDomainCriteria> query)
        {
            var results = await FindAll(query);
            VerifyOneResult(results);
            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<TDomain>> FindAll(ICriteriaDomainQuery<TDomainCriteria> query)
        {
            return OnFindAll(await ExecuteCriteria(query));
        }

        #region Private Methods

        private async Task<IEnumerable<TDomain>> ExecuteQueryResults(TDomainCriteria domainCriteria)
        {
            return await Repository.GetAll<TDomain, TDomainCriteria>(domainCriteria, CancellationToken.None);
        }

        private void VerifyOneResult(IEnumerable<TDomain> results)
        {
            Invariant.IsFalse(results.Count() > 1, () => "More than one result found for query: {0}".FormatInvariantCulture(this));
        }

        #endregion
    }
}
