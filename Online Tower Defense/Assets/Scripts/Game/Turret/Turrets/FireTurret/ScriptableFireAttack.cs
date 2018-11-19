using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleAttack", menuName = "ScriptableTurret/FireAttack")]
public class ScriptableFireAttack : ScriptableAttack {
    public int Level;
    public float Cooldown;
    public float DamageByTime;
    public override AttackBase Attack
    {
        get
        {
            return new FireAttack(Level, DamageByTime, Cooldown);
        }
    }
}

public class FireAttack : AttackBase
{
    public FireAttack(int level, float damageByTime, float cooldown)
    {
        Level = level;
        Cooldown = cooldown;
        AttackEffect = damageByTime;
    }
    public override void ApplyAttack(EnemyController enemy)
    {
        instance.StartCoroutine(DamageByTime(enemy));
    }

    private IEnumerator DamageByTime(EnemyController enemy)
    {
        for (int i = 0; i < 3; i++)
        {
            if (enemy != null)
            {
                enemy.GetComponentInChildren<HealthController>().ApplyHealthChanges(AttackEffect);
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}
