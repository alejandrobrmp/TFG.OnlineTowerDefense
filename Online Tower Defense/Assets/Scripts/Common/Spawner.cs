﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public delegate void AllSpawned();
public class Spawner : MonoBehaviour {

    public GameObject Prefab;
    public event AllSpawned OnAllSpawned;

    public void SpawnWave(Wave wave)
    {
        StartCoroutine(Wave(wave.Count, wave.Timeout));
    }

    private IEnumerator Wave(int count, float timeoutSeconds)
    {
        int i = 0;
        while (i < count)
        {
            Vector3 pos = gameObject.transform.position;
            pos.y += .5f;
            GameObject instance = Instantiate(Prefab, pos, Quaternion.identity, null);
            GameController.Instance.AddEnemy(instance);
            i++;
            if (i != count)
                yield return new WaitForSeconds(timeoutSeconds);
        }
        if (OnAllSpawned != null)
            OnAllSpawned();
    }

}
