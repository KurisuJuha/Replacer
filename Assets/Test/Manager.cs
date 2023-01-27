using UnityEngine;
using JuhaKurisu.PopoTools.Replacer;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace JuhaKurisu
{
    [ExecuteAlways]
    public class Manager : MonoBehaviour
    {
        static Texture2D mainTexture;
        static Manager manager;
        static ushort width => manager._width;
        static ushort height => manager._height;
        [SerializeField] new SpriteRenderer renderer;
        [SerializeField] ushort _width;
        [SerializeField] ushort _height;
        [SerializeField] Color color;

        private void Update()
        {
            manager = manager is null ? this : manager;
            mainTexture = mainTexture is null ? new Texture2D(width, height) : mainTexture;

            renderer.sprite = Sprite.Create(mainTexture, new Rect(0, 0, mainTexture.width, mainTexture.height), Vector2.zero);
        }

        public static void Generate()
        {
            mainTexture = new Texture2D(width, height);
            mainTexture.filterMode = FilterMode.Point;
            Generator generator = new Generator();

            Color[] buffer = generator.Generate(width, height).ToArray();

            mainTexture.SetPixels(buffer);
            mainTexture.Apply();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Manager))]
    public class ManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Generate"))
            {
                Manager.Generate();
            }
        }
    }
#endif
}