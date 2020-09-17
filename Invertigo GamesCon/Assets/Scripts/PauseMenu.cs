using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject screen, rulers, scoreBoard;

    private GameObject player, score;

    GameObject[] playerScore, playerName;

    public bool paused = false;

    public int rank;

    RectTransform transformRect;

    Vector3 nameTempPos, scoreTempPos;

    //Use this for initialization

    void Start()
    {
        screen.SetActive(false);
        scoreBoard.SetActive(false);
        player = GameObject.Find(transform.parent.parent.parent.name + "/Character");
        score = GameObject.Find(transform.parent.parent.parent.name + "/Camera/UI/Score");

        playerName = GameObject.FindGameObjectsWithTag(transform.parent.parent.parent.name + "Name");
        playerScore = GameObject.FindGameObjectsWithTag(transform.parent.parent.parent.name + "Score");

        for (int i = 0; i < playerName.Length; i++)
        {
            playerName[i].SetActive(false);
            playerScore[i].SetActive(false);
        }
    }

    //Update is called once per frame

    void Update()
    {
        rank = score.GetComponent<Score>().rank;

        if (Input.GetKeyDown("p"))
        {
            paused = !paused;
        }
        if (Input.GetKeyDown("r"))
        {
            reload();
        }

        nameTempPos = playerName[0].GetComponent<RectTransform>().localPosition;
        scoreTempPos = playerScore[0].GetComponent<RectTransform>().localPosition;
        // rank 1 = 26.52 
        // rank 2 = 5.45
        // rank 3 = -15.62
        // rank 4 = -36.69

        if (paused)
        {
            setPausePos();
            for (int i = 0; i < playerName.Length; i++)
            {
                setScoreInfo(i);
                playerName[i].SetActive(true);
                playerScore[i].SetActive(true);
            }
            screen.SetActive(true);
            rulers.SetActive(false);

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0;
        }
        if (!paused)
        {
            for (int i = 0; i < playerName.Length; i++)
            {
                playerName[i].SetActive(false);
                playerScore[i].SetActive(false);
            }
            screen.SetActive(false);
            rulers.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1;

            if (Input.GetKey("m") || Gametype_Manager.GameOver)
            {
                scoreBoard.SetActive(true);

                setScoreBoardPos();
                for (int i = 0; i < playerName.Length; i++)
                {
                    setScoreInfo(i);
                    playerName[i].SetActive(true);
                    playerScore[i].SetActive(true);
                }
            }
            else
            {
                scoreBoard.SetActive(false);
            }
        }

        if (Gametype_Manager.GameOver)
        {
            StartCoroutine(ReLoadGame());
        }
    }

    void setScoreInfo(int i)
    {
        playerName[i].GetComponent<Text>().text = " " + (player.GetComponent<Player_Player>().teamID + 1) + ".   " + player.GetComponent<Player_Player>().teamName;
        playerScore[i].GetComponent<Text>().text = " " + player.GetComponent<Player_Player>().playerScore.ToString();
        playerName[i].GetComponent<RectTransform>().localPosition = nameTempPos;
        playerScore[i].GetComponent<RectTransform>().localPosition = scoreTempPos;
    }

    void setPausePos()
    {
        if (rank == 0)
        {
            rank = player.GetComponent<Player_Player>().teamID + 1;
        }
        if (rank == 1)
        {
            nameTempPos.x = 53.26f;
            nameTempPos.y = 26.52f;

            scoreTempPos.x = 134.89f;
            scoreTempPos.y = 26.52f;
        }
        else if (rank == 2)
        {
            nameTempPos.x = 53.26f;
            nameTempPos.y = 5.45f;

            scoreTempPos.x = 134.89f;
            scoreTempPos.y = 5.45f;
        }
        else if (rank == 3)
        {
            nameTempPos.x = 53.26f;
            nameTempPos.y = -15.62f;

            scoreTempPos.x = 134.89f;
            scoreTempPos.y = -15.62f;
        }
        else if (rank == 4)
        {
            nameTempPos.x = 53.26f;
            nameTempPos.y = -36.69f;

            scoreTempPos.x = 134.89f;
            scoreTempPos.y = -36.69f;
        }
    }
    void setScoreBoardPos()
    {
        if (rank == 0)
        {
            rank = player.GetComponent<Player_Player>().teamID + 1;
        }
        if (rank == 1)
        {
            nameTempPos.x = -51.3f;
            nameTempPos.y = 55.4f;

            scoreTempPos.x = 54.5f;
            scoreTempPos.y = 55.4f;
        }
        else if (rank == 2)
        {
            nameTempPos.x = -51.3f;
            nameTempPos.y = 34.33f;

            scoreTempPos.x = 54.5f;
            scoreTempPos.y = 34.33f;
        }
        else if (rank == 3)
        {
            nameTempPos.x = -51.3f;
            nameTempPos.y = 13.26f;

            scoreTempPos.x = 54.5f;
            scoreTempPos.y = 13.26f;
        }
        else if (rank == 4)
        {
            nameTempPos.x = -51.3f;
            nameTempPos.y = -7.81f;

            scoreTempPos.x = 54.5f;
            scoreTempPos.y = -7.81f;
        }
    }

    void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Gametype_Manager.GameOver = false;
        Logging_System.saveKills();
        Debug.Log("SavedKills");
    }

    IEnumerator ReLoadGame()
    {
        yield return new WaitForSeconds(5.0f);
        reload();
    }
}
