using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase {
    public int Level;
    public float Cooldown;
    public float AttackEffect;
    public MonoBehaviour instance;
    public abstract void ApplyAttack(EnemyController enemy);
}

public abstract class ScriptableAttack : ScriptableObject
{
    public abstract AttackBase Attack { get; }
}
