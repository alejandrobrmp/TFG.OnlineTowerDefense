using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(HealthController))]
public class EnemyController : MonoBehaviour {

    public float TimeMovement = 1f / 10f;

    private PathStep currentStep;
    private bool isMoving = true;
    private bool finished = false;

    private void Start()
    {
        StartCoroutine(CalculatePath(StartMovement));
        GetComponentInChildren<HealthController>().OnHealthChanged += EnemyController_OnHealthChanged;
    }

    private void EnemyController_OnHealthChanged(float health)
    {
        if (health <= 0f)
        {
            finished = true;
            GameController.Instance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator CalculatePath(Action<PathStep> callback)
    {
        Vector3 start = GameController.Instance.Spawner.gameObject.transform.position;
        Vector3 end = GameController.Instance.End.transform.position;
        List<Vector3> tiles = new List<Vector3>();
        GameController.Instance.WalkableTiles.ForEach(g => tiles.Add(g.transform.position));
        PathFinder pf = new PathFinder(start, end, tiles);
        callback(pf.CalculatePath());
        yield return null;
    }

    private void StartMovement(PathStep step)
    {
        currentStep = step;
        Vector3 pos = currentStep.Current;
        pos.y += .5f;
        transform.position = pos;
        isMoving = false;
    }

    private void Update()
    {
        if (currentStep != null && !isMoving && !finished)
        {
            isMoving = true;
            StartCoroutine(MoveNext());
            finished = currentStep.NextStep == null;
        }
        else if (currentStep == null)
        {
            GameController.Instance.RemoveEnemy(gameObject);
            GameController.Instance.ModifyLives(-1);
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveNext()
    {
        float t = 0f;
        Vector3 initialPos = transform.position;
        Vector3 pos = currentStep.Current;
        pos.y += .5f;
        while (t < 1f)
        {
            t += Time.deltaTime * TimeMovement;
            transform.position = Vector3.Lerp(initialPos, pos, t);
            yield return null;
        }
        currentStep = currentStep.NextStep;
        isMoving = false;
    }

}
