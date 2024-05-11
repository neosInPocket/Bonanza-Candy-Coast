using System.Collections.Generic;
using UnityEngine;

public class UpdateCastHolder : MonoBehaviour
{
	[SerializeField] private UpdateCast updateCast;
	[SerializeField] private UpdateCastDouble updateCastDouble;
	[SerializeField] private List<MedalsHolder> medalsHolders;

	private void Start()
	{
		updateCastDouble.upgradePurchasedDouble += HandlePurchase;
		updateCast.upgradePurchased += HandlePurchase;

		updateCastDouble.ResetDoubleCardInformation();
		updateCast.ResetCardInformation();
	}

	public void HandlePurchase()
	{
		updateCastDouble.ResetDoubleCardInformation();
		updateCast.ResetCardInformation();
		medalsHolders.ForEach(x => x.HoldMedals());
	}
}
