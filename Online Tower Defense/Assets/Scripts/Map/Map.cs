using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    /// <summary>
    /// Prefab for the Hex
    /// </summary>
    public GameObject HexPrefab;

    /// <summary>
    /// Size of the map in terms of number of hex tiles
    /// This NOT represents the amount of
    /// From a 2d view:
    /// x: width -> X
    /// y: height -> Z
    /// </summary>
    public Vector2 MapSize;

    /// <summary>
    /// Offset for the y and x axis
    /// </summary>
    public Vector2 Offset;

    private void Start()
    {
        for (int x = 0; x < MapSize.x; x++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                Vector3 position = new Vector3(x, 0, y * Offset.y);
                if (y % 2 == 0)
                {
                    position.x += Offset.x;
                }
                GameObject hexInstance = Instantiate(HexPrefab, position, Quaternion.identity, gameObject.transform) as GameObject;
            }
        }
    }

    private void Update()
    {
        
    }
}
