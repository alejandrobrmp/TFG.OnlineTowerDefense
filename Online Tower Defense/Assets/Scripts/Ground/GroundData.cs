using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ground", menuName = "Ground")]
public class GroundData : ScriptableObject {

    [SerializeField]
    public Material Material;
    [SerializeField]
    public Shader touchedShader;

    [SerializeField]
    public HexCoordinates Coordinates;

}
