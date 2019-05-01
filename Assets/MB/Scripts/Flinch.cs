﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flinch : MonoBehaviour
{

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Guard"))
        {
            animator.SetTrigger("Hurt");
        }
    }

}