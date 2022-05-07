using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(TeleportOnTrigger))]
public class TeleportOnTriggerEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private TeleportOnTrigger myObject;
    private SerializedObject soTarget;

    private SerializedProperty teleportPoint;
    private SerializedProperty offset;
    
    private SerializedProperty useTag;
    private SerializedProperty requireInput;
    private SerializedProperty inputName;

    private void OnEnable ()
    {
        myObject = (TeleportOnTrigger)target;
        soTarget = new SerializedObject(target);

        teleportPoint = soTarget.FindProperty("teleportPoint");
        offset = soTarget.FindProperty("offset");
        useTag = soTarget.FindProperty("useTag");
        requireInput = soTarget.FindProperty("requireInput");
        inputName = soTarget.FindProperty("inputName");
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
                if (myObject.teleportPoint)
                {
                    if(GUILayout.Button("Select Teleport Point", UIHelper.ButtonStyle))
                    {
                        myObject.SelectTeleportPoint();
                    }
                }
                else
                {
                    if(GUILayout.Button("Create Teleport Point", UIHelper.ButtonStyle))
                    {
                        myObject.CreateTeleportPoint();
                    }
                }

                if (!myObject.displayTeleportPoint)
                {
                    if(GUILayout.Button("Show Teleport Point Gizmos", UIHelper.ButtonStyle))
                    {
                        myObject.ToggleDisplayTeleportPoint(true);    
                    }
                }
                else
                {
                    if(GUILayout.Button("Hide Teleport Point Gizmos", UIHelper.ButtonStyle))
                    {
                        myObject.ToggleDisplayTeleportPoint(false);  
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(teleportPoint);
                EditorGUILayout.PropertyField(offset);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(useTag);

                if (myObject.useTag)
                {
                    myObject.tagName = EditorGUILayout.TagField("Tag Name : ", myObject.tagName);
                }
                
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(requireInput);

                if (myObject.requireInput)
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