using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Player : MonoBehaviour {

	public GameObject playerID;//var for what object the player is bound to
	public int teamID;//var for what team the player is on. -1 will be no team
    public int teamSlot;
	public float playerHP;//player current hp
	public float playerHPMax;//player max hp
    public float oldHP, newHP, _t; //store the hp of the player before getting hit and after getting hit.  _t variable is used for time to lerp between old and new HP
    public bool reSpawn;
    public Vector3 newGrav;
    public float regenSpeed; //how quiclky player rengens health
    public float regenDelay; // how long youi need to not get hit before regen takes effect

    public Vector3 playerStartPos;

	public GameObject lastHitBy;
    GameObject camera;

	public float playerScore;
    GameObject Winner;
    public string teamName;
    Spawn_System spawnSystem;
    public GameObject spawns;
    //=====================================================================================================================================================
    void Start()
    {
        spawnSystem = spawns.GetComponent<Spawn_System>();
        playerID = transform.gameObject;
        //teamID = -1;
        playerHP = oldHP = newHP = 100.0f;
        playerHPMax = 100.0f;
        playerScore = 0;

        regenSpeed = 20.0f;

        lastHitBy = null;

        playerStartPos = transform.position;

        //teamSlot = manager.teamManager.addToTeam(transform.gameObject,teamID);
        camera = GameObject.Find(transform.parent.name + "/Camera");
        Winner = GameObject.Find(transform.parent.name + "/Camera/UI/Winner");
        Winner.SetActive(false);

        teamName = Gametype_Manager.getTeamName(teamID);
        _t = 1.0f;
    }

    //=====================================================================================================================================================
    void Update()
    {
        regenDelay -= Time.deltaTime;
        if (playerHP < 100 && regenDelay < 0)
        {
            SubFromPlayerHP(-(regenSpeed * Time.deltaTime));
        }
        
        if (playerHP > 100.0f)
        {
            playerHP = 100.0f;
        }
        if (playerHP <= 0.0f)
        {
            //GetLastHitBy().GetComponent<Player_Player>().AddToPlayerScore(1.0f);
            //DeletePlayer();
            //transform.position = playerStartPos;

            Logging_System.sendKill(lastHitBy.transform.position, transform.position);

            //GameObject spawn = spawnSystem.findSpawn();
            //transform.position = spawn.transform.position;
            //newGrav = spawn.transform.up;

            reSpawn = true;

            SetPlayerHP(100);
        }

        if(Gametype_Manager.GameOver)
        {       
            Winner.SetActive(true);
            Winner.GetComponent<Text>().text = Gametype_Manager.getTeamName(Gametype_Manager.winningTeam) + " Team" +" \n Won";
        }
        else
        {
            Winner.SetActive(false);
        }

    }
    public float GetPlayerHP()
	{
		return playerHP;
	}
	public void SetPlayerHP(float _playerHP)
	{
        playerHP = oldHP = newHP = _playerHP; //old and new HP set to _playerHP
        GetLastHitBy().GetComponent<Player_Player>().AddToPlayerScore(1.0f);
    }
    public void AddToPlayerHP(float _playerHP)
	{
		playerHP += _playerHP;
	}
	public void SubFromPlayerHP(float _playerHP)
	{
        oldHP = playerHP; //Stores the HP before subtracting 
		playerHP -= _playerHP; //Subtracts HP from playerHP
        newHP = playerHP; //Stores new subtracted HP
        _t = 0.0f; //Sets time for lerp to 0 because player got hit
	}
	//----------------------------
	public void DeletePlayer()
	{
		Destroy(playerID);
	}
	//----------------------------
	public GameObject GetLastHitBy()
	{
		return lastHitBy;
	}
	public void SetLastHitBy(GameObject _playerID)
	{
		lastHitBy = _playerID;
	}
	//----------------------------
	public float GetPlayerScore()
	{
		return playerScore;
	}
	public void SetPlayerScore(float _playerScore)
	{
		playerScore = _playerScore;
	}
	public void AddToPlayerScore(float _playerScore)
	{
		playerScore += _playerScore;
	}
	public void SubFromPlayerScore(float _playerScore)
	{
		playerScore -= _playerScore;
	}
	//----------------------------
	public int GetTeam()
	{
		return teamID;
	}
	public void SetTeam(int _teamID)
	{
		teamID = _teamID;
	}
}
