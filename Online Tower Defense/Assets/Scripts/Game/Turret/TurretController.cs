using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public float CooldownSeconds = 3f;
    public GameObject BulletPrefab;
    public Transform BulletInstantiationPoint;
    public Vector3 BulletPreparedPointOffset;

    private GameObject target;
    private bool isCoolingDown = false;
    private GameObject bulletInstance;
    private float range = 1.5f;

    private void Start()
    {
        GameController.Instance.EnemiesListListener.Add((int count) =>
        {
            GetComponent<BoxCollider>().enabled = count > 0;
        });
        isCoolingDown = true;
        StartCoroutine(PrepareBullet());
    }

    private void Update()
    {
        CheckExit();
        if (!isCoolingDown && target != null)
        {
            Shoot(target);
            isCoolingDown = true;
            StartCoroutine(PrepareBullet());
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
        bullet.Target = target;
        bullet.IsFiring = true;
        bulletInstance = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Enemy"))
        {
            target = other.gameObject;
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void CheckExit()
    {
        if (target != null &&
            Vector3.Distance(transform.position, target.transform.position) > range)
        {
            target = null;
            GetComponent<BoxCollider>().enabled = true;
        }
    }



}
