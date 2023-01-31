using System.Collections.Generic;

namespace JuhaKurisu.PopoTools.Replacer
{
    public class WhileRule<T> : Rule<T>
    {
        readonly Rule<T> rule;

        public WhileRule(Rule<T> rule)
        {
            this.rule = rule;
        }

        public override IEnumerator<bool> Step(T[,] values)
        {
            while (true)
            {
                IEnumerator<bool> e = rule.Step(values);

                while (e.MoveNext())
                {
                    if (!e.Current) yield break;
                    yield return true;
                }
            }
        }
    }
}