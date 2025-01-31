using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TurnManager))]
public class TurnManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
         TurnManager TurnManager = (TurnManager) target;

        if(GUILayout.Button("Generar Mapa"))
        {
            TurnManager.Generar_Mapa();
        }
        if(GUILayout.Button("Clean Map"))
        {
            TurnManager.Clean_Map();
        }
    }
}
