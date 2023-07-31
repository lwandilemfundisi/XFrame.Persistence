namespace XFrame.Persistence.Queries
{
    public interface IDomainQuery
    {
        public bool SingleResult { get; set; }

        public int? MaximumResults { get; set; }

        public int? FirstResult { get; set; }
    }
}
