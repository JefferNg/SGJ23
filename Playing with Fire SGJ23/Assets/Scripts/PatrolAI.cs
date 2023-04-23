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

    private bool _isIncrementing = true;


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
            changeIndex();
        }

        return _patrolPoints[_index];
    }

    private void changeIndex()
    {
        if (_isIncrementing)
        {
            if (_index == _patrolPoints.Count - 1)
            {
                _isIncrementing = false;
                _index = _index - 1;
            }
            else
            {
                _index++;
            }
        }
        else
        {
            if (_index == 0)
            {
                _isIncrementing = true;
                _index = _index + 1;
            }
            else
            {
                _index = _index - 1;
            }
        }
    }
}
