using System.Linq;
using UnityEngine;

public class ToneSetter : MonoBehaviour
{
	[SerializeField] public AudioSource tone;

	private void Awake()
	{
		var tuner = GameObject.FindObjectOfType<ToneSetter>();

		ToneSetter[] allSettersFound = FindObjectsByType<ToneSetter>(sortMode: FindObjectsSortMode.None);
		var lengthOfFoundObjects = allSettersFound.Length == 1;

		if (!lengthOfFoundObjects)
		{
			var toneSetter = allSettersFound.FirstOrDefault(x => x.gameObject.scene.name != "DontDestroyOnLoad");
			Destroy(toneSetter.gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		tone.volume = ClearSave.musicSounds == 1 ? 1f : 0f;
	}

	public bool SetterToggleForMusic()
	{
		bool disabled = tone.volume < 1f;
		tone.volume = disabled ? 1f : 0f;

		ClearSave.musicSounds = disabled ? 1 : 0;
		ClearSave.SetSaves();

		return !disabled;
	}

	public bool SetterToggleForSounds()
	{
		bool disabled = !(ClearSave.gameSounds != 1);

		ClearSave.gameSounds = !disabled ? 1 : 0;
		ClearSave.SetSaves();

		return disabled;
	}
}
