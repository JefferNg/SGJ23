using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerManager), typeof(Rigidbody2D))]
public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private LayerMask _deathLayerMask;

    private PlayerManager _pm = null;

    private void Start()
    {
        _pm = GetComponent<PlayerManager>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        bool isOnDeathLayer = (_deathLayerMask & (1 << collision.gameObject.layer)) != 0;
        if (isOnDeathLayer)
        {
            if (AudioManager.Instance.huntAmount > 0) {
                _pm.Die();
                
            }
        }
    }
}
