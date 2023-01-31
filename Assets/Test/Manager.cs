using UnityEngine;
using System.Collections;
using JuhaKurisu.PopoTools.Replacer;

namespace JuhaKurisu
{
    [ExecuteAlways]
    public class Manager : MonoBehaviour
    {
        static Texture2D mainTexture;
        static Manager manager;
        static ushort width => manager._width;
        static ushort height => manager._height;
        static IEnumerator gEnumerator;
        static Color[,] colors;


        [SerializeField] new SpriteRenderer renderer;
        [SerializeField] ushort _width;
        [SerializeField] ushort _height;
        [SerializeField] Color color;


        private void Update()
        {
            manager = manager is null ? this : manager;
            mainTexture = mainTexture is null ? new Texture2D(width, height) : mainTexture;
            mainTexture = new Texture2D(width, height);
            mainTexture.filterMode = FilterMode.Point;

            if (gEnumerator is null || !gEnumerator.MoveNext())
            {
                colors = new Color[width, height];
                Generator generator = new Generator();
                gEnumerator = generator.Generate(colors);
                gEnumerator.MoveNext();
            }

            mainTexture.SetPixels(colors.ToArray());
            mainTexture.Apply();
            renderer.gameObject.transform.localScale = new Vector3(100 / (float)mainTexture.width, 100 / (float)mainTexture.height, 1);
            renderer.sprite = Sprite.Create(mainTexture, new Rect(0, 0, mainTexture.width, mainTexture.height), Vector2.zero);
        }
    }
}