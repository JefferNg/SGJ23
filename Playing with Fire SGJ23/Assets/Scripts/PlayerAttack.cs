using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public delegate void EnemyKilledEvent(GameObject enemyObject);
    public event EnemyKilledEvent OnEnemyKilled; 
    [SerializeField] private float attack_cooldown = 3f;

    private void FixedUpdate()
    {
        if (attack_cooldown <= 3)
        {
            attack_cooldown += Time.deltaTime;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (attack_cooldown <= 3)
            return;
        if (Input.GetKey(KeyCode.Mouse0) && other.tag == "Enemy")
        {
            OnEnemyKilled?.Invoke(other.gameObject);
            attack_cooldown = 0;
        }
    }


}
