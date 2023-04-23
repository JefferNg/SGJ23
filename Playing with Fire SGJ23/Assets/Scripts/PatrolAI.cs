using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAI : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> _patrolPoints;

    [SerializeField]
    private float _patrolPointRadius = 0.5f;

    private int _index = 0;


    private void OnDrawGizmosSelected()
    {
        Color color = Color.black;
        float colorChangeAmt = 1.0f / _patrolPoints.Count;
        foreach (Vector2 pos in _patrolPoints)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(pos, _patrolPointRadius);

            color.r += colorChangeAmt;
            color.g += colorChangeAmt;
            color.b += colorChangeAmt;
        }
    }

    public Vector2 NextPoint(Vector2 currentPos)
    {
        // If you're close enough to the point, go to the next patrol point
        if (Vector2.Distance(currentPos, _patrolPoints[_index]) < _patrolPointRadius)
        {
            _index = (_index + 1) % _patrolPoints.Count;
        }

        return _patrolPoints[_index];
    }
}
