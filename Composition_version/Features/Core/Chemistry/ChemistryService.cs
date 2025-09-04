using System.Collections.Generic;
using System.Linq;

namespace Core.Chemistry
{
    public interface IChemistryService
    {
        bool IsAdjusted(IChemistryReaction reaction);
    }

    public class ChemistryService : IChemistryService
    {
        public bool IsAdjusted(IChemistryReaction reaction)
        {
            var reactantAtoms = CountAtoms(reaction.Reactants);
            var productAtoms = CountAtoms(reaction.Products);

            return reactantAtoms.Count == productAtoms.Count &&
                   reactantAtoms.All(kvp => productAtoms.TryGetValue(kvp.Key, out int count) && count == kvp.Value);
        }

        private static Dictionary<string, int> CountAtoms(MoleculesAmountPair[] pairs)
        {
            Dictionary<string, int> atomCounts = new();

            foreach (var moleculePair in pairs)
            {
                foreach (var atomPair in moleculePair.Molecule.Composition)
                {
                    var atomName = atomPair.Atom.Key;
                    var totalAmount = atomPair.Amount * moleculePair.Amount;

                    if (atomCounts.ContainsKey(atomName))
                        atomCounts[atomName] += totalAmount;
                    else
                        atomCounts[atomName] = totalAmount;
                }
            }
            return atomCounts;
        }
    }

    public interface IChemistryReaction
    {
        MoleculesAmountPair[] Reactants { get; }
        MoleculesAmountPair[] Products { get; }
    }

    public class ChemistryReaction : IChemistryReaction
    {
        public MoleculesAmountPair[] Reactants { get; private set; }
        public MoleculesAmountPair[] Products { get; private set; }

        public ChemistryReaction(MoleculesAmountPair[] reactants, MoleculesAmountPair[] products)
        {
            Reactants = reactants;
            Products = products;
        }
    }

    public struct MoleculesAmountPair
    {
        public Molecule Molecule;
        public int Amount;
    }

    public class Molecule
    {
        public AtomsAmountPair[] Composition { get; }

        public Molecule(AtomsAmountPair[] composition)
        {
            Composition = composition;
        }
    }

    public struct AtomsAmountPair
    {
        public Atom Atom;
        public int Amount;
    }

    public class Atom
    {
        public string Key;
        public string Name;
    }

}
