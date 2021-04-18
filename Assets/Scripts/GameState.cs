using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
  // CONFIG
  [SerializeField] [Range(0.1f, 10.0f)] float gameSpeed = 1f;
  [SerializeField] int pointsPerBlock = 37;
  [SerializeField] int startingLives = 3;

  // STATE
  int score = 0;
  public int Score { get { return score; } }

  [SerializeField] int lives = 3;
  public int Lives { get { return lives; } }

  private void Awake() {
    // THERE CAN ONLY BE ONE - singleton pattern
    if (FindObjectsOfType<GameState>().Length > 1) {
      gameObject.SetActive(false); // MUST DEACTIVATE!
      Destroy(gameObject);
    } else {
      DontDestroyOnLoad(gameObject);
    }
    Reset();
  }

  public void Reset() {
    score = 0;
    lives = startingLives;
  }
  private void Update() {
    Time.timeScale = gameSpeed;
  }
  public void AddPointsForBlock() {
    score += pointsPerBlock;
  }
  public void AddLife() {
    lives++;
  }
  public void LoseLife() {
    lives--;
  }
}
