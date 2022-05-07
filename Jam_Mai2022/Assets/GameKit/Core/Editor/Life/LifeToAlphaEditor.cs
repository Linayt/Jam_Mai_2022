using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LifeToAlpha))]
public class LifeToAlphaEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private LifeToAlpha myObject;
    private SerializedObject soTarget;

    private SerializedProperty lifeToTrack;
    private SerializedProperty imageToTweak;
    private SerializedProperty invert;

    private void OnEnable ()
    {
        myObject = (LifeToAlpha)target;
        soTarget = new SerializedObject(target);

        lifeToTrack = soTarget.FindProperty("lifeToTrack");
        imageToTweak = soTarget.FindProperty("imageToTweak");
        invert = soTarget.FindProperty("invert");
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
                EditorGUILayout.PropertyField(lifeToTrack);
                EditorGUILayout.PropertyField(imageToTweak);
                EditorGUILayout.PropertyField(invert);
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