using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableTurret")]
public class ScriptableTurret : ScriptableObject {
    public int Level;
    public ScriptableAttack Attack;
}

public abstract class ScriptableAttack : ScriptableObject
{
    public abstract AttackBase Attack { get; }
}
