using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public Player_Manager manager;

    public string playername;
    public int team = 0;
    public int score = 0;
    public float kills = 0;
    public float deaths = 0;
    public int place = 0;
    public int playerNumber = 0;
    public bool respawn;
    public bool isDead = false;
    public GameObject deathInfo;
    public GameObject lastHitBy;
    float timer = 0;
    public bool dead;


    public GameObject noConnection;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        /*  if(transform.parent.GetComponent<NetworkManager>().connected)
          {
              noConnection.SetActive(false);
          }
          else
          {
              noConnection.SetActive(true);
          }*/

        //noConnection.GetComponent<Text>().text = transform.parent.GetComponent<NetworkManager>().output;

        //if (Observer.logList.Count > 0)
        //{
        //    print(Observer.logList[0] + "                 " + gameObject.name);
        //}

        timer -= Time.deltaTime;

        if (manager.target.health <= 0 && !dead)
        {
            timer = 4;
            dead = true;

            if (!Observer.logList.Contains(this.gameObject))
            {
                Observer.logList.Add(this.gameObject);
            }
        }
        if (dead && timer < 0)
        {
            dead = false;
            deaths++;
            manager.movment.transform.position = Spawn_System.findSpawn().transform.position;
            manager.movment.transform.rotation = Spawn_System.findSpawn().transform.rotation;
            manager.target.health = manager.target.oldHealth = manager.target.newHealth = manager.target.MaxHealth;
            respawn = true;
            deathInfo = transform.gameObject;
            //if (Observer.logList.Contains(this.gameObject))
            //{
            //    Observer.logList.Remove(this.gameObject);
            //}
        }
    }
}