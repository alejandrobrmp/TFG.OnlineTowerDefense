using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder {
    public Vector3 Start;
    public Vector3 End;
    public List<Vector3> ValidPoints;

    public PathFinder(Vector3 start, Vector3 end, List<Vector3> validPoints)
    {
        Start = start;
        End = end;
        ValidPoints = validPoints;
    }

    public PathStep CalculatePath()
    {
        List<Vector3> evaluatedVectors = new List<Vector3>();
        return CalculateStep(Start, End, null, ref evaluatedVectors);
    }

    private PathStep CalculateStep(Vector3 vector, Vector3 endVector, PathStep lastStep, ref List<Vector3> evaluatedVectors)
    {
        PathStep step = new PathStep(vector, ValidPoints, lastStep, ref evaluatedVectors);

        // If its the end return path
        if (PathFinderUtils.CompareVector(vector, endVector))
        {
            return step;
        }

        // If its an end road return null (is not the correct path)
        if (step.AvailableMovements.Count == 0)
        {
            return null;
        }

        step.AvailableMovements = step.AvailableMovements.OrderBy(m => Vector3.Distance(m, End)).ToList();

        // Loop through all possible steps while the next step is not found
        for (int i = 0; i < step.AvailableMovements.Count && step.NextStep == null; i++)
        {
            step.NextStep = CalculateStep(step.AvailableMovements[i], endVector, step, ref evaluatedVectors);
        }

        return step.NextStep == null ? null : step;
        
    }

}

public class PathStep
{
    List<Vector3> EvaluatedVectors;
    public Vector3 Current;
    public List<Vector3> AvailableMovements;
    public PathStep NextStep;
    public PathStep LastStep;

    public PathStep(Vector3 current, List<Vector3> validPoints, PathStep lastStep, ref List<Vector3> evaluatedVectors)
    {
        Current = current;
        LastStep = lastStep;
        EvaluatedVectors = evaluatedVectors;
        evaluatedVectors.Add(current);
        AvailableMovements = GetAvailableMovements(validPoints);
    }

    private List<Vector3> GetAvailableMovements(List<Vector3> validPoints)
    {
        List<Vector3> movements = new List<Vector3>();
        for (float y = -.1f; y <= .1f; y+=.1f)
        {
            foreach (Vector2 offset in PathFinderUtils.OFFSETS)
            {
                Vector3 searchedVector = Current + new Vector3(offset.x, y, offset.y);
                if (validPoints.Exists(v => PathFinderUtils.CompareVector(v, searchedVector)) &&
                    !EvaluatedVectors.Exists(v => PathFinderUtils.CompareVector(v, searchedVector)))
                {
                    Vector3 match = validPoints.Find(v => PathFinderUtils.CompareVector(v, searchedVector));
                    if (LastStep != null ? !PathFinderUtils.CompareVector(match, LastStep.Current) : true)
                    {
                        movements.Add(match);
                    }
                }
            }
        }
        return movements;
    }

}

public static class PathFinderUtils
{
    public static readonly Vector2[] OFFSETS = new Vector2[6]
    {
        new Vector2(1f, 0f),
        new Vector2(.50013f, -.86625f),
        new Vector2(-.50013f, -.86625f),
        new Vector2(-1f, 0f),
        new Vector2(-.50013f, .86625f),
        new Vector2(.50013f, .86625f)
    };

    public const float DEFAULT_DIFFERENCE = .001f;

    public static bool CompareVector(Vector3 vector, Vector3 toCompare, float difference = DEFAULT_DIFFERENCE)
    {
        float diffX = vector.x - toCompare.x;
        if (Mathf.Abs(diffX) > difference)
            return false;

        float diffY = vector.y - toCompare.y;
        if (Mathf.Abs(diffY) > difference)
            return false;

        float diffZ = vector.z - toCompare.z;
        return Math.Abs(diffZ) <= difference;
    }
}
