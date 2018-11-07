using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Serializable
{
    public float x;
    public float y;
    public float z;
    public static implicit operator Vector3(Vector3Serializable rValue)
    {
        return new Vector3(rValue.x, rValue.y, rValue.z);
    }
    public static implicit operator Vector3Serializable(Vector3 rValue)
    {
        return new Vector3Serializable
        {
            x = rValue.x,
            y = rValue.y,
            z = rValue.z
        };
    }
}

[Serializable]
public class GroundDataSerializable
{
    public string ScriptableGround;
    public Vector3Serializable Position;
}

public class GroundData : MonoBehaviour {

    public bool FadeIn = true;
    
    public ScriptableGround ScriptableGround;

    private void Start()
    {
        name = transform.position.ToString();

        if (ScriptableGround == null)
        {
            Debug.Log("No material provided, using default.");
        }
        else
        {
            GetComponentInChildren<Renderer>().material = ScriptableGround.Materials.Materials[0];
            GetComponentInChildren<MaterialSwapper>().materials = ScriptableGround.Materials.Materials.GetRange(1, ScriptableGround.Materials.Materials.Count - 1);
            GetComponentInChildren<GroundVisualsController>().FadeIn = FadeIn;
        }
    }

    public GroundDataSerializable GetSerializable()
    {
        return new GroundDataSerializable
        {
            ScriptableGround = ScriptableGround.name,
            Position = (Vector3Serializable)transform.position
        };
    }

}
