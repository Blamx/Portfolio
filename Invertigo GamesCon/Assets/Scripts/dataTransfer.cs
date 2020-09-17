using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class dataTransfer : MonoBehaviour {

	public static AudioMixer mixer;

	public static bool[] active =  new bool[5];
    public static bool hangar = false;
    public static bool gunGame = false;

    public static string ip = "127.0.0.1";
}