using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewBindings : MonoBehaviour {

	private Dictionary<string, object> data;

	public static ViewBindings Instance { get; private set; }

	private void Awake ()
	{
		Instance = this;
		data = new Dictionary<string, object> ();
	}

	public void BindValue<T> (string key, T value)
	{
		if (data.ContainsKey (key))
		{
			data[key] = (object) value;
		}
		else
		{
			data.Add (key, (object) value);
		}
	}

	public bool TryGetBoundValue<T> (string key, out T value)
	{
		object objval;
		if (data.TryGetValue (key, out objval))
		{
			if ((T) objval != null)
			{
				value = (T) objval;
				return true;
			}
		}

		value = default (T);
		return false;
	}
}
