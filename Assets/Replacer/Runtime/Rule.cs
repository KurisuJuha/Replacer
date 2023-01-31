using System.Collections.Generic;
using JuhaKurisu.PopoTools.Extentions;

namespace JuhaKurisu.PopoTools.Replacer
{
    public abstract class Rule<T>
    {
        public abstract IEnumerator<T[,]> Step(T[,] values);
        public bool Scan(T[,] values, T[,] before, (int x, int y) position)
        {
            for (int x = 0; x < before.GetLength(0); x++)
            {
                for (int y = 0; y < before.GetLength(1); y++)
                {
                    if (!values.IsIndexWithInRange(position.x + x, position.y + y)) return false;
                    if (!before.IsIndexWithInRange(x, y)) return false;

                    if (!values[position.x + x, position.y + y].Equals(before[x, y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}