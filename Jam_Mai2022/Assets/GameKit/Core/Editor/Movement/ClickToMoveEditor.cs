using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(ClickToMove))]
public class ClickToMoveEditor : Editor
{
	[Tooltip("Used to check public variables from the target class")]
	private int toolBarTab;
	private string currentTab;

	[Tooltip("Used to check public variables from the target class")]
	private  ClickToMove myObject;
	private SerializedObject soTarget;

	private SerializedProperty lockOnX;
	private SerializedProperty lockOnY;
	private SerializedProperty lockOnZ;
	
	private SerializedProperty moveSpeed;
	private SerializedProperty lookTowardsDestination;
	
	private SerializedProperty offset;
	private SerializedProperty clickMask;
	private SerializedProperty clickMarker;
	private SerializedProperty animOptions;

	private void OnEnable ()
	{
		myObject = ( ClickToMove)target;
		soTarget = new SerializedObject(target);

		lockOnX = soTarget.FindProperty("lockOnX");
		lockOnY = soTarget.FindProperty("lockOnY");
		lockOnZ = soTarget.FindProperty("lockOnZ");
		
		moveSpeed = soTarget.FindProperty("moveSpeed");
		lookTowardsDestination = soTarget.FindProperty("lookTowardsDestination");
		
		offset = soTarget.FindProperty("offset");
		clickMask = soTarget.FindProperty("clickMask");
		clickMarker = soTarget.FindProperty("clickMarker");
		animOptions = soTarget.FindProperty("animOptions");
	}

	private void InitGUI()
	{
		UIHelper.InitializeStyles();

		soTarget.Update();
		EditorGUI.BeginChangeCheck();
	}

	private void DisplayToolbarMenu()
	{
		EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
		{
			toolBarTab = GUILayout.Toolbar(toolBarTab, new string[] { "Lock", "Movement", "Click","Animation" }, GUILayout.MinHeight(25));

			if (myObject.displayDebugInfo)
			{
				if (GUILayout.Button("Debug ON", UIHelper.GreenButtonStyle, GUILayout.MaxHeight(20f)))
				{
					myObject.displayDebugInfo = !myObject.displayDebugInfo;
				}
			}
			else
			{
				if (GUILayout.Button("Debug OFF", UIHelper.RedButtonStyle, GUILayout.MaxHeight(20f)))
				{
					myObject.displayDebugInfo = !myObject.displayDebugInfo;
				}
			}
		}
		EditorGUILayout.EndHorizontal();
	}

	private void AssignTab()
	{
		currentTab = toolBarTab switch
		{
			0 => "Lock",
			1 => "Movement",
			2 => "Click",
			3 => "Animation",
			_ => currentTab
		};

		//Apply modified properties to avoid data loss
		if (!EditorGUI.EndChangeCheck()) return;
		
		soTarget.ApplyModifiedProperties();
		GUI.FocusControl(null);
	}

	private void HandleTabs()
	{
		switch (currentTab)
		{
			case "Lock":
			{
				EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
				{
					EditorGUILayout.PropertyField(lockOnX);
					EditorGUILayout.PropertyField(lockOnY);
					EditorGUILayout.PropertyField(lockOnZ);
				}
				EditorGUILayout.EndVertical();
			}
			break;

			case "Movement":
			{
				EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
				{
					EditorGUILayout.PropertyField(moveSpeed);
					EditorGUILayout.PropertyField(lookTowardsDestination);
				}
				EditorGUILayout.EndVertical();
			}
			break;

			case "Click":
			{
				EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
				{
					EditorGUILayout.PropertyField(offset);
					EditorGUILayout.PropertyField(clickMask);
					EditorGUILayout.PropertyField(clickMarker);
				}
				EditorGUILayout.EndVertical();
			}
			break;

			case "Animation":
			{
				EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
				{
					EditorGUILayout.PropertyField(animOptions);
				}
				EditorGUILayout.EndVertical();
			}
			break;
		}
	}

	private void HandleDebugMessages()
	{
		if (!myObject.displayDebugInfo) return;
		
		// Context-Specific messages
		switch (toolBarTab)
		{
			case 0:
			{

			}
			break;

			case 1:
			{
				
			}
			break;

			case 2:
			{
				
			}
			break;

			case 3:
			{
				
			}
			break;
		}


	}

	public override void OnInspectorGUI ()
	{
		InitGUI();

		EditorGUILayout.BeginVertical(UIHelper.MainStyle);
		{
			DisplayToolbarMenu();

			AssignTab();

			EditorGUI.BeginChangeCheck();

			HandleTabs();

			if (EditorGUI.EndChangeCheck())
			{
				soTarget.ApplyModifiedProperties();
			}

			HandleDebugMessages();
		}
		EditorGUILayout.EndVertical();
	}
}