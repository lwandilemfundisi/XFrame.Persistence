using System.Security.Principal;
using XFrame.Common;
using XFrame.Common.Extensions;
using XFrame.Persistence.Extensions;
using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.CriteriaQueries
{
    public abstract class CriteriaDomainQuery<TDomain, TDomainCriteria> : IDomainQuery, ICriteriaDomainQuery<TDomainCriteria>
        where TDomain : class
        where TDomainCriteria : DomainCriteria, new()
    {
        private SortOrder sortOrder;
        private TDomainCriteria criteria;
        private IList<AggregateFilter> aggregations;

        public CriteriaDomainQuery()
        {
            MaximumResults = 1000;
        }

        #region Properties

        public IIdentity Id { get; set; }

        public IEnumerable<IIdentity> Ids { get; set; }

        public IEnumerable<IIdentity> NotEqualIds { get; set; }

        public bool SingleResult { get; set; }

        public int? MaximumResults { get; set; }

        public int? FirstResult { get; set; }

        protected virtual bool FailOnNoCriteriaSpecified
        {
            get
            {
                return false;
            }
        }

        private IList<AggregateFilter> Aggregations
        {
            get
            {
                if (aggregations.IsNull())
                {
                    aggregations = new List<AggregateFilter>();
                }

                return aggregations;
            }
        }

        #endregion

        #region ICriteriaDomainQuery Members

        public TDomainCriteria BuildDomainCriteria()
        {
            criteria = new TDomainCriteria
            {
                MaximumResult = MaximumResults,
                FirstResult = FirstResult,
                SortOrder = sortOrder
            };
            criteria.SafeAnd(new EqualityFilter("Id", Id));

            if (Ids.IsNotNull() && Ids.Count() > 0)
            {
                criteria.SafeAnd(new EqualityFilter("Id", Ids, FilterType.In));
            }

            if (NotEqualIds.IsNotNull() && NotEqualIds.Count() > 0)
            {
                criteria.SafeAnd(new EqualityFilter("Id", NotEqualIds, FilterType.NotIn));
            }

            criteria.Aggregations.AddRange(Aggregations);

            OnBuildDomainCriteria(criteria);

            if (FailOnNoCriteriaSpecified)
            {
                Invariant.IsFalse(criteria.IsEmpty(), () => "Criteria requires at least one filter");
            }

            return criteria;
        }

        #endregion

        #region Virtual Methods

        protected virtual void OnBuildDomainCriteria(TDomainCriteria domainCriteria)
        {
        }

        #endregion
    }
}
