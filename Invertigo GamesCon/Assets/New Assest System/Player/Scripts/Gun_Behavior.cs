using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Behavior : MonoBehaviour {
    ///<summary>
    ///Holds Stats for a Gun
    ///</summary>
    public struct GunStats
    {

        public string name;

        public float fireTime; // delay between shots
        public float fireType; // 0 = full auto, 1 =semi, 2 = burst
        public float delay; // Charge time or Burst fire Rate

        public float damage; // amount of damage gun inficts
        public float force; // how much force it applies to objects

        public float aimAssist; // how sticky the weapon is
        public float coneSize; // size of cone

        public float rangeThreshHold; // distance that damage drop off will start
        public float range; // how much damage will drop per unit

        public float swapSpeed; // how fast the weapon can be switched to

        public Vector2 recoilMaxAmount;//the max recoil
        public Vector2 recoilMinAmount;//the min recoil

        public float recoilSpeed; // how fast the gun kicks
        public float centerSpeed; // how fast the gun will center

        public float AdsDuration; // how long the AdsSpeed is active for
        public float AdsSpeed; // how fast the Gun aims down sights(scalar)

        public float viewKick; // how much the gun will kick
        public float viewCenter; // how fast the gun will center
        public float viewKickMax; // the kick Limit      
    }

    public GunStats currentGun;

    public Player_Manager manager;

    float centerTime = 0;
    float kickTime = 0;

    public bool isKicking;
    public bool isCentering;
    public bool hasShot;


    private float adsTime = 0;

    private bool ads;

    float movementAmount = 0;

    private GameObject reticel;

    public Combat_Shoot shootClass;

    public GameObject hipGun;
    public GameObject adsGun;
    public GameObject NoRecoil;

    float recoilY;
    float recoilX;

    Vector3 cameraFwd;
    // Use this for initialization

    public int gunUseing = 0;


    public bool isNew = false;

    //new varialbes
    public bool canShoot;
    public float fireTime = 0;

    //for sniper
    float chargeTime = 0;
    bool charging = false;
    bool shot = false;

    //for burst
    int burstNumber = 3;
    int burstCount = 0;
    float burstDealy = 0.1f;
    float burstTime = 0;
    bool burstShot;
    int kills = 0;

    bool gunGameRules = false;

    void Start ()
    {
        gunGameRules = dataTransfer.gunGame;
	}

	// Update is called once per frame
	void Update ()
    {
        if (!gunGameRules)
        {
            if (manager.playerGamepad.isKeyboard && Input.GetKeyDown("1"))
            {
                gunUseing = 0;
            }
            else if (manager.playerGamepad.isKeyboard && Input.GetKeyDown("2"))
            {
                gunUseing = 1;
            }
            else if (manager.playerGamepad.isKeyboard && Input.GetKeyDown("3"))
            {
                gunUseing = 2;
            }
            else if (manager.playerGamepad.isKeyboard && Input.GetKeyDown("4"))
            {
                gunUseing = 3;
            }
            else if (manager.playerGamepad.isKeyboard && Input.GetKeyDown("5"))
            {
                gunUseing = 4;
            }

            if (!manager.playerGamepad.isKeyboard && manager.playerGamepad.commands.swap)
            {
                SoundManager.createSound(7, manager.shooting.sight.transform.position);
                gunUseing++;
                if(gunUseing > 4)
                {
                    gunUseing = 0;
                }
            }
        }
        if (gunGameRules)
        {
            if (manager.playerStats.kills != kills)
            {
                kills++;
                gunUseing++;
                if (gunUseing > 4)
                {
                    gunUseing = 0;
                }
            }
        }

        canShoot = false;
        currentGun = Gun_loader.gunStats[gunUseing];
        fireTime -= Time.deltaTime;
        chargeTime -= Time.deltaTime;
        burstTime -= Time.deltaTime;

        if (fireTime < 0)
        {                
            if (currentGun.fireType == 0)
            {
                 canShoot = true;
            }
            else if (currentGun.fireType == 1 && !manager.playerGamepad.commands.shoot)
            {
                canShoot = true;
            }
            else if (currentGun.fireType == 2 && !manager.playerGamepad.commands.shoot)
            {
                burstShot = true;
            }
            else if (currentGun.fireType == 2 && manager.playerGamepad.commands.shoot && burstShot)
            {
                burstShot = false;
                burstCount = burstNumber;
            }
            else if (currentGun.fireType == 3 && chargeTime <= 0 && !charging && !shot)
            {
                if (manager.playerGamepad.commands.shoot)
                {
                    chargeTime = 0.55f;
                    charging = true;
                    SoundManager.createSound(2, manager.shooting.sight.transform.position);
                }
            }
            else if (currentGun.fireType == 3 && chargeTime <= 0 && charging)
            {
                canShoot = true;
                charging = false;
                shot = true;
            }
            else if(charging)
            {
                if (!manager.playerGamepad.isKeyboard)
                    GamePadManager.SetControllerRumbleTime(manager.playerGamepad.gamepad, 0.2f, 0.2f, 1 - chargeTime);
            }
        }

        if(burstCount > 0 && burstTime <= 0)
        {
            canShoot = true;
            burstCount--;
            burstTime = burstDealy;
        }
        if (!manager.playerGamepad.commands.shoot)
        {
               shot = false;
        }      
    }
}
