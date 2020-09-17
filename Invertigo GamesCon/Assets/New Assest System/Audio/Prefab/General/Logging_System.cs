using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class Logging_System : MonoBehaviour {

    [DllImport("Logging")]
    public static extern int getSize();

    [DllImport("Logging")]
    public static extern void SaveDataToFile();

    [DllImport("Logging")]
    public static extern void LoadDataFromFile();

    [DllImport("Logging")]
    public static extern void logKill(float x1, float y1, float z1, float x2, float y2, float z2);

    [DllImport("Logging")]
    public static extern float returnVec(bool isKill, int posInArr, int posInVec);

    GameObject kill;
    GameObject death;

    Vector3[] killPos;
    Vector3[] deathPos;

    // Use this for initialization
    void Start ()
    {
       kill = GameObject.Find("KillLocation");
       death = GameObject.Find("DeathLocation");

    }
	
	// Update is called once per frame
	void Update () {

        if (killPos != null)
        {
            for (int i = 0; i < killPos.Length; i++)
            {
                Debug.DrawLine(killPos[i], deathPos[i]);
            }
        }
        if (Input.GetKeyDown("m"))
        {
            saveKills();
            Debug.Log("Saved!");
        }
        if (Input.GetKeyDown("n"))
        {
            LoadDataFromFile();

            killPos = new Vector3[getSize()];
            deathPos = new Vector3[getSize()];

            for (int i = 0; i < getSize(); i++)
            {
                killPos[i] = new Vector3(returnVec(true,i,1), returnVec(true, i, 2), returnVec(true, i, 3));
                deathPos[i] = new Vector3(returnVec(false, i, 1), returnVec(false, i, 2), returnVec(false, i, 3));
            }
            for (int i = 0; i < getSize(); i++)
            {
                kill.transform.position = killPos[i];
                Instantiate<GameObject>(kill);

                death.transform.position = deathPos[i];
                Instantiate<GameObject>(death);
            }
        }


    }

    static public void sendKill(Vector3 kill, Vector3 death)
    {
        logKill(kill.x, kill.y, kill.z, death.x, death.y, death.z);
    }
    static public void saveKills()
    {
        SaveDataToFile();
    }
}
