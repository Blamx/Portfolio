using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchLog : MonoBehaviour
{
    public Player_Manager manager;

    public Text matchLog;

    public List<GameObject> listCheck = new List<GameObject>();
    public List<GameObject> logCheck = new List<GameObject>();

    public GameObject check;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Observer.logList.Count > 0)
        {
            for (int i = 0; i < Observer.logList.Count; i++)
            {
                {/*
                    if (!listCheck.Contains(Observer.logList[i]))
                    {
                        listCheck.Add(Observer.logList[i]);
                        print(manager.playerStats.gameObject.name + "        Observer.loglist[" + i + "]: " + Observer.logList[i].name + "           listCheck[" + i + "]: " + listCheck[i].name);
                        if (Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.parent.parent == null)
                        {
                            Text Log = Instantiate(matchLog, this.transform);
                            Log.name = "Log" + i;
                            Log.text = Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.name;
                            Log.gameObject.transform.position = new Vector3(Log.gameObject.transform.position.x, Log.gameObject.transform.position.y + (0.01f * i), Log.gameObject.transform.position.z);
                            //logCheck.Add(Log.gameObject);
                            //print(Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.name);
                        }
                        else
                        {
                            Text Log = Instantiate(matchLog, this.transform);
                            Log.name = "Log" + i;
                            Log.text = Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.parent.parent.name;
                            Log.gameObject.transform.position = new Vector3(Log.gameObject.transform.position.x, Log.gameObject.transform.position.y + (0.01f * i), Log.gameObject.transform.position.z);
                            //logCheck.Add(Log.gameObject);
                            //print(Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.parent.parent.name);
                        }
                    }
                    //print(listCheck[i] + "         i: " + i + "        List Size: " + listCheck.Count);
                    //print(Observer.logList[i] + "         i: " + i + "        List Size: " + Observer.logList.Count);
                    //if (!listCheck.Contains(Observer.logList[i]))
                    //{

                    //}
                */
                }
                if (Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.parent.parent == null)
                {
                    Text Log = Instantiate(matchLog, this.transform, false);
                    Log.name = "Log" + logCheck.Count;
                    Log.text = Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.name;
                    //Log.gameObject.transform.position = new Vector3(Log.gameObject.transform.position.x, Log.gameObject.transform.position.y + (0.01f * i), Log.gameObject.transform.position.z);
                    logCheck.Add(Log.gameObject);
                    //print(Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.name);
                }
                else
                {
                    Text Log = Instantiate(matchLog, this.transform, false);
                    Log.name = "Log" + logCheck.Count;
                    Log.text = Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.parent.parent.name;
                    //Log.gameObject.transform.position = new Vector3(Log.gameObject.transform.position.x, Log.gameObject.transform.position.y + (0.01f * i), Log.gameObject.transform.position.z);
                    logCheck.Add(Log.gameObject);
                    //print(Observer.logList[i].GetComponent<PlayerStats>().gameObject.name + " Killed by " + Observer.logList[i].GetComponent<PlayerStats>().lastHitBy.gameObject.transform.parent.parent.name);
                }

                if (!manager.playerStats.dead)
                {
                    Observer.logList.Remove(Observer.logList[i]);
                    //   listCheck.Remove(listCheck[i]);
                }
            }

            for (int i = 0; i < logCheck.Count; i++)
            {
                if (logCheck[i] != null)
                    logCheck[i].gameObject.transform.position = new Vector3(logCheck[i].gameObject.transform.position.x, logCheck[i].gameObject.transform.position.y + (0.01f * i), logCheck[i].gameObject.transform.position.z);

            }
        }

        //if (listCheck.Count > 0)
        //{
        //    for (int i = 0; i < listCheck.Count; i++)
        //    {
        //        listCheck.Remove(listCheck[i]);
        //    }
        //}
        for (int i = 0; i < logCheck.Count; i++)
        {
            if (logCheck[i] == null)
                logCheck.Remove(logCheck[i]);
        }
    }
}
