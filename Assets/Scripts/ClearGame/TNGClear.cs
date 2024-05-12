using UnityEngine;
using UserTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using UserFinger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using TMPro;
using System;

public class TNGClear : MonoBehaviour
{
	[SerializeField] public TMP_Text TNGTips;
	[SerializeField] public Animator holder;

	public Action TNGCompleted { get; set; }

	public void ReadyState(Action tngCompletedAction)
	{
		gameObject.SetActive(true);
		TNGCompleted = tngCompletedAction;
		UserTouch.onFingerDown += SpaceMagic;
		TNGTips.text = "WELCOME TO Bonanza Candy Coast!";
	}

	private void SpaceMagic(UserFinger finger)
	{
		UserTouch.onFingerDown -= SpaceMagic;
		UserTouch.onFingerDown += ConstantlyRotated;
		TNGTips.text = "FEEL LIKE A REAL SPACE MAGIC! LET'S FIND OUT HOW TO MANAGE YOUR MAGIC SPHERE";
		holder.SetTrigger("nextAction");
	}

	public void ConstantlyRotated(UserFinger finger)
	{
		UserTouch.onFingerDown -= ConstantlyRotated;
		UserTouch.onFingerDown += AwayFromCenter;
		TNGTips.text = "YOUR SPHERE IS ENCHANTED AND CONSTANTLY ROTATES AROUND ITS CENTER. CONTROL THE RADIUS OF ITS TRAJECTORY BY PRESSING THE SCREEN!";
		holder.SetTrigger("nextAction");
	}

	public void AwayFromCenter(UserFinger finger)
	{
		UserTouch.onFingerDown -= AwayFromCenter;
		UserTouch.onFingerDown += Goal;
		TNGTips.text = "HOLD THE SCREEN - YOUR SPHERE WILL BE MOVE AWAY FROM ITS CENTER. DO THIS AGAIN - SHE WILL BE CLOSER TO IT";
		holder.SetTrigger("nextAction");
	}

	public void Goal(UserFinger finger)
	{
		UserTouch.onFingerDown -= Goal;
		UserTouch.onFingerDown += Lose;
		TNGTips.text = "YOUR GOAL IS TO DESTROY DRONES FLYING FROM ABOVE AND BELOW WITH THE HELP OF A THREAD THAT IS STRETCHED BETWEEN YOUR SPHERE AND ITS CENTER";
		holder.SetTrigger("nextAction");
	}

	public void Lose(UserFinger finger)
	{
		UserTouch.onFingerDown -= Lose;
		UserTouch.onFingerDown += LastTNG;
		TNGTips.text = "DESTROY THE REQUIRED NUMBER OF DRONES TO COMPLETE THE LEVEL! BE CAREFUL AND DON'T TOUCH THE DRONE WITH YOUR SPHERE OR YOU WILL LOSE";
		holder.SetTrigger("nextAction");
	}

	public void LastTNG(UserFinger finger)
	{
		UserTouch.onFingerDown -= LastTNG;
		UserTouch.onFingerDown += TNGPassed;
		TNGTips.text = "GOOD LUCK!";
		holder.SetTrigger("nextAction");
	}

	public void TNGPassed(UserFinger finger)
	{
		TNGCompleted();
		gameObject.SetActive(false);

		UserTouch.onFingerDown -= SpaceMagic;
		UserTouch.onFingerDown -= ConstantlyRotated;
		UserTouch.onFingerDown -= AwayFromCenter;
		UserTouch.onFingerDown -= Goal;
		UserTouch.onFingerDown -= LastTNG;
		UserTouch.onFingerDown -= TNGPassed;
	}

	private void OnDestroy()
	{
		UserTouch.onFingerDown -= SpaceMagic;
		UserTouch.onFingerDown -= ConstantlyRotated;
		UserTouch.onFingerDown -= AwayFromCenter;
		UserTouch.onFingerDown -= Goal;
		UserTouch.onFingerDown -= LastTNG;
		UserTouch.onFingerDown -= TNGPassed;
	}
}
