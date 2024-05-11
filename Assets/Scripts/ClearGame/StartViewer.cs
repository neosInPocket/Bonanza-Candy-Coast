using System;
using UnityEngine;

public class StartViewer : MonoBehaviour
{
	public Action ActionEndValue { get; private set; }

	public void StartMainAction(Action actionEndValue)
	{
		gameObject.SetActive(true);
		ActionEndValue = actionEndValue;
	}

	public void InvokeNextStep()
	{
		ActionEndValue();
		gameObject.SetActive(false);
	}
}
