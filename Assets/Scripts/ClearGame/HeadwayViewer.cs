using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeadwayViewer : MonoBehaviour
{
	[SerializeField] private TMP_Text levelAmountText;
	[SerializeField] private TMP_Text rewardAmount;
	[SerializeField] private TMP_Text headwayProgressText;
	[SerializeField] private Image headwayImage;

	public void SetInfo(int maxProgress, int reward, int level)
	{
		headwayImage.fillAmount = 0;
		rewardAmount.text = reward.ToString();
		levelAmountText.text = $"LEVEL {level}";
		RefreshInfo(0, maxProgress);
	}

	public void RefreshInfo(int currentProgress, int maxProgress)
	{
		headwayImage.fillAmount = (float)currentProgress / (float)maxProgress;
		headwayProgressText.text = $"{currentProgress}/{maxProgress}";
	}
}
