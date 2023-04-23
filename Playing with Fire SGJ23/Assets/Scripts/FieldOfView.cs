using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView: MonoBehaviour
{
    [SerializeField]
    private float _radius = 2.0f;

    [SerializeField]
    private float _angle = 30.0f;

    [SerializeField]
    private LayerMask _obstacleMask;

    [SerializeField]
    private LayerMask _targetMask;

    [SerializeField]
    private FOVRenderer _renderer;

    private List<Vector2> _visibleTargets = new List<Vector2>();

    private void Start()
    {
        if (_renderer != null)
        {
            Vector2 dirCCW, dirCW;
            GetViewDirections(out dirCCW, out dirCW);
            _renderer.Initialize(_radius, _angle, dirCCW, dirCW);
        }
    }

    public float GetRadius()
    {
        return _radius;
    }

    public float GetAngle()
    {
        return _angle;
    }

    /// <summary>
    /// Returns bounding vectors of view (up direction by +/- view angle).
    /// </summary>
    /// <param name="dirCCW">Normalized direction of view, counterclockwise rotated from up</param>
    /// <param name="dirCW">Normalized direction of view, clockwise rotated from up</param>
    /// <returns>Returns an integer based on the passed value.</returns>
    public void GetViewDirections(out Vector2 dirCCW, out Vector2 dirCW)
    {
        // Global-space vector of the transform's up direction
        Vector2 globalRight = transform.right;

        Quaternion fovRotA = Quaternion.AngleAxis(_angle, Vector3.forward);
        Quaternion fovRotB = Quaternion.AngleAxis(-_angle, Vector3.forward);

        // Rotate the up direction by the +/- angle
        dirCCW = fovRotA * globalRight;
        dirCW = fovRotB * globalRight;
    }

    /// <summary>
    /// Searches for targets, and collects them into interal list
    /// </summary>
    public void SearchForTargets()
    {
        _visibleTargets.Clear();

        Vector2 pos = transform.position;
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(pos, _radius, _targetMask);

        for (int i = 0; i < targetsInRadius.Length; i++)
        {
            Transform target = targetsInRadius[i].transform;
            Vector2 targetPos = (Vector2)target.position;
            Vector3 dirToTarget = (targetPos - pos).normalized;
            if (Vector3.Angle(transform.right, dirToTarget) < _angle)
            {
                float distToTarget = Vector2.Distance(pos, targetPos);

                bool isObjectInTheWay = Physics2D.Raycast(pos, dirToTarget, distToTarget, _obstacleMask);
                if (!isObjectInTheWay)
                {
                    _visibleTargets.Add(targetPos);
                }
            }
        }

    }

    /// <summary>
    /// Returns true if there is at least one target in view
    /// </summary>
    public bool CanSeeTarget()
    {

        return _visibleTargets.Count > 0;
    }

    public IEnumerable<Vector2> GetTargetPositionsEnumerator()
    {
        for (int i = 0; i < _visibleTargets.Count; i++)
        {
            yield return _visibleTargets[i];
        }
    }
}
