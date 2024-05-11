using UnityEngine;

public class SettingsSoundClearer : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject music;
    public ToneSetter toneSetter;

    private void Start()
    {
        toneSetter = GameObject.FindObjectOfType<ToneSetter>();

        effect.SetActive(ClearSave.gameSounds == 1);
        music.SetActive(ClearSave.gameSounds == 1);
    }

    public void ToggleMusicLook()
    {
        bool resultValue = toneSetter.SetterToggleForMusic();
        music.SetActive(!resultValue);
    }

    public void ToggleEffectsLook()
    {
        bool resultValue = toneSetter.SetterToggleForSounds();
        effect.SetActive(!resultValue);
    }
}
