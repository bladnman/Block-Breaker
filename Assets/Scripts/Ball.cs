using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
  [SerializeField] Paddle paddle1;
  [SerializeField] bool isLockedToPaddle = true;
  [SerializeField] Vector2 launchVelocity = new Vector2(6f, 15f);
  [SerializeField] AudioSource paddleSound;
  [SerializeField] AudioSource blockSound;

  Vector2 paddleToBallVector;

  void Start() {
    paddleToBallVector = transform.position - paddle1.transform.position;
  }

  // Update is called once per frame
  void Update() {
    LockBallToPaddle();
    LaunchOnMouseClick();
  }

  private void LaunchOnMouseClick() {
    if (Input.GetMouseButtonDown(0)) {
      isLockedToPaddle = false;
      var rb = GetComponent<Rigidbody2D>();
      rb.velocity = launchVelocity;
      // rb.AddForce(new Vector2(2, 3));
    }
  }

  private void LockBallToPaddle() {
    if (false == isLockedToPaddle) return;
    var paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
    transform.position = paddlePos + paddleToBallVector;
  }
  private void OnCollisionEnter2D(Collision2D other) {
    if (true == isLockedToPaddle) return;

    if (other.gameObject.name == "Paddle") {
      paddleSound.Play();
    } else if (other.gameObject.name.Contains("Block")) {
      blockSound.Play();
    }
  }
}
