using UnityEngine;
using System.Collections;

public class UiBob : MonoBehaviour
{
	public float bobrate;
	public float rotrate;
	public float bobdist;
	public float rotdist;
	public float scarate;
	public float scale;

	private void Update ()
	{
		float bpm = 120.0f;

		float b = bobrate * (bpm / 60.0f) * Mathf.PI * 2.0f;
		float r = rotrate * (bpm / 60.0f) * Mathf.PI * 2.0f;
		float s = scarate * (bpm / 60.0f) * Mathf.PI * 2.0f;

		float bob = Mathf.Abs (Mathf.Sin (Time.time * b)) * bobdist;
		float rot = Mathf.Sin (Time.time * r) * rotdist;
		float sca = 1.0f + Mathf.Abs (Mathf.Sin (Time.time * s)) * scale;

		transform.localPosition = new Vector3 (0.0f, bob, 0.0f);
		transform.localRotation = Quaternion.Euler (0.0f, 0.0f, rot);
		transform.localScale = Vector3.one * sca;
	}
}