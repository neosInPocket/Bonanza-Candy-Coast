using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCastDouble : MonoBehaviour
{
	[SerializeField] private TMP_Text buyProgressStatusDouble;
	[SerializeField] private TMP_Text purchaseTxtDouble;
	[SerializeField] private TMP_Text purchaseTextPopupDouble;
	[SerializeField] private Image progressLineImageDouble;
	[SerializeField] private Image clearPurchaseButtonImageDouble;
	[SerializeField] private Button clearPurchaseButtonDouble;
	[SerializeField] private int purchaseMedalsDouble;
	[SerializeField] private Color noMedalsColorDouble;
	[SerializeField] private Color allMedalsColorDouble;
	public Action upgradePurchasedDouble;

	public void ResetDoubleCardInformation()
	{
		buyProgressStatusDouble.text = $"{ClearSave.clearStoreUpgradeTwo}/5";
		purchaseTextPopupDouble.text = purchaseMedalsDouble.ToString();
		progressLineImageDouble.fillAmount = (float)ClearSave.clearStoreUpgradeTwo / 5f;

		if (ClearSave.clearStoreUpgradeTwo < 5)
		{
			if (ClearSave.clearMedals >= purchaseMedalsDouble)
			{
				purchaseTxtDouble.text = "UPGRADE";
				clearPurchaseButtonImageDouble.color = allMedalsColorDouble;
				clearPurchaseButtonDouble.interactable = true;
			}
			else
			{
				purchaseTxtDouble.text = "NO MEDALS";
				clearPurchaseButtonImageDouble.color = noMedalsColorDouble;
				clearPurchaseButtonDouble.interactable = false;
			}
		}
		else
		{
			purchaseTxtDouble.text = "MAX";
			clearPurchaseButtonImageDouble.color = allMedalsColorDouble;
			clearPurchaseButtonDouble.interactable = false;
		}
	}

	public void PurchaseDoubleUpgradeFromMedals()
	{
		ClearSave.clearStoreUpgradeTwo++;
		ClearSave.clearMedals -= purchaseMedalsDouble;
		ClearSave.SetSaves();
		upgradePurchasedDouble?.Invoke();
	}
}
