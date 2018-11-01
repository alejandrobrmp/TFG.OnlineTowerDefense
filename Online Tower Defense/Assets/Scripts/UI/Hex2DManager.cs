using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hex2DManager : MonoBehaviour {

    private MapEditor mapEditor;

    private void Start()
    {
        mapEditor = GetComponentInParent<MapEditor>();
        Debug.Log(mapEditor != null);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            mapEditor.OnMouseOverHex(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            mapEditor.OnMouseClick(gameObject);
        }
    }

}
