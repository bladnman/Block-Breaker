using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

  [SerializeField] int health = 1;
  [SerializeField] bool isBreakable = true;

  GameState gameState;
  bool isAlive = true;

  private void Start() {
    gameState = FindObjectOfType<GameState>();
    gameState.AddBlock();
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (!isBreakable || !isAlive) return;
    health--;

    if (health <= 0) {
      DestroyBlock();
    }
  }
  void DestroyBlock() {
    isAlive = false;
    Destroy(gameObject);
    gameState.DestroyBlock();
  }
}
