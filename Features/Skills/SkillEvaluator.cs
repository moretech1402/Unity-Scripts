using System.Collections.Generic;
using Core;
using Skills;
using Stats;
using Unity.VisualScripting;
using UnityEngine;

public class SkillEvaluator : Core.Singleton<SkillEvaluator>
{
    [SerializeField] ActiveSkill skill;
    [SerializeField] StatsHandlerGO userStats, targetStats;

    [Header("Prefix")]
    [SerializeField] string user = "a"; [SerializeField] string target = "b",
    separator = "_";

    Dictionary<StatKeys, string> parameterDefs = new();

    Dictionary<string, float> GetParametersWithPrefix(string prefix, StatsHandlerGO stats)
    {
        Dictionary<string, float> parameters = new();
        foreach (var param in parameterDefs)
            parameters.Add($"{prefix}{separator}{param.Value}", stats.GetCurrent(param.Key));
        return parameters;
    }

    Dictionary<string, float> GetParameters(StatsHandlerGO userStats, StatsHandlerGO targetStats)
    {
        Dictionary<string, float> parameters = new();

        parameters.AddRange(GetParametersWithPrefix(user, userStats));
        parameters.AddRange(GetParametersWithPrefix(target, targetStats));

        return parameters;
    }

    float Evaluate(string formula, StatsHandlerGO user, StatsHandlerGO target)
    {
        Dictionary<string, float> parameters = GetParameters(user, target);
        return FormulaEvaluator.Evaluate(formula, parameters);
    }

    void Initialize()
    {
        parameterDefs = new();
        var statsParameters = StatsManager.Dict;
        foreach (var statParam in statsParameters)
            parameterDefs.Add(statParam.Key, statParam.Value.Shortening);
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        print(Evaluate(skill.Damage[0].Formula, userStats, targetStats));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
