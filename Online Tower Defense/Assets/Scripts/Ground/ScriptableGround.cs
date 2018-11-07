using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ground", menuName = "Ground")]
public class ScriptableGround : ScriptableObject {

    public ScriptableMaterialList Materials;

    public bool IsSpawn;

    public bool IsFinish;

}
