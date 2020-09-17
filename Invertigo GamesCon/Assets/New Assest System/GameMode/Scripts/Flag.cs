using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
	Vector3 FlagSpawnPos;
	Quaternion FlagSpawnRot;

	// Use this for initialization
	void Start ()
	{
		FlagSpawnPos = transform.position;
		FlagSpawnRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.GetComponent<Flag_Cap_Point>())
		{
			col.gameObject.GetComponent<Flag_Cap_Point>().Score += 1.0f; //add score to 

			//reset flag position, orientation, and velocity
			transform.position = FlagSpawnPos;
			transform.rotation = FlagSpawnRot;
			transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
			transform.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

			//set player scores
			/*for (int i = 0; i < 9; i++)
			{
				if (Team_Manager.teams[col.gameObject.GetComponent<Flag_Cap_Point>().Team, i])
				{
					Team_Manager.teams[col.gameObject.GetComponent<Flag_Cap_Point>().Team, i].GetComponent<Player_Player>().SetPlayerScore(col.gameObject.GetComponent<Flag_Cap_Point>().Score);
				}
			}*/
		}
	}
}
