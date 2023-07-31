using XFrame.Common;
using XFrame.Common.Extensions;
using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Queries.NamedQueries
{
    public abstract class StoredProcedureQuery<TModel, TNamedCriteria> : IDomainQuery, IStoredProcedureQuery<TNamedCriteria>
        where TModel : class
        where TNamedCriteria : NamedCriteria, new()
    {
        public StoredProcedureQuery()
        {
            MaximumResults = 1000;
        }

        public abstract string Name { get; }

        protected virtual string[] OptionalParameters
        {
            get
            {
                return new string[] { };
            }
        }

        protected virtual bool SkipOptionalParameterCheck
        {
            get
            {
                return false;
            }
        }

        public bool SingleResult { get; set; }

        public int? MaximumResults { get; set; }

        public int? FirstResult { get; set; }

        public TNamedCriteria BuildNamedCriteria()
        {
            return new TNamedCriteria()
            {
                Name = Name,
                MaximumResult = MaximumResults,
                FirstResult = FirstResult,
                Parameters = BuildAndValidateParameters()
            };
        }

        public IDictionary<string, object> BuildParameters()
        {
            var parameters = new Dictionary<string, object>();
            OnBuildParameters(parameters);
            return parameters;
        }

        public IDictionary<string, object> BuildAndValidateParameters()
        {
            var parameters = BuildParameters();

            if (!SkipOptionalParameterCheck)
            {
                foreach (var item in parameters)
                {
                    if (!OptionalParameters.Contains(item.Key))
                    {
                        Invariant.ArgumentNotEmpty(item.Value.AsString(), () => "Parameter '{0}' for named query '{1}' may not be null or empty".FormatInvariantCulture(item.Key, Name));
                    }
                }
            }

            return parameters;
        }

        public virtual void OnBuildParameters(IDictionary<string, object> parameters) { }
    }
}
