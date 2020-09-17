using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuction : MonoBehaviour
{
    public Transform brokenGlass;
    public bool isConstant;
    public static bool glassBroken = false;
    public Vector3 suctionDir;

	GameObject ship;
	float shipVel;
	float shipAccel;
	float shipLife;

    public List<GameObject> objects = new List<GameObject>();
    public float speed;
    public float time = 5.0f;

	private void Start()
	{
		ship = GameObject.Find("Broken_Shuttle");
		shipVel = 0;
		shipAccel = 1.25f;
		shipLife = 0;
	}

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {

            objects.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (objects.Contains(other.gameObject))
        {
            objects.Remove(other.gameObject);
        }
    }

    void Update()
    {
        objects.RemoveAll(GameObject => GameObject == null);

        if (!isConstant)
        {
            if (GameObject.Find("glass") == null)
            {
                if (!glassBroken)
                {
                    Instantiate(brokenGlass);
                }
                glassBroken = true;
                if (time > 0.0001)
                {
                    if (time < 0.0001)
                    {
                        time = 0.0001f;
                    }

                    foreach (GameObject obj in objects)
                    {
                        obj.gameObject.GetComponent<Rigidbody>().AddForce(suctionDir * speed * time);
                    }

                    time -= 0.009f;
                }
            }
        }
        else if (isConstant)
        {
            foreach (GameObject obj in objects)
            {
                obj.gameObject.GetComponent<Rigidbody>().AddForce(suctionDir * speed);
            }
        }


		if (glassBroken)
		{
			shipLife += Time.deltaTime;
			if (shipLife < 30.0f)
			{ shipVel += shipAccel * Time.deltaTime; }

			if (shipVel > 5.0f)
			{ shipVel = 5.0f; }

			ship.transform.position = new Vector3(
				ship.transform.position.x + shipVel,
				ship.transform.position.y,
				ship.transform.position.z);
		}
	}
}
