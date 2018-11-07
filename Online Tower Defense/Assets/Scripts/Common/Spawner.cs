using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour {

    public GameObject Prefab;

    private void Start()
    {
        StartCoroutine(L());
    }

    private IEnumerator L()
    {
        yield return new WaitForSeconds(1f);
        Vector3 pos = gameObject.transform.position;
        pos.y+=.5f;
        GameObject instance = Instantiate(Prefab, pos, Quaternion.identity, null);
    }

}
