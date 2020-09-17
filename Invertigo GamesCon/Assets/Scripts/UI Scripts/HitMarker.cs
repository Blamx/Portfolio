using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HitMarker : MonoBehaviour
{

    public Player_Manager manager;
    SpriteRenderer hitMarker;
    public Sprite[] sprites = new Sprite[6];
    public int i = 0;
    float waitforSeconds = 0.02f;
    float currentT;

    // Use this for initialization
    void Start()
    {
        hitMarker = GetComponent<SpriteRenderer>();
        currentT = waitforSeconds;
        hitMarker.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.shooting.hittingPlayer)
        {
            hitMarker.enabled = true;
            currentT -= Time.deltaTime;

            if (currentT <= 0)
            {
                if (i < 5)
                {
                    i++;
                }
                else
                {
                    manager.shooting.hittingPlayer = false;
                    hitMarker.enabled = false;

                }
                if (manager.shooting.isPlayer)
                {
                    hitMarker.color = Color.white;
                }
                else
                {
                    hitMarker.color = Color.white;
                }
                hitMarker.sprite = sprites[i];

                currentT = waitforSeconds;
            }
        }
    }
}
