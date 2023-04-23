using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;

    public float transition_time = 1f;

    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        //button = GetComponent<Button>();
        button = GetComponentInChildren<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transition_time);
        SceneManager.LoadScene(levelIndex);
    }

}
