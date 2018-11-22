using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretController : MonoBehaviour {

    public PlaceableTileController Tile;
    public ScriptableTurret TurretData;
    public float CooldownSeconds = 3f;
    public GameObject BulletPrefab;
    public Transform BulletInstantiationPoint;
    public Vector3 BulletPreparedPointOffset;
    public GameObject BasicEffect;
    public GameObject UpgradedEffect;
    public LevelIndicatorController LevelIndicator;

    public GameObject ActionsPanel;
    public Button UpgradeButton;
    public Button SellButton;

    private List<GameObject> AvailableTargets = new List<GameObject>();
    private GameObject target;
    private bool isCoolingDown = false;
    private GameObject bulletInstance;
    private float range = 2f;
    private ScriptableAttack CurrentLevel;

    private void Start()
    {
        GameController.Instance.OnPlayChange += OnPlayChange;
        GameController.Instance.OnActionsLeftChanged += OnActionsLeftChanged;
        Debug.Log(GameController.Instance.ActionsLeft);

        SellButton.onClick.AddListener(() =>
        {
            GameController.Instance.ModifyActionsLeft(CurrentLevel.Attack.Level);
            Tile.TurretDestroyed();
            GameController.Instance.OnPlayChange -= OnPlayChange;
            GameController.Instance.OnActionsLeftChanged -= OnActionsLeftChanged;
            Destroy(gameObject);
        });

        UpgradeButton.onClick.AddListener(() =>
        {
            GameController.Instance.ModifyActionsLeft(-1);
            ApplyScriptableTurret(TurretData.Levels[TurretData.Levels.IndexOf(CurrentLevel) + 1]);
        });

        EvaulateUpgradable();
        ApplyScriptableTurret(TurretData.Levels[0]);
        isCoolingDown = true;
        StartCoroutine(PrepareBullet());
    }

    private void OnPlayChange(bool value, bool hasMoreWaves)
    {
        GetComponent<SphereCollider>().enabled = value;
        GetComponent<BoxCollider>().enabled = !value;
        LevelIndicator.gameObject.SetActive(!value);
    }

    private void OnActionsLeftChanged(int value)
    {
        EvaulateUpgradable();
    }

    private void Update()
    {
        if (!GameController.Instance.IsPlaying)
            return;
        
        if (!isCoolingDown && target != null)
        {
            Shoot(target);
            isCoolingDown = true;
            StartCoroutine(PrepareBullet());
        }
        else if (target == null)
        {
            SelectTarget();
        }
    }

    public void Upgrade()
    {
        if (TurretData.Levels.Count >= CurrentLevel.Attack.Level)
        {
            ApplyScriptableTurret(TurretData.Levels[CurrentLevel.Attack.Level]);
        }
    }
    
    public void EvaulateUpgradable()
    {
        UpgradeButton.interactable = TurretData.Levels.IndexOf(CurrentLevel) < TurretData.Levels.Count - 1 && GameController.Instance.ActionsLeft > 0;
    }

    public void ApplyScriptableTurret(ScriptableAttack level)
    {
        CurrentLevel = level;
        LevelIndicator.SetLevel(level.Attack.Level);
        SellButton.GetComponentInChildren<Text>().text = "Sell (" + level.Attack.Level + ")";
        EvaulateUpgradable();
        CooldownSeconds = level.Attack.Cooldown;
        switch (level.Attack.Level)
        {
            case 1:
                BasicEffect.SetActive(true);
                break;
            case 2:
                BasicEffect.SetActive(false);
                UpgradedEffect.SetActive(true);
                break;
            default:
                break;
        }
    }

    private IEnumerator PrepareBullet()
    {
        float cooldown = 1f / CooldownSeconds;
        float t = 0f;
        Vector3 initialPos = BulletInstantiationPoint.position;
        Vector3 pos = initialPos + BulletPreparedPointOffset;
        bulletInstance = Instantiate(BulletPrefab, initialPos, Quaternion.identity, transform);
        while (t < 1f)
        {
            t += Time.deltaTime * cooldown;
            bulletInstance.transform.position = Vector3.Lerp(initialPos, pos, t);
            yield return null;
        }
        isCoolingDown = false;
        yield return new WaitForSeconds(3f);
    }

    private void Shoot(GameObject target)
    {
        BulletController bullet = bulletInstance.GetComponent<BulletController>();
        bullet.Attack = CurrentLevel.Attack;
        bullet.Attack.instance = this;
        bullet.Target = target;
        bullet.IsFiring = true;
        bulletInstance = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !AvailableTargets.Contains(other.gameObject))
        {
            AvailableTargets.Add(other.gameObject);
            SelectTarget();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy") && AvailableTargets.Contains(other.gameObject))
        {
            AvailableTargets.Remove(other.gameObject);
            SelectTarget();
        }
    }

    private void SelectTarget()
    {
        GameObject target = null;
        foreach (var t in AvailableTargets)
        {
            if (target == null ||
                (
                target != null && t != null &&
                target.GetComponentInChildren<HealthController>().CurrentHealth >
                t.GetComponentInChildren<HealthController>().CurrentHealth
                ))
            {
                target = t;
            }
        }
        this.target = target;
    }

    private void OnMouseOver()
    {
        if (!GameController.Instance.IsPlaying)
        {
            ActionsPanel.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (!GameController.Instance.IsPlaying)
        {
            ActionsPanel.SetActive(false);
        }
    }

}
