using UnityEngine;

public delegate void OnAttackedDelegate(AttackInfo attackInfo);

public interface IAttackable
{
	event OnAttackedDelegate OnAttacked;
	void Attack(Collision collisionInfo, AttackInfo attackInfo);
}