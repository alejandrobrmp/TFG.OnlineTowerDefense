using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableTurret/Turret")]
public class ScriptableTurret : ScriptableObject {
    public List<ScriptableAttack> Levels;
}
