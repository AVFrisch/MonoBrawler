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
    public bool inv = false;
    public bool dead = false;

    public float speed = 1;
    public float detectRange = 2f;
    public bool left = false;
    public bool right = false;
    public Vector3 detectAdjust = new Vector3(0, 0.5f, 0);
    public Transform playerDetect;
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

            //RaycastHit2D lookR = Physics2D.Raycast(playerDetect.position + detectAdjust, playerDetect.position + Vector3.right, detectRange);
            //RaycastHit2D lookL = Physics2D.Raycast(playerDetect.position + detectAdjust, playerDetect.position + Vector3.left, detectRange);

            //Debug.DrawLine(playerDetect.position, playerDetect.position + Vector3.left);

            //print(lookL.collider);

            //if (lookL.collider == false) { left = false; }
            //else { left = true; }

            //if (lookR.collider == false) { right = false; }
            //else { right = true; }

            //if (left){
            //    //print("Left " + lookL.collider.name);
            //    if (lookL.collider.name == "Player")
            //    {
            //        body2d.velocity = new Vector2(-speed, body2d.velocity.y);
            //    }
            //}
            //else if (right)
            //{
            //    print("Right " + lookR.collider.tag);
            //    if (lookR.collider.tag == "Player")
            //    {
            //        body2d.velocity = new Vector2(speed, body2d.velocity.y);
            //    }
            //}


            body2d.velocity = Vector2.MoveTowards(playerPos.position, transform.position, speed);
            Debug.DrawLine(transform.position, playerPos.position);
            //print();

            strikeTimer -= Time.deltaTime;

            if (strikeTimer < 0)
            {

                animator.SetTrigger("Attack");
                strikeTimer = strikeInterval;

            }

        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (inv)
        {
            print("invulnerable");
        }

        else if (other.CompareTag("Player"))
        {
            health -= 1;
            print("Enemy health is " + health.ToString());
            animator.SetTrigger("Hurt");
        }
    }
}