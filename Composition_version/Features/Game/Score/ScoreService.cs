namespace MC.Game.Score
{
    public interface IScoreService
    {
        public int TotalScore { get; }
        int CurrentCombo { get; }

        public void AddFailed();
        public void AddPoints(int points);
        public void AddSuccessful();
        void Reset();
    }

    public class ScoreService : IScoreService
    {
        const int BaseComboPoints = 50;

        public int CurrentCombo { get; private set; } = 0;

        public int PointsFromItems { get; private set; } = 0;
        public int PointsFromCombo { get; private set; } = 0;

        public int TotalScore => PointsFromItems + PointsFromCombo;


        public void AddFailed()
        {
            CurrentCombo = 0;
        }

        public void AddPoints(int points) => PointsFromItems += points;

        public void AddSuccessful()
        {
            CurrentCombo++;
            PointsFromCombo += BaseComboPoints * CurrentCombo;
        }

        public void Reset()
        {
            CurrentCombo = 0;
            PointsFromItems = 0;
            PointsFromCombo = 0;
        }
    }

}
