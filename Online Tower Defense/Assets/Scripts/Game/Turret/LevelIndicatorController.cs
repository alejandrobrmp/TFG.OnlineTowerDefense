using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIndicatorController : MonoBehaviour {

    public Text LevelIndicatorText;

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    public void SetLevel(int level)
    {
        LevelIndicatorText.text = "Level " + level;
    }

}
