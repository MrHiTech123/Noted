using UnityEngine;

public interface IAI
{
    public void Patrol();
    public void Chase();
    public void Attack();
    public bool IsInSearchArea();
    public bool IsInAttackArea();
    public bool CanAttack();
}
