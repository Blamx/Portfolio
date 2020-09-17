using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour {

    Light lightning;

    public float timer = 0;
    bool flash;
    public int flashNum = 0;

    float[] times = new float[10];
    float[] breaks = new float[10];
    float[] intense = new float[10];


    public GameObject VisualEffect;
    // Use this for initialization
    void Start ()
    {
        lightning = gameObject.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            timer = 10.0f;
            flash = true;
            for (int i = 0; i < 10; i++)
            {
                times[i] = Random.Range(0.001f,0.1f);
                breaks[i] = Random.Range(0.001f, 0.1f);
                intense[i] = Random.Range(2f, 7f);
            }
            flashNum = 0;
        }

        if(flash)
        {
            if (times[flashNum] > 0)
            {
                VisualEffect.SetActive(true);
                times[flashNum] -= Time.deltaTime;
                lightning.intensity = intense[flashNum];
            }
            else if (breaks[flashNum] > 0)
            {
                VisualEffect.SetActive(false);
                breaks[flashNum] -= Time.deltaTime;
                lightning.intensity = 0.3f;
            }
            else
                flashNum++;
            if(flashNum >= 10)
            {
                timer = 10.0f;
                flash = false;
            }
        }


		/*if(Input.GetKey("l"))
        {
            lightning.intensity = 3.0f;
        }
        else
        {
            lightning.intensity = 0f;
        }*/
	}
}
