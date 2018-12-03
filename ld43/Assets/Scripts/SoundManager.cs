using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource musicSource;
    public AudioSource sfxSource;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayMusic(string name)
    {
        AudioClip clip = (AudioClip)Resources.Load("Audio/Music/" + name);
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(string name)
    {
        AudioClip clip = (AudioClip)Resources.Load("Audio/SFX/" + name);
        sfxSource.PlayOneShot(clip);
    }
}
