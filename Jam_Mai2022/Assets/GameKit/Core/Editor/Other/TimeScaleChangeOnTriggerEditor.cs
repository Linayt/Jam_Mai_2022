using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(TimeScaleChangeOnTrigger))]
public class TimeScaleChangeOnTriggerEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private TimeScaleChangeOnTrigger myObject;
    private SerializedObject soTarget;

    private SerializedProperty useTag;
    private SerializedProperty resetTimeScaleOnLeave;
    private SerializedProperty resetTimeScaleOnDestroy;
    private SerializedProperty targetTimeScale;
    private SerializedProperty timeToReach;
    private SerializedProperty timeScaleCurve;

    private void OnEnable ()
    {
        myObject = (TimeScaleChangeOnTrigger)target;
        soTarget = new SerializedObject(target);

        useTag = soTarget.FindProperty("useTag");
        resetTimeScaleOnLeave = soTarget.FindProperty("resetTimeScaleOnLeave");
        resetTimeScaleOnDestroy = soTarget.FindProperty("resetTimeScaleOnDestroy");
        
        targetTimeScale = soTarget.FindProperty("targetTimeScale");
        timeToReach = soTarget.FindProperty("timeToReach");
        timeScaleCurve = soTarget.FindProperty("timeScaleCurve");

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
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(targetTimeScale);
                EditorGUILayout.PropertyField(timeToReach);
                EditorGUILayout.PropertyField(timeScaleCurve);
            }
            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(resetTimeScaleOnLeave);
                EditorGUILayout.PropertyField(resetTimeScaleOnDestroy);
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