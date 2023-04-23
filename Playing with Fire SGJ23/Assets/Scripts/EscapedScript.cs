using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscapedScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            PlayerManager pm = collision.gameObject.GetComponent<PlayerManager>();
            if (pm.CheckIntel()) {
                SceneManager.LoadScene("WinScene");
            }
        }
    }

}
