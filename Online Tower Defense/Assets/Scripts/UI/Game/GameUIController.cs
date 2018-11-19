using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour {

    public Button NextWaveButton;
    public Text ActionsLeft;
    public Text WaveCounter;
    public List<Button> Turrets;

    private void Start()
    {
        GameController.Instance.OnPlayChange += (bool value, bool hasMoreWaves) =>
        {
            NextWaveButton.interactable = !value && hasMoreWaves;
            Turrets.ForEach(t => t.interactable = !value);
        };
        GameController.Instance.OnWaveChange += (int wave, int total) =>
        {
            WaveCounter.text = wave + "/" + total;
        };
        GameController.Instance.OnActionsLeftChanged += (int actions) =>
        {
            ActionsLeft.text = actions.ToString();
        };
        Turrets.ForEach(t =>
        {
            t.onClick.AddListener(() => TurretSelected(t));
        });
        NextWaveButton.onClick.AddListener(NextWave);
    }

    public void TurretSelected(Button selected)
    {
        Turrets.ForEach(t => t.GetComponent<TurretButton>().SetSelected(t == selected));
        GameController.Instance.SelectedTurretToPlace = selected.GetComponent<TurretButton>().Turret;
    }

    public void NextWave()
    {
        GameController.Instance.NextWave();
    }

}
