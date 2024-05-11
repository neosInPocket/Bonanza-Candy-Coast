using UnityEngine;
using UserTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UserFinger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using System;

public class IOBall : MonoBehaviour
{
	[SerializeField] private ParticleSystem[] allParticles;
	[Range(0, 1f)][SerializeField] private float topRestrict;
	[SerializeField] private EdgeCollider2D edgeCollider2D;
	[SerializeField] private LineRenderer lineRenderer;
	[Range(0, 1f)][SerializeField] private float maxRadius;
	[SerializeField] private float[] raiseSpeedsValues;
	[SerializeField] private float[] spinSpeedsValues;
	[SerializeField] private Transform staticBall;
	[SerializeField] private GameObject IOExplosion;
	[SerializeField] private Collider2D mainCollider;
	[SerializeField] private DestroyString destroyString;
	private Vector2 centerPosition;
	private Vector2 currentPosition;
	private float maxRadiusValue;
	public bool Allowed { get; set; }
	public bool raiseAction { get; set; }
	private Vector2 deviceSize;
	private float currentRadius;
	private float currentAngle;
	private float raiseSpeed;
	private float spinSpeed;
	private int currentDirection;
	private float yOffset;
	private Vector2[] colliderPositions;


	private void Start()
	{
		UserTouch.onFingerDown += StartRaiseRadius;
		UserTouch.onFingerUp += StopRaiseRadius;

		deviceSize = new Vector2(Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize);
		centerPosition = new Vector2(0, deviceSize.y * topRestrict - deviceSize.y);
		maxRadiusValue = deviceSize.x * maxRadius;
		yOffset = -centerPosition.y;

		raiseSpeed = raiseSpeedsValues[ClearSave.clearStoreUpgradeOne];
		spinSpeed = spinSpeedsValues[ClearSave.clearStoreUpgradeTwo] * Mathf.Deg2Rad;
		transform.position = centerPosition;
		currentDirection = -1;

		lineRenderer.positionCount = 2;
		lineRenderer.SetPosition(0, centerPosition);
		lineRenderer.SetPosition(1, centerPosition);
		staticBall.transform.position = centerPosition;

		colliderPositions = new Vector2[2];
		colliderPositions[0] = centerPosition;
		colliderPositions[1] = centerPosition;
	}

	private void Update()
	{
		if (!Allowed) return;

		if (raiseAction)
		{
			currentRadius += currentDirection * raiseSpeed * Time.deltaTime;
			if (currentRadius > maxRadiusValue)
			{
				currentRadius = maxRadiusValue;
			}
			else
			{
				if (currentRadius < 0)
				{
					currentRadius = 0;
				}
			}
		}

		currentAngle += spinSpeed * Time.deltaTime;
		currentPosition.x = Mathf.Cos(currentAngle);
		currentPosition.y = Mathf.Sin(currentAngle);
		currentPosition *= currentRadius;
		currentPosition.y -= yOffset;
		transform.position = currentPosition;
		RefreshRenderers();
	}

	public void RefreshRenderers()
	{
		var color = CalculateColor(Vector2.Distance(transform.position, centerPosition) / maxRadiusValue);
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;

		lineRenderer.SetPosition(1, transform.position);
		colliderPositions[1] = transform.position;
		edgeCollider2D.points = colliderPositions;
	}

	public void StartRaiseRadius(UserFinger finger)
	{
		if (!Allowed) return;
		currentDirection *= -1;
		raiseAction = true;
	}

	public void StopRaiseRadius(UserFinger finger)
	{
		if (!Allowed) return;
		raiseAction = false;
	}

	public Color CalculateColor(float value)
	{
		float redValue = Mathf.Clamp01(value);
		float greenValue = Mathf.Clamp01(1 - value);

		return new Color(redValue, greenValue, 0f);
	}

	public Action IODestroyed { get; set; }

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Drone>(out Drone drone))
		{
			IODestroyed?.Invoke();
			Allowed = false;
			raiseAction = false;
			UserTouch.onFingerDown -= StartRaiseRadius;
			UserTouch.onFingerUp -= StopRaiseRadius;
			Instantiate(IOExplosion, transform.position, Quaternion.identity, null);

			foreach (var ps in allParticles)
			{
				ps.Stop();
			}

			mainCollider.enabled = false;
			destroyString.gameObject.SetActive(false);
		}
	}


	private void OnDestroy()
	{
		UserTouch.onFingerDown -= StartRaiseRadius;
		UserTouch.onFingerUp -= StopRaiseRadius;
	}
}
