using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public Light hitLight;
    public PlayerScript playerChar;
    private Animator lightAnimator;
    public bool GameOver = false;


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

        if (Input.GetAxis("Cancel") != 0)
        {

            print("bye");
            Application.Quit();
            
        }

        if (Input.GetAxis("Submit")!= 0 && GameOver)
        {

            print("Restarting...");
            SceneManager.LoadScene("Main");

        }


    }

    public void ParryEffect()
    {

        lightAnimator.SetTrigger("Parry");

    }

}
