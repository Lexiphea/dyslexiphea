using UnityEngine;

public interface IAttackable
{
	void Attack(Collision collisionInfo, AttackerInfo attackerInfo);
}