using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationScript : MonoBehaviour
{
    public Player_Manager manager;

    public Animator anim;

    private float inputV, inputH;

    private bool running, jumping;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.playerGamepad.commands.flip || manager.movment.jumping)
        {
            jumping = true;
        }
        else
        {
            jumping = false;
        }
        
        running = manager.movment.runningAnimation;
        inputV = manager.movment.stickVelocity.y;
        inputH = manager.movment.stickVelocity.x;

        anim.SetBool("running", running);
        anim.SetBool("jumping", jumping);
        anim.SetFloat("inputV", inputV);
        anim.SetFloat("inputH", inputH);
    }
}
