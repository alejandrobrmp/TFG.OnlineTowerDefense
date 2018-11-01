using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundData : MonoBehaviour {

    public bool FadeIn = true;
    public ScriptableGround ScriptableGround;

    private Vector3 position;

    private void Start()
    {
        position = transform.position;
        name = position.ToString();

        if (ScriptableGround == null)
        {
            Debug.Log("No material provided, using default.");
        }
        else
        {
            Debug.Log(ScriptableGround.name);
            GetComponentInChildren<Renderer>().material = ScriptableGround.Materials.Materials[0];
            GetComponentInChildren<MaterialSwapper>().materials = ScriptableGround.Materials.Materials.GetRange(1, ScriptableGround.Materials.Materials.Count - 1);
            GetComponentInChildren<GroundVisualsController>().FadeIn = FadeIn;
        }
    }

}
