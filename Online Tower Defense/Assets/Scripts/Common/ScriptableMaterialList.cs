using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Material List", menuName = "ScriptableMaterialList")]
public class ScriptableMaterialList : ScriptableObject {

    public List<Material> Materials;

}
