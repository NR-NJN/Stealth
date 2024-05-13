using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MapGen))]
public class MapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        MapGen gen = (MapGen)target;

        if (DrawDefaultInspector())
        {
            if (gen.autoUpdate)
            {
               gen.MapGenerate();
            }
        }

        if (GUILayout.Button("Generate"))
        {
            gen.MapGenerate();
        }
    }
}
