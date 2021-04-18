using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {
  [SerializeField] float screenWidthInUnits = 16f;

  float widthHalf;
  float xMin;
  float xMax;
  void Start() {
    widthHalf = GetComponent<Renderer>().bounds.size.x / 2;
    xMin = widthHalf;
    xMax = screenWidthInUnits - widthHalf;
  }

  void Update() {
    var curPos = transform.position;
    var xUnits = Input.mousePosition.x / Screen.width * screenWidthInUnits;
    var newPos = new Vector2(curPos.x, curPos.y);
    newPos.x = Mathf.Clamp(xUnits, xMin, xMax);
    transform.position = newPos;
  }
}
