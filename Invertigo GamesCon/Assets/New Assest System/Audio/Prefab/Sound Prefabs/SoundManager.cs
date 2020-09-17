using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {



    public static GameObject[] playerSounds = new GameObject[1];
    public  GameObject[] Sounds = new GameObject[1];
    public static int maxSound = 30;
    public static int soundCount = 0;
    //public static GameObject[] SoundObjs = new GameObject[maxSound];
    public static List<GameObject> soundObjs = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        playerSounds = Sounds;

	}
	
	// Update is called once per frame
	void Update ()
    {
      //  Debug.Log("SoundCount: " + soundCount);
	}

    public static void createSound(int sound, Vector3 pos)
    {
        if (soundCount >= maxSound)
        {
            if(soundObjs.Count > 0)
            soundObjs[0].GetComponent<SoundInstance>().delete();
        }
        else
        {
            GameObject shootSound = Instantiate(SoundManager.playerSounds[sound]);
            shootSound.transform.position = pos;
            //Instantiate(shootSound);
            soundObjs.Add(shootSound);
        }
    }
}
