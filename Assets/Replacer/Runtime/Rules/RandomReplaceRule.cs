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

        public override IEnumerator<T[,]> Step(T[,] values)
        {


            yield return values;
        }
    }
}