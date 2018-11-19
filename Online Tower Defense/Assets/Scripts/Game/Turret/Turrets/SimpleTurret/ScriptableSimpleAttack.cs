using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleAttack", menuName = "ScriptableTurret/SimpleAttack")]
public class ScriptableSimpleAttack : ScriptableAttack
{
    public int Level;
    public float Cooldown;
    public float Damage;
    public override AttackBase Attack
    {
        get
        {
            return new SimpleAttack(Level, Damage, Cooldown);
        }
    }
}
public class SimpleAttack : AttackBase
{
    public SimpleAttack(int level, float damage, float cooldown)
    {
        Level = level;
        Cooldown = cooldown;
        AttackEffect = damage;
    }
    public override void ApplyAttack(EnemyController enemy)
    {
        enemy.GetComponentInChildren<HealthController>().ApplyHealthChanges(AttackEffect);
    }
}
