using UnityEngine;

public class TestInteractable : BaseInteractable
{
	public override void OnInteractStart(BaseInteractionController controller)
	{
		Debug.Log("Start Test");
	}

	public override void OnInteractEnd(BaseInteractionController controller)
	{
		Debug.Log("End Test");
	}
}