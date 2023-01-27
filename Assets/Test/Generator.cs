using System.Collections.Generic;
using UnityEngine;
using JuhaKurisu.PopoTools.Replacer;

namespace JuhaKurisu
{
    public class Generator
    {
        public Color[,] Generate(int width, int height)
        {
            Color[,] colors = new Color[width, height];
            colors.Fill(Color.black);

            RandomReplaceRule<Color> rule = new RandomReplaceRule<Color>();

            IEnumerator<Color[,]> enumerator = rule.Step(colors);
            while (enumerator.MoveNext())
            {
                colors = enumerator.Current;
            }

            return colors;
        }
    }
}