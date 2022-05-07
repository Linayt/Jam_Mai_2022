using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LifeModifierOnCollision))]
public class LifeModifierOnCollisionEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private LifeModifierOnCollision myObject;
    private SerializedObject soTarget;

    private SerializedProperty lifeAmountChanged;
    private SerializedProperty destroyAfter;
    private SerializedProperty useTag;

    private void OnEnable ()
    {
        myObject = (LifeModifierOnCollision)target;
        soTarget = new SerializedObject(target);

        lifeAmountChanged = soTarget.FindProperty("lifeAmountChanged");
        destroyAfter = soTarget.FindProperty("destroyAfter");
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
                EditorGUILayout.PropertyField(destroyAfter);
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