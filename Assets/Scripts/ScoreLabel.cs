using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLabel : MonoBehaviour {

  TMP_Text label;
  GameState gameState;

  private void Start() {
    label = GetComponent<TMP_Text>();
    gameState = FindObjectOfType<GameState>();
  }
  void Update() {
    label.text = gameState.Score.ToString();
  }
}
