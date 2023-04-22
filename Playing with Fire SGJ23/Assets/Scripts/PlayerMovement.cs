using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float sprint = 20f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontal * sprint, vertical * sprint);
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }

        
    }
}
