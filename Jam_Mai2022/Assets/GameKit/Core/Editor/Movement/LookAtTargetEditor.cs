using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(LookAtTarget))]
public class LookAtTargetEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private LookAtTarget myObject;
    private SerializedObject soTarget;

    private SerializedProperty targetTransform;
    private SerializedProperty rotationSpeed;
    private SerializedProperty useTag;

    private void OnEnable ()
    {
        myObject = (LookAtTarget)target;
        soTarget = new SerializedObject(target);

        useTag = soTarget.FindProperty("useTag");
        targetTransform = soTarget.FindProperty("targetTransform");
        rotationSpeed = soTarget.FindProperty("rotationSpeed");
    }

    public override void OnInspectorGUI ()
    {
        UIHelper.InitializeStyles();

        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginVertical(UIHelper.MainStyle);
        {
            EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(useTag);

                if (myObject.useTag)
                {
                    myObject.tagName = EditorGUILayout.TagField(myObject.tagName);
                }
                else
                {
                    EditorGUILayout.LabelField("Target :", GUILayout.MaxWidth(70f));
                    myObject.targetTransform = (Transform)EditorGUILayout.ObjectField(myObject.targetTransform, typeof(Transform), true);
                    //EditorGUILayout.PropertyField(targetTransform);
                }
                    
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.PropertyField(rotationSpeed);
            
        }
        EditorGUILayout.EndVertical();


        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}