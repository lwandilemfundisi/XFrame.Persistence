using System.Collections;
using XFrame.Common;
using XFrame.Common.Extensions;
using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Extensions
{
    public static class DomainCriteriaExtensions
    {
        public static void SafeAnd(this DomainCriteria value, EqualityFilter filter)
        {
            if (filter.IsNotNull())
            {
                if (filter.Value.IsNotNull())
                {
                    if (filter.Value is string && filter.Value.AsString().IsNullOrEmpty())
                    {
                        return;
                    }

                    value.Filter &= filter;
                }
            }
        }

        public static void SafeOr(this DomainCriteria value, EqualityFilter filter)
        {
            if (filter.IsNotNull())
            {
                if (filter.Value.IsNotNull())
                {
                    if (filter.Value is string && filter.Value.AsString().IsNullOrEmpty())
                    {
                        return;
                    }

                    value.Filter |= filter;
                }
            }
        }

        public static void RequiredAnd(this DomainCriteria value, EqualityFilter filter)
        {
            Invariant.IsFalse(filter.IsNull() || filter.Value.IsNull() || (filter.Value is string && filter.Value.AsString().IsNullOrEmpty()), () => "'{0}' filter is required".FormatInvariantCulture(filter.Property));
            value.SafeAnd(filter);
        }

        public static void SafeAnd(this DomainCriteria value, BaseFilter filter)
        {
            if (filter.IsNotNull())
            {
                value.Filter &= filter;
            }
        }

        public static string BuildQueryClause(this DomainCriteria value)
        {
            var query = "{0};{1}".FormatInvariantCulture(BuildFilter(value.Filter), BuildAggregations(value)).Trim(';');

            if (value.MaximumResult.IsNotNull())
            {
                query += ";TOP({0})".FormatInvariantCulture(value.MaximumResult);
            }

            if (value.SortOrder.IsNotNull())
            {
                query += ";OrderBy({0},{1})".FormatInvariantCulture(value.SortOrder.PropertyName, value.SortOrder.SortOrderType.ToString());
            }

            return query;
        }

        private static string BuildFilter(BaseFilter baseFilter)
        {
            var equalityFilter = baseFilter as EqualityFilter;

            if (equalityFilter.IsNotNull())
            {
                return "{0} {1} {2}".FormatInvariantCulture(equalityFilter.Property, GetOperand(equalityFilter.Filter), GetValue(equalityFilter));
            }

            var andfilter = baseFilter as AndFilter;

            if (andfilter.IsNotNull())
            {
                return "({0} AND {1})".FormatInvariantCulture(BuildFilter(andfilter.LeftFilter), BuildFilter(andfilter.RightFilter));
            }

            var orfilter = baseFilter as OrFilter;

            if (orfilter.IsNotNull())
            {
                return "({0} OR {1})".FormatInvariantCulture(BuildFilter(orfilter.LeftFilter), BuildFilter(orfilter.RightFilter));
            }

            var nullFilter = baseFilter as NullFilter;

            if (nullFilter.IsNotNull())
            {
                return "{0} null".FormatInvariantCulture(nullFilter.Property);
            }

            var aggregateFilter = baseFilter as AggregateFilter;

            if (aggregateFilter.IsNotNull())
            {
                return "{0}({1})".FormatInvariantCulture(aggregateFilter.AggregateType.ToString(), aggregateFilter.PropertyName);
            }

            return "NO FILTER";
        }

        private static string BuildAggregations(DomainCriteria domainCriteria)
        {
            var aggregations = string.Empty;

            if (domainCriteria.Aggregations.Count > 0)
            {
                aggregations = "AGGREGATES: ";

                foreach (var item in domainCriteria.Aggregations)
                {
                    aggregations += "{0}({1}),".FormatInvariantCulture(item.AggregateType.ToString(), item.PropertyName);
                }

            }

            return aggregations.TrimEnd(',');
        }

        private static string GetValue(EqualityFilter filter)
        {
            switch (filter.Filter)
            {
                case FilterType.Equal:
                case FilterType.NotEqual:
                case FilterType.GreaterThanOrEqualTo:
                case FilterType.LessThanOrEqualTo:
                case FilterType.Like:
                    {
                        return filter.Value.AsString();
                    }
                case FilterType.In:
                    {
                        var enumerable = filter.Value as IEnumerable;
                        var result = string.Empty;
                        if (enumerable.IsNotNull())
                        {
                            var enumerator = enumerable.GetEnumerator();
                            while (enumerator.MoveNext())
                            {
                                result += enumerator.Current.AsString() + ",";
                            }
                        }
                        return result.Trim(',');
                    }
                case FilterType.NotNull:
                    {
                        return "not null";
                    }
                case FilterType.Null:
                    {
                        return "null";
                    }
                default:
                    {
                        return filter.Value.AsString();
                    }
            }
        }

        private static string GetOperand(FilterType filterType)
        {
            switch (filterType)
            {
                case FilterType.Equal:
                    {
                        return "=";
                    }
                case FilterType.NotEqual:
                    {
                        return "!=";
                    }
                case FilterType.GreaterThanOrEqualTo:
                    {
                        return ">=";
                    }
                case FilterType.In:
                    {
                        return "in";
                    }
                case FilterType.LessThanOrEqualTo:
                    {
                        return "<=";
                    }
                case FilterType.Like:
                    {
                        return "like";
                    }
                case FilterType.NotNull:
                    {
                        return "not null";
                    }
                case FilterType.Null:
                    {
                        return "null";
                    }
                default:
                    {
                        return "UNKNOWN";
                    }
            }
        }
    }
}
