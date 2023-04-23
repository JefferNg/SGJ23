using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    private float speed = 5f;

    public float sprint = 20f;
    public float sprint_cooldown_base = 3f;
    private float sprint_cooldown = 3f;
    private bool moving = false;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (sprint_cooldown < sprint_cooldown_base)
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
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 inputVector = new Vector2(horizontal, vertical);
        inputVector.Normalize();


        if (Input.GetKeyDown(KeyCode.LeftShift) && sprint_cooldown >= sprint_cooldown_base) {
            dash();
        }

        rb.velocity = inputVector * speed;
     
        speed = Mathf.Lerp(speed, baseSpeed, 0.05f);
        Debug.Log(sprint_cooldown);
    }

    public void dash()
    {
        speed = sprint;
        sprint_cooldown = 0;
        //rb.velocity = direction * sprint;
    }

}
