using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    GameObject hitmark;
    GameObject NoController;
    GamePad_Manager gamePad;
    GameObject ironSight;
    private float hitTime;
    // Use this for initialization
    void Start()
    {
        ironSight = GameObject.Find("Iron");
        hitmark = GameObject.Find(transform.parent.name + "/Camera/UI/hitMarker");
        NoController = GameObject.Find(transform.parent.name + "/Camera/UI/NoController");
        gamePad = transform.parent.gameObject.GetComponent<GamePad_Manager>();
    }

    // Update is called once per frame
    void Update()
    {


        if (gamePad.getGamePadConnected())
        {
            NoController.SetActive(false);
        }
        else
        {
            NoController.SetActive(false);
        }

        if (transform.gameObject.GetComponent<Combat_Shoot>().Hit)
        {
            hitTime = 0.3f;
            hitmark.SetActive(true);
            transform.gameObject.GetComponent<Combat_Shoot>().Hit = false;
        }

        hitTime -= Time.deltaTime;// time the hitmarker is displayed

        //hit marker display
        if (hitTime < 0)
        {
            hitmark.SetActive(false);
        }
    }
}

