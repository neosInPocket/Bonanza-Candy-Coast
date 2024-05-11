using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCardViewer : MonoBehaviour
{
	[SerializeField] private TMP_Text cardResultText;
	[SerializeField] private TMP_Text medalsAmountRewarded;
	[SerializeField] private TMP_Text rightButtonText;

	public void RevealCard(int medalsGained)
	{
		gameObject.SetActive(true);

		if (medalsGained > 0)
		{
			cardResultText.text = "LEVEL COMPLETED!";
			rightButtonText.text = "NEXT LEVEL";
			medalsAmountRewarded.text = medalsGained.ToString();
		}
		else
		{
			cardResultText.text = "YOU LOSE";
			rightButtonText.text = "TRY AGAIN";
			medalsAmountRewarded.text = "0";
		}
	}

	public void RevealNextLevel()
	{
		SceneManager.LoadScene("ClearGame");
	}

	public void RevealStartState()
	{
		SceneManager.LoadScene("ClearDefault");
	}
}
