#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FishController))]
public class FishControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FishController fishController = (FishController)target;

        fishController.minSpeed = EditorGUILayout.Slider("Min Speed", fishController.minSpeed, 0, 10);
        fishController.maxSpeed = EditorGUILayout.Slider("Max Speed", fishController.maxSpeed, 0, 10);

        if (GUILayout.Button("Reset Speeds"))
        {
            fishController.minSpeed = 0;
            fishController.maxSpeed = 10;
        }

        DrawDefaultInspector();
    }
}
#endif