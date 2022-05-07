using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(Rotator))]
public class RotatorEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private Rotator myObject;
    private SerializedObject soTarget;


    private SerializedProperty angularVelocity;
    private SerializedProperty rotateSpeed;
    private SerializedProperty useInput;
    private SerializedProperty inputName;

    private void OnEnable ()
    {
        myObject = (Rotator)target;
        soTarget = new SerializedObject(target);

        angularVelocity = soTarget.FindProperty("angularVelocity");
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

            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(angularVelocity);
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