using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonHolder : MonoBehaviour
{
	[SerializeField] private TMP_Text levelHolder;

	private void Start()
	{
		HoldLevel();
	}

	public void HoldLevel()
	{
		levelHolder.text = $"LEVEL {ClearSave.clearLevel}";
	}

	public void JumpToNextLevel()
	{
		SceneManager.LoadScene("ClearGame");
	}

	public void ClearQuit()
	{
		Application.Quit();
	}
}
