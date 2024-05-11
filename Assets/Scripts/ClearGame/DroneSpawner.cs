using System.Collections;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
	[SerializeField] private Drone droneInstance;
	[SerializeField] private float topRestriction;
	[SerializeField] private Vector2 dronesSpawnDelays;
	[SerializeField] private Vector2 randomDronesSizes;
	[SerializeField] private float xSpawnOffsetValue;
	private float xSpawnOffset;
	private Vector4 screenBounds;

	private void Start()
	{
		screenBounds = new Vector4(
					-Camera.main.orthographicSize * Camera.main.aspect,
					2 * Camera.main.orthographicSize * topRestriction - Camera.main.orthographicSize,
					Camera.main.orthographicSize * Camera.main.aspect,
					-Camera.main.orthographicSize
				);

		xSpawnOffset = screenBounds.z * xSpawnOffsetValue;
	}

	public void StartDroneSpawn()
	{
		StartCoroutine(DroneSpawn());
	}

	public void StopDroneSpawn()
	{
		StopAllCoroutines();
	}

	public IEnumerator DroneSpawn()
	{
		var drone = Instantiate(droneInstance);
		Vector2 moveDirection;
		var randomSize = Random.Range(randomDronesSizes.x, randomDronesSizes.y);
		var dronePosition = ChooseDroneDirectionAndPosition(randomSize, out moveDirection);

		drone.InitializeDrone(moveDirection, screenBounds, dronePosition, randomSize);
		yield return new WaitForSeconds(Random.Range(dronesSpawnDelays.x, dronesSpawnDelays.y));
		StartCoroutine(DroneSpawn());
	}

	public Vector2 ChooseDroneDirectionAndPosition(float size, out Vector2 direction)
	{
		var randomValue = Random.Range(0, 1f);
		Vector2 returnPosition;
		returnPosition.x = Random.Range(screenBounds.x + size / 2 + xSpawnOffset, screenBounds.z - size / 2 - xSpawnOffset);

		if (randomValue <= 0.5f)
		{
			direction = Vector2.up;
			returnPosition.y = screenBounds.w - size;
		}
		else
		{
			direction = Vector2.down;
			returnPosition.y = screenBounds.y + size;
		}

		return returnPosition;
	}
}
