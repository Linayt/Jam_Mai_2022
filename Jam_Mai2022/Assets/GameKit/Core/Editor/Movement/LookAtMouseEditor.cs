using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(LookAtMouse))]
public class LookAtMouseEditor : Editor
{
    private SerializedObject soTarget;

    private SerializedProperty rotationAxis;
    private SerializedProperty rotationSpeed;
    private SerializedProperty layerMask;
    private SerializedProperty offset;

    private void OnEnable ()
    {
        soTarget = new SerializedObject(target);

        rotationAxis = soTarget.FindProperty("rotationAxis");
        rotationSpeed = soTarget.FindProperty("rotationSpeed");
        layerMask = soTarget.FindProperty("layerMask");
        offset = soTarget.FindProperty("offset");
    }

    public override void OnInspectorGUI ()
    {
        UIHelper.InitializeStyles();

        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginVertical(UIHelper.MainStyle);
        {
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(rotationAxis);
                EditorGUILayout.PropertyField(rotationSpeed);
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(layerMask);
                EditorGUILayout.PropertyField(offset);
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();


        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}