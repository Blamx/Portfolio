using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Team_Manager : MonoBehaviour
{

    public static int[] teamScore = new int[4];

    void Start()
    {


    }

    //=====================================================================================================================================================
    void Update()
    {

    }
    public static void Addscore(int team, int score)
    {
        teamScore[team] += score;
    }
    public static int getScore(int team)
    {
        return teamScore[team];
    }
    public static void wipe()
    {
        for (int i = 0; i < teamScore.Length; i++)
        {
            teamScore[i] = 0;
        }
    }
}
