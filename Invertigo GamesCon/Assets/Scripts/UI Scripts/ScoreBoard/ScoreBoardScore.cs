using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardScore : MonoBehaviour {

    public string playerName;
    public int score;
    public int place;
    public float Kd;

    public Text username;
    public Text _score;
    public Text _Kd;
    public Text _Rank;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        username.text = playerName;
        _score.text = score.ToString();
        _Kd.text = Kd.ToString();
        _Rank.text = place.ToString();

	}
}
