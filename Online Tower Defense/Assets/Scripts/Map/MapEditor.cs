using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditor : MonoBehaviour {
    
    public GameObject HexPrefab;
    public Vector2 Offset;

    private List<GameObject> hexInstances = new List<GameObject>();

    private void MakeGrid(Vector2 mapSize)
    {
        Vector3 startupPoint = mapSize / 2;
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
                hexInstances.Add(hexInstance);
            }
        }
    }
}
