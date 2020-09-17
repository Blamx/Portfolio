using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControls : MonoBehaviour
{

    public GameObject masterValue, masterSlider;

    static float value ;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //dataTransfer.mixer.SetFloat("Master_Volume", masterSlider.GetComponent<Slider>().value);
        //masterValue.GetComponent<Text>().text =((int)(masterSlider.GetComponent<Slider>().value * 100)).ToString();
        //dataTransfer.mixer.GetFloat("Master_Volume",out value);

        if (Input.GetKey(KeyCode.KeypadPlus) && value <= 1)
        {
            value += 0.01f;
        }
        else if (Input.GetKey(KeyCode.KeypadMinus) && value >= 0)
        {
            value -= 0.01f;
        }

        dataTransfer.mixer.SetFloat("Master_Volume", value);
        masterSlider.GetComponent<Slider>().value = value;
        masterValue.GetComponent<Text>().text = ((int)(value * 100)).ToString();
    }
}
