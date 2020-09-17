using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayersInMenu : MonoBehaviour {

    public GameObject p1, p2, p3, p4;

    int playerNum;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerNum = 0;
        for (int i = 0; i < dataTransfer.active.Length; i++)
        { 
            if(dataTransfer.active[i])
            {
                playerNum++;
            }
        }

		if(playerNum > 3)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(true);
        }
        else if (playerNum > 2)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(true);
            p4.SetActive(false);
        }
        else if (playerNum > 1)
        {
            p1.SetActive(true);
            p2.SetActive(true);
            p3.SetActive(false);
            p4.SetActive(false);
        }
        else if (playerNum > 0)
        {
            p1.SetActive(true);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
        }
        else
        {
            p1.SetActive(false);
            p2.SetActive(false);
            p3.SetActive(false);
            p4.SetActive(false);
        }

    }
}
