using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private float _defaultFOVSearchDelay = 0.5f;

    private Coroutine _fovSearchCoroutine = null;
    private List<FieldOfView> _enemyFieldsOfView = new List<FieldOfView>();

    void Start()
    {
        // Register all of the enemies with the manager;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            FieldOfView fov;
            if (enemy.TryGetComponent<FieldOfView>(out fov))
            {
                _enemyFieldsOfView.Add(fov);
            }
        }

        // Kickoff the fov search coroutine
        _fovSearchCoroutine = StartCoroutine(DelayedFOVSearch(_defaultFOVSearchDelay));
    }

    public void RemoveFOV(FieldOfView fov)
    {
        _enemyFieldsOfView.Remove(fov);
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
}
