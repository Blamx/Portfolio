using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundInstance : MonoBehaviour
{
	public List<AudioClip> sounds;
	public AudioSource audSource;

	public Vector2 PitchShift = new Vector2(1, 1);

	void Start ()
	{
        SoundManager.soundCount++;
        audSource = GetComponent<AudioSource>();

		int randSound = Random.Range(0, sounds.Count);

		audSource.pitch = Random.Range(PitchShift.x, PitchShift.y);
		audSource.PlayOneShot(sounds[0]);
	}
	
	void Update ()
	{
		if (!audSource.isPlaying)
		{ 
            SoundManager.soundObjs.Remove(this.gameObject);
			Destroy(this.gameObject);
		}
	}

	void setPos(Vector3 pos)
	{
		transform.position = pos;
	}

	void setParent(GameObject parent)
	{
		setParent(parent);
	}


    public void delete()
    {  
        SoundManager.soundObjs.Remove(this.gameObject);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        SoundManager.soundCount--;
        audSource.Stop();
        Destroy(audSource);
    }
    //void addSoundClip(AudioClip clip)
    //{
    //	sounds.Add(clip);
    //}
    //
    //void addSoundClips(AudioClip[] clips)
    //{
    //	for (int i = 0; i < clips.Length; i++)
    //	{
    //		sounds.Add(clips[i]);
    //	}
    //}
}
