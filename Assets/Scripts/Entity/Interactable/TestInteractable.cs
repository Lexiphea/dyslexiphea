using UnityEngine;

public class TestInteractable : BaseInteractable
{
	public override void OnInteractStart(InteractionController controller)
	{
		Debug.Log("Start Test");
	}

	public override void OnInteractEnd(InteractionController controller)
	{
		Debug.Log("End Test");
	}
}