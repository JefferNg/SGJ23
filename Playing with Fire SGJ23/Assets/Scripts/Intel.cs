using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Intel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject obj = collision.gameObject;
        if (obj.name == "Player") {
            PlayerManager p = obj.GetComponent<PlayerManager>();

            p.CollectIntel();

            Destroy(this.gameObject);

        }
    }
}
