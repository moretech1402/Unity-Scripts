using System.Collections.Generic;
using System.Linq;
using Core.Chemistry;
using UnityEngine;

namespace MC.Game.Alchemy
{
    [CreateAssetMenu(fileName = "Polymer", menuName = "Game/Alchemy/Polymer")]
    public class PolymerSO : ChemicalSpeciesSO
    {
        [SerializeField] MoleculeSOAmountPair[] _monomersComposition;

        public MoleculeSOAmountPair[] MonomersComposition => _monomersComposition;

        public override string Formula
        {
            get
            {
                // 1. Add the total composition of atoms.
                Dictionary<string, int> totalAtoms = new();

                foreach (var monomer in MonomersComposition)
                {
                    if (monomer == null) continue;

                    var molecule = monomer.Molecule;
                    if (molecule == null) continue;

                    var atoms = molecule.Atoms;
                    if (atoms == null || atoms.Length == 0) continue;

                    foreach (var atomPair in atoms)
                    {
                        var atomKey = atomPair.Atom.Key;
                        var totalAmount = atomPair.Amount * monomer.Amount;

                        if (totalAtoms.TryGetValue(atomKey, out int currentAmount))
                            totalAtoms[atomKey] = currentAmount + totalAmount;
                        else
                            totalAtoms[atomKey] = totalAmount;
                    }
                }

                // 2.Order atoms alphabetically for a standard formula (eg CH4, no H4C).
                var sortedAtoms = totalAtoms.OrderBy(kvp => kvp.Key);

                // 3. Build the formula chain.
                string formula = string.Empty;
                foreach (var kvp in sortedAtoms)
                {
                    formula += kvp.Key;
                    if (kvp.Value > 1)
                        formula += kvp.Value;
                }

                return formula;
            }
        }

        public override AtomsAmountPair[] Atoms
        {
            get
            {
                Dictionary<Atom, int> totalAtoms = new();

                foreach (var monomerPair in MonomersComposition)
                {
                    foreach (var atomPair in monomerPair.Atoms)
                    {
                        if (totalAtoms.ContainsKey(atomPair.Atom))
                            totalAtoms[atomPair.Atom] += atomPair.Amount;
                        else
                            totalAtoms[atomPair.Atom] = atomPair.Amount;
                    }
                }

                List<AtomsAmountPair> result = new();
                foreach (var kvp in totalAtoms)
                {
                    result.Add(new AtomsAmountPair() { Atom = kvp.Key, Amount = kvp.Value });
                }
                return result.ToArray();
            }
        }
    }

    [System.Serializable]
    public class MoleculeSOAmountPair
    {
        public MoleculeSO Molecule;
        public int Amount = 1;

        public override string ToString()
        {
            var formatedAmount = Amount > 1 ? Amount.ToString() : string.Empty;
            return $"{formatedAmount} {Molecule.Formula}";
        }

        public AtomsAmountPair[] Atoms
        {
            get
            {
                List<AtomsAmountPair> atoms = new();
                foreach (var atomPair in Molecule.Atoms)
                {
                    atoms.Add(
                        new AtomsAmountPair() { Atom = atomPair.Atom, Amount = atomPair.Amount * Amount });
                }
                return atoms.ToArray();
            }
        }
    }
}
