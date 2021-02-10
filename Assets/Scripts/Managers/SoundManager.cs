using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	
	public AudioSource EffectsSource;
	public AudioSource MusicSource;

	public float LowPitchRange = .95f;
	public float HighPitchRange = 1.05f;

	
	public static SoundManager Instance = null;

	
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		
		else if (Instance != this)
		{
			Destroy(gameObject);
		}

	
		
		DontDestroyOnLoad(gameObject);
	}

	public void PlayClip(AudioClip clip)
	{
		EffectsSource.clip = clip;
		EffectsSource.Play();
	}


	public void Play()
	{
		
		EffectsSource.Play();
	}

	public void SetClip(AudioClip clip)
    {
		EffectsSource.clip = clip;
    }

	public void Pause()
	{
		
		EffectsSource.Pause();
	}

	public void PlayMusic(AudioClip clip)
	{
		MusicSource.clip = clip;
		MusicSource.Play();
	}

	
	public void RandomSoundEffect(params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0, clips.Length);
		float randomPitch = Random.Range(LowPitchRange, HighPitchRange);

		EffectsSource.pitch = randomPitch;
		EffectsSource.clip = clips[randomIndex];
		EffectsSource.Play();
	}

}

