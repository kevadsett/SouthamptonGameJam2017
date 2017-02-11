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
}