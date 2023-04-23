using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float _defaultFOVSearchDelay = 0.5f;

    private Coroutine _fovSearchCoroutine = null;
    private List<FieldOfView> _enemyFieldsOfView = new List<FieldOfView>();
    private List<EnemyController> _enemyControllers = new List<EnemyController>();

    void Start()
    {
        // Register all of the enemies with the manager;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            AddEnemy(enemy);
        }

        // Subscribe to the player kill enemy event
        PlayerAttack attackScript = FindObjectOfType<PlayerAttack>();
        if (attackScript != null)
        {
            attackScript.OnEnemyKilled += onPlayerKilledEnemy;
        }

        // Kickoff the fov search coroutine
        _fovSearchCoroutine = StartCoroutine(DelayedFOVSearch(_defaultFOVSearchDelay));
    }

    public void OnDestroy()
    {
        // Unsubscribe to the player attack enemy event
        PlayerAttack attackScript = FindObjectOfType<PlayerAttack>();
        if (attackScript != null)
        {
            attackScript.OnEnemyKilled -= onPlayerKilledEnemy;
        }

        if (_fovSearchCoroutine != null)
        {
            StopCoroutine(_fovSearchCoroutine);
        }
    }

    public void AddEnemy(GameObject enemy)
    {
        FieldOfView fov;
        if (enemy.TryGetComponent<FieldOfView>(out fov))
        {
            _enemyFieldsOfView.Add(fov);
        }

        EnemyController controller;
        if (enemy.TryGetComponent<EnemyController>(out controller))
        {
            _enemyControllers.Add(controller);
        }
    }

    public void RemoveEnemy(EnemyController enemy)
    {
        _enemyControllers.Remove(enemy);
        _enemyFieldsOfView.Remove(enemy.GetComponent<FieldOfView>());

    }

    private IEnumerator DelayedFOVSearch(float delay)
    {
        WaitForSeconds delayTimer = new WaitForSeconds(delay);
        while (true)
        {
            foreach (FieldOfView fov in _enemyFieldsOfView)
            {
                fov.SearchForTargets();
            }
            yield return delayTimer;
        }
    }

    private void onPlayerKilledEnemy(GameObject enemyObject)
    {
        EnemyController enemy = null;
        if (enemyObject.TryGetComponent<EnemyController>(out enemy))
        {
            RemoveEnemy(enemy);
            Destroy(enemyObject);
        }
        
    }
}
