using UnityEngine;
using System.Collections;

public class EdBob : MonoBehaviour
{
	public float bobrate;
	public float rotrate;
	public float bobdist;
	public float rotdist;

	private void Update ()
	{
		float bpm;
		if (ViewBindings.Instance.TryGetBoundValue ("bpm", out bpm))
		{
			float b = bobrate * (bpm / 60.0f) * Mathf.PI * 2.0f;
			float r = rotrate * (bpm / 60.0f) * Mathf.PI * 2.0f;

			float bob = Mathf.Abs (Mathf.Sin (Time.time * b)) * bobdist;
			float rot = Mathf.Sin (Time.time * r) * rotdist;

			transform.localPosition = new Vector3 (0.0f, bob, 0.0f);
			transform.localRotation = Quaternion.Euler (0.0f, 0.0f, rot);
		}
	}
}