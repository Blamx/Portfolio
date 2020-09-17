using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "FeetCube")
        {

        }
        else if (other.GetComponent<Target>())
        {
            //other.GetComponent<Target>().oldHealth = other.GetComponent<Target>().health;
            other.GetComponent<Target>().health = 0;
            other.GetComponent<Target>().newHealth = other.GetComponent<Target>().oldHealth = other.GetComponent<Target>().health;
            other.gameObject.transform.parent.parent.GetComponent<PlayerStats>().lastHitBy = this.gameObject;
        }
        else
        {
            Destroy(other.gameObject);
        }

        if (GameObject.Find("Suction Zone"))
        {
            GameObject.Find("Suction Zone").GetComponent<SpaceSuction>().objects.RemoveAll(GameObject => GameObject == null);
        }
    }
}
