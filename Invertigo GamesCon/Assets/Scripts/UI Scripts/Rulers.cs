using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rulers : MonoBehaviour
{
    public Player_Manager manager;

    private GameObject player, cam;

    float angle;

    float t;

    RectTransform rect;

    public Text text;

    Vector3 tempPos;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find(transform.parent.parent.parent.parent.name + "/Character");
        cam = GameObject.Find(transform.parent.parent.parent.parent.name + "/Camera");
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        angle = Vector3.Angle(manager.movment.transform.up, manager.cam.transform.forward);

        tempPos = rect.localPosition;

        if (angle < 90)
        {
            t = 1 - (angle / 90.0f);

            text.text = ((int)(90 - angle)).ToString();

            tempPos.y = Mathf.Lerp(0, -49.5f, t);
        }
        else if (angle > 90)
        {
            t = angle - 90;

            t = (angle / 90.0f) - 1;

            text.text = ((int)(angle - 90)).ToString();

            tempPos.y = Mathf.Lerp(0, 53.5f, t);
        }

        rect.localPosition = tempPos;

        //print(angle + "                 " + t + "         " + rect.localPosition);
    }
}
