using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

  GameState gameState;

  private void Start() {
    gameState = FindObjectOfType<GameState>();
  }

  public void LoadNext() {
    var currentIndex = SceneManager.GetActiveScene().buildIndex;
    var numberOfScenes = SceneManager.sceneCountInBuildSettings;

    var nextScene = currentIndex >= numberOfScenes - 1 ? 0 : currentIndex + 1;
    SceneManager.LoadScene(nextScene);
  }
  public void Restart() {

    gameState.Reset();

    // assume that start menu is 0
    SceneManager.LoadScene(1);
  }
}
