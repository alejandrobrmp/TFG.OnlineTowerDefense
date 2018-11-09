using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public Button NextWaveButton;

    private void Start()
    {
        GameController.Instance.EnemiesListListener.Add((int count) =>
        {
            NextWaveButton.interactable = count == 0;
        });
        NextWaveButton.onClick.AddListener(NextWave);
    }

    public void NextWave()
    {
        GameController.Instance.NextWave();
    }

}
