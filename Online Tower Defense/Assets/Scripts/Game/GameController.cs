using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave
{
    public int Count { get; set; }
    public float Timeout { get; set; }
}

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
    public int CurrentWave = 0;
    public bool IsPlaying = false;

    public Spawner Spawner;
    public List<GameObject> EnemiesInScene = new List<GameObject>();
    public List<Action<int>> EnemiesListListener = new List<Action<int>>();
    public GameObject End;
    public List<GameObject> WalkableTiles = new List<GameObject>();
    public List<GameObject> PlaceableTiles = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
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

    private void Start()
    {
        EnemiesListListener.Add((int count) =>
        {
            if (count == 0)
            {
                IsPlaying = false;
                TogglePlaceableTiles();
            }
        });
    }

    public void NextWave()
    {
        if (CurrentWave <= Waves.Length)
        {
            IsPlaying = true;
            TogglePlaceableTiles();
            CurrentWave++;
            Spawner.SpawnWave(Waves[CurrentWave - 1]);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        if (!EnemiesInScene.Contains(enemy))
        {
            EnemiesInScene.Add(enemy);
            NotifyListeners(EnemiesInScene.Count);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (EnemiesInScene.Contains(enemy))
        {
            EnemiesInScene.Remove(enemy);
            NotifyListeners(EnemiesInScene.Count);
        }
    }

    private void NotifyListeners(int count)
    {
        EnemiesListListener.ForEach(l => l(count));
    }

    private void TogglePlaceableTiles()
    {
        PlaceableTiles.ForEach(p => p.GetComponentInChildren<PlaceableTileController>(true).ToogleEnable());
    }

}
