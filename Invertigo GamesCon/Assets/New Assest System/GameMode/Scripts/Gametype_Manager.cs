using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Gametype_Manager : MonoBehaviour
{
    public int scoreLimit = 200;
    float timer = 0;
    bool countDown;
    float countDownTime = 7.0f;

	enum GameType
	{
		FFA           = 0,
		TeamDM        = 1,
		MultiTeamDM   = 2,
					  
		KotH          = 3,
		MobileKotH    = 4,
		CapturePoints = 5,

		CtF           = 6,
		Infection     = 7
	};

    public struct gameModeScoring
    {
        public int kill;
        public int assist;
    };

	public static int gameMode = 0;
    public static int winningTeam;
    public static bool GameOver;

    public static gameModeScoring currentGameModeScoring = new gameModeScoring();

	void Start ()
	{
        gameModeScoring FFA = new gameModeScoring();
        FFA.kill = 100;
        FFA.assist = 25;
        currentGameModeScoring = FFA;
	}
	
	void Update ()
	{
        if(countDown)
        {
            timer -= Time.deltaTime;
        }

		if(gameMode == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Team_Manager.getScore(i) >= scoreLimit)
                {
                    winningTeam = i;
                    GameOver = true;
                    Debug.Log("Team" + getTeamName(i) + "Won");
                }
            }
        }
        if(GameOver && !countDown)
        {
            timer = countDownTime;
            countDown = true;
        }
        if (timer < 0 && countDown && (Input.GetKeyDown(KeyCode.Return) || GamePadManager.end))
        {
            backToMenu();
        }
	}

    public static string getTeamName(int team)
    {
        if(team == 0)
        {
            return "Alpha";
        }
        else if (team == 1)
        {
            return "Bravo";
        }
        else if (team == 2)
        {
            return "Charlie";
        }
        else if (team == 3)
        {
            return "Delta";
        }
        return "NaN";
    }

    public static void backToMenu()
    {
        GameOver = false;
        Team_Manager.wipe();
        Spawn_System.wipe();
        CameraResizeing.wipe();
        SceneManager.LoadScene("Menu");
    }
}
