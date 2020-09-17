using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheck : MonoBehaviour {

    public Player_Manager manager;
    public List<GameObject> objects = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "hitBox")
        {
            objects.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (objects.Contains(other.gameObject))
        {
            objects.Remove(other.gameObject);
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
      /*  if (objects.Count > 1)
            Debug.Log(objects[1].name);*/
        for (int i = 1; i < objects.Count; i++)
        {
            if (!NetworkManager.online)
            {
                if (objects[i].GetComponent<NewMovment>().manager.playerStats.dead)
                {
                    objects.Remove(objects[i].gameObject);
                }
                if (manager.playerStats.respawn)
                {
                    objects.Remove(objects[i].gameObject);
                }
            }
        }
	}
}
