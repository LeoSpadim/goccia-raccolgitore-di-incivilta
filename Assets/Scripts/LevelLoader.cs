using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {
  [SerializeField] private Animator transition;
  public AudioClip[] audioClips;
  public AudioSource audioSource;

  public void Update() {
    if (Input.GetKeyDown(KeyCode.Escape)) {
      Application.Quit();
    }
  }

  public void PlayGame() {
    StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex + 1));
    audioSource.PlayOneShot(audioClips[0]);
  }
  public void Scoreboard() {
    StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex + 2));
    audioSource.PlayOneShot(audioClips[0]);
  }
  public void RestartGame() {
    StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex - 1));
    audioSource.PlayOneShot(audioClips[2]);
  }
  public void QuitToMenu() {
    StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex - 2));
    audioSource.PlayOneShot(audioClips[1]);
  }

  IEnumerator Transition(int levelIndex) {
    transition.SetTrigger("Start");
    yield return new WaitForSeconds(1.0f);
    SceneManager.LoadScene(levelIndex);
  }
}
