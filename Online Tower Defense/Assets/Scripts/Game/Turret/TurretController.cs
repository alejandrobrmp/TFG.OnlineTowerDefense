using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public float CooldownSeconds = 3f;
    public GameObject BulletPrefab;
    public Transform BulletInstantiationPoint;
    public Vector3 BulletPreparedPointOffset;

    private List<GameObject> AvailableTargets = new List<GameObject>();
    private GameObject target;
    private bool isCoolingDown = false;
    private GameObject bulletInstance;
    private float range = 2f;
    private int level = 1;
    private ScriptableTurret Data;

    private void Start()
    {
        GameController.Instance.EnemiesListListener.Add((int count) =>
        {
            GetComponent<SphereCollider>().enabled = count > 0;
        });
        isCoolingDown = true;
        StartCoroutine(PrepareBullet());
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
    }

    public void ApplyScriptableTurret(ScriptableTurret data)
    {
        Data = data;
        switch (data.Level)
        {
            case 1:
                break;
            case 2:
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
        bulletInstance = Instantiate(BulletPrefab, initialPos, Quaternion.identity, null);
        while (t < 1f)
        {
            t += Time.deltaTime * cooldown;
            bulletInstance.transform.position = Vector3.Lerp(initialPos, pos, t);
            yield return null;
        }
        isCoolingDown = false;
    }

    private void Shoot(GameObject target)
    {
        BulletController bullet = bulletInstance.GetComponent<BulletController>();
        bullet.Attack = Data.Attack.Attack;
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
                target != null &&
                target.GetComponentInChildren<HealthController>().CurrentHealth >
                t.GetComponentInChildren<HealthController>().CurrentHealth
                ))
            {
                target = t;
            }
        }
        this.target = target;
    }

    //private void CheckExit()
    //{
    //    if (target == null || (target != null &&
    //        Vector3.Distance(transform.position, target.transform.position) > range))
    //    {
    //        Debug.Log("Target reset");
    //        Debug.Log((target == null) + " : " + Vector3.Distance(transform.position, target.transform.position));
    //        target = null;
    //        //GetComponent<SphereCollider>().enabled = true;
    //    }
    //}



}
