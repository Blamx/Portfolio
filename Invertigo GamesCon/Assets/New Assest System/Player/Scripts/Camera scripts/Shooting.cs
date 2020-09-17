using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Player_Manager manager;

    RaycastHit raycastHit;
    public RaycastHit wallCheck;
    public bool rayhit;
    public bool conehit;

    public bool hittingPlayer = false;
    public bool isPlayer;
    bool damage = false;

    int currentgun = 0;

    public bool laser = false;

    public GameObject sight;

    public GameObject playerHit;

    bool HasShot;

    public Vector3 pointHit;

    public Vector3 pointHitNormal;

    public bool bulletHole = false;

    public bool connection;

    NetworkManager net;
    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("GameManager").GetComponent<NetworkManager>())
        {
            net = GameObject.Find("GameManager").GetComponent<NetworkManager>();
            connection = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        //some hp stuff
        {
            if (manager.target.health <= 0)
            {
                net.isDead = true;
            }
            else
            {
                net.isDead = false;
            }
        }

        damage = false;
        if (manager.HitCone.objects.Count > 1)
            conehit = true;
        else
            conehit = false;
        for (int i = 1; i < manager.HitCone.objects.Count; i++)
        {
            Physics.Raycast(transform.position, manager.HitCone.objects[i].transform.position - transform.position, out wallCheck);
        }

        if (manager.playerGamepad.commands.shoot && manager.gunBehavior.currentGun.fireType != 2)
        {
            if (manager.gunBehavior.canShoot == true && manager.target.health > 0)
            {
                manager.cam.centerTime = 2.0f;

                if (manager.gunBehavior.currentGun.name == "Auto")
                {
                    SoundManager.createSound(0, sight.transform.position);//auto 
                    if (!manager.playerGamepad.isKeyboard)
                        GamePadManager.SetControllerRumbleTime(manager.playerGamepad.gamepad, 0.2f, 0.2f, 0.2);
                }
                if (manager.gunBehavior.currentGun.name == "SemiAuto")
                {
                    SoundManager.createSound(1, sight.transform.position);//semi
                    if (!manager.playerGamepad.isKeyboard)
                        GamePadManager.SetControllerRumbleTime(manager.playerGamepad.gamepad, 0.6f, 0.6f, 0.2);
                }
                if (manager.gunBehavior.currentGun.name == "Charge")//sniper shot NOT sniper charge
                {
                    SoundManager.createSound(3, sight.transform.position);
                    if (!manager.playerGamepad.isKeyboard)
                        GamePadManager.SetControllerRumbleTime(manager.playerGamepad.gamepad, 0.8f, 0.8f, 0.3);
                }
                if (manager.gunBehavior.currentGun.name == "SMG")//smg
                {
                    SoundManager.createSound(4, sight.transform.position);
                    if (!manager.playerGamepad.isKeyboard)
                        GamePadManager.SetControllerRumbleTime(manager.playerGamepad.gamepad, 0.15f, 0.15f, 0.2);
                }

                laser = true;
                manager.gunBehavior.canShoot = false;
                manager.gunBehavior.fireTime = manager.gunBehavior.currentGun.fireTime;

                if (manager.playerGamepad.commands.aim)
                    rayhit = Physics.Raycast(transform.position, transform.forward, out raycastHit, 1000.0f, manager.cam.GetComponent<Camera>().cullingMask);
                else
                    rayhit = Physics.Raycast(sight.transform.position, transform.forward, out raycastHit, 1000.0f, manager.cam.GetComponent<Camera>().cullingMask);

                if (conehit)
                {
                    for (int i = 1; i < manager.HitCone.objects.Count; i++)
                    {
                        Physics.Raycast(transform.position, manager.HitCone.objects[i].transform.position - transform.position, out wallCheck);
                        if (wallCheck.collider.gameObject.name == "hitBox")
                        {
                            if (manager.HitCone.objects[i].gameObject.GetComponent<Target>())
                            {
                                SoundManager.createSound(8, sight.transform.position);//hit indicator
                                manager.hitMarker.i = 0;
                                hittingPlayer = true;
                                isPlayer = manager.HitCone.objects[i].gameObject.GetComponent<Target>().isPlayer;
                                if (!net.connected)
                                {
                                    manager.HitCone.objects[i].gameObject.GetComponent<Target>().oldHealth = manager.HitCone.objects[i].gameObject.GetComponent<Target>().health;
                                    manager.HitCone.objects[i].gameObject.GetComponent<Target>().health -= manager.gunBehavior.currentGun.damage;
                                    manager.HitCone.objects[i].gameObject.GetComponent<Target>().newHealth = manager.HitCone.objects[i].gameObject.GetComponent<Target>().health;
                                    damage = true;
                                }
                                net.sendDam((int)manager.gunBehavior.currentGun.damage, manager.HitCone.objects[i].gameObject.GetComponent<Target>().ID);
                                //net.output = manager.HitCone.objects[i].gameObject.GetComponent<Target>().ID.ToString();
                                if (manager.HitCone.objects[i].gameObject.GetComponent<Target>().isPlayer && manager.HitCone.objects[i].gameObject.GetComponent<Target>().health <= 0)
                                {
                                    if (!net.connected)
                                    {
                                        manager.playerStats.kills++;
                                        manager.playerStats.score += /*Gametype_Manager.currentGameModeScoring.kill*/ 1;
                                        //score is not going to work for now
                                        Team_Manager.Addscore(manager.playerStats.team, Gametype_Manager.currentGameModeScoring.kill);
                                    }
                                }
                                if (manager.HitCone.objects[i].gameObject.GetComponent<Target>().isPlayer && manager.HitCone.objects[i].gameObject.name == "hitBox")
                                {
                                    if (!net.connected)
                                        manager.HitCone.objects[i].gameObject.transform.parent.parent.gameObject.GetComponent<PlayerStats>().lastHitBy = manager.movment.gameObject;
                                }
                            }
                        }
                    }
                }
                if (rayhit)
                {
                    pointHit = raycastHit.point;

                    if (raycastHit.collider.gameObject.name == "hitBox")
                    {
                        bulletHole = false;
                        pointHitNormal = raycastHit.normal;
                        pointHit = raycastHit.point;
                    }
                    else
                    {
                        bulletHole = true;
                        pointHitNormal = raycastHit.normal;
                        pointHit = raycastHit.point;
                    }

                    if (!damage && raycastHit.collider.gameObject.GetComponent<Target>())
                    {
                        raycastHit.collider.gameObject.GetComponent<Target>().health -= manager.gunBehavior.currentGun.damage;
                    }
                    if (raycastHit.collider.gameObject.GetComponent<Pushable_Object>())
                    {
                        raycastHit.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * manager.gunBehavior.currentGun.force, raycastHit.point);
                    }
                }
                manager.cam.recoilTarget.transform.Rotate(-manager.cam.transform.right * manager.gunBehavior.currentGun.recoilMaxAmount.y * 10, Space.World);
                manager.cam.recoilTarget.transform.Rotate(-manager.cam.transform.up * Random.Range(manager.gunBehavior.currentGun.recoilMinAmount.x * 10, manager.gunBehavior.currentGun.recoilMaxAmount.x * 10), Space.World);
                manager.armMove.transform.position -= manager.cam.transform.forward * manager.gunBehavior.currentGun.viewKick;
            }

        }
        else if (manager.gunBehavior.canShoot && manager.gunBehavior.currentGun.fireType == 2)
        {
            if (manager.gunBehavior.canShoot == true && manager.target.health > 0)
            {
                manager.cam.centerTime = 2.0f;
                SoundManager.createSound(5, sight.transform.position);//3rb
                if (!manager.playerGamepad.isKeyboard)
                    GamePadManager.SetControllerRumbleTime(manager.playerGamepad.gamepad, 0.2f, 0.2f, 0.2);
                laser = true;
                manager.gunBehavior.canShoot = false;
                manager.gunBehavior.fireTime = manager.gunBehavior.currentGun.fireTime;
                if (manager.playerGamepad.commands.aim)
                    rayhit = Physics.Raycast(transform.position, transform.forward, out raycastHit, 1000.0f, manager.cam.GetComponent<Camera>().cullingMask);
                else
                    rayhit = Physics.Raycast(sight.transform.position, transform.forward, out raycastHit, 1000.0f, manager.cam.GetComponent<Camera>().cullingMask);

                if (manager.HitCone.objects.Count > 1)
                    conehit = true;
                else
                    conehit = false;

                if (conehit)
                {
                    for (int i = 1; i < manager.HitCone.objects.Count; i++)
                    {
                        Physics.Raycast(transform.position, manager.HitCone.objects[i].transform.position - transform.position, out wallCheck);
                        if (wallCheck.collider.gameObject.name == "hitBox")
                        {
                            if (manager.HitCone.objects[i].gameObject.GetComponent<Target>())
                            {
                                SoundManager.createSound(8, sight.transform.position);//hit indicator
                                //if (Mathf.FloorToInt(Random.Range(0, 4)) == 0)
                                //{
                                //    SoundManager.createSound(9, sight.transform.position);//take damage (1 in 4 chance)
                                //}
                                manager.hitMarker.i = 0;
                                hittingPlayer = true;
                                isPlayer = manager.HitCone.objects[i].gameObject.GetComponent<Target>().isPlayer;

                                manager.HitCone.objects[i].gameObject.GetComponent<Target>().oldHealth = manager.HitCone.objects[i].gameObject.GetComponent<Target>().health;
                                manager.HitCone.objects[i].gameObject.GetComponent<Target>().health -= manager.gunBehavior.currentGun.damage;
                                manager.HitCone.objects[i].gameObject.GetComponent<Target>().newHealth = manager.HitCone.objects[i].gameObject.GetComponent<Target>().health;

                                if (manager.HitCone.objects[i].gameObject.GetComponent<Target>().isPlayer && manager.HitCone.objects[i].gameObject.GetComponent<Target>().health <= 0)
                                {
                                    manager.playerStats.kills++;
                                    manager.playerStats.score += /*Gametype_Manager.currentGameModeScoring.kill*/ 1;
                                    //score is not going to work for now
                                    Team_Manager.Addscore(manager.playerStats.team, Gametype_Manager.currentGameModeScoring.kill);
                                }
                                if (manager.HitCone.objects[i].gameObject.GetComponent<Target>().isPlayer && manager.HitCone.objects[i].gameObject.name == "hitBox")
                                {
                                    manager.HitCone.objects[i].gameObject.transform.parent.parent.gameObject.GetComponent<PlayerStats>().lastHitBy = manager.movment.gameObject;
                                }
                            }
                        }
                    }
                }
                if (rayhit)
                {
                    pointHit = raycastHit.point;

                    if (raycastHit.collider.gameObject.name == "hitBox")
                    {
                        bulletHole = false;
                        pointHitNormal = raycastHit.normal;
                        pointHit = raycastHit.point;
                    }
                    else
                    {
                        bulletHole = true;
                        pointHitNormal = raycastHit.normal;
                        pointHit = raycastHit.point;
                    }

                    if (raycastHit.collider.gameObject.GetComponent<Pushable_Object>())
                    {
                        raycastHit.collider.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * manager.gunBehavior.currentGun.force, raycastHit.point);
                    }
                }
                manager.cam.recoilTarget.transform.Rotate(-manager.cam.transform.right * manager.gunBehavior.currentGun.recoilMaxAmount.y * 10, Space.World);
                manager.cam.recoilTarget.transform.Rotate(-manager.cam.transform.up * Random.Range(manager.gunBehavior.currentGun.recoilMinAmount.x * 10, manager.gunBehavior.currentGun.recoilMaxAmount.x * 10), Space.World);
                manager.armMove.transform.position -= manager.cam.transform.forward * manager.gunBehavior.currentGun.viewKick;
            }
        }

    }
}
