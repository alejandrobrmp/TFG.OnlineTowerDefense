using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour {

    public GameObject prefab;
    private List<GameObject> hexInstances = new List<GameObject>();

    private void Start()
    {
        foreach (GroundDataSerializable item in LevelSelector.LS.SelectedLevel)
        {
            ScriptableGround scriptableGround = GetScriptableGround(item.ScriptableGround);
            if (scriptableGround != null)
            {
                GameObject instance = Instantiate(prefab, (Vector3)item.Position, Quaternion.identity, gameObject.transform);
                GroundData data = instance.GetComponent<GroundData>();
                data.FadeIn = true;
                data.ScriptableGround = scriptableGround;
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
        string commonPath = "Assets/Materials/Ground";
        switch (name)
        {
            case "Dirt":
                return (ScriptableGround)AssetDatabase.LoadAssetAtPath(commonPath + "/Dirt/Dirt.asset", typeof(ScriptableGround));
            case "Grass":
                return (ScriptableGround)AssetDatabase.LoadAssetAtPath(commonPath + "/Grass/Grass.asset", typeof(ScriptableGround));
            case "Stone":
                return (ScriptableGround)AssetDatabase.LoadAssetAtPath(commonPath + "/Stone/Stone.asset", typeof(ScriptableGround));
        }
        return null;
    }

}
