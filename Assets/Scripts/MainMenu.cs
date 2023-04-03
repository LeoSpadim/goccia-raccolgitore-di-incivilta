using UnityEngine;

public class MainMenu : MonoBehaviour {
  // Assingables
  [SerializeField] private Sprite audioON;
  [SerializeField] private Sprite audioOFF;
  [SerializeField] private GameObject audioButton;
  [SerializeField] private AudioClip[] audioClips;
  [SerializeField] private AudioSource audioSource;

  private void Start() {
    audioSource.GetComponent<AudioSource>();
  }

  // Buttons actions
  public void SetLanguage() {
    Localisation.language = Localisation.language == Localisation.Language.English ? Localisation.Language.Italian : Localisation.Language.English;
    audioSource.PlayOneShot(audioClips[1]);
  }
  public void MuteAudio() {
    audioButton.GetComponent<UnityEngine.UI.Image>().sprite = AudioListener.volume == 0 ? audioON : audioOFF;
    AudioManager.instance.MuteAudio();
    audioSource.PlayOneShot(audioClips[1]);
  }
}
