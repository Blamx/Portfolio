using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHole : MonoBehaviour
{
    public Player_Manager manager;

    public GameObject bulletHole;
    public GameObject bulletHoleGlow;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (manager.shooting.bulletHole)
        {
            GameObject hole = Instantiate(bulletHole);
            hole.transform.position = manager.shooting.pointHit;
            hole.transform.position = hole.transform.position + (Vector3.Normalize(manager.shooting.pointHitNormal) * 0.05f);
            hole.transform.forward = manager.shooting.pointHitNormal;

            GameObject holeGlow = Instantiate(bulletHoleGlow);
            holeGlow.transform.position = manager.shooting.pointHit;
            holeGlow.transform.position = holeGlow.transform.position + (Vector3.Normalize(manager.shooting.pointHitNormal) * 0.06f);
            holeGlow.transform.forward = manager.shooting.pointHitNormal;
 
            manager.shooting.bulletHole = false;
        }
    }
}
