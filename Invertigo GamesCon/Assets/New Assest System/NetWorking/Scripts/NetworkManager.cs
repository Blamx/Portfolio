using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using System.Threading;
using UnityEngine;

public class NetworkManager : MonoBehaviour {


    [DllImport("udptcpClient")]
    public static extern int init(string IP);

    [DllImport("udptcpClient")]
    public static extern int update(string message);

    [DllImport("udptcpClient")]
    public static extern int checkPlayers();

    [DllImport("udptcpClient")]
    public static extern int getID();

    [DllImport("udptcpClient")]
    public static extern player getPlayer(int ID);

    [DllImport("udptcpClient")]
    public static extern void setPlayer(player p);

    [DllImport("udptcpClient")]
    public static extern void UpdateLoop();

    [DllImport("udptcpClient")]
    public static extern player getPlayerList(int listLocation);

    [DllImport("udptcpClient")]
    public static extern void tcpReceive();
    [DllImport("udptcpClient")]
    public static extern void udpReceive();


    [DllImport("udptcpClient")]
    public static extern int getDamage();

    [DllImport("udptcpClient")]
    public static extern void sendDamage(int ID, int newHp);

    [DllImport("udptcpClient")]
    public static extern IntPtr getDebug();

    [DllImport("udptcpClient")]
    public static extern void dead(bool state);
    public struct Vec
    {
        public float x, y, z;
    }


    public struct player
    {
        public Vec pos;
        public Vec rot;
        public int hp;
        public int ID;

        public bool death;
    }

    int numberOfPlayers = 0;

    public int ID;

    List<player> playerList;

    List<GameObject> playerModels;

    public GameObject currentPlayer;

    public bool playerSpawn = false;

    public GameObject cube;

    public string IP = "127.0.0.1";

    public bool connected = false;

    bool running = true;

    public int listSize = 0;

    public string output = "null";

    public bool isDead = false;

    public static bool online;
    bool playerset;

    int timesHit = 0;

    Thread connectionThread;
    Thread updateThread;
    Thread udp;
    Thread tcp;
    // Use this for initialization
    void Start()
    {
        connectionThread = new Thread(CheckForConnection);

        //udp = new Thread(udpUpdate);
        //tcp = new Thread(tcpUpdate);
        updateThread = new Thread(callUpdateLoop);
        connectionThread.Start();
        updateThread.Start();

        playerModels = new List<GameObject>();
        playerList = new List<player>();

        online = false;

        IP = dataTransfer.ip;
    }

    // Update is called once per frame
    void Update()
    {
        //string msg = "";


        listSize = playerList.Count;
        Application.runInBackground = true;


        if (connected)
        {
            online = true;
            //sendDam(0, 10);
            numberOfPlayers = checkPlayers();
            ID = getID();

            if (connectionThread.IsAlive)
            {
                connectionThread.Join();
                //tcp.Start();
                //udp.Start();
            }


            if (GameObject.Find("Player5") && !playerset)
            {
                currentPlayer = GameObject.Find("Player5");
                Vector3 pos = currentPlayer.GetComponent<Player_Manager>().movment.transform.position;
                Vector3 rot = currentPlayer.GetComponent<Player_Manager>().movment.transform.eulerAngles;

                //msg = currentPlayer.GetComponent<Player_Manager>().movment.transform.position.ToString();

                player t = new player();
                t.pos.x = pos.x;
                t.pos.y = pos.y;
                t.pos.z = pos.z;

                t.rot.x = rot.x;
                t.rot.y = rot.y;
                t.rot.z = rot.z;

                t.ID = ID;
                setPlayer(t);

               
                dead(isDead);
                //playerset = true;
            }

            if (numberOfPlayers > 0)
            {
                if (playerList.Count < numberOfPlayers)
                {
                    player basePlayer = new player();
                    playerList.Add(basePlayer);
                    playerList[playerList.Count - 1] = getPlayerList(playerList.Count - 1);
                    if(playerList.Count - 1 != getID())
                    {
                        cube.transform.GetChild(0).GetChild(0).GetComponent<Target>().ID = playerList.Count - 1;
                    }
                    else
                    {
                        cube.transform.GetChild(0).GetChild(0).GetComponent<Target>().ID = playerList.Count;
                    }
                    playerModels.Add(Instantiate<GameObject>(cube));
                }
                else if (playerList.Count > 0)
                {
                    for (int i = 0; i < playerList.Count; i++)
                    {
                        playerList[i] = getPlayerList(i);
                        //output = playerModels[i].GetComponent<NetPlayer>().target.ID.ToString();

                       
                        Vector3 position;

                        position.x = playerList[i].pos.x;
                        position.y = playerList[i].pos.y;
                        position.z = playerList[i].pos.z;

                        Vector3 rotation;

                        rotation.x = playerList[i].rot.x;
                        rotation.y = playerList[i].rot.y;
                        rotation.z = playerList[i].rot.z;


                        playerModels[i].transform.position = position;
                        playerModels[i].transform.eulerAngles = rotation;

                        if(playerList[i].death == true)
                        {
                            playerModels[i].transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                           
                        }
                        else
                        {
                            playerModels[i].transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
                        
                        }


                        //output = playerList[i].death.ToString();

                        //msg += i + "/" + playerList[i].ID.ToString() + ": " + position.ToString(); 
                    }
                }

            }
            else
            {
                //msg = "player not active";
            }


            int dam = getDamage();

            if(dam > 0)
            {
                currentPlayer.GetComponent<Player_Manager>().target.health -= dam;
                timesHit++;
            }


            //output = timesHit.ToString() + " /  "  + dam;
            dam = 0;

            //msg = getDamage().ToString();//checkPlayers().ToString() + " / " + playerList.Count;
        }


      
        //output = msg;
        //output = Marshal.PtrToStringAnsi(getDebug());

    }
    void CheckForConnection()
    {
        while (!connected)
        {
            if (init(IP) == 1)
            {
                connected = true;
            }
        }
    }

    void callUpdateLoop()
    {
        while (running)
        {
            Thread.Sleep(50);
            if (connected)
                UpdateLoop();

        }
    }

    void udpUpdate()
    {
        while (running)
        {
            if (connected)
                udpReceive();
        }
    }
    void tcpUpdate()
    {
        while (running)
        {
            if (connected)
                tcpReceive();
        }
    }
    private void OnDestroy()
    {
        running = false;
        //if (updateThread.IsAlive)
        //updateThread.Abort();
        //connectionThread.Join();
        //udp.Join();
        //tcp.Join();
    }
    public void sendDam(int dam, int id)
    {
        if (connected)
        {
            sendDamage(dam, id);

            //output = id.ToString();
        }
    }

}
