using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserClone : MonoBehaviour
{
    public Player_Manager manager;
    public GameObject autoLaser, semiLaser, slugLaser, subLaser, burstLaser, sparkPrefab;
    private GameObject laserPrefab;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 1;

        if(manager.gunBehavior.currentGun.name == "Auto")
        {
            laserPrefab = autoLaser;
        }
        else if (manager.gunBehavior.currentGun.name == "SemiAuto")
        {
            laserPrefab = semiLaser;
        }
        else if (manager.gunBehavior.currentGun.name == "Charge")
        {
            laserPrefab = slugLaser;
        }
        else if (manager.gunBehavior.currentGun.name == "SMG")
        {
            laserPrefab = subLaser;
        }
        else if (manager.gunBehavior.currentGun.name == "Burst")
        {
            laserPrefab = burstLaser;
        }

        if (manager.shooting.laser)
        {
            GameObject laser = (GameObject)Instantiate(laserPrefab, transform.position,transform.rotation);
            laser.transform.rotation = manager.cam.transform.rotation;
            laser.GetComponent<laserBeam>().pointHit = manager.shooting.pointHit;

            manager.shooting.laser = false;

            GameObject sparks = (GameObject)Instantiate(sparkPrefab, transform.position, transform.rotation);
            sparks.transform.rotation = manager.cam.transform.rotation;
            sparks.GetComponent<laserBeam>().pointHit = manager.shooting.pointHit;
        }
    }
}
