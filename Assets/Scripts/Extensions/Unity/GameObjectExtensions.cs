using System;
using UnityEngine;

public static class GameObjectExtensions
{
	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T result = gameObject.GetComponent<T>();
		if (result == null)
		{
			result = gameObject.AddComponent<T>();
		}
		return result;
	}

	public static void DisableAllComponentsExcept(this GameObject gameObject, Type[] exceptionTypes)
	{
		Behaviour[] behaviours = gameObject.GetComponents(typeof(Behaviour)) as Behaviour[];
		if (behaviours != null)
		{
			foreach (Behaviour behaviour in behaviours)
			{
				foreach (Type exceptionType in exceptionTypes)
				{
					if (behaviour.GetType() != exceptionType)
					{
						MonoBehaviour.Destroy(behaviour.gameObject);
						behaviour.enabled = false;
					}
				}
			}
		}
	}

	public static void EnableAllComponentsExcept(this GameObject gameObject, Type[] exceptionTypes)
	{
		Behaviour[] behaviours = gameObject.GetComponents(typeof(Behaviour)) as Behaviour[];
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