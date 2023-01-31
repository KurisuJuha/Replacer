using System.Collections.Generic;
using JuhaKurisu.PopoTools.Extentions;

namespace JuhaKurisu.PopoTools.Replacer
{
    public abstract class Rule<T>
    {
        public abstract IEnumerator<T[,]> Step(T[,] values);

        public (int x, int y)[] ScanAll(T[,] values, T[,] before)
        {
            List<(int x, int y)> ret = new List<(int x, int y)>();

            for (int x = 0; x < values.GetLength(0); x++)
            {
                for (int y = 0; y < values.GetLength(1); y++)
                {
                    if (!Scan(values, before, (x, y))) continue;
                    ret.Add((x, y));
                }
            }

            return ret.ToArray();
        }

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