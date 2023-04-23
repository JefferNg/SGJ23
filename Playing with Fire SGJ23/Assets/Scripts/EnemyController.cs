using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(FieldOfView), typeof(PatrolAI))]
public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Patrol,
        Hunt
    }

    private State _state = State.Patrol;

    [SerializeField]
    private float _patrolSpeed = 4.0f;

    [SerializeField]
    private float _huntSpeed = 10.0f;

    private FieldOfView _fov = null;
    private Rigidbody2D _rb = null;
    private PatrolAI _patrolAI = null;

    private IPathfinder _pathfinder = new DirectFollow();

    // Start is called before the first frame update
    void Start()
    {
        _fov = GetComponent<FieldOfView>();
        _rb = GetComponent<Rigidbody2D>();
        _patrolAI = GetComponent<PatrolAI>();
        _pathfinder.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case State.Patrol:
                patrolUpdate();
                break;
            case State.Hunt:
                huntUpdate();
                break;
            default:
                break;
        }
    }

    private void patrolUpdate()
    {
        if(_fov.CanSeeTarget())
        {
            AudioManager.Instance.PlaySongHunt();
            AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.encounter, 0.6f, new Vector2(1, 1));
            _state = State.Hunt;
            
        }
    }

    private void huntUpdate()
    {
        if (!_fov.CanSeeTarget())
        {
            AudioManager.Instance.PlaySongPatrol();
            _state = State.Patrol;
        }
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Patrol:
                patrolMove();
                break;
            case State.Hunt:
                huntMove();
                break;
            default:
                break;
        }
    }

    private void patrolMove()
    {
        moveToPoint(_patrolAI.NextPoint(transform.position), _patrolSpeed);
    }
    private void huntMove()
    {
        moveToPoint(_pathfinder.NextPoint(transform.position), _huntSpeed);
    }

    private void moveToPoint(Vector2 target, float speed)
    {
        Vector2 dir = (target - (Vector2)transform.position).normalized;
        _rb.velocity = dir * speed;

        transform.rotation = Quaternion.LookRotation(Vector3.forward,Vector3.Cross(Vector3.forward, dir));
    }
}
