using Core.Chemistry;
using UnityEngine;

namespace MC.Game.Alchemy
{
    public abstract class ChemicalSpeciesSO : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public int Value;

        public abstract string Formula { get; }

        public abstract AtomsAmountPair[] Atoms { get; }
    }

}
