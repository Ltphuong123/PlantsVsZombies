using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(LevelData))]
public class LevelDataEditor : Editor
{
    private SerializedProperty timeProp;
    private SerializedProperty waveDatasProp;

    private void OnEnable()
    {
        timeProp = serializedObject.FindProperty("time");
        waveDatasProp = serializedObject.FindProperty("waveDatas");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Level Settings", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(timeProp, new GUIContent("Level Time (sec)"));

        EditorGUILayout.Space(10);

        EditorGUILayout.LabelField("Wave Datas", EditorStyles.boldLabel);

        // Hiển thị list waveDatas
        for (int i = 0; i < waveDatasProp.arraySize; i++)
        {
            SerializedProperty waveProp = waveDatasProp.GetArrayElementAtIndex(i);
            SerializedProperty waveTypeProp = waveProp.FindPropertyRelative("waveType");
            SerializedProperty startDelayProp = waveProp.FindPropertyRelative("startDelay");
            SerializedProperty zombieGroupsProp = waveProp.FindPropertyRelative("zombieGroups");

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Wave {i + 1}", EditorStyles.boldLabel);
            if (GUILayout.Button("X", GUILayout.Width(20)))
            {
                waveDatasProp.DeleteArrayElementAtIndex(i);
                break;
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.PropertyField(waveTypeProp);
            EditorGUILayout.PropertyField(startDelayProp, new GUIContent("Start Delay (s)"));

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Zombie Groups", EditorStyles.miniBoldLabel);

            for (int j = 0; j < zombieGroupsProp.arraySize; j++)
            {
                SerializedProperty groupProp = zombieGroupsProp.GetArrayElementAtIndex(j);
                SerializedProperty zombiesInRowsProp = groupProp.FindPropertyRelative("zombiesInRows");

                EditorGUILayout.BeginVertical("helpbox");
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Group {j + 1}");
                if (GUILayout.Button("X", GUILayout.Width(20)))
                {
                    zombieGroupsProp.DeleteArrayElementAtIndex(j);
                    break;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.PropertyField(zombiesInRowsProp, true);
                EditorGUILayout.EndVertical();
            }

            if (GUILayout.Button("Add Zombie Group"))
            {
                zombieGroupsProp.InsertArrayElementAtIndex(zombieGroupsProp.arraySize);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(10);
        }

        if (GUILayout.Button("Add Wave"))
        {
            waveDatasProp.InsertArrayElementAtIndex(waveDatasProp.arraySize);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
