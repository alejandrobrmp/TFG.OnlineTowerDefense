using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {
    
    public GameObject Prefab;
    public GameObject PlaceableIndicator;
    public List<ScriptableGround> Grounds;

    private List<GameObject> hexInstances = new List<GameObject>();

    private void Start()
    {
        List<GroundDataSerializable> items = LevelSelector.LS.SelectedLevel;
        foreach (GroundDataSerializable item in items)
        {
            ScriptableGround scriptableGround = GetScriptableGround(item.ScriptableGround);
            if (scriptableGround != null)
            {
                GameObject instance = Instantiate(Prefab, (Vector3)item.Position, Quaternion.identity, gameObject.transform);
                GroundData data = instance.GetComponent<GroundData>();
                data.FadeIn = true;
                data.ScriptableGround = scriptableGround;

                MeshFilter mf = instance.GetComponentInChildren<MeshFilter>();
                if (scriptableGround.IsSpawn || scriptableGround.IsFinish || item.ScriptableGround.Equals("Grass"))
                {
                    instance.transform.GetChild(0).gameObject.AddComponent<NavMeshSourceTag>();

                    if (scriptableGround.IsSpawn)
                    {
                        GameController.Instance.Spawner = instance.GetComponent<Spawner>();
                        GameController.Instance.Spawner.enabled = true;
                    }
                    else if (scriptableGround.IsFinish)
                    {
                        GameController.Instance.End = instance;
                        GameController.Instance.WalkableTiles.Add(instance);
                    }
                    else
                    {
                        GameController.Instance.WalkableTiles.Add(instance);
                    }
                }
                if (scriptableGround.IsPlaceable)
                {
                    Vector3 pos = instance.transform.position;
                    pos.y += .06f;
                    Instantiate(PlaceableIndicator, pos, Quaternion.identity, instance.transform);
                    GameController.Instance.PlaceableTiles.Add(instance);
                }

                hexInstances.Add(instance);
            }
            else
            {
                Debug.LogError("ScriptableGround is null");
            }
        }
    }

    public ScriptableGround GetScriptableGround(string name)
    {
        return Grounds.Find((g) => g.name.Equals(name));
    }

}
