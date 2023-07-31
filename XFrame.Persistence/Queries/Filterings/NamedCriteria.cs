using XFrame.Common.Extensions;

namespace XFrame.Persistence.Queries.Filterings
{
    public class NamedCriteria
    {
        #region Constructors

        public NamedCriteria()
        {
            Parameters = new Dictionary<string, object>();
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public int? MaximumResult { get; set; }

        public int? FirstResult { get; set; }

        public IDictionary<string, object> Parameters { get; set; }

        #endregion

        #region Methods

        public string BuildQueryString()
        {
            return ToString();
        }

        #endregion

        #region Virtual Methods

        public override string ToString()
        {
            var result = "{0}:".FormatInvariantCulture(Name);

            if (MaximumResult.HasValue)
            {
                result += "(MaximumResult: {0})".FormatInvariantCulture(MaximumResult);
            }

            if (FirstResult.HasValue)
            {
                result += "(FirstResult: {0})".FormatInvariantCulture(FirstResult);
            }

            foreach (var item in Parameters)
            {
                result += "({0}:{1})".FormatInvariantCulture(item.Key, item.Value);
            }

            return result;
        }

        #endregion
    }
}
