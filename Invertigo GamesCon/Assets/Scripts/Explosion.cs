using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float force, radius, damage;

    float current;

    public GameObject explosionEffect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Target>().health < 0)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    if (nearbyObject.GetComponent<Target>())
                    {
                        float dist = Vector3.Distance(nearbyObject.gameObject.transform.position, this.gameObject.transform.position);
                        if (dist <= 2)
                        {
                            nearbyObject.GetComponent<Target>().oldHealth = nearbyObject.GetComponent<Target>().health;
                            nearbyObject.GetComponent<Target>().health -= damage;
                            nearbyObject.GetComponent<Target>().newHealth = nearbyObject.GetComponent<Target>().health;
                        }
                        else if (dist > radius)
                        {
                            dist = radius;

                            nearbyObject.GetComponent<Target>().oldHealth = nearbyObject.GetComponent<Target>().health;
                            nearbyObject.GetComponent<Target>().health -= damage * (1 - (dist / radius));
                            nearbyObject.GetComponent<Target>().newHealth = nearbyObject.GetComponent<Target>().health;
                        }
                        //print(dist + "   " + radius + "      " + (damage * (1 - (dist / radius))));

                        nearbyObject.GetComponent<Target>().oldHealth = nearbyObject.GetComponent<Target>().health;
                        nearbyObject.GetComponent<Target>().health -= damage * (1 - (dist / radius));
                        nearbyObject.GetComponent<Target>().newHealth = nearbyObject.GetComponent<Target>().health;
                        //nearbyObject.GetComponent<Target>().health -= 60.0f;
                    }

                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
        }
    }
}
