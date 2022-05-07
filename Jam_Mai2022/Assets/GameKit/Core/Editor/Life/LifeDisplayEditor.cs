using System;
using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(LifeDisplay))]
public class LifeDisplayEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private LifeDisplay myObject;
    private SerializedObject soTarget;

    private SerializedProperty lifeToDisplay;
    private SerializedProperty lifeBarSlider;
    private SerializedProperty lifeBarVisualType;
    private SerializedProperty lifeBarImage;

    private void OnEnable ()
    {
        myObject = (LifeDisplay)target;
        soTarget = new SerializedObject(target);

        lifeToDisplay = soTarget.FindProperty("lifeToDisplay");
        lifeBarVisualType = soTarget.FindProperty("lifeBarVisualType");
        lifeBarSlider = soTarget.FindProperty("lifeBarSlider");
        lifeBarImage = soTarget.FindProperty("lifeBarImage");
    }

    public override void OnInspectorGUI ()
    {
        UIHelper.InitializeStyles();

        soTarget.Update();
        EditorGUI.BeginChangeCheck();

        EditorGUILayout.BeginVertical(UIHelper.MainStyle);
        {
            EditorGUILayout.PropertyField(lifeToDisplay);
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(lifeBarVisualType);
                
                switch (myObject.lifeBarVisualType)
                {
                    case LifeDisplay.LifeBarVisualType.Image:
                        EditorGUILayout.PropertyField(lifeBarImage);
                        break;
                    case LifeDisplay.LifeBarVisualType.Slider:
                        EditorGUILayout.PropertyField(lifeBarSlider);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();


        if (myObject.lifeToDisplay == null)
        {
            EditorGUILayout.BeginVertical(UIHelper.WarningStyle);
            {
                EditorGUILayout.LabelField("No Life To Display set ! Please add one as reference", EditorStyles.boldLabel);
            }
            EditorGUILayout.EndVertical();
        }
        
        switch (myObject.lifeBarVisualType)
        {
            case LifeDisplay.LifeBarVisualType.Image:
                if (myObject.lifeBarImage == null)
                {
                    EditorGUILayout.BeginVertical(UIHelper.WarningStyle);
                    {
                        EditorGUILayout.LabelField("No Image set ! Please add one as reference", EditorStyles.boldLabel);
                    }
                    EditorGUILayout.EndVertical();
                }
                break;
            case LifeDisplay.LifeBarVisualType.Slider:
                if (myObject.lifeBarSlider == null)
                {
                    EditorGUILayout.BeginVertical(UIHelper.WarningStyle);
                    {
                        EditorGUILayout.LabelField("No Slider set ! Please add one as reference", EditorStyles.boldLabel);
                    }
                    EditorGUILayout.EndVertical();
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (EditorGUI.EndChangeCheck())
        {
            soTarget.ApplyModifiedProperties();
        }
    }
}