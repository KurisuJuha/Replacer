using System.Collections.Generic;

namespace JuhaKurisu.PopoTools.Replacer
{
    public abstract class Rule<T>
    {
        public abstract IEnumerator<T[,]> Step(T[,] values);
    }
}