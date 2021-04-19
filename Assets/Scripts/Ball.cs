using UnityEngine;

public class Ball : MonoBehaviour {
  [SerializeField] bool isLockedToPaddle = true;
  [SerializeField] Vector2 launchVelocity = new Vector2(6f, 15f);
  [SerializeField] AudioSource paddleSound;
  [SerializeField] AudioSource blockSound;
  [SerializeField] float randomFactor = 2f;

  Paddle paddle1;
  Vector2 paddleToBallVector;
  Rigidbody2D rb;
  Level level;
  bool isAlive = true;

  void Start() {
    paddle1 = FindObjectOfType<Paddle>();
    level = FindObjectOfType<Level>();
    rb = GetComponent<Rigidbody2D>();

    var paddleSize = paddle1.GetComponent<Renderer>().bounds.size;
    paddleToBallVector = new Vector2(0, (paddle1.transform.position.y + (paddleSize.y / 3)));
  }
  void Update() {
    LockBallToPaddle();
    LaunchOnMouseClick();
  }

  private void LaunchOnMouseClick() {
    if (!isLockedToPaddle) return;

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

    // slow to search, maybe use tag?
    // PADDLE HIT
    if (other.gameObject.GetComponent<Paddle>() != null) {
      paddleSound.Play();
    }

    // BLOCK HIT
    else if (other.gameObject.GetComponent<Block>() != null) {
      blockSound.Play();
    }

    AddRandomVelocity();
  }
  void AddRandomVelocity() {
    Vector2 randVector = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));
    // float xTweak = rb.velocity.x > 0 ? randVector.x : randVector.x * -1;
    // float yTweak = rb.velocity.y > 0 ? randVector.y : randVector.y * -1;
    // Vector2 finalTweak = new Vector2(xTweak, yTweak);
    // rb.velocity += finalTweak;
    rb.velocity += randVector;
  }
  private void OnTriggerEnter2D(Collider2D other) {
    var loseCollider = other.gameObject.GetComponent<LoseCollider>();
    if (loseCollider != null) {
      Destroy();
    }
  }
  public void Destroy() {
    if (!isAlive) return;
    level.DestroyBall();
    isAlive = false;
    Destroy(gameObject);
  }
}
