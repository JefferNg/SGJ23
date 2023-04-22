using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float sprint = 20f;
    public float sprint_cooldown = 5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (sprint_cooldown < 5)
        {
            sprint_cooldown += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift) && sprint_cooldown >= 5)
        {
            if (Input.GetKey(KeyCode.W))
            {
                dash(Vector2.up);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                dash(Vector2.left);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dash(Vector2.down);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dash(Vector2.right);
            }

            sprint_cooldown = 0;
        }
        else
        {
            rb.velocity = new Vector2(horizontal * speed, vertical * speed);
        }

        
    }

    public void dash(Vector2 direction)
    {
        rb.velocity = direction * sprint;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collided");
    }
}
