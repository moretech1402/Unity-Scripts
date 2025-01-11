using System.Collections.Generic;
using Stats;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class FormulaEvaluator : MonoBehaviour
{
    public static float Evaluate(string formula, Dictionary<string, float> parameters)
    {
        var expression = new Expression(formula);
        foreach(var param in parameters){
            expression.Parameters[param.Key] = param.Value;
        }
        return (float)expression.Evaluate(null);
    }
}
