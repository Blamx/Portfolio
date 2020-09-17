using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    public float MaxHealth = 100;
    public float health;
    public float oldHealth, newHealth, prevhp;
    public float healthRegen = 1;
    public float regenDelay = 1;
    public float regenTime = 1;
    public bool isPlayer = false;
    public bool destroyOnDeath = false;
    float timer = 0;

    public int ID = -1;
    // Use this for initialization
    void Start()
    {
        health = prevhp = oldHealth = newHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if(health != prevhp)
        {
            
            timer = regenDelay;
        }

        if (timer < 0 && health < MaxHealth && health > 0)
        {
            health += healthRegen;
            timer = regenTime;
            if (health > MaxHealth)
            {
                health = oldHealth = newHealth = MaxHealth;
            }
        }

        if (destroyOnDeath && health < 0)
        {
            Destroy(gameObject);
        }

        prevhp = health;

    }
}
