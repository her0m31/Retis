using UnityEngine;
using System.Collections;

public class AddScore : MonoBehaviour {
  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("AddScore") && GameManager.State.Value == GameManager.GameState.Playing) {
      GameManager.Score.Value += 1;
    }
  }
}
