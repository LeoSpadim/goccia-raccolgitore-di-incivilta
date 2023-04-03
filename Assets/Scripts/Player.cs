using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  // Player movement
  private Vector2 upPos = new Vector2(-10.0f, -3.6f);
  private Vector2 downPos = new Vector2(-10.0f, -8.6f);
  private float transitionSpeed = Mathf.Clamp01(0.1f);

  // Assingables
  [SerializeField] private SpawnManager spawnManager;
  public SpriteRenderer[] spriteRenderers;
  public AudioClip[] audioClips;

  // Private
  [SerializeField] private int health = 3;
  private int highScore = 0;
  private AudioSource audioSource;
  private int audioIndex = 4;

  // Public
  public bool isPlayerDead = false;
  public int points = 0;

  private void Start() {
    spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    audioSource = GetComponent<AudioSource>();
    highScore = PlayerPrefs.GetInt("HighScore", 0);
  }

  private void Update() {
    if (isPlayerDead) return;
    PlayerController();
  }

  // Player colision handler
  private void OnTriggerEnter2D(Collider2D other) {
    switch (other.gameObject.tag) {
      case "Obstacles":
        health -= 1;
        if (health <= 0) {
          audioSource.PlayOneShot(audioClips[9]);
          SetScore();
          isPlayerDead = true;
        }
        for (int i = 8; i >= 6; i--) {
          if (spriteRenderers[i].enabled) {
            spriteRenderers[i].enabled = false;
            break;
          }
        }
        audioSource.PlayOneShot(audioClips[3]);
        StartCoroutine(BlinkAnim());
        break;
      case "Collectables":
        Destroy(other.gameObject);
        points += 150;
        for (int i = 1; i <= 5; i++) {
          if (!spriteRenderers[i].enabled) {
            spriteRenderers[i].enabled = true;
            break;
          }
        }
        audioSource.PlayOneShot(audioClips[audioIndex]);
        if(audioIndex < 8 ){
          audioIndex += 1;
        } else {
          audioIndex = 4;
        }
        break;
      case "Checkpoint":
        points += 500;
        for (int i = 1; i <= 5; i++) {
          if (spriteRenderers[i].enabled) {
            spriteRenderers[i].enabled = false;
          }
        }
        for (int i = 6; i <= 8; i++) {
          if (!spriteRenderers[i].enabled) {
            spriteRenderers[i].enabled = true;
            break;
          }
        }
        if (health < 3) health += 1;
        audioSource.PlayOneShot(audioClips[2]);
        break;
    }
  }

  // Blink animation when the player collides with an obstacle
  private IEnumerator BlinkAnim() {
    float blinkTime = 1.0f;
    float blinkInterval = 0.06f;

    // Ignore collisions with obstacles
    GameObject[] obstaclesCollider = GameObject.FindGameObjectsWithTag("Obstacles");
    foreach (GameObject obstacle in obstaclesCollider) {
      if (obstacle != null && obstacle.CompareTag("Obstacles")) {
        Collider2D obstacleCollider = obstacle.GetComponent<Collider2D>();
        if (obstacleCollider != null) { Physics2D.IgnoreCollision(obstacleCollider, GetComponent<Collider2D>(), true); }
      }
    }

    // Get all active sprite renderers
    List<SpriteRenderer> activeSpriteRenderers = new List<SpriteRenderer>();

    // Add all sprite renderers that are active before the blink animation to the list
    foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>()) {
      if (sr.enabled) {
        activeSpriteRenderers.Add(sr);
      }
    }

    // Disable all sprite renderers
    foreach (SpriteRenderer sr in activeSpriteRenderers) { sr.enabled = false; }

    // Enable only the sprite renderers that were active before the blink animation
    foreach (SpriteRenderer sr in spriteRenderers) {
      if (sr.enabled) {
        activeSpriteRenderers.Add(sr);
      }
    }

    // Do the blink animation by enabling and disabling the sprite renderers
    for (float t = 0f; t < blinkTime; t += blinkInterval) {
      foreach (SpriteRenderer sr in activeSpriteRenderers) { sr.enabled = !sr.enabled; }
      yield return new WaitForSeconds(blinkInterval);
    }

    // Enable collisions with obstacles
    foreach (GameObject obstacle in obstaclesCollider) {
      if (obstacle != null && obstacle.CompareTag("Obstacles")) {
        Collider2D obstacleCollider = obstacle.GetComponent<Collider2D>();
        if (obstacleCollider != null) { Physics2D.IgnoreCollision(obstacleCollider, GetComponent<Collider2D>(), false); }
      }
    }

    // Enable all sprite renderers that were active before the blink animation
    foreach (SpriteRenderer sr in activeSpriteRenderers) { sr.enabled = true; }
  }

  // Save the score in the PlayerPrefs
  private void SetScore() {
    if (points > highScore) {
      highScore = points;
      PlayerPrefs.SetInt("HighScore", highScore);
    }
    PlayerPrefs.SetInt("Pontuacao", points);
  }

  // Get the player input and move the player
  private void PlayerController() {
    if (Input.GetMouseButtonDown(0) && !isPlayerDead && spawnManager.canSpawn == true) {
      Vector2 pos = transform.position;

      if (pos == upPos) {
        StartCoroutine(MovePlayer(downPos));
        audioSource.PlayOneShot(audioClips[0]);
      } else if (pos == downPos) {
        StartCoroutine(MovePlayer(upPos));
        audioSource.PlayOneShot(audioClips[1]);
      }
    }
  }

  // Move the player to the target position
  private IEnumerator MovePlayer(Vector3 targetPos) {
    while (Vector3.Distance(transform.position, targetPos) > 0.05f) {
      transform.position = Vector2.Lerp(transform.position, targetPos, transitionSpeed);
      yield return null;
    }
    transform.position = targetPos;
  }
}
