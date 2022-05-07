using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LifeModifierWhileInTrigger))]
public class LifeModifierWhileInTriggerEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private LifeModifierWhileInTrigger myObject;
    private SerializedObject soTarget;

    private SerializedProperty lifeAmountChanged;
    private SerializedProperty damageFrequency;
    private SerializedProperty resetCooldownOnLeave;
    private SerializedProperty useTag;

    private void OnEnable ()
    {
        myObject = (LifeModifierWhileInTrigger)target;
        soTarget = new SerializedObject(target);

        lifeAmountChanged = soTarget.FindProperty("lifeAmountChanged");
        damageFrequency = soTarget.FindProperty("damageFrequency");
        resetCooldownOnLeave = soTarget.FindProperty("resetCooldownOnLeave");
        useTag = soTarget.FindProperty("useTag");
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
                EditorGUILayout.PropertyField(lifeAmountChanged);
                EditorGUILayout.PropertyField(damageFrequency);
                EditorGUILayout.PropertyField(resetCooldownOnLeave);
            } 
            EditorGUILayout.EndVertical();
        
            EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(useTag);
                if (myObject.useTag)
                {
                    myObject.tagName = EditorGUILayout.TagField(myObject.tagName);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        
        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}