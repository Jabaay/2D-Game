using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetBool("TurnLeft", true); // play TurnLeft animation
            anim.SetBool("TurnRight", false);
        }

        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetBool("TurnRight", true); // play TurnLeft animation
            anim.SetBool("TurnLeft", false);
        }


        if (Input.GetKeyUp(KeyCode.A) ||Input.GetKeyUp(KeyCode.LeftArrow)
                || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow)) {
            anim.SetBool("TurnRight", false);
            anim.SetBool("TurnLeft", false);
        }


    }



}
