using System.Linq;
using UnityEngine;

public class Paddle : MonoBehaviour {
  [SerializeField] float screenWidthInUnits = 16f;

  GameState gameState;

  float widthHalf;
  float xMin;
  float xMax;
  void Start() {
    gameState = FindObjectOfType<GameState>();

    widthHalf = GetComponent<Renderer>().bounds.size.x / 2;
    xMin = widthHalf;
    xMax = screenWidthInUnits - widthHalf;
  }

  void Update() {
    var curPos = transform.position;
    float xUnits;
    // use closest ball
    if (gameState.Autoplay) {
      // var ballTransform = GetClosestBallLinq();
      var ballTransform = GetClosestToBottomBall();
      xUnits = ballTransform.position.x;
    }
    // use mouse
    else {
      xUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
    var newPos = new Vector2(curPos.x, curPos.y);
    newPos.x = Mathf.Clamp(xUnits, xMin, xMax);
    transform.position = newPos;
  }
  Transform GetClosestToBottomBall() {
    var allBalls = FindObjectsOfType<Ball>();
    Transform tMin = null;
    float minY = Mathf.Infinity;
    foreach (var ball in allBalls) {
      var t = ball.transform;
      if (t.position.y < minY) {
        tMin = t;
        minY = t.position.y;
      }
    }
    return tMin;
  }
  Transform GetClosestBallLinq() {

    var allBalls = FindObjectsOfType<Ball>();
    var closestBall = allBalls.OrderBy(item => (item.transform.position - transform.position).sqrMagnitude)
                              .FirstOrDefault();
    //  .Take(3)   //or use .FirstOrDefault();  if you need just one
    //  .ToArray();
    return closestBall.transform;
  }
}
