using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
	private static Dictionary<string, AudioPlayer> Players = new Dictionary<string, AudioPlayer> ();

	public AudioSource[] sources;

	private void Awake ()
	{
		Players.Add (name, this);
	}
		
	private void Play (Vector3 position)
	{
		AudioSource source = null;
		int count = 0;
		while ((source == null || source.isPlaying) && count < sources.Length)
		{
			source = sources[Random.Range(0, sources.Length)];
			count++;
		}

		source.transform.position = position;
		source.Play ();
	}

	private bool IsPlaying()
	{
		AudioSource source = null;
		for (int i = 0; i < sources.Length; i++)
		{
			source = sources [i];
			if (source.isPlaying)
			{
				return true;
			}
		}
		return false;
	}

	public static void PlaySound (string soundName, Vector3 position)
	{
		if (Players.ContainsKey (soundName))
		{
			Players[soundName].Play (position);
		}
		else
		{
			Debug.LogError ("OH NO THERE IS NO SOUND TO PLAY! " + soundName);
		}
	}

	public static bool IsPlaying(string soundName)
	{
		if (Players.ContainsKey (soundName))
		{
			AudioPlayer player = Players [soundName];
			return player.IsPlaying ();
		}
		return false;
	}
}