using System.Collections.Generic;
using System.Linq;

namespace MC.Game.Characters
{
    public interface ICharacterService
    {
        ClassSO[] ChangeableClasses { get; }
        ClassSO[] InStorageClasses { get; }

        bool TryEquip(ClassSO @class);
        void SaveInStorage(ClassSO @class);
        bool ValidateCurrentClasses();
    }

    public class CharacterService : ICharacterService
    {
        const int _maxChangeableClasses = 3;
        const int _initialClassIndex = 1;

        public ClassSO[] ChangeableClasses { get; } = new ClassSO[_maxChangeableClasses];

        private readonly List<ClassSO> _inStorageClasses;
        public ClassSO[] InStorageClasses => _inStorageClasses.ToArray();

        readonly Character _playerCharacter;

        public CharacterService(ClassSO[] availableClasses)
        {
            for (int i = 0; i < availableClasses.Length && i < _maxChangeableClasses; i++)
            {
                ChangeableClasses[i] = availableClasses[i];
            }

            _inStorageClasses = new(availableClasses.Length - ChangeableClasses.Length);
            for (int i = _maxChangeableClasses; i < availableClasses.Length; i++)
            {
                _inStorageClasses.Add(availableClasses[i]);
            }

            _playerCharacter = new Character(ChangeableClasses[_initialClassIndex]);
        }

        public bool TryEquip(ClassSO @class)
        {
            if (!ChangeableClasses.Contains(null)) return false;

            _inStorageClasses.Remove(@class);
            for (int i = 0; i < ChangeableClasses.Length; i++)
            {
                if (ChangeableClasses[i] == null)
                {
                    ChangeableClasses[i] = @class;
                    return true;
                }
            }
            
            return false;
        }

        public void SaveInStorage(ClassSO @class)
        {
            if (_playerCharacter.Class == @class) _playerCharacter.Class = null;

            for (int i = 0; i < ChangeableClasses.Length; i++)
            {
                if (ChangeableClasses[i] == @class)
                {
                    ChangeableClasses[i] = null;
                    break;
                }
            }

            _inStorageClasses.Add(@class);
        }

        public bool ValidateCurrentClasses() =>
            ChangeableClasses.Count() == _maxChangeableClasses && !ChangeableClasses.Any(c => c == null);
    }

    public class Character
    {
        public ClassSO Class { get; set; }

        public Character(ClassSO equippedClass)
        {
            Class = equippedClass;
        }
    }
}

