using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeSize : MonoBehaviour {

    public Player_Manager manager;

    [Range(0,100)]
    public float Size = 1;

    public GameObject coneMat;

    public PlayerCheck coneDetect;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Vector3 Cone = new Vector3(manager.gunBehavior.currentGun.coneSize * 25, 200, manager.gunBehavior.currentGun.coneSize * 25);

        transform.localScale = Cone;
        if(coneDetect.objects.Count > 1)
        {
            coneMat.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else
        {
            coneMat.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    
}
