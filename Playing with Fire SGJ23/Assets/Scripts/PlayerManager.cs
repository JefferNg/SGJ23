using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    private bool intelCollected = false;

    public void Die() {
        AudioManager.Instance.Reset();
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
