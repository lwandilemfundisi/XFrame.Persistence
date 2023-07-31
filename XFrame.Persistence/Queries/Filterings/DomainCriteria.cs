using XFrame.Common.Extensions;

namespace XFrame.Persistence.Queries.Filterings
{
    public class DomainCriteria
    {
        #region Constructors

        public DomainCriteria(BaseFilter filter)
            : this()
        {
            Filter = filter;
        }

        public DomainCriteria()
        {
            AssociatedFilters = new Dictionary<string, DomainCriteria>();
            Aggregations = new List<AggregateFilter>();
            OptimiseQuery = false;
            SelectDistinct = true;
        }

        #endregion

        #region Methods

        public bool IsEmpty()
        {
            if (Filter.IsNull())
            {
                var isEmpty = true;

                foreach (var item in AssociatedFilters.Values)
                {
                    isEmpty &= item.IsEmpty();

                    if (!isEmpty)
                    {
                        break;
                    }
                }

                return isEmpty;
            }

            return false;
        }

        #endregion

        #region Properties

        public bool OptimiseQuery { get; set; }

        public SortOrder SortOrder { get; set; }

        public bool IsValid
        {
            get
            {
                return Filter != null || AssociatedFilters.Count > 0;
            }
        }

        public bool SelectDistinct { get; set; }

        public BaseFilter Filter { get; set; }

        public IDictionary<string, DomainCriteria> AssociatedFilters { get; private set; }

        public IList<AggregateFilter> Aggregations { get; private set; }

        public int? MaximumResult { get; set; }

        public int? FirstResult { get; set; }

        #endregion

        #region Virtual Methods

        public override string ToString()
        {
            var result = string.Empty;

            if (OptimiseQuery)
            {
                result += "(OptimiseQuery: {0})".FormatInvariantCulture(OptimiseQuery);
            }

            if (SortOrder.IsNotNull())
            {
                result += "(SortOrder: {0})".FormatInvariantCulture(SortOrder);
            }

            if (!SelectDistinct)
            {
                result += "(SelectDistinct: {0})".FormatInvariantCulture(SelectDistinct);
            }

            if (MaximumResult.HasValue)
            {
                result += "(MaximumResult: {0})".FormatInvariantCulture(MaximumResult);
            }

            if (FirstResult.HasValue)
            {
                result += "(FirstResult: {0})".FormatInvariantCulture(FirstResult);
            }

            result += "{0}".FormatInvariantCulture(Filter);

            foreach (var item in AssociatedFilters)
            {
                result += "(AssociatedFilters for {0}: {1})".FormatInvariantCulture(item.Key, item);
            }

            foreach (var item in Aggregations)
            {
                result += "(Aggregation: {0})".FormatInvariantCulture(item);
            }

            return result;
        }

        #endregion
    }
}
