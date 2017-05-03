using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : BaseInteractable
{
	[SerializeField]
	private string destinationSceneName = null;

	public override void OnInteractStart(InteractionController controller)
	{
		/*
		play teleport activation animation
		play teleport activation audio
		delay
		*/
	}

	public override void OnInteractEnd(InteractionController controller)
	{
		/*
		play teleport activated animation
		play teleport activated audio
		*/
		if (this.destinationSceneName != null && this.destinationSceneName.Length > 0)
		{
			SceneManager.LoadScene(this.destinationSceneName);
		}
	}
}