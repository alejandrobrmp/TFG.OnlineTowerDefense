using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundData : MonoBehaviour {

    public ScriptableGround ScriptableGround;

    private void Start()
    {
        if (ScriptableGround == null)
        {
            Debug.Log("No material provided, using default.");
        }
        else
        {
            Debug.Log(ScriptableGround.name);
            GetComponentInChildren<Renderer>().material = ScriptableGround.Materials.Materials[0];
            GetComponentInChildren<MaterialSwapper>().materials = ScriptableGround.Materials.Materials.GetRange(1, ScriptableGround.Materials.Materials.Count - 1);
        }
    }

}
