using System.Runtime.InteropServices;
using UnityEngine;

public class AllocConsoleHelper : MonoBehaviour
{
	[DllImport("kernel32")]
	private static extern bool AllocConsole();

	[DllImport("kernel32")]
	private static extern bool FreeConsole();

	void Awake()
	{
		AllocConsole();
	}

	void OnDestroy()
	{
		FreeConsole();
	}
}