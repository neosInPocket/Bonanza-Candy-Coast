using System;
using UnityEngine;

public class DestroyString : MonoBehaviour
{
	public Action DroneDestroyed { get; set; }

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Drone>(out Drone drone))
		{
			DroneDestroyed?.Invoke();
			drone.DestroyDrone();
		}
	}
}
