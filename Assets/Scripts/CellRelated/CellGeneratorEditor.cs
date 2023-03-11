using System.Collections;
using System.Collections.Generic;
using CellRelated;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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
