using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_System : MonoBehaviour {

    // Use this for initialization

    public GamePadManager gamePadManager;
    const int MAXSPAWNS = 64;

    public static GameObject[] spawns = new GameObject[MAXSPAWNS];
    public static GameObject[] players = new GameObject[MAXSPAWNS];
    public static bool[] safe = new bool[MAXSPAWNS];
    public static GameObject[] safeSpawns = new GameObject[MAXSPAWNS];
    static int spawnsSet = 0;
    static int playersSet = 0;

    static RaycastHit rayOutData;

    bool firstLoop = false;

	void Start ()
    {
        foreach (GameObject temp in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if(temp.name == "Spawn")
            {
               
                if(spawnsSet < MAXSPAWNS)
                {
                    spawns[spawnsSet] = temp;
                    spawnsSet++;     
                }
                else
                {
                    Debug.Log("Spawn Limit Reached");
                }
            }
        }

        foreach (GameObject temp in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (temp.name == "hitBox")
            {

                if (spawnsSet < MAXSPAWNS)
                {
                    players[playersSet] = temp;
                    playersSet++;
                }
                else
                {
                    Debug.Log("Spawn Limit Reached");
                }
            }
        }


    }
	
	// Update is called once per frame
	void Update ()
    {

       if(gamePadManager.numberOfPlayers != playersSet)
        {
            playersSet = 0;

            foreach (GameObject temp in GameObject.FindObjectsOfType(typeof(GameObject)))
            {
                if (temp.name == "hitBox")
                {

                    if (spawnsSet < MAXSPAWNS)
                    {
                        players[playersSet] = temp;
                        playersSet++;
                    }
                    else
                    {
                        Debug.Log("Player Limit Reached");
                    }
                }
            }
            Debug.Log("new player count: " + playersSet);

        }


    }
    public static GameObject findSpawn()
    {

        spawnsSet = 0;
        foreach (GameObject temp in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (temp.name == "Spawn")
            {

                if (spawnsSet < MAXSPAWNS)
                {
                    spawns[spawnsSet] = temp;
                    spawnsSet++;
                }
                else
                {
                    Debug.Log("Spawn Limit Reached");
                }
            }
        }

        foreach (GameObject temp in GameObject.FindObjectsOfType(typeof(GameObject)))
        {
            if (temp.name == "hitBox")
            {

                if (playersSet < MAXSPAWNS)
                {
                    players[playersSet] = temp;
                    playersSet++;
                }
                else
                {
                    Debug.Log("Spawn Limit Reached");
                }
            }
        }

        for (int i = 0; i < playersSet; i++)
        {
            for (int j = 0; j < spawnsSet; j++)
            {
                bool hit = Physics.Raycast(spawns[j].transform.position, spawns[j].transform.position + players[i].transform.position, out rayOutData);
                if(hit)
                {
                    if(rayOutData.transform.name != "hitBox")
                    {
                        safe[j] = true;
                    }
                    else
                    {
                        safe[j] = false;
                    }
                    //Debug.Log(rayOutData.transform.name);
                }
            }
        }
        int safeNum = 0;
        for (int i = 0; i < spawnsSet; i++)
        {
            if(safe[i])
            {
                safeSpawns[safeNum] = spawns[i];
                safeNum++;
            }
        }

            return safeSpawns[Random.Range(0,spawnsSet -1)];
    }

    public static void wipe()
    {
        spawnsSet = 0;
        players = new GameObject[MAXSPAWNS];
        spawns = new GameObject[MAXSPAWNS];
    }
}
