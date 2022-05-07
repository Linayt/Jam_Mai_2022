using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helper
{
	public enum FXFeedbackType {ParticleSystem, VFXGraph, Instantiate, None};
	public enum VerticalAxis { Y, Z };
	

	#region Curved Lerp
	/////// Float ///////
	/// 
	/// <summary>
	/// Returns a float lerped between two values according to an AnimationCurve.
	/// </summary>
	public static float CurvedLerp (float minValue, float maxValue, AnimationCurve curve, float t)
	{
		float curveEvaluate = curve.Evaluate(t);

		float lerpedValue = Mathf.Lerp(minValue, maxValue, curveEvaluate);

		return lerpedValue;
	}
	
	/// <summary>
	/// Returns a float lerped between two values according to an AnimationCurve.
	/// </summary>
	public static float CurvedLerp (float minValue, float maxValue, float t)
	{
		AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
		float curveEvaluate = animCurve.Evaluate(t);

		float lerpedValue = Mathf.Lerp(minValue, maxValue, curveEvaluate);

		return lerpedValue;
	}
	#endregion
	
	/// <summary>
	/// Returns a Vector3 corresponding to Input Direction on two axis
	/// </summary>
	/// <param name="hInputName">Horizontal Input Name used in the Input Manager</param>
	/// <param name="vInputName">Vertical Input Name used in the Input Manager</param>
	/// <param name="verticalAxis">Which axis is considered up ?</param>
	/// <returns>Normalized movement direction</returns>
	public static Vector3 GetMovementDirection (string hInputName, string vInputName, VerticalAxis verticalAxis)
	{
		Vector3 moveDir = Vector3.zero;

		moveDir.x = Input.GetAxis(hInputName);
		if (verticalAxis == VerticalAxis.Y)
		{
			moveDir.y = Input.GetAxis(vInputName);
		}
		else
		{
			moveDir.z = Input.GetAxis(vInputName);
		}

		if (moveDir.magnitude > 1f)
		{
			moveDir.Normalize();
		}
		
		return moveDir;
	}

	/// <summary>
	/// Returns a if target GameObject is on the ground (Round check shape)
	/// </summary>
	/// <param name="from">Pivot position</param>
	/// <param name="radius">Range of the ground check</param>
	/// <param name="wallAvoidanceLayers">Which Layers are considered as Ground ?</param>
	/// <returns>Is the object on the ground ?</returns>
	public static bool GroundCheck (Vector3 from, Vector3 offset, Transform t, float radius, LayerMask wallAvoidanceLayers)
	{
		Vector3 checkPos = from + offset.WorldToLocalSpace(t);
		return Physics.CheckSphere(checkPos, radius, wallAvoidanceLayers);
	}

	/// <summary>
	/// Returns a if target GameObject is on the ground (Square check shape)
	/// </summary>
	/// <param name="from">Pivot position</param>
	/// <param name="halfExtents">Half extents of the ground check</param>
	/// <param name="rotation">Rotation used for the square check</param>
	/// <param name="wallAvoidanceLayers">Which Layers are considered as Ground ?</param>
	/// <returns>Is the object on the ground ?</returns>
	public static bool BoxGroundCheck (Vector3 from, Vector3 halfExtents, Quaternion rotation, LayerMask wallAvoidanceLayers)
	{
		return Physics.CheckBox(from, halfExtents, rotation, wallAvoidanceLayers);
	}

	/// <summary>
	/// Checks for wall collision. Used to prevent movement against walls
	/// </summary>
	/// <param name="t">Transform used for reference</param>
	/// <param name="offset">Offset from GameObject's pivot point</param>
	/// <param name="halfExtents">Size of the check box</param>
	/// <param name="wallAvoidanceLayers">Which layers are considered as Walls ?</param>
	/// <returns>Is the object facing a wall ?</returns>
	public static bool WallCheck (this Transform t, Vector3 offset, Vector3 halfExtents, LayerMask wallAvoidanceLayers)
	{
		Vector3 localOffset = offset.WorldToLocalSpace(t);

		Vector3 checkPos = t.position + localOffset;

		return Physics.CheckBox(checkPos, halfExtents, t.rotation, wallAvoidanceLayers);
	}

	/// <summary>
	/// Converts an angle in degrees into a direction, depending on specified transform
	/// </summary>
	/// <param name="t">Transform used for reference</param>
	/// <param name="angleInDegrees">Offset from GameObject's pivot point</param>
	/// <returns>Is the object facing a wall ?</returns>
	public static Vector3 DirFromAngle (this float angleInDegrees, Transform t)
	{
		angleInDegrees += t.eulerAngles.y;

		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
	
	public static void SmoothLookAt(this Transform t, Transform targetTransform, float rotationSpeed, float delta)
	{
		Quaternion rot = t.GetLookAtRotation(targetTransform);

		t.rotation =  Quaternion.RotateTowards(t.rotation, rot, rotationSpeed * delta );
	}
	
	public static void SmoothLookAt(this Transform t, Vector3 targetPosition, float rotationSpeed, float delta)
	{
		Quaternion rot = t.GetLookAtRotation(targetPosition);

		t.rotation =  Quaternion.RotateTowards(t.rotation, rot, rotationSpeed * delta );
	}
	
	/// <summary>
	/// Converts an angle in degrees into a direction
	/// </summary>
	/// <param name="angleInDegrees">Angle in degrees</param>
	/// <returns>Direction converted from target angle</returns>
	public static Vector3 DirFromAngle (float angleInDegrees)
	{
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}


	private static readonly Dictionary<float, WaitForSeconds> waitDictionary = new Dictionary<float, WaitForSeconds>();
	/// <summary>
	/// A more efficient take on the WaitForSeconds used in Coroutines.
	/// </summary>
	/// <param name="time">Wait time</param>
	/// <returns></returns>
	public static WaitForSeconds GetWait(float time)
	{
		if (waitDictionary.TryGetValue(time, out var wait))
		{
			return wait;
		}

		waitDictionary[time] = new WaitForSeconds(time);
		return waitDictionary[time];
	}

	private static PointerEventData _eventDataCurrentPosition;
	private static List<RaycastResult> _results;

	/// <summary>
	/// Checks if cursor is over UI
	/// </summary>
	/// <returns>Is cursor over UI ?</returns>
	public static bool IsOverUi ()
	{
		_eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
		_results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
		return _results.Count > 0;
	}
}



