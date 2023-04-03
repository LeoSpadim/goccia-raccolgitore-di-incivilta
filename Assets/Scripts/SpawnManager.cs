using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
  // Assingables
  [SerializeField] private GameObject[] objectsPrefabs;
  [SerializeField] private Vector2[] spawnPos;

  // Private
  [SerializeField] private float scrollSpeed = 15f;
  [SerializeField] private float repeatRate = 1.28f;
  [SerializeField] private bool checkpoint = false;

  // Public
  public Player player;
  public bool canSpawn = false;
  public float timer = 0f;

  private void Update() {
    if (canSpawn) {
      UpdateDifficulty();
    }
    ManageObjects();
  }

  // Spawn controller
  public IEnumerator SpawnObjects() {
    while (!player.isPlayerDead && canSpawn) {
      if (checkpoint == false) {
        int randomObject = Random.Range(0, 13);
        int randomPos = Random.Range(0, spawnPos.Length);
        Instantiate(objectsPrefabs[randomObject], spawnPos[randomPos], Quaternion.identity, transform);
        yield return new WaitForSeconds(repeatRate);
      } else {
        Instantiate(objectsPrefabs[14], spawnPos[1], Quaternion.identity, transform);
        yield return new WaitForSeconds(repeatRate * 1.2f);
        checkpoint = false;
      }
    }
  }

  // Difficulty controller
  private void UpdateDifficulty() {
    timer += Time.deltaTime;
    if (timer >= 15.0f) {
      checkpoint = true;
      scrollSpeed += 5.0f;
      repeatRate = (16f / scrollSpeed) * 1.2f;
      timer = 0f;
    }
  }

  // Destroy objects that are out of the screen
  private void ManageObjects() {
    foreach (Transform child in transform) {
      child.position += Vector3.left * scrollSpeed * Time.deltaTime;
      if (child.position.x < -25) {
        Destroy(child.gameObject);
      }
    }
  }
}
