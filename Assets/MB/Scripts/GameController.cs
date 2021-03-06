﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Light hitLight;
    public PlayerScript playerChar;
    private Animator lightAnimator;


    // Start is called before the first frame update
    void Start()
    {

        hitLight = hitLight.GetComponent<Light>();
        playerChar = playerChar.GetComponent<PlayerScript>();
        lightAnimator = hitLight.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        //if (playerChar.health == 3)
        //{
        //    hitLight.intensity = 0;
        //}
        //else if (playerChar.health == 2)
        //{
        //    hitLight.intensity = 1;
        //}
        //else if (playerChar.health == 1)
        //{
        //    hitLight.intensity = 2;
        //}
        //else if (playerChar.health == 0)
        //{
        //    hitLight.intensity = 3;
        //}
        //else
        //{
        //    hitLight.intensity = 10;
        //}


    }

    public void ParryEffect()
    {

        lightAnimator.SetTrigger("Parry");

    }

}
