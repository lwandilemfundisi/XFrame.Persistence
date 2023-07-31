using XFrame.Common.Extensions;

namespace XFrame.Persistence.Queries.Filterings
{
    public class OrFilter : BaseFilter
    {
        #region Constructors

        public OrFilter(BaseFilter leftFilter, BaseFilter rightFilter)
        {
            LeftFilter = leftFilter;
            RightFilter = rightFilter;
        }

        #endregion

        #region Properties

        public BaseFilter LeftFilter { get; private set; }
        public BaseFilter RightFilter { get; private set; }

        #endregion

        #region Virtual Methods

        public override IEnumerable<EqualityFilter> GetEqualityFilters()
        {
            var filters = new List<EqualityFilter>();

            if (LeftFilter.IsNotNull())
            {
                filters.AddRange(LeftFilter.GetEqualityFilters());
            }

            if (RightFilter.IsNotNull())
            {
                filters.AddRange(RightFilter.GetEqualityFilters());
            }

            return filters;
        }

        public override string ToString()
        {
            return "({0} OR {1})".FormatInvariantCulture(LeftFilter, RightFilter);
        }

        #endregion
    }
}
