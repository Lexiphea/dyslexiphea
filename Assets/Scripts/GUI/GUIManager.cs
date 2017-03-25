using System.Collections.Generic;
using UnityEngine;

public static class GUIManager
{
	private static Dictionary<string, GUIControl> controls = new Dictionary<string, GUIControl>();

	public static void Register(GUIControl control)
	{
		if (control == null)
		{
			throw new UnityException("GUIManager cannot register a null GUIControl...");
		}
		else if (control.name == null || control.name.Length < 1)
		{
			throw new UnityException("GUIManager cannot register a GUIControl with no name...");
		}
		else if (controls.ContainsKey(control.name))
		{
			throw new UnityException("GUIManager does not support registering duplicate GUIControl names...");
		}
		else
		{
			Debug.Log("Registered: " + control.name);
			controls.Add(control.name, control);
		}
	}

	public static void Unregister(GUIControl control)
	{
		if (control != null)
		{
			Debug.Log("Unregistered: " + control.name);
			controls.Remove(control.name);
		}
	}

	public static bool TryGet<T>(string name, out T control) where T : GUIControl
	{
		GUIControl result;
		if (controls.TryGetValue(name, out result) && (control = result as T) != null)
		{
			return true;
		}
		control = null;
		return false;
	}

	public static void Show(string name)
	{
		GUIControl result;
		if (controls.TryGetValue(name, out result) && !result.gameObject.activeSelf)
		{
			result.OnShow();
		}
	}

	/// <summary>
	/// Attempts to show the desired GUIControl. If it succeeds the previous control will be hidden.
	/// </summary>
	public static void TryShow(string name, GUIControl previousControl)
	{
		GUIControl result;
		if (controls.TryGetValue(name, out result) && !result.IsVisible)
		{
			result.OnShow();
			if (previousControl != null && previousControl.IsVisible)
			{
				previousControl.OnHide();
			}
		}
	}

	public static void Hide(string name)
	{
		GUIControl result;
		if (controls.TryGetValue(name, out result) && result.gameObject.activeSelf)
		{
			result.OnHide();
		}
	}

	/// <summary>
	/// Returns true if any GUIControl currently has focus.
	/// </summary>
	public static bool ControlHasFocus()
	{
		foreach (GUIControl control in controls.Values)
		{
			if (control.HasFocus)
			{
				return true;
			}
		}
		return false;
	}
}