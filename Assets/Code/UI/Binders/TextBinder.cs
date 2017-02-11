using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Text))]
public class TextBinder : MonoBehaviour
{
	[SerializeField] private string key;

	private Text text;

	private void Awake ()
	{
		text = GetComponent<Text> ();
	}

	private void Update ()
	{
		string t;
		if (ViewBindings.Instance.TryGetBoundValue (key, out t))
		{
			text.text = t;
		}
		else
		{
			text.text = string.Empty;
		}
	}
}