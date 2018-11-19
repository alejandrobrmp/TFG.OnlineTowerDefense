using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleAttack", menuName = "ScriptableTurret/FreezingAttack")]
public class ScriptableFreezingAttack : ScriptableAttack {
    public int Level;
    public float Cooldown;
    public float SlowingRatio;
    public override AttackBase Attack
    {
        get
        {
            return new FreezingAttack(Level, SlowingRatio, Cooldown);
        }
    }
}

public class FreezingAttack : AttackBase
{
    public FreezingAttack(int level, float slowingRatio, float cooldown)
    {
        Level = level;
        Cooldown = cooldown;
        AttackEffect = slowingRatio;
    }
    public override void ApplyAttack(EnemyController enemy)
    {
        instance.StartCoroutine(Slow(enemy));
    }

    private IEnumerator Slow(EnemyController enemy)
    {
        float initial = enemy.TimeMovement;
        enemy.TimeMovement *= AttackEffect / 100;
        enemy.GetComponentInChildren<HealthController>().ApplyHealthChanges(-10);
        yield return new WaitForSeconds(2f);
        enemy.TimeMovement = initial;
    }
}
