using System;
using UnityEngine;

public static class ComponentExtensions
{
	public static T GetOrAddComponent<T>(this Component component) where T : Component
	{
		T result = component.gameObject.GetComponent<T>();
		if (result == null)
		{
			result = component.gameObject.AddComponent<T>();
		}
		return result;
	}

	public static void DisableAllComponentsExcept(this Component component, Type[] exceptionTypes)
	{
		Behaviour[] behaviours = component.gameObject.GetComponents(typeof(Behaviour)) as Behaviour[];
		if (behaviours != null)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				foreach (Type exceptionType in exceptionTypes)
				{
					if (behaviour.GetType() != exceptionType)
					{
						behaviour.enabled = false;
					}
				}
			}
		}
	}

	public static void EnableAllComponentsExcept(this Component component, Type[] exceptionTypes)
	{
		Behaviour[] behaviours = component.gameObject.GetComponents(typeof(Behaviour)) as Behaviour[];
		if (behaviours != null)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				foreach (Type exceptionType in exceptionTypes)
				{
					if (behaviour.GetType() != exceptionType)
					{
						behaviour.enabled = true;
					}
				}
			}
		}
	}
}