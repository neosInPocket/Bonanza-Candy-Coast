using System.Collections;
using UnityEngine;

public class Drone : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Vector2 randomSpinSpeeds;
	[SerializeField] private Vector2 randomVelocities;
	[SerializeField] private Rigidbody2D droneRigid;
	[SerializeField] private float destroyOffset;
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] private CircleCollider2D circleCollider2D;

	private float spin;
	private float velocity;
	private Vector4 screen;
	private Vector3 rotation;
	private float size;
	private int rotationDirection;

	public void InitializeDrone(Vector2 moveDirection, Vector4 screenBounds, Vector2 position, float size)
	{
		spriteRenderer.size = new Vector2(size, size);
		spin = Random.Range(randomSpinSpeeds.x, randomSpinSpeeds.y);
		velocity = Random.Range(randomVelocities.x, randomVelocities.y);
		screen = screenBounds;
		transform.position = position;

		droneRigid.velocity = moveDirection * velocity;
		rotationDirection = Random.Range(0, 2) == 2 ? 1 : -1;
		this.size = size;
		circleCollider2D.radius = size / 2;
	}

	private void Update()
	{
		var pos = transform.position;
		if (
			pos.x < screen.x - size - destroyOffset ||
			pos.x > screen.z + size + destroyOffset ||
			pos.y > screen.y + size + destroyOffset ||
			pos.y < screen.w - size - destroyOffset

		)
		{
			Destroy(gameObject);
		}

		rotation.z += rotationDirection * spin * Time.deltaTime;
		transform.eulerAngles = rotation;
	}

	public void DestroyDrone()
	{
		Destroy(gameObject);
		Instantiate(destroyEffect, transform.position, Quaternion.identity, null);
	}

	public IEnumerator DestroyRoutine()
	{
		spriteRenderer.enabled = false;
		destroyEffect.SetActive(true);
		yield return new WaitForSeconds(1f);
	}
}
