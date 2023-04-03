using UnityEngine;

public class AudioManager : MonoBehaviour {
  public static AudioManager instance;

  private void Awake() {
    if (instance == null) {
      instance = this;
      DontDestroyOnLoad(gameObject);
    } else {
      Destroy(gameObject);
    }
  }

  public void MuteAudio() {
    if(AudioListener.volume == 0) {
      AudioListener.volume = 1;
    } else {
      AudioListener.volume = 0;
    }
  }
}
