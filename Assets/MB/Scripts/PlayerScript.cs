using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] float jumpForce = 4.0f;

    private Vector3 adjSpawnPos;
    private Quaternion adjSpawnRot;

    public GameObject spark;
    public GameObject parryNote;
    public GameObject hit1;
    public GameObject hit2;

    private float inputX;
    private Animator animator;
    private Rigidbody2D body2d;
    private SpriteRenderer spriteRenderer;
    public GameController gameController;
    private bool combatIdle = false;
    private bool isGrounded = true;

    private bool parry = false;

    public float parryTime = 0.5f;

    public float health = 3f;
    public float iFrames = 10f;
    public bool inv = false;
    public bool dead = false;

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

        if (dead) {

            animator.SetTrigger("Death");
            return;

        }

        // -- Handle input and movement --
        inputX = Input.GetAxis("Horizontal");

        if (inputX > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (inputX < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


        //GetComponent<Transform>().localRotation.Set(0, 0, 0, 0);

        // Move
        body2d.velocity = new Vector2(inputX * speed, body2d.velocity.y);

        // -- Handle Animations --
        isGrounded = IsGrounded();
        animator.SetBool("Grounded", isGrounded);

        //Parry
        if (Input.GetMouseButtonDown(1))
        {
            parry = true;
        }

        if (parry){
            
            //print("Parrying, parryTime = " + parryTime);
            parryTime -= Time.deltaTime;

            if (parryTime < 0)
            {
                animator.SetInteger("AnimState", 0);
                parry = false;
                parryTime = 0.5f;
            }
            else
            {
                animator.SetInteger("AnimState", 1);
            }

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

        if (inv)
        {

            //print("Hit during inv frame, " + iFrames.ToString() + "out of 10");


            return;
            
        }
        else if (dead)
        {

            return;

        }
        else
        {
            if (other.CompareTag("Hitbox"))
            {

                if (parry)
                {
                    
                    //print("Parry! Health is " + health.ToString());
                    adjSpawnPos = new Vector3(other.transform.position.x, other.transform.position.y + 1.5f, other.transform.position.z);
                    Quaternion upwards = Quaternion.LookRotation(Vector3.up);
                    Instantiate(spark, adjSpawnPos, upwards);
                    Instantiate(parryNote, adjSpawnPos, upwards);
                    gameController.ParryEffect();
                    parryTime = 0.25f;
                    //print(other.GetContact(0));
                    //inv = true;
                }
                else
                {
                    health -= 1;
                    adjSpawnPos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                    Quaternion upwards = Quaternion.LookRotation(Vector3.up);
                    Instantiate(hit1, adjSpawnPos, upwards);
                    //print("Hit! Health is " + health.ToString());

                    if (health == 3) { spriteRenderer.color = clrHP3; }
                    else if (health == 2) { spriteRenderer.color = clrHP2; }
                    else if (health == 1) { spriteRenderer.color = clrHP1; }
                    else {
                        dead = true;
                        gameController.GameOver = true;
                        spriteRenderer.color = clrHP0;
                    }

                    animator.SetTrigger("Hurt");
                }

            }
        }

    }
}
