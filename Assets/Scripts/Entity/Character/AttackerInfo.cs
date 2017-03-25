using System;
using UnityEngine;

[Serializable]
public class AttackerInfo
{
	[SerializeField]
	private GameObject attacker;
	[SerializeField]
	private GameObject weapon;
	[SerializeField]
	[Tooltip("The position of the attacker when the attack began.")]
	private Vector3 attackPosition;
	[SerializeField]
	[Tooltip("The rotation of the attacker when the attack began.")]
	private Quaternion attackRotation;
	private float damage;

	public AttackerInfo(GameObject attacker, GameObject weapon, Vector3 attackPosition, Quaternion attackRotation, float damage)
	{
		this.attacker = attacker;
		this.weapon = weapon;
		this.attackPosition = attackPosition;
		this.attackRotation = attackRotation;
		this.damage = damage;
	}
}