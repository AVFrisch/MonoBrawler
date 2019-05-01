using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bandit : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D body2d;

    public float strikeInterval = 3f;
    private float strikeTimer;
    public float health = 3f;
    public float follow = 1f;
    public bool inv = false;
    public bool dead = false;

    public float speed = 1.5f;
    public float detectRange = 2f;
    public bool left = false;
    public bool right = false;
    public Vector3 playerDir;
    public Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
        body2d = GetComponent<Rigidbody2D>();
        strikeTimer = strikeInterval;


    }

    // Update is called once per frame
    void Update()
    {

        if (health < 1)
        {

            animator.SetTrigger("Death");
            dead = true;

        }

        if (dead)
        {
            return;
        }
        else
        {

            playerDir = (transform.position - playerPos.position);

            if (playerDir.x < follow && playerDir.x > -follow)
            {
                animator.SetInteger("AnimState", 0);
                if (strikeTimer < 0)
                {

                    animator.SetTrigger("Attack");
                    strikeTimer = strikeInterval;

                }
            }
            else if (playerDir.x > 0)
            {
                animator.SetInteger("AnimState", 2);
                body2d.velocity = new Vector2(-speed, body2d.velocity.y);
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (playerDir.x < 0)
            {
                animator.SetInteger("AnimState", 2);
                body2d.velocity = new Vector2(speed, body2d.velocity.y);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            strikeTimer -= Time.deltaTime;

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (inv)
        {
            
        }

        else if (other.CompareTag("Hitbox"))
        {
            health -= 1;
            //print("Enemy health is " + health.ToString());
            animator.SetTrigger("Hurt");
        }

    }
}