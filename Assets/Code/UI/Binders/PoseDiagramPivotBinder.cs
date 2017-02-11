using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (RectTransform))]
public class PoseDiagramPivotBinder : MonoBehaviour
{
	public string Key;

	private RectTransform _rectTransform;

	private void Awake ()
	{
		_rectTransform = GetComponent<RectTransform> ();
	}

	private void Update ()
	{
		if (ViewBindings.Instance == null)
		{
			return;
		}
		float f;
		if (ViewBindings.Instance.TryGetBoundValue (Key, out f))
		{
			float newXPivotPoint = ((1.0f - f) * 0.5f) + 0.5f;
			RectTransform rectTransform = GetComponent<RectTransform>();
			rectTransform.pivot = new Vector2 (newXPivotPoint, rectTransform.pivot.y);
		}
		else
		{
			_rectTransform.pivot = Vector2.zero;
		}
	}
}