using System;

namespace MC.Core.Stats
{
    public class Stat
    {
        public StatId Id { get; private set; }
        public StatValue Value { get; private set; }

        public Stat(StatId id, StatValue statValue)
        {
            Id = id;
            Value = statValue;
        }

        internal static Stat Default()
        {
            return new(StatId.Default(), StatValue.Default());
        }
    }

    public sealed class StatId
    {
        public string Key { get; }

        public StatId(string key)
        {
            Key = key;
        }

        internal static StatId Default()
        {
            return new("Ataque");
        }

        public override bool Equals(object obj) =>
            obj is StatId other && Key == other.Key;

        public override int GetHashCode() =>
            Key.GetHashCode();

        public override string ToString()
        {
            return Key;
        }
    }

    public class StatValue
    {
        public float Base { get; private set; }
        public float Modifier { get; private set; }

        public float Current { get; private set; }

        public float Final => Current + Modifier;

        public StatValue(float baseValue)
        {
            Base = baseValue;
            Current = baseValue;
            Modifier = 0;
        }

        internal static StatValue Default()
        {
            return new(1);
        }

        public void ModifyBase(float amount, bool modifyCurrent = true)
        {
            Base += amount;
            if (modifyCurrent)
                Current += amount;
        }

        internal void ModifyCurrent(float amount)
        {
            Current = MathF.Min(
                MathF.Max(Current + amount, 0),
                Final
            );
        }

        public void Consume(float amount) => ModifyCurrent(Current - amount);

        public void Restore(float amount) => ModifyCurrent(Current + amount);

        internal void SetBase(float v) => Base = v;
        internal void AddModifier(float value) => Modifier += value;

        public override string ToString()
        {
            return $"{Final}";
        }
    }

}
