using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP : MonoBehaviour {

    public GameObject StartV;
    public List<GameObject> Vectors;
    public GameObject EndV;

	void Start () {
        List<Vector3> list = new List<Vector3>();
        Vectors.ForEach(v => list.Add(v.transform.position));
        PathFinder pf = new PathFinder(StartV.transform.position, EndV.transform.position, list);
        PathStep step = pf.CalculatePath();
	}
}
