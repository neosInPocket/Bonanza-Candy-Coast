using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UpdateCast : MonoBehaviour
{
	[SerializeField] private TMP_Text buyProgressStatus;
	[SerializeField] private TMP_Text purchaseTxt;
	[SerializeField] private TMP_Text purchaseTextPopup;
	[SerializeField] private Image progressLineImage;
	[SerializeField] private Image clearPurchaseButtonImage;
	[SerializeField] private Button clearPurchaseButton;
	[SerializeField] private int purchaseMedals;
	[SerializeField] private Color noMedalsColor;
	[SerializeField] private Color allMedalsColor;
	public Action upgradePurchased;

	public void ResetCardInformation()
	{
		buyProgressStatus.text = $"{ClearSave.clearStoreUpgradeOne}/5";
		purchaseTextPopup.text = purchaseMedals.ToString();
		progressLineImage.fillAmount = (float)ClearSave.clearStoreUpgradeOne / 5f;

		if (ClearSave.clearStoreUpgradeOne < 5)
		{
			if (ClearSave.clearMedals >= purchaseMedals)
			{
				purchaseTxt.text = "UPGRADE";
				clearPurchaseButtonImage.color = allMedalsColor;
				clearPurchaseButton.interactable = true;
			}
			else
			{
				purchaseTxt.text = "NO MEDALS";
				clearPurchaseButtonImage.color = noMedalsColor;
				clearPurchaseButton.interactable = false;
			}
		}
		else
		{
			purchaseTxt.text = "MAX";
			clearPurchaseButtonImage.color = allMedalsColor;
			clearPurchaseButton.interactable = false;
		}
	}

	public void PurchaseUpgradeFromMedals()
	{
		ClearSave.clearStoreUpgradeOne++;
		ClearSave.clearMedals -= purchaseMedals;
		ClearSave.SetSaves();
		upgradePurchased?.Invoke();
	}
}
