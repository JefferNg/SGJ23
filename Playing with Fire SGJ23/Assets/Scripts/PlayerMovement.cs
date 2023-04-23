using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed = 5f;
    private float speed = 5f;

    public float sprint = 15f;
    public const float sprint_cooldown_base = 2f;
    //private float sprint_cooldown = sprint_cooldown_base;
    private bool canSprint = true;
    private Coroutine sprintCoroutine = null;

    public Rigidbody2D rb;

    [Header("Sprint Sprite Animation")]
    [SerializeField]
    private GameObject starSprite = null;
    [SerializeField]
    private Animator starAnimator = null;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        starSprite.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (sprint_cooldown < sprint_cooldown_base)
        //{
        //    sprint_cooldown += Time.deltaTime;
        //}

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


        if (Input.GetKeyDown(KeyCode.LeftShift) && canSprint) {
            dash();
        }

        rb.velocity = inputVector * speed;
     
        speed = Mathf.Lerp(speed, baseSpeed, 0.05f);
        //Debug.Log(sprint_cooldown);
    }

    public void dash()
    {
        speed = sprint;
        //sprint_cooldown = 0;
        //canSprint = false;
        sprintCoroutine = StartCoroutine(SprintCooldown(sprint_cooldown_base));
        //rb.velocity = direction * sprint;
    }

    private IEnumerator SprintCooldown(float timer) {
        canSprint = false;

        while (timer > 0) {
            timer -= Time.deltaTime;
            yield return null;
        }

        starAnimator.SetTrigger("Flash");
        // draw circle HUD
        
        
        //yield return new WaitForSeconds(timer);
        canSprint = true;
       // return null;
    }
 
}
