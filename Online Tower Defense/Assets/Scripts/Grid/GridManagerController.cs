using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HexCellMetrics
{
    public const float outerRadius = 1f;

    public const float innerRadius = outerRadius * 0.866025404f;

    public static Vector3[] corners = {
        new Vector3(0f, 0f, outerRadius),
        new Vector3(innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(0f, 0f, -outerRadius),
        new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
        new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
        new Vector3(0f, 0f, outerRadius)
    };
}

public class GridManagerController : MonoBehaviour {

    public int width = 6;
    public int height = 6;

    public GroundData defaultData;
    public HexCell cellPrefab;
    private HexCell[] cells;

    HexMesh hexMesh;

    private bool canSelect = true;

    private void Awake()
    {
        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++, i++)
            {
                Vector3 position = new Vector3
                {
                    x = (x + z * 0.5f - z / 2) * (HexCellMetrics.innerRadius * 2f),
                    y = 0f,
                    z = z * (HexCellMetrics.outerRadius * 1.5f)
                };

                cells[i] = CreateCell(position);

                // DEBUG
                GroundData data = new GroundData
                {
                    Coordinates = new HexCoordinates(x, 0, z),
                    Material = defaultData.Material,
                    touchedShader = defaultData.touchedShader
                };

                cells[i].GroundData = data;
            }
        }

        hexMesh = GetComponentInChildren<HexMesh>();
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    private HexCell CreateCell(Vector3 position)
    {
        return Instantiate<HexCell>(cellPrefab, position, Quaternion.identity, gameObject.transform);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canSelect)
        {
            canSelect = false;
            HandleInput();
        }

        if (Input.GetMouseButtonUp(0) && !canSelect)
        {
            canSelect = true;
        }
    }

    void HandleInput()
    {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit))
        {
            TouchCell(hit.point);
        }
    }

    void TouchCell(Vector3 position)
    {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = (int)(coordinates.X + coordinates.Z * width + coordinates.Z / 2);
        HexCell cell = cells[index];
        cell.GroundManager.Touched();
    }

}
