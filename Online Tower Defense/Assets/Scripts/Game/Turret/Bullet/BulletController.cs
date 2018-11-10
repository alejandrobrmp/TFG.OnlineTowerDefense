using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public GameObject Target;
    public float Speed = 50f;
    public bool IsFiring = false;

    public AttackBase Attack;

    private void Update()
    {
        if (IsFiring)
        {
            if (Target == null)
            {
                Destroy(gameObject);
                return;
            }

            Vector3 direction = Target.transform.position - transform.position;
            float distance = Speed * Time.deltaTime;

            if (direction.magnitude <= distance)
            {
                if (Attack != null)
                {
                    EnemyController ec = Target.GetComponent<EnemyController>();
                    Attack.ApplyAttack(ec);
                }
                Destroy(gameObject);
            }
            else
            {
                transform.Translate(direction.normalized * distance, Space.World);
            }
        }
    }

}
