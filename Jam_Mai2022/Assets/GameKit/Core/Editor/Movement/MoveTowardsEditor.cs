using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(MoveTowards))]
public class MoveTowardsEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private MoveTowards myObject;
    private SerializedObject soTarget;

    private SerializedProperty targetTransform;
    private SerializedProperty targetTag;
    private SerializedProperty lookAtTarget;
    private SerializedProperty moveSpeed;
    private SerializedProperty minDistance;
    private SerializedProperty rotationSpeed;

    private void OnEnable ()
    {
        myObject = (MoveTowards)target;
        soTarget = new SerializedObject(target);

        targetTransform = soTarget.FindProperty("targetTransform");
        targetTag = soTarget.FindProperty("targetTag");
        lookAtTarget = soTarget.FindProperty("lookAtTarget");
        moveSpeed = soTarget.FindProperty("moveSpeed");
        minDistance = soTarget.FindProperty("minDistance");
        rotationSpeed = soTarget.FindProperty("rotationSpeed");
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
                EditorGUILayout.PropertyField(targetTransform);

                myObject.targetTag = EditorGUILayout.TagField("Target Tag", myObject.targetTag);
            }
            EditorGUILayout.EndVertical();
            

            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(lookAtTarget);
                if (myObject.lookAtTarget)
                {
                    EditorGUILayout.PropertyField(rotationSpeed);
                }
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(moveSpeed);
                EditorGUILayout.PropertyField(minDistance);
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