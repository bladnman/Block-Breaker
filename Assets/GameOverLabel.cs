using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverLabel : MonoBehaviour {
  TMP_Text label;
  GameState gameState;

  private void Start() {
    label = GetComponent<TMP_Text>();
    gameState = FindObjectOfType<GameState>();

    if (gameState.Lives > 0) {
      label.text = "YOU WIN!";
    }
  }
}
