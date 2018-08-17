using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[InitializeOnLoad]
public class Autosave : ScriptableObject {

	static Autosave()
    {
        EditorApplication.playModeStateChanged += (PlayModeStateChange obj) =>
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
            {
                Debug.Log("Saving scene before play...");
                EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene());
                AssetDatabase.SaveAssets();
                Debug.Log("All saved.");
            }
        };
    }
}
