using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour {

    public Text ErrorDisplay;
    public GameObject HexPrefab;
    public GameObject Grid2D;
    public Vector2 Offset;
    public GameObject Hex3DPrefab;
    public GameObject PlacedObjects;
    public GameObject CreateGridPanel;

    private Vector2 currentMapSize;
    private ScriptableGround Material;
    private GameObject SelectedObjectInstance;
    private List<GameObject> hexInstances = new List<GameObject>();
    private bool isFading = false;
    private float initialCameraY = 0f;

    private void Start()
    {
        CreateGridPanel.SetActive(true);
        initialCameraY = Camera.main.transform.position.y;
    }

    private void Update()
    {
        float scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
        {
            if (scroll > 0 && Grid2D.transform.position.y < 5f ||
                scroll < 0 && Grid2D.transform.position.y > 0f)
            {
                float absScroll = Mathf.Abs(scroll);
                float yValue = scroll > 0 ? (.1f * absScroll) : (-.1f * absScroll);
                Vector3 newPosition = Grid2D.transform.position + new Vector3(0f, yValue, 0f);
                newPosition.y = Mathf.Clamp(newPosition.y, 0f, 5f);
                Grid2D.transform.position = newPosition;
                UpdateGrid();
                Destroy(SelectedObjectInstance);
                SelectedObjectInstance = null;
                Vector3 cameraPos = Camera.main.transform.position;
                cameraPos.y += yValue;
                cameraPos.y = Mathf.Clamp(cameraPos.y, initialCameraY, initialCameraY + 5f);
                Camera.main.transform.position = cameraPos;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (!CreateGridPanel.activeSelf)
            {
                CreateGridPanel.SetActive(true);
            }
        }
    }

    private void UpdateGrid()
    {
        for (int i = 0; i < Grid2D.transform.childCount; i++)
        {
            GameObject tile = Grid2D.transform.GetChild(i).gameObject;
            tile.SetActive(!AnyObjectInPosition(tile.transform.position));
        }
    }

    public void MakeGrid(Vector2 mapSize)
    {
        currentMapSize = mapSize;
        float startX = 0f;
        float endX = 0f;
        if (hexInstances.Count > 0)
        {
            foreach (var item in hexInstances)
            {
                Destroy(item);
            }
            hexInstances.Clear();
        }
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                Vector3 position = new Vector3(x, Grid2D.transform.position.y, y * Offset.y);
                if (y % 2 == 0)
                {
                    position.x += Offset.x;
                }
                GameObject hexInstance = Instantiate(HexPrefab, position, Quaternion.identity, Grid2D.transform) as GameObject;
                hexInstances.Add(hexInstance);
                if (position.x < startX)
                {
                    startX = position.x;
                }
                else if (position.x > startX)
                {
                    endX = position.x;
                }
            }
        }
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.x = (endX - startX) / 2;
        Camera.main.transform.position = cameraPos;
    }

    public void SetMaterial(ScriptableGround prefab)
    {
        Material = prefab;
        ErrorDisplay.text = string.Empty;
        if (SelectedObjectInstance != null)
        {
            DissolveController dissolveController = SelectedObjectInstance.GetComponentInChildren<DissolveController>();
            isFading = true;
            StartCoroutine(dissolveController.FadeOut(2f, () =>
            {
                StartCoroutine(ChangeMaterialCallback());
            }));
        }
    }

    private IEnumerator ChangeMaterialCallback()
    {
        Vector3 position = SelectedObjectInstance.transform.position;
        Destroy(SelectedObjectInstance);
        SelectedObjectInstance = Instantiate(Hex3DPrefab, position, Quaternion.identity, transform);
        SelectedObjectInstance.GetComponentInChildren<MeshCollider>().enabled = false;
        GroundData groundData = SelectedObjectInstance.GetComponent<GroundData>();
        groundData.ScriptableGround = Material;
        groundData.FadeIn = true;
        yield return new WaitForSeconds(1f);
        isFading = false;
    }

    public void OnMouseOverHex(GameObject hexTile)
    {
        Vector3 position = hexTile.transform.position;
        if (Material == null)
        {
            ErrorDisplay.text = "Select a material first!";
            return;
        }
        if (SelectedObjectInstance != null)
        {
            if (SelectedObjectInstance.transform.position.Equals(position))
            {
                return;
            }
            else
            {
                if (!AnyObjectInPosition(position) && !isFading)
                {
                    SelectedObjectInstance.transform.position = position;
                }
            }
        }
        else
        {
            SelectedObjectInstance = Instantiate(Hex3DPrefab, position, Quaternion.identity, transform);
            SelectedObjectInstance.GetComponentInChildren<MeshCollider>().enabled = false;
            GroundData groundData = SelectedObjectInstance.GetComponent<GroundData>();
            groundData.ScriptableGround = Material;
            groundData.FadeIn = PlacedObjects.transform.childCount == 0;
            if (groundData.FadeIn)
            {
                isFading = true;
                StartCoroutine(WaitForFadeIn());
            }
        }
        if (Input.GetMouseButton(0))
        {
            OnMouseClick(hexTile);
        }
    }

    private IEnumerator WaitForFadeIn()
    {
        yield return new WaitForSeconds(1f);
        isFading = false;
    }

    private bool AnyObjectInPosition(Vector3 position)
    {
        for (int i = 0; i < PlacedObjects.transform.childCount; i++)
        {
            if (PlacedObjects.transform.GetChild(i).position.Equals(position))
            {
                return true;
            }
        }
        return false;
    }

    public void OnMouseClick(GameObject hexTile)
    {
        if (SelectedObjectInstance != null && !isFading)
        {
            SelectedObjectInstance.transform.parent = PlacedObjects.transform;
            SelectedObjectInstance = null;
            hexTile.SetActive(false);
        }
    }

    public void Reset()
    {
        for (int i = 0; i < PlacedObjects.transform.childCount; i++)
        {
            Destroy(PlacedObjects.transform.GetChild(i).gameObject);
        }
        MakeGrid(currentMapSize);
    }

    public List<GroundDataSerializable> GetGroundDataInstances()
    {
        List<GroundDataSerializable> data = new List<GroundDataSerializable>();
        for (int i = 0; i < PlacedObjects.transform.childCount; i++)
        {
            data.Add(PlacedObjects.transform.GetChild(i).gameObject.GetComponent<GroundData>().GetSerializable());
        }
        return data;
    }

}
