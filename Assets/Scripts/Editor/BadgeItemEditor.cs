using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BadgeData), true)]
public class BadgeItemEditor : Editor
{
    public SerializedProperty
    badge_Type;
    
    BadgeData _playerController;

    void OnEnable()
    {
        // Setup the SerializedProperties
        _playerController = (BadgeData)target;
        badge_Type = serializedObject.FindProperty("m_BadgeType");        
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(badge_Type);

        BadgeData.BadgeType st = (BadgeData.BadgeType)badge_Type.enumValueIndex;

        switch (st)
        {
            case BadgeData.BadgeType.Time:
                _playerController.Hours = EditorGUILayout.IntField("Hours", _playerController.Hours);

                break;

            case BadgeData.BadgeType.LastTrophy:
                _playerController.Hours = EditorGUILayout.IntField("Hours.", _playerController.Hours);
                break;
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_playerController);            
        }
        serializedObject.ApplyModifiedProperties();
    }

}
