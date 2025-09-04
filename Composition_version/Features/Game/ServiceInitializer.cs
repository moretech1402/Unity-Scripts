using MC.Game.Inventory;
using MC.Core;
using MC.Core.Unity.Animations;
using UnityEngine;
using MC.Game.Alchemy;
using Core.Chemistry;
using MC.Game.Score;

namespace MC.Game
{
    public class ServiceInitializer : MonoBehaviour
    {
        [SerializeField] ChemicalSpeciesSO[] _inventoryChemicalsAvailable;

        void Awake()
        {
            ServiceLocator.Register<ITweenService>(new DOTweenService());

            ServiceLocator.Register<IInventoryService>(new InventoryService(_inventoryChemicalsAvailable));
            ServiceLocator.Register<IAlchemyService>(new AlchemyService(
                new ChemistryService(), new ScoreService()));
        }
    }

}
