using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool intelCollected = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("DeathScene");
    }

    public void CollectIntel() {
        Debug.Log("Intel collected");
        intelCollected = true;
    }

    public bool CheckIntel() {
        return intelCollected;
    }

}
