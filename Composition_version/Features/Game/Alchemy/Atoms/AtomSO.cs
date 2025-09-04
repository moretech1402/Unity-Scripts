using Core.Chemistry;
using UnityEngine;

namespace MC.Game.Alchemy
{
    [CreateAssetMenu(fileName = "Atom", menuName = "Game/Alchemy/Atom")]
    public class AtomSO : ChemicalSpeciesSO
    {
        [SerializeField] string _key;

        public string Key => _key;

        public override string Formula => Key;

        public override AtomsAmountPair[] Atoms =>
            new AtomsAmountPair[] { new(){ Atom = ToDomain(), Amount = 1 } };

        public Atom ToDomain() => new() { Key = Key, Name = Name };
    }
}
