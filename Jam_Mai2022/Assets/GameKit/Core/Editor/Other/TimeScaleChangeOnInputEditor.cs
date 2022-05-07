using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(TimeScaleChangeOnInput))]
public class TimeScaleChangeOnInputEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private TimeScaleChangeOnInput myObject;
    private SerializedObject soTarget;

    private SerializedProperty revertMode;
    private SerializedProperty inputName;
    private SerializedProperty targetTimeScale;
    private SerializedProperty timeToReach;
    private SerializedProperty timeScaleCurve;
    private SerializedProperty revertTimerDuration;

    private void OnEnable ()
    {
        myObject = (TimeScaleChangeOnInput)target;
        soTarget = new SerializedObject(target);

        revertMode = soTarget.FindProperty("revertMode");
        inputName = soTarget.FindProperty("inputName");
        targetTimeScale = soTarget.FindProperty("targetTimeScale");
        timeToReach = soTarget.FindProperty("timeToReach");
        timeScaleCurve = soTarget.FindProperty("timeScaleCurve");
        revertTimerDuration = soTarget.FindProperty("revertTimerDuration");
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
                EditorGUILayout.PropertyField(inputName);
                EditorGUILayout.PropertyField(revertMode);
            }
            EditorGUILayout.EndVertical();


            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(targetTimeScale);
                EditorGUILayout.PropertyField(timeToReach);
                EditorGUILayout.PropertyField(timeScaleCurve);
            }
            EditorGUILayout.EndVertical();

            if (myObject.revertMode == TimeScaleChangeOnInput.RevertMode.Timer)
            {
                EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
                {
                    EditorGUILayout.PropertyField(revertTimerDuration);
                }
                EditorGUILayout.EndVertical();
            }
        }
        EditorGUILayout.EndVertical();


        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}