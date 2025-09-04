using NUnit.Framework;

namespace Core.Chemistry.Tests
{
    public class ChemistryServiceTest
    {
        IChemistryService _chemistryService;

        [SetUp]
        public void Setup()
        {
            _chemistryService = new ChemistryService();
        }

        public class AdjustedReactionStub : IChemistryReaction
        {
            public MoleculesAmountPair[] Reactants { get; }
            public MoleculesAmountPair[] Products { get; }

            public AdjustedReactionStub(
                MoleculesAmountPair[] reactants = null, MoleculesAmountPair[] products = null)
            {
                Reactants = reactants ?? GenerateReactants();
                Products = products ?? GenerateProducts();
            }

            // 2H₂ + O₂ → 2H₂O
            private MoleculesAmountPair[] GenerateReactants()
            {
                var h2 = new Molecule(new[] {
                    new AtomsAmountPair { Atom = new Atom { Key = "H" }, Amount = 2 } }
                );
                var o2 = new Molecule(new[] {
                    new AtomsAmountPair { Atom = new Atom { Key = "O" }, Amount = 2 } }
                );

                return new[] {
                    new MoleculesAmountPair { Molecule = h2, Amount = 2 },
                    new MoleculesAmountPair { Molecule = o2, Amount = 1 } };
            }

            private MoleculesAmountPair[] GenerateProducts()
            {
                var h2o = new Molecule(new[] {
                    new AtomsAmountPair { Atom = new Atom { Key = "H" }, Amount = 2 },
                    new AtomsAmountPair { Atom = new Atom { Key = "O" }, Amount = 1 } }
                );

                return new[] { new MoleculesAmountPair { Molecule = h2o, Amount = 2 } };
            }
        }

        public class UnadjustedReactionStub : IChemistryReaction
        {
            public MoleculesAmountPair[] Reactants { get; }
            public MoleculesAmountPair[] Products { get; }

            public UnadjustedReactionStub()
            {
                // H₂ + O₂ → H₂O
                var h2 = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "H" }, Amount = 2 } });
                var o2 = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "O" }, Amount = 2 } });
                var h2o = new Molecule(
                    new[] {
                        new AtomsAmountPair { Atom = new Atom { Key = "H" }, Amount = 2 },
                        new AtomsAmountPair { Atom = new Atom { Key = "O" }, Amount = 1 } });

                Reactants = new[] { new MoleculesAmountPair { Molecule = h2, Amount = 1 }, new MoleculesAmountPair { Molecule = o2, Amount = 1 } };
                Products = new[] { new MoleculesAmountPair { Molecule = h2o, Amount = 1 } };
            }
        }

        public class ReactantsStubFactory
        {
            public static MoleculesAmountPair[] Get(int number = 1)
            {
                return number switch
                {
                    1 => GetFirst(),
                    2 => GetSecond(),
                    _ => new AdjustedReactionStub().Reactants,
                };
            }

            private static MoleculesAmountPair[] GetFirst()
            {
                var dr = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "Dr" }, Amount = 1 } });
                var te = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "Te" }, Amount = 1 } });
                var he = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "He" }, Amount = 1 } });

                return new[] {
                    new MoleculesAmountPair { Molecule = dr, Amount = 4 },
                    new MoleculesAmountPair { Molecule = te, Amount = 4 },
                    new MoleculesAmountPair { Molecule = he, Amount = 2 }
                };
            }

            // 8Dr + 4He + 2Te2 -> 
            private static MoleculesAmountPair[] GetSecond()
            {
                var dr = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "Dr" }, Amount = 1 } });
                var he = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "He" }, Amount = 1 } });
                var te2 = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "Te" }, Amount = 2 } });

                return new[] {
                    new MoleculesAmountPair { Molecule = dr, Amount = 8 },
                    new MoleculesAmountPair { Molecule = he, Amount = 4 },
                    new MoleculesAmountPair { Molecule = te2, Amount = 2 },
                };
            }
        }

        public class ProductsStubFactory
        {
            public static MoleculesAmountPair[] Get(int number = 1)
            {
                return number switch
                {
                    1 => GetFirst(),
                    2 => GetSecond(),
                    _ => new AdjustedReactionStub().Products,
                };
            }

            // 2Te2 + 2Dr2He
            private static MoleculesAmountPair[] GetFirst()
            {
                var te2 = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "Te" }, Amount = 2 } });
                var dr2He = new Molecule( new[] {
                        new AtomsAmountPair { Atom = new Atom { Key = "Dr" }, Amount = 2 },
                        new AtomsAmountPair { Atom = new Atom { Key = "He" }, Amount = 1 } });

                return new[] {
                    new MoleculesAmountPair { Molecule = te2, Amount = 2 },
                    new MoleculesAmountPair { Molecule = dr2He, Amount = 2 },
                };
            }

            // -> 4Dr2He + 2Te2
            private static MoleculesAmountPair[] GetSecond()
            {
                var dr2He = new Molecule(new[] {
                        new AtomsAmountPair { Atom = new Atom { Key = "Dr" }, Amount = 2 },
                        new AtomsAmountPair { Atom = new Atom { Key = "He" }, Amount = 1 },
                    });
                var te2 = new Molecule(
                    new[] { new AtomsAmountPair { Atom = new Atom { Key = "Te" }, Amount = 2 } });

                return new[] {
                    new MoleculesAmountPair { Molecule = dr2He, Amount = 4 },
                    new MoleculesAmountPair { Molecule = te2, Amount = 2 },
                };
            }
        }

        [
            TestCase(-1, -1),
            TestCase(1, 1),
            TestCase(2, 2),
        ]
        public void WhenReceiveAdjustedReaction_ShouldReturnTrue(int reactantsNumber, int productsNumber)
        {
            var reactants = ReactantsStubFactory.Get(reactantsNumber);
            var products = ProductsStubFactory.Get(productsNumber);
            Assert.IsTrue(_chemistryService.IsAdjusted(new ChemistryReaction(reactants, products)));
        }

        [Test]
        public void WhenReceiveUnadjustedReaction_ShouldReturnFalse()
        {
            var reaction = new UnadjustedReactionStub();
            Assert.IsFalse(_chemistryService.IsAdjusted(reaction));
        }
    }
}