using UnityEngine;

namespace Ailments
{
    [CreateAssetMenu(menuName = "Ailments/Damage")]
    public class AilmentDamage : Ailment
    {
        [SerializeField] string damageFormula = "a.hp * 0.05";
        public override void Apply()
        {
            throw new System.NotImplementedException();
        }
    }

}
