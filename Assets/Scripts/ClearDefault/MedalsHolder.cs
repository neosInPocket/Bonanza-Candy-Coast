using TMPro;
using UnityEngine;

public class MedalsHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text mainHolder;

	private void Start()
	{
		HoldMedals();
	}

	public void HoldMedals()
	{
		mainHolder.text = ClearSave.clearMedals.ToString();
	}
}
