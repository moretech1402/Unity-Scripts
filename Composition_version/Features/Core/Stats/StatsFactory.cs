using MC.Core.Stats.Definition;

namespace MC.Core.Stats
{
    public interface IStatsFactory
    {
        IStatsHandler CreateStatsHandlerFrom(IStatsContainerData container);
        StatId GetStatID(StatIdDefinitionSO stat);
    }
}
