using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Bandit : MonoBehaviour
{
    private Animator animator;

    public float strikeInterval = 3f;

    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        strikeInterval -= Time.deltaTime;

        if (strikeInterval < 0)
        {
            animator.
        }

    }
}
