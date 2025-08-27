using System.Collections.Generic;
using Core;
using Skills;
using Stats;
using Unity.VisualScripting;
using UnityEngine;

public class SkillEvaluator : Core.Singleton<SkillEvaluator>
{
    [Header("Prefix")]
    [SerializeField] string user = "a"; [SerializeField] string target = "b",
    separator = "_";

    Dictionary<StatKeys, string> parameterDefs = new();

    public static string User => Instance.user;
    public static string Target => Instance.target;
    public static string Separator => Instance.separator;
    public static Dictionary<StatKeys, string> ParameterDefs => Instance.parameterDefs;

    static Dictionary<string, float> GetParametersWithPrefix(string prefix, StatsHandlerBase stats)
    {
        Dictionary<string, float> parameters = new();
        foreach (var param in ParameterDefs)
            parameters.Add($"{prefix}{Separator}{param.Value}", stats.GetCurrent(param.Key));
        return parameters;
    }

    static Dictionary<string, float> GetParameters(StatsHandlerBase userStats, StatsHandlerBase targetStats)
    {
        Dictionary<string, float> parameters = new();

        parameters.AddRange(GetParametersWithPrefix(User, userStats));
        parameters.AddRange(GetParametersWithPrefix(Target, targetStats));

        return parameters;
    }

    public static float Evaluate(string formula, StatsHandlerBase user, StatsHandlerBase target)
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

    void Start() => Initialize();
}
