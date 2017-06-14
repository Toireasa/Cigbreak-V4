using UnityEngine;
using System.Collections;
using UnityEditor;
using CigBreak;
using System.Linq;
using System;
using System.Reflection;

[CustomEditor(typeof(GameItemData), true)]
public class GameItemEditor : Editor
{
    int powerupIndex = 0;

    PowerupData powerup = null;
    string[] powerupMethods = null;


    private void OnEnable()
    {
        // If this is a powerup prepare the method display
        powerup = target as PowerupData;
        if (powerup != null)
        {
            var powerupMethods = typeof(PowerUp).GetMethods().Where(m => m.DeclaringType == typeof(PowerUp)).Select(m => m.Name).ToList();
            string selected = serializedObject.FindProperty("effectMethod").stringValue;
            powerupIndex = Mathf.Max(0, powerupMethods.IndexOf(selected));
            this.powerupMethods = powerupMethods.ToArray();
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Display method dropdown for powerup
        PowerupData powerup = target as PowerupData;
        if (powerup != null)
        {
            GUILayout.Space(20f);
            GUILayout.Label("Powerup Method");

            powerupIndex = EditorGUILayout.Popup(powerupIndex, powerupMethods);
            serializedObject.FindProperty("effectMethod").stringValue = powerupMethods[powerupIndex];
        }

        SerializedProperty guid = serializedObject.FindProperty("guid");

        GUILayout.Space(20f);

        GUILayout.Label("GUID: " + guid.stringValue);

        if (guid.stringValue == string.Empty)
        {
            if (GUILayout.Button("Set GUID"))
            {
                guid.stringValue = System.Guid.NewGuid().ToString();
            }
        }

        serializedObject.ApplyModifiedProperties();

        
    }
}
