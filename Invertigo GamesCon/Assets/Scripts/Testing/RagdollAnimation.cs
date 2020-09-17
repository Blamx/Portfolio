using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollAnimation : MonoBehaviour
{

    public Component[] bones;
    public Animator anim;

    // Use this for initialization
    void Start()
    {
        bones = gameObject.GetComponentsInChildren<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            killRagdoll();
        }
    }

    void killRagdoll()
    {
        foreach (Rigidbody ragdoll in bones)
        {
            ragdoll.isKinematic = false;
            ragdoll.useGravity = false;
        }
        anim.enabled = false;
    }
}
