using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(RotateAround))]
public class RotateAroundEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private RotateAround myObject;
    private SerializedObject soTarget;

    private SerializedProperty targetTransform;
    private SerializedProperty rotateAxis;
    private SerializedProperty rotateSpeed;
    private SerializedProperty useInput;
    private SerializedProperty inputName;

    private void OnEnable ()
    {
        myObject = (RotateAround)target;
        soTarget = new SerializedObject(target);

        targetTransform = soTarget.FindProperty("targetTransform");
        rotateAxis = soTarget.FindProperty("rotateAxis");
        rotateSpeed = soTarget.FindProperty("rotateSpeed");
        useInput = soTarget.FindProperty("useInput");
        inputName = soTarget.FindProperty("inputName");
        
    }

    public override void OnInspectorGUI ()
    {
        UIHelper.InitializeStyles();

        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginVertical(UIHelper.MainStyle);
        {
            EditorGUILayout.PropertyField(targetTransform);

            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(rotateAxis);
                EditorGUILayout.PropertyField(rotateSpeed);
                
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(useInput);
                if (myObject.useInput)
                {
                    EditorGUILayout.PropertyField(inputName);
                }
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