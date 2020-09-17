using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;

public class MenuStates : MonoBehaviour
{
	public AudioMixer mixer;

	//0 = baseMenu 1 = localMultiplayer;
	public static int menuState = 0;


    public GameObject main, local, options, back;

    public GameObject[] camTargets = new GameObject[2];

    public Text gunGame;

    public GameObject camPivot;

    public string IP;

    public InputField IPField;
    // Use this for initialization
    void Start()
    {
		dataTransfer.mixer = mixer;
    }

    // Update is called once per frame
    void Update()
    {
        IP = IPField.text;
        dataTransfer.ip = IP;

        if (menuState != 0)
        {
            back.SetActive(true);
        }
        if (menuState == 0)
        {
            main.SetActive(true);
            local.SetActive(false);
            back.SetActive(false);
            options.SetActive(false);
        }
        else if (menuState == 1)
        {
            main.SetActive(false);
            local.SetActive(true);
            options.SetActive(false);
        }
        else if (menuState == 2)
        {
            main.SetActive(false);
            local.SetActive(false);
            options.SetActive(true);
        }

        if (camTargets.Length > menuState)
        {
            camPivot.transform.rotation = Quaternion.Lerp(camPivot.transform.rotation, camTargets[menuState].transform.rotation, 0.1f);
            camPivot.transform.position = Vector3.Lerp(camPivot.transform.position, camTargets[menuState].transform.position, 0.1f);
        }

        if (dataTransfer.gunGame == false)
        {
            gunGame.text = "GunGame: OFF";
        }
        else if (dataTransfer.gunGame == true)
        {
            gunGame.text = "GunGame: ON";
        }
    }

    public void setMenu(int state)
    {
        menuState = state;
    }
    public void setGunGame()
    {
        if (dataTransfer.gunGame == false)
        {
            dataTransfer.gunGame = true;
            gunGame.text = "GunGame: ON";
        }
        else if (dataTransfer.gunGame == true)
        {
            dataTransfer.gunGame = false;
            gunGame.text = "GunGame: OFF";
        }
    }
}
