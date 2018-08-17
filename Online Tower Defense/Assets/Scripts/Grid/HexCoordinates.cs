using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(HexCoordinates))]
public class HexCoordinatesDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HexCoordinates coordinates = new HexCoordinates(
            property.FindPropertyRelative("x").intValue,
            property.FindPropertyRelative("y").intValue,
            property.FindPropertyRelative("z").intValue
        );

        GUI.Label(position, coordinates.ToString());
    }
}

[System.Serializable]
public class HexCoordinates {

    [SerializeField]
    private int x, y, z;

	public int X
    {
        get
        {
            return x;
        }
    }
    
    public int Y
    {
        get
        {
            return y;
        }
    }

    public int Z
    {
        get
        {
            return z;
        }
    }

    public HexCoordinates(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static HexCoordinates FromOffsetCoordinates(int x, int y, int z)
    {
        return new HexCoordinates(x - z / 2, y, z);
    }

    public static HexCoordinates FromPosition(Vector3 position)
    {
        float x = position.x / (HexCellMetrics.innerRadius * 2f);
        float y = -x;
        float offset = position.z / (HexCellMetrics.outerRadius * 3f);
        x -= offset;
        y -= offset;

        int iX = Mathf.RoundToInt(x);
        int iY = Mathf.RoundToInt(y);
        int iZ = Mathf.RoundToInt(-x - y);

        if (iX + iY + iZ != 0)
        {
            float dX = Mathf.Abs(x - iX);
            float dY = Mathf.Abs(y - iY);
            float dZ = Mathf.Abs(-x - y - iZ);

            if (dX > dY && dX > dZ)
            {
                iX = -iY - iZ;
            }
            else if (dZ > dY)
            {
                iZ = -iX - iY;
            }
        }

        return new HexCoordinates(iX, iY, iZ);
    }

    public override string ToString()
    {
        return "(" + x + ", " + y + ", " + z + ")";
    }

}
