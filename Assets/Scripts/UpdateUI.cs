using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateUI : MonoBehaviour {
  // Assingables
  public Player player;
  public SpawnManager spawnManager;
  public LevelLoader levelLoader;

  [SerializeField] private GameObject Hud;
  [SerializeField] private GameObject Tutorial;
  [SerializeField] private TMPro.TextMeshProUGUI scoreCounter;

  private void Start() {
    Inicialize();
  }

  private void Update() {
    // Update score
    scoreCounter.text = player.points.ToString();

    if (player.isPlayerDead) {
      StartCoroutine(EndGame());
    }
  }

  // Before game starts (Tutorial) rules
  public void Inicialize() {
    foreach (SpriteRenderer sprite in player.spriteRenderers) { sprite.enabled = false; }
    Hud.SetActive(false);
    spawnManager.canSpawn = false;
  }

  // After game starts rules
  public void StartGame() {
    levelLoader.audioSource.PlayOneShot(levelLoader.audioClips[2]);
    for (int i = 0; i <= 8; i++) {
      if (i == 0 || (i >= 6 && i <= 8)) {
        player.spriteRenderers[i].enabled = true;
      }
    }
    Tutorial.GetComponent<Animator>().SetTrigger("FadeOut");
    spawnManager.canSpawn = true;
    spawnManager.timer = 0f;
    Hud.SetActive(true);
    spawnManager.StartCoroutine(spawnManager.SpawnObjects());
  }

  // At player death
  public IEnumerator EndGame() {
    yield return new WaitForSeconds(0.9f);
    UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }
}
