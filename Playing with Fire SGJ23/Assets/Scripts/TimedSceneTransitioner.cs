using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedSceneTransitioner : MonoBehaviour
{
    [SerializeField]
    private float _delay = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (_delay < 0.0f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EnemyPlacement");
        }
        _delay -= Time.deltaTime;
    }
}
