using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent (typeof (Image))]
public class UISwap : MonoBehaviour
{
	public float swaprate;
	public bool invert;

	private Image image;

	private void Awake ()
	{
		image = GetComponent<Image> ();
	}

	private void Update ()
	{
		float bpm = 120.0f;

		float s = swaprate* (bpm / 60.0f) * Mathf.PI * 2.0f;
		float swap = Mathf.Sin (Time.time * s);

		transform.localScale = swap > 0 ^ invert ? Vector3.zero : Vector3.one;
	}
}