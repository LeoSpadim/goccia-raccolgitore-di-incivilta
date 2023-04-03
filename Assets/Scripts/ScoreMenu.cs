using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreMenu : MonoBehaviour {
  // Assingables
  public TMPro.TextMeshProUGUI lastScore;
  public TMPro.TextMeshProUGUI highestScore;

  private void Start() {
    int score = PlayerPrefs.GetInt("Pontuacao");
    int highScore = PlayerPrefs.GetInt("HighScore");

    lastScore.text = score.ToString();
    highestScore.text = highScore.ToString();
  }

  // Buttons actions
}
