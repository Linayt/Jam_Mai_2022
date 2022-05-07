using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Life))]
public class LifeEditor : Editor
{
	[Tooltip("Used to check public variables from the target class")]
	private int toolBarTab;
	private string currentTab;
	
	Life myObject;
	SerializedObject soTarget;

	private SerializedProperty maxLife;

	private SerializedProperty invincibilityDuration;

	private SerializedProperty animator;
	private SerializedProperty hitParameterName;
	
	private SerializedProperty OnDamageTaken;
	private SerializedProperty OnDeath;

	private void OnEnable ()
	{
		myObject = (Life)target;
		soTarget = new SerializedObject(target);

		////

		maxLife = soTarget.FindProperty("maxLife");
		invincibilityDuration = soTarget.FindProperty("invincibilityDuration");

		animator = soTarget.FindProperty("animator");
		hitParameterName = soTarget.FindProperty("hitParameterName");
		
		OnDamageTaken = soTarget.FindProperty("OnDamageTaken");
		OnDeath = soTarget.FindProperty("OnDeath");
	}
	
	private void DisplayToolbarMenu()
	{
		EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
		{
			toolBarTab = GUILayout.Toolbar(toolBarTab, new string[] { "General", "Events" }, GUILayout.MinHeight(25));

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
			0 => "General",
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
			case "General":
			{
				EditorGUILayout.BeginVertical(UIHelper.MainStyle);
				{
					EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
					{
						EditorGUILayout.PropertyField(maxLife);
						EditorGUILayout.LabelField("Start Life ", GUILayout.MaxWidth(80));
						myObject.startLife = EditorGUILayout.IntSlider(myObject.startLife, 0, myObject.maxLife);
					}

					EditorGUILayout.EndHorizontal();
			
					EditorGUILayout.BeginHorizontal(UIHelper.SubStyle1);
					{
						EditorGUILayout.PropertyField(invincibilityDuration);
					}
					EditorGUILayout.EndHorizontal();

					EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
					{
						EditorGUILayout.PropertyField(animator);

						if (myObject.animator != null)
						{
							EditorGUILayout.BeginVertical(UIHelper.SubStyle2);
							{
								EditorGUILayout.PropertyField(hitParameterName);
							}
							EditorGUILayout.EndVertical();

						}
					}
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndVertical();
			}
				break;

			case "Events":
			{
				EditorGUILayout.BeginVertical(UIHelper.MainStyle);
				{
					EditorGUILayout.BeginVertical(UIHelper.SubStyle1);
					{
						EditorGUILayout.PropertyField(OnDamageTaken);
						EditorGUILayout.PropertyField(OnDeath);
					}
					EditorGUILayout.EndVertical();
				}
				EditorGUILayout.EndVertical();
			}
				break;
		}
	}

	public override void OnInspectorGUI ()
	{
		UIHelper.InitializeStyles();
		
		soTarget.Update();
		EditorGUI.BeginChangeCheck();

		if(!Application.isPlaying)
		{
			myObject.CurrentLife = myObject.startLife;
		}

		EditorGUILayout.BeginHorizontal(UIHelper.MainStyle);
		{
			Rect space = GUILayoutUtility.GetRect(GUIContent.none, UIHelper.MainStyle, GUILayout.Height(20), GUILayout.Width(EditorGUIUtility.currentViewWidth));
			EditorGUI.ProgressBar(space, (float)myObject.CurrentLife / (float)myObject.maxLife, "Current Life");
		}
		EditorGUILayout.EndHorizontal();

		DisplayToolbarMenu();

		AssignTab();
		
		EditorGUI.BeginChangeCheck();

		HandleTabs();

		if (EditorGUI.EndChangeCheck())
		{
			soTarget.ApplyModifiedProperties();
		}
	}
}
