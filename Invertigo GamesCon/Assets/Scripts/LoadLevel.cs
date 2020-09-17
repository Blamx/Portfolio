using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour {

    Color temp;

    public void LoadMap1()
    {
        
        dataTransfer.hangar = true;
        SceneManager.LoadScene("Hangar");
        
    }

    public void LoadMap2()
    {
        SceneManager.LoadScene("Engine-4Player");
    }

    public void LoadMap3()
    {
        SceneManager.LoadScene("HangarNetwork");
    }
    public void LoadMap4()
    {
        SceneManager.LoadScene("Arena");
    }

    public void hover()
    {
        temp = GetComponent<Image>().color;
        temp.a = 0.0f;
        GetComponent<Image>().color = temp;
    }

    public void hoverOff()
    {
        temp = GetComponent<Image>().color;
        temp.a = 1.0f;
        GetComponent<Image>().color = temp;
    }
}
