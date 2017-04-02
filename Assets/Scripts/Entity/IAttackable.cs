using UnityEngine;

public interface IAttackable
{
	void Attack(Collision collisionInfo, AttackInfo attackInfo);
}