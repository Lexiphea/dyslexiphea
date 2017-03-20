using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{	
	[SerializeField]
	private Transform target = null;
	[SerializeField]
	private bool smoothX = true;
	[SerializeField]
	private bool smoothY = true;
	[SerializeField]
	private float dampening = 20.0f;
	[SerializeField]
	private Vector3 offset = new Vector3(0.0f, 0.0f, -20.0f);

	void Update()
	{
		if (this.target != null)
		{
			Vector3 newPos = this.offset + this.target.position;
			newPos -= this.transform.position;

			if (this.smoothX)
			{
				newPos.x /= this.dampening;
			}

			if (this.smoothY)
			{
				newPos.y /= this.dampening;
			}

			this.transform.position += newPos;
		}
	}	
}