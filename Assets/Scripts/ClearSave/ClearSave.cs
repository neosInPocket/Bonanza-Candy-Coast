using UnityEngine;

public class ClearSave : MonoBehaviour
{
	[SerializeField] public bool clearInitialize;
	[SerializeField] public int _clearLevelDefault;
	[SerializeField] public int _clearMedalsDefault;
	[SerializeField] public int _clearStoreUpgradeOneDefault;
	[SerializeField] public int _clearStoreUpgradeTwoDefault;
	[SerializeField] public int _gameSoundsDefault;
	[SerializeField] public int _musicSoundsDefault;
	[SerializeField] public int _nurtureAllowedDefault;
	[SerializeField] public string[] savesKeys;
	private static string[] saveKeysStatic;

	public static int clearLevel;
	public static int clearMedals;
	public static int clearStoreUpgradeOne;
	public static int clearStoreUpgradeTwo;
	public static int gameSounds;
	public static int musicSounds;
	public static int nurtureAllowed;

	private void Awake()
	{
		saveKeysStatic = savesKeys;

		if (clearInitialize)
		{
			clearLevel = _clearLevelDefault;
			clearMedals = _clearMedalsDefault;
			clearStoreUpgradeOne = _clearStoreUpgradeOneDefault;
			clearStoreUpgradeTwo = _clearStoreUpgradeTwoDefault;
			gameSounds = _gameSoundsDefault;
			musicSounds = _musicSoundsDefault;
			nurtureAllowed = _nurtureAllowedDefault;

			SetSaves();
		}
		else
		{
			LoadClearSaves();
		}
	}

	public static void SetSaves()
	{
		PlayerPrefs.SetInt(saveKeysStatic[0], clearLevel);
		PlayerPrefs.SetInt(saveKeysStatic[1], clearMedals);
		PlayerPrefs.SetInt(saveKeysStatic[2], clearStoreUpgradeOne);
		PlayerPrefs.SetInt(saveKeysStatic[3], clearStoreUpgradeTwo);
		PlayerPrefs.SetInt(saveKeysStatic[4], gameSounds);
		PlayerPrefs.SetInt(saveKeysStatic[5], musicSounds);
		PlayerPrefs.SetInt(saveKeysStatic[6], nurtureAllowed);
	}

	public void LoadClearSaves()
	{
		clearLevel = PlayerPrefs.GetInt(savesKeys[0], _clearLevelDefault);
		clearMedals = PlayerPrefs.GetInt(savesKeys[1], _clearMedalsDefault);
		clearStoreUpgradeOne = PlayerPrefs.GetInt(savesKeys[2], _clearStoreUpgradeOneDefault);
		clearStoreUpgradeTwo = PlayerPrefs.GetInt(savesKeys[3], _clearStoreUpgradeTwoDefault);
		gameSounds = PlayerPrefs.GetInt(savesKeys[4], _gameSoundsDefault);
		musicSounds = PlayerPrefs.GetInt(savesKeys[5], _musicSoundsDefault);
		nurtureAllowed = PlayerPrefs.GetInt(savesKeys[6], _nurtureAllowedDefault);
	}
}
