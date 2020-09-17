using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;



public class Gun_loader : MonoBehaviour {

    [DllImport("GunStats")]
    public static extern int init();

    [DllImport("GunStats")]
    public static extern IntPtr GetName(int pos);

    [DllImport("GunStats")]
    public static extern float GetStat(int stat, int pos);

    public static Gun_Behavior.GunStats[] gunStats;


    enum gunStuff
    { 
        fireType = 0,
        fireTime = 1,
        delay = 2,
  
        damage = 3,
        force = 4,

        aimAssist = 5,
        coneSize = 6,

        RangeTheshold = 7,
        Range = 8,

        swapSpeed = 9,

        recoilMaxAmountX = 10,
        recoilMinAmountX = 11,

        recoilMaxAmountY = 12,
        recoilMinAmountY = 13,

        recoilSpeed = 14,
        centerSpeed = 15,

        AdsDuration = 16,
        AdsSpeed = 17,

        viewKick = 18, 
        viewCenter = 19,
        viewKickMax = 20
};
    // Use this for initialization
    void Start () {
        // Debug.Log(LoadDataFromFile());
        int numberOfGuns = init();

        gunStats = new Gun_Behavior.GunStats[numberOfGuns];

        for (int i = 0; i < numberOfGuns; i++)
        {
            gunStats[i].name = Marshal.PtrToStringAnsi(GetName(i));

            gunStats[i].fireTime = GetStat((int)gunStuff.fireTime, i);
            gunStats[i].fireType = GetStat((int)gunStuff.fireType, i);
            gunStats[i].delay = GetStat((int)gunStuff.delay, i);

            gunStats[i].damage = GetStat((int)gunStuff.damage, i);
            gunStats[i].force = GetStat((int)gunStuff.force, i);

            gunStats[i].aimAssist = GetStat((int)gunStuff.aimAssist, i);
            gunStats[i].coneSize = GetStat((int)gunStuff.coneSize, i);

            gunStats[i].swapSpeed = GetStat((int)gunStuff.swapSpeed, i);

            gunStats[i].rangeThreshHold = GetStat((int)gunStuff.RangeTheshold, i);
            gunStats[i].range = GetStat((int)gunStuff.Range, i);

            gunStats[i].recoilMaxAmount = new Vector2(GetStat((int)gunStuff.recoilMaxAmountX, i), GetStat((int)gunStuff.recoilMaxAmountY, i));
            gunStats[i].recoilMinAmount = new Vector2(GetStat((int)gunStuff.recoilMinAmountX, i), GetStat((int)gunStuff.recoilMinAmountY, i));

            gunStats[i].recoilSpeed = GetStat((int)gunStuff.recoilSpeed, i);
            gunStats[i].centerSpeed = GetStat((int)gunStuff.centerSpeed, i);

            gunStats[i].AdsSpeed = GetStat((int)gunStuff.AdsSpeed, i);
            gunStats[i].AdsDuration = GetStat((int)gunStuff.AdsDuration, i);

            gunStats[i].viewKick = GetStat((int)gunStuff.viewKick, i);
            gunStats[i].viewCenter = GetStat((int)gunStuff.viewCenter, i);
            gunStats[i].viewKickMax = GetStat((int)gunStuff.viewKickMax, i);
        }


       /* for (int i = 0; i < numberOfGuns; i++)
        {
            Debug.Log(gunStats[i].name);
            Debug.Log("FireTime: " + gunStats[i].fireTime);
            Debug.Log("FireType: " + gunStats[i].fireType);
            Debug.Log("Damage: " + gunStats[i].damage);
            Debug.Log("recoilMax: " + gunStats[i].recoilMaxAmount);
            Debug.Log("recoilMin: " + gunStats[i].recoilMinAmount);
            Debug.Log("force: " + gunStats[i].force);
            Debug.Log("recoilSpeed: " + gunStats[i].recoilSpeed);
            Debug.Log("adsDuration: " + gunStats[i].AdsDuration);
            Debug.Log("kick: " + gunStats[i].viewKick);
            Debug.Log("kickCenter: " + gunStats[i].viewCenter);
            Debug.Log("kickMax: " + gunStats[i].viewKickMax);
            Debug.Log("Delay: " + gunStats[i].delay);
            Debug.Log("ConeSize: " + gunStats[i].coneSize);
            Debug.Log("range: " + gunStats[i].range);
            Debug.Log("rangeThresh: " + gunStats[i].rangeThreshHold);
            Debug.Log("SwapSpeed: " + gunStats[i].swapSpeed);
            Debug.Log("AimAssist: " + gunStats[i].aimAssist);

        }*/
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
