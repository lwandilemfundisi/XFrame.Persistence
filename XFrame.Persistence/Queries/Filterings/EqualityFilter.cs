using System.Collections;
using XFrame.Common.Extensions;

namespace XFrame.Persistence.Queries.Filterings
{
    public class EqualityFilter : BaseFilter
    {
        #region Constructors

        public EqualityFilter(string property, object value)
            : this(property, value, FilterType.Equal, string.Empty)
        {
        }

        public EqualityFilter(string property, object value, string alias)
            : this(property, value, FilterType.Equal, alias)
        {
        }

        public EqualityFilter(string property, object value, FilterType filter)
            : this(property, value, filter, string.Empty)
        {
        }

        public EqualityFilter(string property, FilterType filter)
            : this(property, null, filter, string.Empty)
        {
        }

        public EqualityFilter(string property, object value, FilterType filter, string alias)
        {
            Property = property;
            Value = value;
            Filter = filter;
            Alias = alias;
        }

        #endregion

        #region Properties

        public FilterType Filter { get; private set; }

        public string Property { get; private set; }

        public object Value { get; private set; }

        public string Alias { get; private set; }

        #endregion

        #region Virtual Methods

        public override IEnumerable<EqualityFilter> GetEqualityFilters()
        {
            return new EqualityFilter[] { this };
        }

        #endregion

        #region Virtual Methods

        public override string ToString()
        {
            return "({0} {1} {2})".FormatInvariantCulture(Property, Filter.ToString().ToUpperInvariant(), ToStringValue());
        }

        #endregion

        #region Private Methods

        private string ToStringValue()
        {
            if (Filter == FilterType.In)
            {
                var enumerable = Value as IEnumerable;

                if (enumerable.IsNotNull())
                {
                    return enumerable.EnumerateObjects().ToCSV(o => o.AsString());
                }
            }

            return Value.AsString();
        }

        #endregion
    }
}
