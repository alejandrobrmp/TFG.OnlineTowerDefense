using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    /// <summary>
    /// Prefab for the Hex
    /// </summary>
    public GameObject HexPrefab;

    /// <summary>
    /// Offset for the y and x axis
    /// </summary>
    public Vector2 Offset;

    private List<GameObject> hexInstances = new List<GameObject>();

    private void Start()
    {
        
    }

    /// <summary>
    /// This will make a grid by the given parameters
    /// </summary>
    /// <param name="mapSize">Size of the map</param>
    /// <param name="startupPoint">Startup point, positive from here, leave undefined for 0,0</param>
    /// <param name="groundData">Ground data, leave null to use the default values from the prefab</param>
    public void MakeGrid(Vector2 mapSize, Vector3 startupPoint = default(Vector3), ScriptableGround groundData = null)
    {
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 position = startupPoint + new Vector3(x, 0, y * Offset.y);
                if (y % 2 == 0)
                {
                    position.x += Offset.x;
                }
                GameObject hexInstance = Instantiate(HexPrefab, position, Quaternion.identity, gameObject.transform) as GameObject;
                hexInstance.GetComponent<GroundData>().ScriptableGround = groundData;
                hexInstances.Add(hexInstance);
            }
        }

    }

}
