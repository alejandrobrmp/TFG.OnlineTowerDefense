using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase {
    protected float Damage;
    public abstract void ApplyAttack(EnemyController enemy);
}

[CreateAssetMenu(fileName = "SimpleAttack", menuName = "ScriptableTurret/SimpleAttack")]
public class ScriptableSimpleAttack : ScriptableAttack
{
    public float Damage;
    public override AttackBase Attack
    {
        get
        {
            return new SimpleAttack(Damage);
        }
    }
}
public class SimpleAttack : AttackBase
{
    public SimpleAttack(float damage)
    {
        Damage = damage;
    }
    public override void ApplyAttack(EnemyController enemy)
    {
        enemy.GetComponentInChildren<HealthController>().ApplyHealthChanges(Damage);
    }
}
