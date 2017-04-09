using System;
using UnityEngine;

[Serializable]
public class AttackInfo
{
	[SerializeField]
	private GameObject attacker;
	[SerializeField]
	private GameObject attack;
	[SerializeField]
	[Tooltip("The position of the attacker when the attack began.")]
	private Vector3 attackPosition;
	[SerializeField]
	[Tooltip("The rotation of the attacker when the attack began.")]
	private Quaternion attackRotation;
	private float damage;

	public AttackInfo(GameObject attacker, GameObject attack, Vector3 attackPosition, Quaternion attackRotation, float damage)
	{
		this.attacker = attacker;
		this.attack = attack;
		this.attackPosition = attackPosition;
		this.attackRotation = attackRotation;
		this.damage = damage;
	}
}