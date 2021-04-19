using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Block : MonoBehaviour {

  [SerializeField] int startingHealth = 1;
  [SerializeField] bool isBreakable = true;
  [SerializeField] GameObject blockSparkles;
  [SerializeField] Sprite fullHealthSprite;
  [SerializeField] Sprite paritallyInjuredSprite;
  [SerializeField] Sprite veryInjuredSprite;

  Level level;
  int health;
  bool isAlive = true;

  private void Start() {
    health = startingHealth;
    level = FindObjectOfType<Level>();
    if (isBreakable) {
      level.AddBlock();
    }
  }

  private void OnCollisionEnter2D(Collision2D other) {
    if (!isBreakable || !isAlive) return;
    health--;
    // dead
    if (health <= 0) {
      DestroyBlock();
    } else {
      UpdateSkin();
    }
  }
  void UpdateSkin() {
    // partially injured
    if (health <= startingHealth * 0.75f) {
      if (null == paritallyInjuredSprite) return;
      GetComponent<SpriteRenderer>().sprite = paritallyInjuredSprite;
    }
    // very injured
    else if (health <= startingHealth * 0.33f) {
      if (null == veryInjuredSprite) return;
      GetComponent<SpriteRenderer>().sprite = veryInjuredSprite;
    }
  }
  void DestroyBlock() {
    isAlive = false;
    TriggerVFX();
    Destroy(gameObject);

    level.DestroyBlock();
  }
  void TriggerVFX() {
    GameObject sparkles = Instantiate(blockSparkles, transform.position, transform.rotation);
    Destroy(sparkles, 2f);
  }
}
