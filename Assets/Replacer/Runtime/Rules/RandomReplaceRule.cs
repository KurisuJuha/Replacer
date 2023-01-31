using System.Collections.Generic;
using JuhaKurisu.PopoTools.Extentions;

namespace JuhaKurisu.PopoTools.Replacer
{
    public class RandomReplaceRule<T> : Rule<T>
    {
        readonly T[,] before;
        readonly T[,] after;

        public RandomReplaceRule(T[,] before, T[,] after)
        {
            this.before = before;
            this.after = after;
        }

        public RandomReplaceRule(T[] before, T[] after)
        {
            this.before = before.To2D();
            this.after = after.To2D();
        }

        public RandomReplaceRule(T before, T after)
        {
            this.before = new T[1, 1];
            this.after = new T[1, 1];

            this.before[0, 0] = before;
            this.after[0, 0] = after;
        }

        public override IEnumerator<T[,]> Step(T[,] values)
        {


            yield return values;
        }
    }
}