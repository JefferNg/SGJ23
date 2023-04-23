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

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            if (attack_cooldown <= 3) {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.knife_miss, 0.7f, new Vector2(0.3f, 0.4f));
            } else {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.knife_miss, 0.7f, new Vector2(1.4f, 1.5f));
            }
        }
    }

        private void OnTriggerStay2D(Collider2D other)
    {
        if (attack_cooldown <= 3)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            if (other.tag == "Enemy") {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.knife, 1, new Vector2(0.95f, 1.05f));
                OnEnemyKilled?.Invoke(other.gameObject);
                attack_cooldown = 0;
            }
        }
    }


}
