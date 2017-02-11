using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LimbModel
{
	public int PositionCount;
	public int CurrentPosition;
	public LimbModel (int positionCount)
	{
		PositionCount = positionCount;
	}

	public void Randomise()
	{
		CurrentPosition = Random.Range (0, PositionCount);
	}
}
