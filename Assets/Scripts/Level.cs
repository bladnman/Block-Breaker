using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
  [SerializeField] GameObject ballPrefab;

  // blocks
  int blockCount = 0;
  int hitBlockCount = 0;
  public int BlockCount { get { return blockCount; } }
  public int HitBlockCount { get { return hitBlockCount; } }
  public int ActiveBlockCount { get { return blockCount - hitBlockCount; } }
  // balls
  int ballCount = 0;
  public int BallCount { get { return ballCount; } }

  GameState gameState;

  private void Start() {
    gameState = FindObjectOfType<GameState>();
  }
  public void AddBlock() {
    blockCount++;
  }
  public void DestroyBlock() {
    hitBlockCount++;
    gameState.AddPointsForBlock();
    if (ActiveBlockCount < 1) {
      DoLevelCleared();
    }
  }
  public void DestroyBall() {
    ballCount--;
    Debug.Log($"{BallCount} : BallCount");   // M@: 
    if (BallCount < 1) {
      DoLevelFailed();
    }
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.N)) {
      DoLevelCleared();
    } else if (Input.GetKeyDown(KeyCode.B)) {
      DoAddNewBall();
    }
  }
  void DoLevelCleared() {
    gameState.AddLife();
    var sceneLoader = FindObjectOfType<SceneLoader>();
    sceneLoader.LoadNext();
  }
  void DoLevelFailed() {
    gameState.LoseLife();

    // GAME OVER - no more lives
    if (gameState.Lives < 1) {
      DoGameOver();
    }

    // NEXT BALL
    else {
      DoAddNewBall();
    }
  }
  void DoAddNewBall() {
    Instantiate(ballPrefab, new Vector3(0, 0, 0), Quaternion.identity);
  }
  void DoGameOver() {
    SceneManager.LoadScene("Game Over");
  }
}
