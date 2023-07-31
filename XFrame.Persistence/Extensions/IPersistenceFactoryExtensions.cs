using XFrame.Common;
using XFrame.Common.Extensions;
using XFrame.Persistence.Queries.Filterings;

namespace XFrame.Persistence.Extensions
{
    public static class IPersistenceFactoryExtensions
    {
        public static async Task SaveAsync<TDomainCriteria>(this IPersistenceFactory persistanceFactory, object entity)
            where TDomainCriteria : DomainCriteria
        {
            Invariant.IsNotNull(persistanceFactory, () => $"IPersistenceFactory null in '{typeof(IPersistenceFactoryExtensions).PrettyPrint()}'");
            await persistanceFactory.GetPersistence(entity.GetType()).Save(entity, CancellationToken.None);
        }

        public static async Task SaveAsync<TDomain, TDomainCriteria>(this IPersistenceFactory persistanceFactory, TDomain entity)
            where TDomain : class
            where TDomainCriteria : DomainCriteria
        {
            Invariant.IsNotNull(persistanceFactory, () => $"IPersistenceFactory null in '{typeof(IPersistenceFactoryExtensions).PrettyPrint()}'");
            await persistanceFactory.GetPersistence<TDomain>().Save(entity, CancellationToken.None);
        }
    }
}
