using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    PauseMenu menu;

    private GameObject pauseMenu;

    void Start()
    {
        pauseMenu = GameObject.Find(transform.parent.parent.name);
    }

    public void resume()
    {
        pauseMenu.GetComponent<PauseMenu>().paused = false;
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
