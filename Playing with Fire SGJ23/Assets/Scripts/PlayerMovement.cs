using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

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
        if (sprint_cooldown < 5)
        {
            sprint_cooldown += Time.deltaTime;
        }

        LookAtMouse();
        Move();
    }

    private void LookAtMouse()
    {
        Vector2 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (Vector2)mousePos - new Vector2(transform.position.x, transform.position.y);
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

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

}
