namespace MC.Core.Stats
{
    public interface IStatsHandler
    {
        float GetFinal(StatId stat);
        void Consume(StatId stat, float delta);
        void Modify(StatId stat, float amount);
        void ModifyCurrent(StatId stat, float amount);
    }
}