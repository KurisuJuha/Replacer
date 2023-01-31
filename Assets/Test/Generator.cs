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
            colors[colors.GetLength(0) / 2, colors.GetLength(1) / 2] = Color.red;

            Rule<Color> rule = new SequenceRule<Color>(
                new WhileRule<Color>(
                    new RandomReplaceRule<Color>(
                        new Color[] {
                            Color.red,
                            Color.black,
                            Color.black
                        },
                        new Color[] {
                            Color.white,
                            Color.white,
                            Color.red
                        }
                    )
                ),
                new WhileRule<Color>(
                    new RandomReplaceRule<Color>(
                        new Color[] {
                            Color.white,
                            Color.white,
                            Color.red
                        },
                        new Color[] {
                            Color.red,
                            Color.black,
                            Color.black
                        }
                    )
                )
            );

            IEnumerator<bool> enumerator = rule.Step(colors);
            while (enumerator.MoveNext())
            {
                yield return null;
            }
        }
    }
}