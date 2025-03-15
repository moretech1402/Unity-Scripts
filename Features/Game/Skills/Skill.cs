using System;
using UnityEngine;

namespace Skills
{
    [Serializable]
    public class Skill{
        [SerializeField] SkillData data;
        [SerializeField] int level, skillPoints;
    }

    public abstract class SkillData : ScriptableObject
    {
        [SerializeField] protected string _name = "Bola de fuego";
    }

}
