using UnityEditor;
using UnityEngine;

namespace CellRelated
{
    [CustomEditor(typeof(CellGenerator))]
    public class CellGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var cellGenerator = (CellGenerator) target;
            if(GUILayout.Button("Build Object"))
            {
                cellGenerator.GenerateCells();
            }
        }
    }
}
