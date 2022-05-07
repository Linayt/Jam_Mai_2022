using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(MoveTowardsClickDirection))]
public class MoveTowardsClickDirectionEditor : Editor
{
    [Tooltip("Used to check public variables from the target class")]
    private MoveTowardsClickDirection myObject;
    private SerializedObject soTarget;

    private SerializedProperty cam;
    private SerializedProperty lockOnX;
    private SerializedProperty lockOnY;
    private SerializedProperty lockOnZ;
    private SerializedProperty clickMask;
    private SerializedProperty moveSpeed;
    private SerializedProperty useInertia;
    private SerializedProperty keepVelocity;
    private SerializedProperty displayDirection;
    
    private SerializedProperty rigid;
    private SerializedProperty lineRenderer;

    private void OnEnable ()
    {
        myObject = (MoveTowardsClickDirection)target;
        soTarget = new SerializedObject(target);

        cam = soTarget.FindProperty("cam");
        lockOnX = soTarget.FindProperty("lockOnX");
        lockOnY = soTarget.FindProperty("lockOnY");
        lockOnZ = soTarget.FindProperty("lockOnZ");
        
        clickMask = soTarget.FindProperty("clickMask");
        moveSpeed = soTarget.FindProperty("moveSpeed");
        useInertia = soTarget.FindProperty("useInertia");
        keepVelocity = soTarget.FindProperty("keepVelocity");
        displayDirection = soTarget.FindProperty("displayDirection");
        rigid = soTarget.FindProperty("rigid");
        lineRenderer = soTarget.FindProperty("lineRenderer");
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
                EditorGUILayout.PropertyField(rigid);
                EditorGUILayout.PropertyField(cam);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(displayDirection);
                if (myObject.displayDirection)
                {
                    EditorGUILayout.PropertyField(lineRenderer);
                }
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(lockOnX);
                EditorGUILayout.PropertyField(lockOnY);
                EditorGUILayout.PropertyField(lockOnZ);
                EditorGUILayout.PropertyField(clickMask);
            }
            EditorGUILayout.EndVertical();
            
            EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
            {
                EditorGUILayout.PropertyField(moveSpeed);
                EditorGUILayout.PropertyField(useInertia);
                EditorGUILayout.PropertyField(keepVelocity);
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