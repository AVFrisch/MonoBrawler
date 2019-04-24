using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] float jumpForce = 4.0f;

    public GameObject spark;

    private float inputX;
    private Animator animator;
    private Rigidbody2D body2d;
    private SpriteRenderer spriteRenderer;
    public GameController gameController;
    private bool combatIdle = false;
    private bool isGrounded = true;
    private bool flip = false;
    private bool direction = false;
    private bool lastdir = false;

    private bool parry = false;

    public float parryTime = 1f;

    public float health = 3f;
    public float iFrames = 10f;
    public bool inv = false;

    private Color clrHP3 = new Color(255, 255, 255);
    private Color clrHP2 = new Color(255, 128, 0);
    private Color clrHP1 = new Color(255, 0, 0);
    private Color clrHP0 = new Color(0, 0, 0);

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
            parry = true;
        }

        //Parry
        if (parry){
            
            //print("Parrying, parryTime = " + parryTime);
            parryTime -= Time.deltaTime;

            if (parryTime < 0)
            {
                animator.SetInteger("AnimState", 0);
                parry = false;
                parryTime = 1f;
            }
            else
            {
                animator.SetInteger("AnimState", 1);
            }

        }

        //iFrames
        //if (inv)
        //{
        //    if (iFrames < 1)
        //    {
        //        iFrames = 10f;
        //        inv = false;
        //        print("iFrames off");
        //    }
        //    else
        //    {
        //        iFrames -= 1;
        //        print("iFrames -, now" + iFrames.ToString());
        //    }
        //}


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

        if (inv)
        {

            print("Hit during inv frame, " + iFrames.ToString() + "out of 10");


            return;
            
        }
        else
        {
            if (other.CompareTag("Hitbox"))
            {

                if (parry)
                {
                    print("Parry! Health is " + health.ToString());
                    //spawn the hit effect here
                    Instantiate(spark, other.transform.position, other.transform.rotation);
                    gameController.ParryEffect();
                    //print(other.GetContact(0));
                    //inv = true;
                }
                else
                {
                    health -= 1;
                    print("Hit! Health is " + health.ToString());

                    if (health == 3) { spriteRenderer.color = clrHP3; }
                    else if (health == 2) { spriteRenderer.color = clrHP2; }
                    else if (health == 1) { spriteRenderer.color = clrHP1; }
                    else { spriteRenderer.color = clrHP0; }

                    animator.SetTrigger("Hurt");
                }

            }
        }

    }
}
