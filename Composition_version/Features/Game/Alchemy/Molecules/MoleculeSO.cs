using System;
using System.Linq;
using Core.Chemistry;
using UnityEngine;

namespace MC.Game.Alchemy
{
    [CreateAssetMenu(fileName = "Molecule", menuName = "Game/Alchemy/Molecule")]
    public class MoleculeSO : ChemicalSpeciesSO
    {
        [SerializeField] AtomSOAmountPair[] _composition;

        public AtomSOAmountPair[] Composition => _composition;

        public override string Formula
        {
            get
            {
                string formula = string.Empty;
                foreach (var pair in Composition)
                {
                    formula += pair.ToString();
                }
                return formula;
            }
        }

        public override AtomsAmountPair[] Atoms => AtomSOAmountPair.ToDomain(_composition);

        public Molecule ToDomain() => new(Atoms);
    }

    [Serializable]
    public class AtomSOAmountPair
    {
        public AtomSO Atom;
        public int Amount;

        public override string ToString()
        {
            var formatedAmount = Amount > 1 ? Amount.ToString() : string.Empty;
            return $"{Atom.Key}<sub>{formatedAmount}</sub>";
        }

        public static AtomsAmountPair[] ToDomain(AtomSOAmountPair[] pairs) =>
            pairs.Select(pair => pair.ToDomain()).ToArray();

        public AtomsAmountPair ToDomain() => new() { Atom = Atom.ToDomain(), Amount = Amount };
    }
}
