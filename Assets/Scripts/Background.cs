using UnityEngine;

public class Background : MonoBehaviour {
  // Assingables
  [SerializeField] private SpawnManager spawnManager;

  // Private
  private Vector2 startPos;
  private float repeatWidth;

  // Public
  [SerializeField] public float scrollSpeed = 1.0f;

  private void Start() {
    startPos = transform.position;
    repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
  }

  private void Update() {
    if(!spawnManager.canSpawn) return;
    transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
    scrollSpeed += 0.12f * Time.deltaTime;
    if (scrollSpeed > 32.6f) {
      scrollSpeed = 32.6f;
    }

    if (transform.position.x < startPos.x - repeatWidth) {
      transform.position = startPos;
    }
  }
}
