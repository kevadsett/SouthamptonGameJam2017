using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
	public AudioSource[] musics;
	public float[] volumes;
	public bool[] shouldFades;
	public float fadeSpeed;

	private static MusicPlayer instance;

	private void Awake ()
	{
		instance = this;
	}

	private void Update ()
	{
		for (int i = 0; i < musics.Length; i++)
		{
			if (shouldFades [i])
			{
				volumes [i] -= fadeSpeed * Time.deltaTime;
				musics[i].volume = Mathf.Clamp01 (volumes[i]);

				if (volumes [i] <= 0.0f)
				{
					volumes [i] = 0.0f;
					shouldFades [i] = false;
					musics [i].Stop ();
				}
			}
		}
	}

	public static void Play (int id)
	{
		instance.musics[id].volume = instance.volumes[id] = 1.0f;
		instance.shouldFades[id] = false;

		if (instance.musics[id].isPlaying == false)
		{
			instance.musics[id].Play ();
		}
	}

	public static void FadeOut (int id)
	{
		instance.shouldFades[id] = true;
	}
}
