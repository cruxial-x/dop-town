#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraFollow))]
public class CameraFollowEditor : Editor
{
    SerializedProperty minCameraPos;
    SerializedProperty maxCameraPos;

    private void OnEnable()
    {
        minCameraPos = serializedObject.FindProperty("minCameraPos");
        maxCameraPos = serializedObject.FindProperty("maxCameraPos");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(minCameraPos, new GUIContent("Min Camera Position"));
        EditorGUILayout.PropertyField(maxCameraPos, new GUIContent("Max Camera Position"));

        if (GUILayout.Button("Reset Positions"))
        {
            minCameraPos.vector3Value = Vector3.zero;
            maxCameraPos.vector3Value = Vector3.zero;
        }

        serializedObject.ApplyModifiedProperties();

        DrawDefaultInspector();
    }
}
#endif