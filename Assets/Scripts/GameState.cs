using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
  int blockCount = 0;
  public int BlockCount { get { return blockCount; } }
  int hitCount = 0;
  public int HitCount { get { return hitCount; } }
  public int ActiveCount { get { return blockCount - hitCount; } }

  public void AddBlock() {
    blockCount++;
  }
  public void DestroyBlock() {
    hitCount++;
    if (ActiveCount < 1) {
      DoLevelCleared();
    }
  }
  void DoLevelCleared() {
    var sceneLoader = FindObjectOfType<SceneLoader>();
    sceneLoader.LoadNext();
  }
}
