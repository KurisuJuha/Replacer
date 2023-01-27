using System.Collections.Generic;
using System.Linq;
using System;
using JuhaKurisu.PopoTools.Extentions;

namespace JuhaKurisu.PopoTools.Replace
{
    public static class Array2DReplacer
    {
        public static T[,] Fill<T>(this T[,] self, T value)
        {
            for (int x = 0; x < self.GetLength(0); x++)
            {
                for (int y = 0; y < self.GetLength(1); y++)
                {
                    self[x, y] = value;
                }
            }

            return self;
        }

        public static T[] ToArray<T>(this T[,] self)
        {
            T[] ret = new T[self.Length];
            int width = self.GetLength(0);
            int height = self.GetLength(1);

            for (int y = 0; y < self.GetLength(1); y++)
            {
                for (int x = 0; x < self.GetLength(0); x++)
                {
                    ret[y * width + x] = self[x, y];
                }
            }

            return ret;
        }

        public static bool Scan<T>(this T[,] self, T[,] before, (int x, int y) pos)
        {
            int width = before.GetLength(0);
            int height = before.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (!(self.GetLength(0) > pos.x + x)) return false;
                    if (!(self.GetLength(1) > pos.y + y)) return false;
                    if (!(self[pos.x + x, pos.y + y].Equals(before[x, y]))) return false;
                }
            }

            return true;
        }

        public static (int x, int y)[] ScanAll<T>(this T[,] self, T[,] before)
        {
            List<(int x, int y)> ret = new List<(int x, int y)>();

            int width = self.GetLength(0);
            int height = self.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (self.Scan(before, (x, y))) ret.Add((x, y));
                }
            }

            return ret.ToArray();
        }

        public static (int x, int y, int r)[] ScanAllDirections<T>(this T[,] self, T[,] before)
        {
            List<(int x, int y, int r)> ret = new List<(int x, int y, int r)>();

            for (int r = 0; r < 4; r++)
            {
                ret.AddRange(self.ScanAll(before.Rotate(r)).Select(s => (s.x, s.y, r)));
            }

            return ret.ToArray();
        }

        public static T[,] Replace<T>(this T[,] self, T[,] after, (int x, int y) pos)
        {
            for (int x = 0; x < after.GetLength(0); x++)
            {
                for (int y = 0; y < after.GetLength(1); y++)
                {
                    if (!(self.GetLength(0) > x + pos.x && self.GetLength(1) > y + pos.y)) throw new Exception();
                    self[x + pos.x, y + pos.y] = after[x, y];
                }
            }


            return self;
        }

        public static bool RandomReplace<T>(this T[,] self, T[,] before, T[,] after)
        {
            if (before.GetLength(0) != after.GetLength(0)) throw new Exception();
            if (before.GetLength(1) != after.GetLength(1)) throw new Exception();

            var transforms = new List<(int x, int y, int r)>();

            transforms = self.ScanAllDirections(before).ToList();

            if (transforms.Count <= 0) return false;

            // ランダムに選ぶ
            int index = UnityEngine.Random.Range(0, transforms.Count);
            var transform = transforms[index];

            // 置き換え
            self = self.Replace(after.Rotate(transform.r), (transform.x, transform.y));

            return true;
        }

        public static bool RandomReplace<T>(this T[,] self, T[] before, T[] after) => self.RandomReplace(before.To2D(), after.To2D());

        public static bool RandomReplace<T>(this T[,] self, T before, T after) => self.RandomReplace(new T[] { before }, new T[] { after });

        public static T[,] AllReplace<T>(this T[,] self, T[,] before, T[,] after)
        {
            if (before.GetLength(0) != after.GetLength(0)) throw new Exception();
            if (before.GetLength(1) != after.GetLength(1)) throw new Exception();

            var positions = new List<(int x, int y, int r)>();

            while (true)
            {
                // 走査して追加
                positions = self.ScanAllDirections(before).ToList();

                // ないなら終了
                if (positions.Count == 0) break;

                // あるだけ調べる
                while (true)
                {
                    // もう候補がないならbreak
                    if (positions.Count == 0) break;

                    // ランダムに一つ選ぶ
                    int index = UnityEngine.Random.Range(0, positions.Count);
                    (int x, int y, int r) position = positions[index];
                    positions.RemoveAt(index);

                    // 変更されているならbreak
                    if (!self.Scan(before.Rotate(position.r), (position.x, position.y))) break;

                    // 置き換え
                    self.Replace(after.Rotate(position.r), (position.x, position.y));
                }
            }

            return self;
        }

        public static T[,] AllReplace<T>(this T[,] self, T[] before, T[] after) => self.AllReplace(before.To2D(), after.To2D());

        public static T[,] AllReplace<T>(this T[,] self, T before, T after) => self.AllReplace(new T[] { before }, new T[] { after });
    }
}