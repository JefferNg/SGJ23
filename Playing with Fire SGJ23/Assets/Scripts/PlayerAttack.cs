using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public delegate void EnemyKilledEvent(GameObject enemyObject);
    public event EnemyKilledEvent OnEnemyKilled; 
    [SerializeField] private float attack_cooldown = 3f;

    [Header("Sprint Sprite Animation")]
    [SerializeField]
    private GameObject starSprite = null;
    [SerializeField]
    private Animator starAnimator = null;

    public PlayerSprite ps; 

    //private PlayerSprite ps = null;

    private bool canKnife = true;


    private void Awake() {
        //ps = gameObject.GetComponent<PlayerSprite>();

    }

    private void Start() {
        starSprite.transform.localScale = Vector3.zero;

    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //starAnimator.SetTrigger("Flash");
            if (!canKnife) {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.knife_miss, 0.7f, new Vector2(0.3f, 0.4f));
                
            } else {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.knife_miss, 0.7f, new Vector2(1.4f, 1.5f));
                StartCoroutine(ps.KnifeAnim());
            }
        }
    }

        private void OnTriggerStay2D(Collider2D other)
    {
        if (!canKnife)
            return;
        if (Input.GetKeyDown(KeyCode.Mouse0)) {

            if (other.tag == "Enemy") {
                AudioManager.Instance.PlaySoundEffect(AudioManager.Sfx.knife, 1, new Vector2(0.95f, 1.05f));
                OnEnemyKilled?.Invoke(other.gameObject);
                AudioManager.Instance.HuntUpdate(-1);
                StartCoroutine(KillCooldown(attack_cooldown));
            }
        }
    }

    private IEnumerator KillCooldown(float timer) {
        canKnife = false;

        while (timer > 0) {
            timer -= Time.deltaTime;
            yield return null;
        }

        starAnimator.SetTrigger("Flash");
        // draw circle HUD
        //yield return new WaitForSeconds(timer);
        canKnife = true;
        // return null;
    }


}
