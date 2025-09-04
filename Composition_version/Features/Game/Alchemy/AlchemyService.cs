using System.Collections.Generic;
using Core.Chemistry;
using MC.Game.Inventory;
using MC.Game.Score;

namespace MC.Game.Alchemy
{
    public interface IAlchemyService
    {
        int CurrentCombo { get; }

        void End(InventoryItem[] inventoryItems);
        ChemicalSpeciesSO[] GetMonomers(ChemicalSpeciesSO product);
        int GetTotalScore();
        bool IsAdjusted(ChemistryReaction reaction);
        bool IsTutorialCompleted();
        void Reset();
        void SuccessfulReaction();
        void UnsuccessfulReaction();
    }

    public class AlchemyService : IAlchemyService
    {
        readonly IChemistryService _chemistryService;
        readonly IScoreService _scoreService;

        int _successfulReactions = 0;

        public AlchemyService(IChemistryService chemistryService, IScoreService scoreService)
        {
            _chemistryService = chemistryService;
            _scoreService = scoreService;
        }

        public int CurrentCombo => _scoreService.CurrentCombo;

        public void End(InventoryItem[] bag)
        {
            foreach (var item in bag)
            {
                _scoreService.AddPoints(item.Value);
            }
        }

        public ChemicalSpeciesSO[] GetMonomers(ChemicalSpeciesSO product)
        {
            if (product is MoleculeSO || product is AtomSO)
            {
                return new ChemicalSpeciesSO[] { product };
            }

            if (product is PolymerSO polymer)
            {
                List<ChemicalSpeciesSO> monomers = new();
                foreach (var monomerPair in polymer.MonomersComposition)
                {
                    monomers.Add(monomerPair.Molecule);
                }
                return monomers.ToArray();
            }

            return new ChemicalSpeciesSO[0];
        }

        public int GetTotalScore() => _scoreService.TotalScore;

        public bool IsAdjusted(ChemistryReaction reaction)
        {
            bool isAdjusted = _chemistryService.IsAdjusted(reaction);
            if (isAdjusted) _successfulReactions++;
            return isAdjusted;
        }

        public bool IsTutorialCompleted() => _successfulReactions > 0;

        public void Reset()
        {
            _successfulReactions = 0;
            _scoreService.Reset();
        }

        public void SuccessfulReaction() => _scoreService.AddSuccessful();

        public void UnsuccessfulReaction() => _scoreService.AddFailed();
    }
}
