namespace XFrame.Persistence.Queries.Filterings
{
    public class BaseFilter
    {
        #region Constructors

        internal BaseFilter()
        {
        }

        #endregion

        #region Static Methods

        public static BaseFilter operator &(BaseFilter expressionOne, BaseFilter expressionTwo)
        {
            if (expressionOne != null && expressionTwo != null)
            {
                return new AndFilter(expressionOne, expressionTwo);
            }
            if (expressionOne != null)
            {
                return expressionOne;
            }
            if (expressionTwo != null)
            {
                return expressionTwo;
            }
            return null;
        }

        public static BaseFilter BitwiseAnd(BaseFilter expressionOne, BaseFilter expressionTwo)
        {
            return expressionOne & expressionTwo;
        }

        public static BaseFilter operator |(BaseFilter expressionOne, BaseFilter expressionTwo)
        {
            if (expressionOne != null && expressionTwo != null)
            {
                return new OrFilter(expressionOne, expressionTwo);
            }
            if (expressionOne != null)
            {
                return expressionOne;
            }
            if (expressionTwo != null)
            {
                return expressionTwo;
            }
            return null;
        }

        public static BaseFilter BitwiseOr(BaseFilter expressionOne, BaseFilter expressionTwo)
        {
            return expressionOne | expressionTwo;
        }

        #endregion

        #region Methods

        public virtual IEnumerable<EqualityFilter> GetEqualityFilters()
        {
            return new EqualityFilter[] { };
        }

        #endregion
    }
}
