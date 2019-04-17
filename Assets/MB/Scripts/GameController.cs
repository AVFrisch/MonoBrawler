using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public HitLight hitLight;
    public GuardScript playerChar;


    // Start is called before the first frame update
    void Start()
    {

        playerChar = GetComponent<GuardScript>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (playerChar.health < 3)
        {
            hitLight.enabled = true;
        }


    }
}
