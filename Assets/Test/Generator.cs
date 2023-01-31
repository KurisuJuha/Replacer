using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JuhaKurisu.PopoTools.Replacer;

namespace JuhaKurisu
{
    public class Generator
    {
        public IEnumerator Generate(Color[,] colors)
        {
            colors.Fill(Color.black);

            Rule<Color> rule = new OneRule<Color>(
                new RandomReplaceRule<Color>(Color.black, Color.red),
                new RandomReplaceRule<Color>(Color.black, Color.blue)
            );

            IEnumerator<bool> enumerator = rule.Step(colors);
            while (enumerator.MoveNext())
            {
                yield return null;
            }
        }
    }
}