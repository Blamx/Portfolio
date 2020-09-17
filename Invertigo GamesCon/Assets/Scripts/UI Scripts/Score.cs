using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Player_Manager manager;
    private GameObject player;
    public Text PlayerScore, TopScore, Position, gunName;
    private float score;
    private static float topScore, secondHighest, lowestScore, secondLowest;
    bool checkForTop = false;
    static int numOfTop;
    public int rank;
    public Sprite[] fireSelectorList = new Sprite[5];
    public GameObject fireSelector;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find(transform.parent.parent.parent.name + "/Character");
        topScore = secondHighest = lowestScore = secondLowest = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        score = Team_Manager.getScore(manager.playerStats.team); //Get score from Player_Player script

        if (score >= topScore) //Checks and sets the top score to whoever has the highest score 
        {
            if (!checkForTop) //Check if this player has already been checked for being top player
            {
                topScore = score; //topScore is static so everyone sees the same top score
                Position.text = "TIE";
                Position.color = Color.yellow;
                checkForTop = true;
                numOfTop++; //Increases the number of top players
                rank = 1;
            }
            else if (numOfTop >= 2)
            {
                topScore = score; //topScore is static so everyone sees the same top score
                Position.text = "TIE";
                Position.color = Color.yellow;
                rank = 1;
            }
            else
            {
                topScore = score; //topScore is static so everyone sees the same top score
                Position.text = "WINNING"; //Tells the player if theyre winning if in first place
                Position.color = Color.green;
                rank = 1;
            }
        }
        else if (score < topScore && score >= secondHighest) //Checks if player is the second highest
        {
            secondHighest = score;
            rank = 2;
            if (checkForTop)
            {
                numOfTop--; //Subtracts one from the num of top players 
                checkForTop = false;
            }
        }
        else
        {
            Position.text = "LOSING"; //Tells the player they are losing
            Position.color = Color.red;
            if (score < secondHighest)
            {
                if (lowestScore <= score)
                {
                    lowestScore = score;
                    rank = 3;
                }
                else
                {
                    secondLowest = score;
                    rank = 4;
                }
            }
            if (checkForTop)
            {
                numOfTop--; //Subtracts one from the num of top players
                checkForTop = false;
            }
        }

        PlayerScore.text = "> " + score.ToString(); //Converts the float variable to string

        if (score >= topScore && numOfTop < 2) //If there is only one player with the highest score
        {
            TopScore.text = "   " + secondHighest.ToString(); //Sets the topscore for the player as the second highest
        }
        else
        {
            TopScore.text = "   " + topScore.ToString(); //Else it sets the top score as topscore
        }

        gunName.text = manager.gunBehavior.currentGun.name;

        if (manager.gunBehavior.currentGun.name == "Auto")
        {
            fireSelector.GetComponent<SpriteRenderer>().sprite = fireSelectorList[0];
        }
        else if (manager.gunBehavior.currentGun.name == "SemiAuto")
        {
            fireSelector.GetComponent<SpriteRenderer>().sprite = fireSelectorList[1];
        }
        else if (manager.gunBehavior.currentGun.name == "Charge")
        {
            fireSelector.GetComponent<SpriteRenderer>().sprite = fireSelectorList[2];
        }
        else if (manager.gunBehavior.currentGun.name == "SMG")
        {
            fireSelector.GetComponent<SpriteRenderer>().sprite = fireSelectorList[3];
        }
        else if (manager.gunBehavior.currentGun.name == "Burst")
        {
            fireSelector.GetComponent<SpriteRenderer>().sprite = fireSelectorList[4];
        }

        /* if (manager.playerStats.team == 0)
         {
             teamName.text = "Aplha";
         }
         else if (manager.playerStats.team == 1)
         {
             teamName.text = teamName.text = "Bravo";
         }
         else if (manager.playerStats.team == 2)
         {
             teamName.text = teamName.text = "Charile";
         }
         else if (manager.playerStats.team == 3)
         {
             teamName.text = teamName.text = "Delta";
         }
         else if (manager.playerStats.team == 4)
         {
             teamName.text = teamName.text = "Echo";
         }*/
        //teamName.text = rank.ToString();

        manager.playerStats.place = rank;

        PlayerScore.color = Color.green; //Sets colour to green 
        TopScore.color = Color.red; //Sets colour to red
    }
}
