using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelSelector : MonoBehaviour {
    public static LevelSelector LS;

    public GameObject levelPrefab;
    public Image container;
    public List<GroundDataSerializable> SelectedLevel;

    private string[] files;

    private void Awake()
    {
        if (LS == null)
        {
            DontDestroyOnLoad(gameObject);
            LS = this;
        }
        else
        {
            if (LS != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "Maps");
        if (Directory.Exists(path))
        {
            files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo f = new FileInfo(files[i]);
                GameObject instance = Instantiate(levelPrefab, container.transform);
                instance.GetComponentInChildren<Text>().text = f.Name;
                int tempI = i;
                instance.GetComponent<Button>().onClick.AddListener(() => LevelSelected(tempI));
            }
        }
    }

    private void LevelSelected(int index)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(files[index], FileMode.Open);
        SelectedLevel = (List<GroundDataSerializable>)bf.Deserialize(file);
        file.Close();

        SceneManager.LoadScene(2);
    }

}
