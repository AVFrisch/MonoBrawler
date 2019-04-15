using UnityEngine;
using System.Collections;

public class GuardScript : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] float jumpForce = 4.0f;

    private float inputX;
    private Animator animator;
    private Rigidbody2D body2d;
    private SpriteRenderer spriteRenderer;
    private bool combatIdle = false;
    private bool isGrounded = true;
    private bool left = false;
    private bool flip = false;
    private bool direction = false;
    private bool lastdir = false;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // -- Handle input and movement --
        inputX = Input.GetAxis("Horizontal");

        if (inputX < 0)
        {
            direction = false;
        }
        else if (inputX > 0)
        {
            direction = true;
        }

        if (direction != lastdir)
        {
            flip = true;
        }

        if (flip)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            flip = false;
        }

        lastdir = direction;


        GetComponent<Transform>().localRotation.Set(0, 0, 0, 0);

        // Move
        body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

        // -- Handle Animations --
        isGrounded = IsGrounded();
        animator.SetBool("Grounded", isGrounded);

        //Death
        if (Input.GetKeyDown("k"))
            animator.SetTrigger("Death");
        //Hurt
        else if (Input.GetKeyDown("h"))
            animator.SetTrigger("Hurt");
        //Recover
        else if (Input.GetKeyDown("r"))
            animator.SetTrigger("Recover");
        //Change between idle and combat idle
        else if (Input.GetKeyDown("i"))
            combatIdle = !combatIdle;
        else if (Input.GetKeyDown("j"))
        {

        }



        //Attack
        else if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }

        //Jump
        else if (Input.GetKeyDown("space") && isGrounded)
        {
            animator.SetTrigger("Jump");
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
        }

        //Walk
        else if (Mathf.Abs(inputX) > Mathf.Epsilon && isGrounded)
            animator.SetInteger("AnimState", 2);
        //Combat idle
        else if (combatIdle)
            animator.SetInteger("AnimState", 1);
        //Idle
        else
            animator.SetInteger("AnimState", 0);

    }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, -Vector3.up, 0.03f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("hit! at " + Time.fixedTime);
    }
}
