using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserBeam : MonoBehaviour
{
    public float destroyTime;
    public Vector3 pointHit;

    ParticleSystem laser;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = pointHit;
        
        Destroy(this.gameObject, destroyTime);
    }
}
