using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrow : MonoBehaviour
{
    public Player_Manager manager;

    public GameObject grenadePrefab;

    public float throwForce;

    bool thrown = false;

    public float coolDown;
    float time;

    void Start()
    {
        time = coolDown;
    }

    // Update is called once per frame
    void Update()
    {
        if ((manager.playerGamepad.isKeyboard && Input.GetKeyDown("g")) || (!manager.playerGamepad.isKeyboard && manager.playerGamepad.commands.grenade) && !thrown)
        {
            GameObject grenade = Instantiate(grenadePrefab, transform.position + transform.forward, transform.rotation);
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
            thrown = true;
            time = coolDown;
        }

        if (thrown)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0 && thrown)
        {
            thrown = false;
        }
    }
}
