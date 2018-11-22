using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Wave
{
    public int Count { get; set; }
    public float Timeout { get; set; }
}

public delegate void IsPlaying(bool value, bool hasMoreWaves);
public delegate void WaveChange(int wave, int total);
public delegate void ActionsLeftChanged(int actions);
public class GameController : MonoBehaviour {

    public static GameController Instance;
    public static readonly Wave[] Waves = new Wave[5]
    {
        new Wave() { Count = 3, Timeout = 3f },
        new Wave() { Count = 6, Timeout = 2f },
        new Wave() { Count = 10, Timeout = 1f },
        new Wave() { Count = 15, Timeout = .7f },
        new Wave() { Count = 20, Timeout = .3f },
    };
    public int CurrentWave;
    public event WaveChange OnWaveChange;
    public bool IsPlaying = false;
    public event IsPlaying OnPlayChange;
    public int ActionsLeft = 3;
    public event ActionsLeftChanged OnActionsLeftChanged;
    public int Lives = 12;
    public GameObject LivesText;

    public Spawner Spawner;
    public bool AllSpawned;
    public List<GameObject> EnemiesInScene = new List<GameObject>();
    public GameObject End;
    public List<GameObject> WalkableTiles = new List<GameObject>();
    public List<GameObject> PlaceableTiles = new List<GameObject>();

    public GameObject SelectedTurretToPlace;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        Spawner.OnAllSpawned += () => AllSpawned = true;
        if (OnActionsLeftChanged != null)
            OnActionsLeftChanged(ActionsLeft);

        OnPlayChange += (bool value, bool hasMoreWaves) =>
        {
            if (!hasMoreWaves)
                EndGame(true);
        };

        ModifyLives(0);
    }

    public void ModifyLives(int value)
    {
        Lives += value;
        if (Lives == 0)
            EndGame(false);
        LivesText.GetComponent<Text>().text = "Lives: " + Lives;
        LivesText.GetComponent<Animator>().ResetTrigger("PlayModified");
        LivesText.GetComponent<Animator>().SetTrigger("PlayModified");
    }

    private void EndGame(bool win)
    {
        PlayerPrefs.SetInt("EndScreen", 1);
        PlayerPrefs.SetInt("Win", win ? 1 : 0);
        SceneManager.LoadScene(0);
    }

    public void ModifyActionsLeft(int value)
    {
        ActionsLeft += value;
        if (OnActionsLeftChanged != null)
            OnActionsLeftChanged(ActionsLeft);
    }

    public void NextWave()
    {
        if (HasMoreWaves())
        {
            IsPlaying = true;
            if (OnPlayChange != null)
                OnPlayChange(IsPlaying, HasMoreWaves());

            CurrentWave++;
            if (OnWaveChange != null)
                OnWaveChange(CurrentWave, Waves.Length);
            Spawner.SpawnWave(Waves[CurrentWave - 1]);
            AllSpawned = false;
        }
    }

    public bool HasMoreWaves()
    {
        return CurrentWave < Waves.Length;
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!EnemiesInScene.Contains(enemy))
        {
            EnemiesInScene.Add(enemy);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (EnemiesInScene.Contains(enemy))
        {
            EnemiesInScene.Remove(enemy);
            if (EnemiesInScene.Count == 0 && AllSpawned)
            {
                IsPlaying = false;
                if (OnPlayChange != null)
                    OnPlayChange(IsPlaying && HasMoreWaves(), HasMoreWaves());
                ActionsLeft += 3;
                if (OnActionsLeftChanged != null)
                    OnActionsLeftChanged(ActionsLeft);
            }
        }
    }

}
