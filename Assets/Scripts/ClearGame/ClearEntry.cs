using System;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class ClearEntry : MonoBehaviour
{
	[SerializeField] private IOBall iOBall;
	[SerializeField] private DroneSpawner droneSpawner;
	[SerializeField] private FinalCardViewer finalCardViewer;
	[SerializeField] private HeadwayViewer headwayViewer;
	[SerializeField] private StartViewer startViewer;
	[SerializeField] private TNGClear tNGClear;
	[SerializeField] private DestroyString destroyString;
	public int currentDrones { get; private set; }
	public int maxDrones { get; private set; }
	public int maxDronesReward { get; private set; }


	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		InitializeEntry();
	}

	public void InitializeEntry()
	{
		var clearLevel = ClearSave.clearLevel;
		maxDrones = (int)(2f * Mathf.Log(clearLevel + 1) + 2);
		maxDronesReward = (int)(45f * Mathf.Log(clearLevel + 1));

		headwayViewer.SetInfo(maxDrones, maxDronesReward, clearLevel);

		if (ClearSave.nurtureAllowed == 1)
		{
			ClearSave.nurtureAllowed = 0;
			ClearSave.SetSaves();
			tNGClear.ReadyState(TNGCompleted);
		}
		else
		{
			TNGCompleted();
		}
	}

	public void TNGCompleted()
	{
		startViewer.StartMainAction(MainActionPassed);
	}

	public void MainActionPassed()
	{
		iOBall.Allowed = true;
		droneSpawner.StartDroneSpawn();

		iOBall.IODestroyed += OnIODestroyed;
		destroyString.DroneDestroyed += OnDroneDestroy;
	}

	private void OnDroneDestroy()
	{
		currentDrones++;
		if (currentDrones >= maxDrones)
		{
			currentDrones = maxDrones;
			ClearSave.clearLevel++;
			ClearSave.clearMedals += maxDronesReward;
			ClearSave.SetSaves();
			finalCardViewer.RevealCard(maxDronesReward);

			iOBall.IODestroyed -= OnIODestroyed;
			destroyString.DroneDestroyed -= OnDroneDestroy;
			iOBall.Allowed = false;
			droneSpawner.StopDroneSpawn();
		}

		headwayViewer.RefreshInfo(currentDrones, maxDrones);
	}

	private void OnIODestroyed()
	{
		finalCardViewer.RevealCard(-1);

		iOBall.IODestroyed -= OnIODestroyed;
		destroyString.DroneDestroyed -= OnDroneDestroy;
		iOBall.Allowed = false;
		droneSpawner.StopDroneSpawn();
	}

	private void OnDestroy()
	{
		iOBall.IODestroyed -= OnIODestroyed;
		destroyString.DroneDestroyed -= OnDroneDestroy;
	}
}
