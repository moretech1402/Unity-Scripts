using UnityEngine;

namespace Core.Contracts
{
    public abstract class KeyMod<T>
    {
        [SerializeField] protected T key;
        [SerializeField] protected string formula;

        public T Key => key;
        public string Formula => formula;

        public abstract float GetValue();

        public KeyMod(T key, string formula = "10"){
            this.key = key;
            this.formula = formula;
        }
    }

}
