using UnityEngine;
using UnityEditor;

//Change all Dummyclass references into the class name
[CustomEditor(typeof(EventsOnInput))]
public class EventsOnInputEditor : Editor
{
	[Tooltip("Used to check public variables from the target class")]
	private int toolBarTab;
	private string currentTab;

	[Tooltip("Used to check public variables from the target class")]
	private EventsOnInput myObject;
	private SerializedObject soTarget;

	private SerializedProperty inputName;
	private SerializedProperty triggeredEvents;
	private SerializedProperty loop;

	private void OnEnable ()
	{
		myObject = (EventsOnInput)target;
		soTarget = new SerializedObject(target);
		
		inputName = soTarget.FindProperty("inputName");
		
		loop = soTarget.FindProperty("loop");
		triggeredEvents = soTarget.FindProperty("triggeredEvents");
		
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
			toolBarTab = GUILayout.Toolbar(toolBarTab, new string[] { "Input", "Events"}, GUILayout.MinHeight(25));

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
			0 => "Input",
			1 => "Events",
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
			case "Input":
			{
				EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
				{
					EditorGUILayout.PropertyField(inputName);
				}
				EditorGUILayout.EndHorizontal();
			}
			break;

			case "Events":
			{
				EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
				{
					EditorGUILayout.PropertyField(loop);
					EditorGUILayout.PropertyField(triggeredEvents);
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