using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapEditorActions : MonoBehaviour {

    public MapEditor mapEditor;

    public void Reset()
    {
        mapEditor.Reset();
    }

    public void Save()
    {
        List<GroundDataSerializable> toSerialize = mapEditor.GetGroundDataInstances();
        BinaryFormatter bf = new BinaryFormatter();
        string path = Path.Combine(Application.persistentDataPath, "Maps");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        FileStream file = File.Open(Path.Combine(path, "MapData-" + Directory.GetFiles(path).Length + ".map"), FileMode.Create);
        bf.Serialize(file, toSerialize);
        file.Close();
        Back();
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
