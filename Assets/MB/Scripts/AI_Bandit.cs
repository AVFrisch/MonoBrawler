using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bandit : MonoBehaviour
{
    private Animator animator;

    public float strikeInterval = 3f;
    private float strikeTimer;
    public float health = 3f;
    public bool inv = false;
    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
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