using System.Collections.Generic;

namespace JuhaKurisu.PopoTools.Replacer
{
    public class SequenceRule<T> : Rule<T>
    {
        Rule<T>[] rules;

        public SequenceRule(params Rule<T>[] rules)
        {
            this.rules = rules;
        }

        public override IEnumerator<bool> Step(T[,] values)
        {
            foreach (var rule in rules)
            {
                IEnumerator<bool> e = rule.Step(values);

                while (e.MoveNext())
                {
                    yield return e.Current;
                }
            }
        }
    }
}