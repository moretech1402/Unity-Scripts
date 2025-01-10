using Stats;
using UnityEngine;

[CreateAssetMenu(menuName = "Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    [SerializeField] string _name;
//    [SerializeField] AspectSheet aspect;
    [SerializeField] StatsData stats;

    public string Name => _name;
//    public AspectSheet Aspect => aspect;
    public StatsData Stats => stats;
}
