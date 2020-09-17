using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player_Manager manager;
    private GameObject player;
    public RawImage hp;
    Color tempColor;

    public float offset = 10.7f;

    private float start, end, t;

    // Use this for initialization
    void Start()
    {
        hp = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        tempColor = hp.color;
        tempColor.a = 1 - (manager.target.health / manager.target.MaxHealth);
        hp.color = tempColor;
        gameObject.GetComponent<RectTransform>().localScale = new Vector3(offset, 6.030001f, 5.264463f);
    }
}
