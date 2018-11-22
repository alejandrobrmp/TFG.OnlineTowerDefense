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

    private List<string> files = new List<string>();

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
        Debug.Log(Application.persistentDataPath);
        string defaultLevels = Path.Combine(Application.streamingAssetsPath, "DefaultMaps");
        string path = Path.Combine(Application.persistentDataPath, "Maps");
        LoadLevelsInPath(defaultLevels);
        LoadLevelsInPath(path);
    }

    private void LoadLevelsInPath(string path)
    {
        if (Directory.Exists(path))
        {
            int initial = files.Count;
            files.AddRange(Directory.GetFiles(path, "*.map"));
            for (int i = initial; i < files.Count; i++)
            {
                FileInfo f = new FileInfo(files[i]);
                GameObject instance = Instantiate(levelPrefab, container.transform);
                instance.GetComponentInChildren<Text>().text = f.Name.Substring(0, f.Name.LastIndexOf("."));
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
