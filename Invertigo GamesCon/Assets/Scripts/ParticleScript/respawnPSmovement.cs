using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPSmovement : MonoBehaviour {

    public Vector3 rotateSpeed;
    public float descendSpeed;
    public GameObject player;

    ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > (player.transform.position.y))
        {
            //Debug.Break();
            transform.position = new Vector3(transform.position.x, transform.position.y - descendSpeed, transform.position.z);
        }

        if (!ps.IsAlive())
        {
            Debug.Break();  
            //player.GetComponent<RespawnPS>().manager.playerStats.isDead = false;
        }
    }
}
