using UnityEngine;
using System.Collections.Generic;

namespace JuhaKurisu.PopoTools.Replacer
{
    public class OneRule<T> : Rule<T>
    {
        readonly Rule<T>[] rules;

        public OneRule(params Rule<T>[] rules)
        {
            this.rules = rules;
        }

        public override IEnumerator<bool> Step(T[,] values)
        {
            Rule<T> rule = rules[Random.Range(0, rules.Length)];

            IEnumerator<bool> e = rule.Step(values);

            while (e.MoveNext())
            {
                yield return e.Current;
            }
        }
    }
}